export default {
  site: {
    title: "Visual Engineering Research",
    description: "Searchable visual engineering research repository",
    baseUrl: "/",
    language: "en",
    siteUrl: "https://visual.echelonfoundry.com/",
  } /**/,
  repository: {
    name: "Visual-Engineering",
    sourceUrl: "https://github.com/kemiller2002/Visual-Engineering",
  },
  content: {
    include: ["**/*.md"],
    exclude: [
      "node_modules/**",
      "dist/**",
      "build/**",
      "build-reports/**",
      ".git/**",
      ".github/**",
      ".research-publisher/**",
      "coverage/**",
      "tmp/**",
      "temp/**",
      "input-documents/**",
      "prompts/**",
      "packages/**",
      // Tooling and product documentation for @echelon-foundry/visual-engineering.
      // These are not research artifacts and are not published to the research site.
      "README.md",
      "docs/**",
      "src/**",
      "tests/**",
      "npm/**",
      "scripts/**",
      // Executable experiment implementations are engineering artifacts. Canonical
      // research records under content/projects/** link to them when appropriate.
      "experiments/**",
      "**/archive/**",
      "**/archives/**",
    ],
    drafts: false,
  },
  metadata: {
    mode: "compatible",
    strictInCI: true,
  },
  output: {
    directory: "dist",
    catalog: "data/research-catalog.json",
    diagnostics: "data/build-diagnostics.json",
  },
};
