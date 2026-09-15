namespace VisualEngineering.Core

open System
open System.IO
open System.Text.Json

/// A minimal, explicit JSON value model.
///
/// Every JSON document this tool writes or emits is built from these constructors, so the
/// published schemas are visible in one place and cannot drift through reflection settings.
type JsonValue =
    | JNull
    | JBool of bool
    | JInt of int
    | JString of string
    | JArray of JsonValue list
    | JObject of (string * JsonValue) list

[<RequireQualifiedAccess>]
module Json =

    let ofStringOption value =
        match value with
        | Some text -> JString text
        | None -> JNull

    let ofStrings values = JArray [ for value in values -> JString value ]

    let rec private write (writer: Utf8JsonWriter) value =
        match value with
        | JNull -> writer.WriteNullValue()
        | JBool value -> writer.WriteBooleanValue value
        | JInt value -> writer.WriteNumberValue value
        | JString value -> writer.WriteStringValue value
        | JArray items ->
            writer.WriteStartArray()
            for item in items do
                write writer item
            writer.WriteEndArray()
        | JObject fields ->
            writer.WriteStartObject()
            for name, field in fields do
                writer.WritePropertyName name
                write writer field
            writer.WriteEndObject()

    /// Serializes to indented JSON with a trailing newline. Deterministic for a given value.
    let serialize (value: JsonValue) =
        use stream = new MemoryStream()

        (use writer = new Utf8JsonWriter(stream, JsonWriterOptions(Indented = true, SkipValidation = false))
         write writer value)

        Text.Encoding.UTF8.GetString(stream.ToArray()) + "\n"

    /// Parses a JSON document, returning a readable error instead of throwing.
    let tryParse (text: string) : Result<JsonDocument, string> =
        try
            Ok(JsonDocument.Parse text)
        with :? JsonException as error ->
            Error error.Message

    let tryProperty (name: string) (element: JsonElement) =
        if element.ValueKind <> JsonValueKind.Object then
            None
        else
            match element.TryGetProperty name with
            | true, value -> Some value
            | _ -> None

    let tryString name element =
        tryProperty name element
        |> Option.bind (fun value ->
            if value.ValueKind = JsonValueKind.String then
                Option.ofObj (value.GetString())
            else
                None)

    let tryInt name element =
        tryProperty name element
        |> Option.bind (fun value ->
            if value.ValueKind = JsonValueKind.Number then
                match value.TryGetInt32() with
                | true, number -> Some number
                | _ -> None
            else
                None)

    let tryBool name element =
        tryProperty name element
        |> Option.bind (fun value ->
            match value.ValueKind with
            | JsonValueKind.True -> Some true
            | JsonValueKind.False -> Some false
            | _ -> None)

    let arrayItems name element =
        match tryProperty name element with
        | Some value when value.ValueKind = JsonValueKind.Array -> value.EnumerateArray() |> List.ofSeq
        | _ -> []

    /// Converts a parsed element back into the explicit model, used to preserve
    /// user authored keys when the tool rewrites a shared configuration file.
    let rec ofElement (element: JsonElement) : JsonValue =
        match element.ValueKind with
        | JsonValueKind.Object ->
            JObject
                [ for property in element.EnumerateObject() -> property.Name, ofElement property.Value ]
        | JsonValueKind.Array -> JArray [ for item in element.EnumerateArray() -> ofElement item ]
        | JsonValueKind.String -> JString(element.GetString() |> Option.ofObj |> Option.defaultValue "")
        | JsonValueKind.Number ->
            match element.TryGetInt32() with
            | true, number -> JInt number
            | _ -> JString(element.GetRawText())
        | JsonValueKind.True -> JBool true
        | JsonValueKind.False -> JBool false
        | _ -> JNull

    /// Replaces or appends a field while preserving the order of existing fields.
    let setField name value (fields: (string * JsonValue) list) =
        if fields |> List.exists (fun (key, _) -> key = name) then
            fields |> List.map (fun (key, existing) -> key, (if key = name then value else existing))
        else
            fields @ [ name, value ]
