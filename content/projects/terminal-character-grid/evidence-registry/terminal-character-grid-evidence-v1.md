---
id: EVR-VE-TCG-001
title: Terminal / Character-Grid Evidence Registry
version: 1.1
project: terminal-character-grid
status: active
work_item: GH-17
purposes:
  - reference
  - verify
audiences:
  - researcher
  - contributor
---

# Terminal / Character-Grid Evidence Registry

This registry records the evidence used by
[RP-VE-TCG-2026-DD75](../research-execution-package/RP-VE-TCG-2026-DD75--terminal-character-grid-family.md)
and the abstraction boundary in
[CN-VE-TCG-2026-6CA0](../concept/CN-VE-TCG-2026-6CA0--terminal-character-grid-abstraction-boundary.md).

All sources were retrieved on 2026-09-27. The session network policy blocked
`ibm.com`, `bitsavers.org`, `w3.org`, and `x3270.miraheze.org`; every source
below was therefore read from a GitHub-hosted copy, and the copy is named. No
claim in this registry is based on a source that was not opened in that
session. Background knowledge that could not be verified against an opened
source is listed separately under [Unverified background](#unverified-background)
and must not be cited as evidence.

## Evidence type legend

- **primary-specification** — the architecture or standard itself.
- **reference-implementation** — source code of a maintained emulator; shows
  behavior as implemented, not as architected.
- **standard** — accessibility or web-platform standard text.
- **repository** — an existing Visual Engineering or Forma record.
- **practitioner-source** — application source written against a platform
  by its users; shows how the platform is used, not how it is architected.

## IBM 3270 (reference profile)

### EV-VE-TCG-2026-CA91 — Row-major character buffer

**Type:** primary-specification.
**Source:** IBM, *3270 Information Display System Data Stream Programmer's
Reference*, GA23-0059-07 (Eighth Edition, June 1992), Chapter 1, "The Device
Buffer", p. 1-3 and Figure 1-1. Read from the scanned copy
`documentation/IBM 3270 Documentation/IBM 3270 Data Stream Programmers Reference 1-4.pdf`
in `github.com/Open3270/Open3270` at commit `76b89a51a1a41607baff026c4c02b3adaa16c243`.

**Finding used:** "Each character storage location in the buffer maps to a
character position on the display." For a 12-row, 80-column display, "row 1
maps to the first 80 character storage positions in the character buffer, row 2
maps to the second 80 … The sequence is the same regardless of the size of the
display." A keyed character is stored at the cursor position, "then the cursor
advances one position"; "Before a character can be entered, the cursor must be
positioned in an unprotected field."

**Supports:** fixed character grid; coordinate ↔ linear address equivalence
(`address = row × columns + column`); row-major ordering as a structural
property of the 3270, not a stylistic choice.

**Does not prove:** that users read or scan 3270 screens in row-major order.
Buffer order is a storage and transmission order.

### EV-VE-TCG-2026-732A — Fields are delimited by attribute cells

**Type:** primary-specification.
**Source:** GA23-0059-07, Chapter 1, pp. 1-4 to 1-5, Figure 1-2 (same copy as
EV-VE-TCG-2026-CA91).

**Finding used:** "Field attributes define the start of a field and control the
characteristics of the field … the field attribute takes up a character
position on the display screen and appears as a blank. The field is defined as
the field attribute position plus the character positions up to, but not
including, the next field attribute." A screen with fields is *formatted*;
without fields it is *unformatted* and used "in free-form manner". "A field can
wrap from the end of one row to the beginning of the next row … also … from the
last location on the screen to the first location."

**Supports:** explicit field boundaries; the boundary itself consumes a grid
cell in the 3270 (a profile-specific cost); field extent is determined by
position, not by a declared width.

**Does not prove:** that a web implementation must reserve a visible cell for
the boundary. That is a 3270 encoding property.

### EV-VE-TCG-2026-E645 — Field characteristics

**Type:** primary-specification.
**Source:** GA23-0059-07, Chapter 1, "Field Attributes", pp. 1-5 to 1-6.

**Finding used:** A field attribute defines: protected or unprotected ("A
protected field cannot be modified by the operator … Unprotected fields are
classified as input fields"); alphanumeric or numeric; autoskip ("The cursor
skips over fields that are defined as protected and numeric"); nondisplay or
display/intensified display (nondisplay input is stored but not displayed;
"Some devices cannot intensify characters … and highlight characters in a
different manner"); detectable or nondetectable (selector pen).

**Supports:** protected vs editable as a first-class field property; masked
input as an attribute rather than a separate control; device-dependent
emphasis rendering (emphasis was never guaranteed to be color or brightness).

### EV-VE-TCG-2026-C4C6 — Extended field and character attributes

**Type:** primary-specification.
**Source:** GA23-0059-07, Chapter 1, pp. 1-5 to 1-6, Figure 1-3, Table 1-1;
Table of Tables entries 4-4 to 4-8 (field validation, mandatory fill,
mandatory entry, attribute defaults) observed in the same copy.

**Finding used:** Extended field attributes define "color, character set,
field validation, field outlining, and extended highlighting" and "do not
occupy positions in the character buffer". Extended highlighting values
include underscore, blink, and reverse video. Field validation includes
mandatory-fill and mandatory-entry behavior (Chapter 4).

**Supports:** required-field state and non-color emphasis (underscore,
reverse video, outlining) are authentic 3270 capabilities; color is an
optional extension, not the base encoding.

### EV-VE-TCG-2026-DFA5 — Attention identifiers (action keys)

**Type:** primary-specification.
**Source:** GA23-0059-07, Chapter 3, "Attention Identification (AID)",
pp. 3-9 to 3-10 and Table 3-4.

**Finding used:** Operator actions that "initiate an enter operation" include
"Pressing a program function or program attention key" and "Pressing the
Enter, Clear, or Clear Partition key". Table 3-4 lists AIDs for PF1 through
PF24, PA1 through PA3, Clear, Clear Partition, Enter, Test Req/Sys Req,
selector pen attention, and magnetic readers. "Once the AID is set, it remains
set and input is inhibited until" a write command with keyboard restore.

**Supports:** Enter, Clear, and PF1–PF24 are *transmissions to the
application*, not local editing commands; input inhibition after an action is
an authentic state that needs a visible status.

**Important negative finding:** **Reset does not appear in the AID table.**
Reset is a local terminal function (see EV-VE-TCG-2026-3E25), so treating
Reset as an application submission would misdescribe the 3270.

### EV-VE-TCG-2026-21B3 — Default and alternate screen size

**Type:** primary-specification.
**Source:** GA23-0059-07, Chapter 2 (partitions), OCR of scanned pages in the
same copy.

**Finding used:** "For non-SNA environments, the default size is 1920
characters (24 x 80) on the display screen. The alternate size is
implementation defined." Erase/Write uses the default size; Erase/Write
Alternate uses the alternate size. "When the Clear key is pressed, the device
may either be set to the default size or remain the same."

**Supports:** 24 × 80 as the architectural default; a second geometry as a
normal, application-selected condition. Geometry is a parameter, not a
constant.

### EV-VE-TCG-2026-D0F7 — Model geometries in a maintained emulator

**Type:** reference-implementation.
**Source:** `github.com/pmattes/x3270` at commit
`c459b5019a58d429b394d939c8822bcf4565c771`, `include/3270ds.h` lines 446–453
and `x3270/fb-x3270` lines 1016–1019.

**Finding used:** `MODEL_2` 24 × 80, `MODEL_3` 32 × 80, `MODEL_4` 43 × 80,
`MODEL_5` 27 × 132; menu labels "Model 3 (80x32)" etc.

**Supports:** 32 × 80 is a real 3270-family geometry as implemented by a
widely used emulator. The IBM primary source opened in this session states
only that the alternate size is implementation defined.

### EV-VE-TCG-2026-3E25 — Tab, BackTab, Reset, and Clear as implemented

**Type:** reference-implementation.
**Source:** x3270 (same commit), `Common/kybd.c` (`Tab_action` ≈ line 1931,
`BackTab_action` ≈ 1950, `do_reset` ≈ 2009, `Reset_action` ≈ 2081,
`Home_action` ≈ 2096, `Clear_action` ≈ 2875) and `Common/ctlr.c`
(`next_unprotected` ≈ line 623).

**Finding used:** Tab moves the cursor to `next_unprotected(cursor_addr)`: the
next buffer address after an unprotected field attribute, searching forward
and wrapping around the buffer. BackTab searches backward the same way. Home
moves to the first unprotected field. Reset flushes typeahead and
half-composed input and otherwise performs a local controller reset; it sends
nothing to the host. Clear erases the buffer, moves the cursor to address 0,
and sends the Clear AID.

**Supports:** 3270 focus order is *derived from grid position* (row-major
order of editable fields, with wraparound), not declared independently.
Reset is local; Clear is local erase plus transmission.

**Does not prove:** that wraparound is desirable on the web, where Tab must
also be able to leave the terminal region (see CN-VE-TCG-2026-6CA0).

## IBM 5250 (comparison profile)

### EV-VE-TCG-2026-2966 — 5250 geometry, indicators, and keys

**Type:** reference-implementation.
**Source:** `github.com/tn5250/tn5250` at commit
`b8448fecd2825bc8e1aa3361371844fb8f25697d`, `lib5250/display.c`
(`tn5250_dbuffer_set_size(…, 24, 80)` ≈ line 1890; 27 × 132 in
`tn5250_display_clear_unit_alternate` ≈ line 1920) and `lib5250/terminal.h`
lines 74–92.

**Finding used:** 24 × 80 default and 27 × 132 alternate geometry; indicator
states `X_SYSTEM`, `INSERT`, `INHIBIT`; a keyboard lock state; a message line;
keys `K_F24`, `K_ROLLUP`, `K_HELP`, `K_SYSREQ`, and `K_FIELDEXIT`.

**Supports:** fixed grid, status indicators, keyboard lock, and numbered
function keys generalize beyond the 3270. The key vocabulary differs (Field
Exit, Roll Up, Help), so action identifiers must be extensible rather than a
closed PF list.

## Sources added on 2026-09-27 (second session)

A second session (WI-0004) retried the blocked topics. `ibm.com`,
`bitsavers.org`, `archive.org`, `wikipedia.org`, and `x3270.miraheze.org`
were still blocked by the network policy; the sources below were read from
git clones of public repositories, named with their commits. The Open3270
mirror was rechecked and holds no further OIA documentation.

### EV-VE-TCG-2026-1EB5 — The OIA is drawn outside the presentation space

**Type:** reference-implementation.
**Source:** `github.com/pmattes/x3270` at commit
`c459b5019a58d429b394d939c8822bcf4565c771`: `Common/c3270/cscreen.c`
`set_status_row(int hard_rows, int emulator_rows)` (line 234; the OIA is
placed only `if (hard_rows > emulator_rows …)`, line 242, with an optional
"line over OIA"); `c3270/screen.c` `draw_oia()` (line 2622; the OIA row is
`status_row`, rows `r >= maxROWS` are outside the 3270 display, and an
underlined blank row may separate them, lines 2755–2770; cursor position is
printed on the OIA as `row/col`, line 2854); `Common/vstatus.c` keyboard-lock
messages `"X -f"` (line 123), `"X SYSTEM"` (line 191), and `"X Wait"`
(lines 223, 280).

**Finding used:** c3270 draws the Operator Information Area on terminal rows
beyond the emulated model's rows (`maxROWS`), never inside them, optionally
separated by a ruled line, and uses it for keyboard-lock/system indicators
and the cursor position. When the terminal has no spare row, c3270 omits the
OIA rather than overwriting application rows (`model_min_xtra`).

**Supports:** assumption A-3 *as implemented by a maintained emulator*: the
3270 status line is not one of the application's rows. A Model 2 screen is
therefore 24 application rows plus a separate status line.

**Does not prove:** the physical 3278/3279 layout; IBM display-station
documentation remains unread.

### EV-VE-TCG-2026-D9CB — PC text mode is a row-major cell buffer with per-cell attributes

**Type:** reference-implementation (BIOS/VGA emulation).
**Source:** `github.com/dosbox-staging/dosbox-staging` at commit
`c8ad29d897580fa3802bfd222bd00db7d3ad0442`: `src/ints/int10_modes.cpp`
mode table (line 38: mode `0x003`, `M_TEXT`, text width 80, text height 25;
line 53: mode `0x019`, 80 × 43; lines 87–89: 80 × 28, 80 × 30, 80 × 34);
`src/ints/int10.h` `struct VideoModeBlock` field comments (lines 198–222:
"Text mode width & height in number of characters"); `src/ints/int10_char.cpp`
`WriteChar` (lines 521–537: `address += (row*cols+col)*2`, then the character
byte and, when used, the attribute byte).

**Finding used:** BIOS text mode 3 is 80 × 25 character cells. Each cell is a
character byte followed by an attribute byte at `(row × columns + column) × 2`.
Other text modes change only the row count (25, 28, 30, 34, 43).

**Supports:** for DOS text mode, invariants I-1 (declared fixed grid) and
row-major addressing hold as in the 3270; per-cell attributes are the
equivalent of 3270 extended character attributes. All 25 rows are
application rows: the mode has no device status line, so a status or
function-key row is something the application draws.

**Does not prove:** how DOS applications conventionally laid out status and
key rows; that is practice, not the mode.

### EV-VE-TCG-2026-8081 — 5250 per-row selection fields ("Work with" subfiles)

**Type:** practitioner-source.
**Source:** `github.com/PoC-dev/as400-sfltemplates` at commit
`3228b1997e02508fa2f1e25c0471608a981b72a7`: `enu/v_lodalldf.dds` (line 39
`R MAINSFL SFL`; line 42 `OPT 1A B 7 3VALUES(' ' '2' '3' '4' '5')`; line 88
`3 2'Type options, press Enter.'`; line 90 `4 3'2=Change  3=Copy …`);
`v_lodallpg.rpgle` (lines 213–266: "handle OPT choices", `WHENEQ '2'` … `'5'`,
`MOVE *BLANK OPT` after processing); `readme.md` lines 91–92 ("Use `READC` in
a loop to read (user) changed subfile records … clear the Opt field upon
return-on-success") and lines 471–478 (column ruler: `Opt` column, then
fields separated by at least two blanks).

**Finding used:** a list screen gives every repeated row a one-character
input/output (`B`) field constrained to listed values, announced by an
instruction line and an option legend above the rows. On Enter the program
reads only the changed rows, acts on each, and blanks the option on success.

**Supports:** GAP-TCG-10 as a real, recurring idiom of the 5250 platform:
per-row selection fields belong in the generic family, positioned in the
first column of the repeated rows, with the legend as their description and
Enter as the single submission.

**Does not prove:** IBM's own CUA guidance for list panels (not reachable) or
that users prefer this idiom to row focus plus action keys.

### EV-VE-TCG-2026-C2ED — A cell is 1ch only to within sub-pixel rounding

**Type:** repository (Forma verification).
**Source:** `kemiller2002/forma` pull request #47, commit `d8fea43`
("Close CharacterGrid GAP-TCG-11"), measured in Chromium with the Forma
monospaced stack at 16px text.

**Finding used:** the glyph advance measured 9.640625px while `1ch` computed
to 9.625px, so a run of *n* characters exceeds *n* cells by about
0.0156 × *n* px (≈ 1.2px over 78 cells). Forma absorbs the error with a
length-proportional negative end margin so the cell grid stays exact
without a text-spacing override, and rendered positions stay within 1px at
1280, 390, and 320 CSS px and at 200 % text.

**Supports:** refines A-5: `1ch` is a stable cell width for layout, but an
implementation must not assume text fills its cells exactly.

### EV-VE-TCG-2026-DE6C — Text-spacing overrides can be absorbed without breaking the grid

**Type:** repository (Forma verification).
**Source:** `kemiller2002/forma` pull request #47, commits `d8fea43` and
`533c410`; tests in `tests/browser/character-grid-verification.spec.mjs`.

**Finding used:** with column and row tracks of at least one cell that grow
to their content, a WCAG 1.4.12 override (letter-spacing 0.12em, word-spacing
0.16em, line-height 1.5) widens shared columns: no run overlaps another, runs
that start in the same column stay aligned across rows, table cells keep
their text inside their cells, and the page gains no horizontal overflow.
The same tests fail against the earlier fixed `1ch` tracks.

**Supports:** EV-VE-TCG-2026-E852 can be met by a contained grid, not only by
reflow: the grid relationship that must survive is shared-column alignment,
not an absolute cell width.

## Accessibility and web platform

All WCAG text below was read from `github.com/w3c/wcag` at commit
`71c891a49f50f58766597a8980f6b2d2eedd5679` (`guidelines/sc/**` and
`understanding/**`). WCAG 2.2 is the conformance baseline.

### EV-VE-TCG-2026-62B1 — Reflow and its two-dimensional exception

**Type:** standard. SC 1.4.10 Reflow (AA), `guidelines/sc/21/reflow.html` and
`understanding/21/reflow.html`.

**Finding used:** Content presents without two-dimensional scrolling at 320
CSS pixels "Except for parts of the content which require two-dimensional
layout for usage or meaning". Understanding text: such sections have an
exception, "However, sections of content within the two-dimensional layout,
such as each cell within a table, would still need to meet this success
criterion", and authors "can improve the user's experience by making efforts to
reduce scrolling".

**Supports:** a contained, horizontally scrollable terminal viewport is
*conditionally* permissible. Whether a given character-grid screen "requires
two-dimensional layout for usage or meaning" is a per-screen judgment that
this registry does not settle (see HY-VE-TCG-2026-8750).

### EV-VE-TCG-2026-7B66 — Pause, Stop, Hide

**Type:** standard. SC 2.2.2 (A), `guidelines/sc/20/pause-stop-hide.html`.

**Finding used:** moving or scrolling information that starts automatically,
lasts more than five seconds, and is presented in parallel with other content
needs a mechanism to pause, stop, or hide it unless essential.

**Supports:** a long SequentialReveal must be interruptible.

### EV-VE-TCG-2026-3BDE — Animation from Interactions

**Type:** standard. SC 2.3.3 (AAA), `guidelines/sc/21/animation-from-interactions.html`.

**Finding used:** "Motion animation triggered by interaction can be disabled,
unless the animation is essential to the functionality or the information
being conveyed."

### EV-VE-TCG-2026-93E7 — prefers-reduced-motion

**Type:** standard (Editor's Draft source). Media Queries Level 5,
`mediaqueries-5/Overview.bs` in `github.com/w3c/csswg-drafts` (main branch,
retrieved 2026-09-27), section `prefers-reduced-motion`.

**Finding used:** values `no-preference | reduce`; `reduce` "Indicates that
user has notified the system that they prefer an interface that removes or
replaces the types of motion-based animation that either trigger discomfort
for those with vestibular motion sensitivity" (text continues).

### EV-VE-TCG-2026-61DF — Status messages

**Type:** standard. SC 4.1.3 (AA), `guidelines/sc/21/status-messages.html`.

**Finding used:** status messages "can be programmatically determined through
role or properties such that they can be presented to the user by assistive
technologies without receiving focus".

### EV-VE-TCG-2026-FEFD — Sequence and focus order

**Type:** standard. SC 1.3.2 Meaningful Sequence (A) and SC 2.4.3 Focus Order
(A).

**Finding used:** when sequence affects meaning, "a correct reading sequence
can be programmatically determined"; focusable components "receive focus in an
order that preserves meaning and operability".

### EV-VE-TCG-2026-E9AC — Use of color

**Type:** standard. SC 1.4.1 (A). Color "is not used as the only visual means
of conveying information, indicating an action, prompting a response, or
distinguishing a visual element."

### EV-VE-TCG-2026-E852 — Resize text, text spacing, focus not obscured

**Type:** standard. SC 1.4.4 Resize Text (AA: 200 percent "without loss of
content or functionality"), SC 1.4.12 Text Spacing (AA), SC 2.4.11 Focus Not
Obscured (Minimum) (AA, WCAG 2.2).

**Supports:** a fixed grid must survive 200 % text and text-spacing overrides
without clipping characters, and a focused field inside a contained scroller
must not be entirely hidden.

### EV-VE-TCG-2026-C6E8 — Character key shortcuts

**Type:** standard. SC 2.1.4 (A): single-character shortcuts need a turn-off,
remap, or active-only-on-focus mechanism.

**Supports:** terminal action keys must not be bound to bare printable
characters globally; function keys and modified keys avoid the criterion.

## Repository evidence

### EV-VE-TCG-2026-E22E — Existing Visual Engineering doctrine

**Type:** repository. `agent-context/UI-FOUNDATIONS.md` (lines ≈ 49, 57, 84,
105, 118 at base commit `c1e7c32`).

**Finding used:** "Ensure keyboard order, reading order, focus order, and
visual order tell the same story"; "Keep source order semantically correct and
avoid CSS reordering that conflicts with reading or focus order"; "Manage
density according to the task"; support "reduced motion, forced colors, and
assistive technology".

**Supports:** the terminal family inherits these rules; it does not need new
doctrine for them.

## Unverified background

The following are widely repeated practitioner descriptions that shaped the
questions asked, but no primary source for them was opened in the first
session. The status column records what later sources established.
They are **assumptions**, recorded in CN-VE-TCG-2026-6CA0, and must not be
cited as evidence until verified.

| Topic | Background claim | Status |
| --- | --- | --- |
| 3270 OIA | The Operator Information Area occupies a line below the application rows (row 25 on a 24-row model) and shows keyboard-lock and system indicators. | Supported as implemented by EV-VE-TCG-2026-1EB5 (x3270); IBM display-station documentation still unread. |
| DOS text mode | PC text mode is commonly 80 × 25 cells with per-cell foreground/background attributes; applications drew their own status and function-key rows. | Grid, row-major cells, and per-cell attributes supported by EV-VE-TCG-2026-D9CB; the absence of a device status line is supported; the application layout convention is still unverified. |
| BBS / ANSI | BBS screens used ECMA-48 ("ANSI") cursor positioning over serial links; at low line rates characters visibly arrived one at a time. | Unverified. Retried on 2026-09-27 (WI-0004); no ECMA-48 or BBS source was reachable. |
| 3270 screen update | A 3270 write is applied to the buffer as a unit and the screen is not painted character by character in a user-perceivable sequence. | Partially supported: EV-VE-TCG-2026-CA91 and -DFA5 describe block transfer of a whole data stream; perceptual painting speed of specific devices was not verified. |
