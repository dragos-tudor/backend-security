import settings from "./settings.json" with { type: "json" }
import { startLiveServer } from "/serving.js"

const addContextOptions = (serverOptions, contextOptions) => Object.assign(serverOptions, contextOptions)
const createContextOptions = (cwd) => ({ context: {cwd} })
const loadServerCertificates = (serverOptions) => Object.assign(serverOptions,
  {cert: Deno.readTextFileSync(serverOptions.cert), key: Deno.readTextFileSync(serverOptions.key)})
const resolveCwd = () => Deno.args[0] ?? import.meta.dirname

startLiveServer(
  addContextOptions(
    loadServerCertificates(settings.serverOptions),
    createContextOptions(resolveCwd())
));