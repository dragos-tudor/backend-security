import { bundle } from  "https://deno.land/x/emit@0.38.1/mod.ts";
import { encodeHex } from "https://deno.land/std@0.207.0/encoding/hex.ts";

const wwwroot = "/workspaces/backend-security/Security.Sample/wwwroot"
try { Deno.mkdirSync(wwwroot, { recursive: true }); } catch {;}

const { code: home } = await bundle("./home/home.jsx")
const encodedHome = new TextEncoder().encode(home)
const hashHome = await crypto.subtle.digest("SHA-256", encodedHome)
const homePath = `/home.${encodeHex(hashHome)}.js`
Deno.writeFileSync(wwwroot + homePath, encodedHome)

const { code: app } = await bundle("./bootstrapping.js")
const appBundle = app
  .replaceAll("../home.jsx", homePath)
  .replaceAll("./home.jsx", homePath)
const encodedApp = new TextEncoder().encode(appBundle)
const hashApp = await crypto.subtle.digest("SHA-256", encodedApp);
const appPath = `/app.${encodeHex(hashApp)}.js`
Deno.writeFileSync(wwwroot + appPath, encodedApp)

const encodedIndexCss = Deno.readFileSync("./index.css")
const hashIndexCss = await crypto.subtle.digest("SHA-256", encodedIndexCss)
const indexCssPath = `/index.${encodeHex(hashIndexCss)}.css`
Deno.writeFileSync(wwwroot + indexCssPath, encodedIndexCss)

const indexHtml = Deno.readTextFileSync("./index.html")
const encodedIndex = new TextEncoder().encode(
  indexHtml
    .replace("/index.css", indexCssPath)
    .replace("/bootstrapping.js", appPath)
)
Deno.writeFileSync(wwwroot + "/index.html", encodedIndex)




