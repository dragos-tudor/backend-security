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

const bundleApp = async (cwd, homeName) => {
  const { code: appBundle } = await bundle(cwd + "/bootstrapping.js");
  const encodedApp = new TextEncoder().encode(
    appBundle
      .replaceAll("../home.jsx", homeName)
      .replaceAll("./home.jsx", homeName));
  const hashApp = await crypto.subtle.digest("SHA-256", encodedApp);
  const appName = `app.${encodeHex(hashApp)}.js`;
  return { appName, encodedApp };
}

const bundleHome = async (cwd) => {
  const { code: homeBundle } = await bundle(cwd + "/home/home.jsx");
  const encodedHome = new TextEncoder().encode(homeBundle);
  const hashHome = await crypto.subtle.digest("SHA-256", encodedHome);
  const homeName = `home.${encodeHex(hashHome)}.js`;
  return { homeName, encodedHome };
}

const compileIndexCss = async (cwd) => {
  const encodedIndexCss = Deno.readFileSync(cwd + "/index.css");
  const hashIndexCss = await crypto.subtle.digest("SHA-256", encodedIndexCss);
  const indexCssName = `index.${encodeHex(hashIndexCss)}.css`;
  return { indexCssName, encodedIndexCss };
}

const compileIndexHtml = (cwd, indexCssName, appName) => {
  const indexHtml = Deno.readTextFileSync(cwd + "/index.html");
  const encodedIndexHtml = new TextEncoder().encode(
    indexHtml
      .replace("/index.css", indexCssName)
      .replace("/bootstrapping.js", appName)
  );
  return { indexHtmlName: "index.html", encodedIndexHtml };
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

console.log("[publishing]", "bundle home and app files")
const { homeName, encodedHome } = await bundleHome(source);
const { appName, encodedApp } = await bundleApp(source, homeName);

console.log("[publishing]", "compile index css and html files")
const { indexCssName, encodedIndexCss } = await compileIndexCss(source);
const { indexHtmlName, encodedIndexHtml } = compileIndexHtml(source, indexCssName, appName);


console.log("[publishing]", "copy bundled and compiled files to wwwroot directory")
Deno.writeFileSync(target + "/" + homeName, encodedHome)
Deno.writeFileSync(target + "/" + appName, encodedApp)
Deno.writeFileSync(target + "/" + indexCssName, encodedIndexCss)
Deno.writeFileSync(target + "/" + indexHtmlName, encodedIndexHtml)
console.log("[publishing]", "publish sample www at", target)