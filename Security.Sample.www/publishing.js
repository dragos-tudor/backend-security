import { bundle } from  "https://deno.land/x/emit@0.38.1/mod.ts";
import { encodeHex } from "https://deno.land/std@0.207.0/encoding/hex.ts";

const cleanWWWRoot = (wwwroot) => {
  for (const dirEntry of Deno.readDirSync(wwwroot))
    dirEntry.isFile && Deno.removeSync(wwwroot + dirEntry.name)
}

const ensureWWWRoot = (wwwroot) => {
  try { Deno.mkdirSync(wwwroot, { recursive: true }); } catch { ; }
}

const bundleApp = async (homeName) => {
  const { code: appBundle } = await bundle("./bootstrapping.js");
  const encodedApp = new TextEncoder().encode(
    appBundle
      .replaceAll("../home.jsx", homeName)
      .replaceAll("./home.jsx", homeName));
  const hashApp = await crypto.subtle.digest("SHA-256", encodedApp);
  const appName = `app.${encodeHex(hashApp)}.js`;
  return { appName, encodedApp };
}

const bundleHome = async () => {
  const { code: homeBundle } = await bundle("./home/home.jsx");
  const encodedHome = new TextEncoder().encode(homeBundle);
  const hashHome = await crypto.subtle.digest("SHA-256", encodedHome);
  const homeName = `home.${encodeHex(hashHome)}.js`;
  return { homeName, encodedHome };
}

const refreshIndexCss = async () => {
  const encodedIndexCss = Deno.readFileSync("./index.css");
  const hashIndexCss = await crypto.subtle.digest("SHA-256", encodedIndexCss);
  const indexCssName = `index.${encodeHex(hashIndexCss)}.css`;
  return { indexCssName, encodedIndexCss };
}

const refreshIndexHtml = (indexCssName, appName) => {
  const indexHtml = Deno.readTextFileSync("./index.html");
  const encodedIndex = new TextEncoder().encode(
    indexHtml
      .replace("/index.css", indexCssName)
      .replace("/bootstrapping.js", appName)
  );
  return { indexName: "index.html", encodedIndex };
}

const wwwroot = Deno.args[0] ?? "/workspaces/backend-security/Security.Sample/wwwroot/";

(ensureWWWRoot(wwwroot), console.log("[publishing]", "ensure wwwroot directory"));
(cleanWWWRoot(wwwroot), console.log("[publishing]", "clean wwwroot files"));

console.log("[publishing]", "bundle home and app files")
const { homeName, encodedHome } = await bundleHome();
const { appName, encodedApp } = await bundleApp(homeName);

console.log("[publishing]", "refresh index css and html files")
const { indexCssName, encodedIndexCss } = await refreshIndexCss();
const { indexName, encodedIndex } = refreshIndexHtml(indexCssName, appName);

console.log("[publishing]", "publish files")
Deno.writeFileSync(wwwroot + homeName, encodedHome)
Deno.writeFileSync(wwwroot + appName, encodedApp)
Deno.writeFileSync(wwwroot + indexCssName, encodedIndexCss)
Deno.writeFileSync(wwwroot + indexName, encodedIndex)
console.log("[publishing]", "published files at", wwwroot)