import { bundle } from  "/emit.ts"
import { encodeHex } from "/hex.ts"

/* WEBPACK, PARCEL, ROLLUP bundlers-free! */
/*
  - hash-based modules versioning.
  - lazy loading module [eg. home module].
  - multiple bundled root modules.
  - css-in-js modules.
  - pure script, 100% control.
*/

const bundleIndex = async (cwd, homeName) => {
  const { code: indexBundle } = await bundle(cwd + "/index.js");
  const indexWithHashes = indexBundle
      .replaceAll("../home.jsx", "/" + homeName)
      .replaceAll("./home.jsx", "/" + homeName)
  const encodedIndex = new TextEncoder().encode(indexWithHashes);
  const hashIndex = await crypto.subtle.digest("SHA-256", encodedIndex);
  const indexName = `index.${encodeHex(hashIndex)}.js`;
  return { indexName, encodedIndex };
}

const bundleHome = async (cwd) => {
  const { code: homeBundle } = await bundle(cwd + "/frontend-components/home/home.jsx");
  const encodedHome = new TextEncoder().encode(homeBundle);
  const hashHome = await crypto.subtle.digest("SHA-256", encodedHome);
  const homeName = `home.${encodeHex(hashHome)}.js`;
  return { homeName, encodedHome };
}

const compileIndexHtml = (cwd, indexName, indexCssName) => {
  const indexHtml = Deno.readTextFileSync(cwd + "/index.html");
  const indexHtmlWithHashes = indexHtml
    .replace("index.js", indexName)
    .replace("index.css", indexCssName)
  const encodedIndexHtml = new TextEncoder().encode(indexHtmlWithHashes);
  return { indexHtmlName: "index.html", encodedIndexHtml };
}

const compileIndexCss = async (cwd) => {
  const indexCss = await Deno.readTextFile(cwd + "/index.css");
  const encodedIndexCss = new TextEncoder().encode(indexCss);
  const hashIndexCss = await crypto.subtle.digest("SHA-256", encodedIndexCss);
  const indexCssName = `index.${encodeHex(hashIndexCss)}.css`;
  return { indexCssName, encodedIndexCss };
}

const compileSettings = async (cwd) => {
  const settings = await Deno.readTextFile(cwd + "/settings.js");
  const encodedSettings = new TextEncoder().encode(settings);
  return { settingsName: "settings.js", encodedSettings }
}

const copyFiles = (source, target) =>
  Deno.readDirSync(source)
    .filter(entry => entry.isFile)
    .forEach(moveFile(source, target))

const moveFile = (source, target) => (file) =>
  Deno.writeTextFileSync(
    target + "/" + file.name,
    Deno.readTextFileSync(source + "/" + file.name))

const makeDirectories = (...dirs) =>
  dirs.forEach(dir => Deno.mkdirSync(dir, { recursive: true }))

const removeDirectories = (...dirs) =>
  dirs.forEach(dir => { try { Deno.removeSync(dir, { recursive: true })} catch {;} })


const target = "/workspaces/backend-security/Security.Sample/frontend-app/wwwroot";
const targetScripts = target + "/scripts";
const targetImages = target + "/images";

const source = import.meta.dirname
const sourceScripts = source + "/scripts";
const sourceImages = source + "/images";

console.log("[publishing]", "remove scripts and images app wwwroot directories")
removeDirectories(target)

console.log("[publishing]", "make scripts and images app wwwroot directories")
makeDirectories(targetScripts, targetImages)

console.log("[publishing]", "bundle home file")
const { homeName, encodedHome } = await bundleHome(source)

console.log("[publishing]", "bundle index file")
const { indexName, encodedIndex } = await bundleIndex(source, homeName)

console.log("[publishing]", "copy images to app wwwroot directory")
copyFiles(sourceImages, targetImages)

console.log("[publishing]", "copy scripts to app wwwroot directory")
copyFiles(sourceScripts, targetScripts)

console.log("[publishing]", "compile index css file")
const { indexCssName, encodedIndexCss } = await compileIndexCss(source)

console.log("[publishing]", "compile index html file")
const { indexHtmlName, encodedIndexHtml } = compileIndexHtml(source, indexName, indexCssName);

console.log("[publishing]", "compile settings file")
const { settingsName, encodedSettings } = await compileSettings(source)

console.log("[publishing]", "publish bundled and compiled files to app wwwroot directory")
Deno.writeFileSync(target + "/" + homeName, encodedHome)
Deno.writeFileSync(target + "/" + indexName, encodedIndex)
Deno.writeFileSync(target + "/" + indexCssName, encodedIndexCss)
Deno.writeFileSync(target + "/" + settingsName, encodedSettings)
Deno.writeFileSync(target + "/" + indexHtmlName, encodedIndexHtml)
console.log("[publishing]", "published www sample to", target)