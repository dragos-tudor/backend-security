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

const bundleMod = async (cwd, homeName) => {
  const { code: modBundle } = await bundle(cwd + "/mod.js");
  const encodedMod = new TextEncoder().encode(
    modBundle
      .replaceAll("../home.jsx", "/" + homeName)
      .replaceAll("./home.jsx", "/" + homeName));
  const hashMod = await crypto.subtle.digest("SHA-256", encodedMod);
  const modName = `mod.${encodeHex(hashMod)}.js`;
  return { modName, encodedMod };
}

const bundleHome = async (cwd) => {
  const { code: homeBundle } = await bundle(cwd + "/components/home/home.jsx");
  const encodedHome = new TextEncoder().encode(homeBundle);
  const hashHome = await crypto.subtle.digest("SHA-256", encodedHome);
  const homeName = `home.${encodeHex(hashHome)}.js`;
  return { homeName, encodedHome };
}

const compileIndexHtml = (cwd, modName) => {
  const indexHtml = Deno.readTextFileSync(cwd + "/index.html");
  const encodedIndexHtml = new TextEncoder().encode(
    indexHtml
      .replace("mod.js", modName)
  );
  return { indexHtmlName: "index.html", encodedIndexHtml };
}

const encodeSettings = async (cwd) => {
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


const target = "/workspaces/backend-security/Security.Sample/wwwroot";
const targetScripts = target + "/scripts";
const targetImages = target + "/images";

console.log("[publishing]", "make scripts and images wwwroot directories")
removeDirectories(target)
makeDirectories(targetScripts, targetImages)


const source = import.meta.dirname
const sourceScripts = source + "/scripts";
const sourceImages = source + "/images";

console.log("[publishing]", "copy scripts and images files to wwwroot directories")
copyFiles(sourceScripts, targetScripts)
copyFiles(sourceImages, targetImages)

console.log("[publishing]", "bundle home and mod files")
const { homeName, encodedHome } = await bundleHome(source);
const { modName, encodedMod } = await bundleMod(source, homeName);

console.log("[publishing]", "compile index html file")
const { indexHtmlName, encodedIndexHtml } = compileIndexHtml(source, modName);

console.log("[publishing]", "encode settings file")
const { settingsName, encodedSettings } = await encodeSettings(source)


console.log("[publishing]", "publish bundled and compiled files to wwwroot directory")
Deno.writeFileSync(target + "/" + homeName, encodedHome)
Deno.writeFileSync(target + "/" + modName, encodedMod)
Deno.writeFileSync(target + "/" + settingsName, encodedSettings)
Deno.writeFileSync(target + "/" + indexHtmlName, encodedIndexHtml)
console.log("[publishing]", "published sample www to", target)