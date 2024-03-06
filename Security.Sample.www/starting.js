import { startLiveServer } from "/serving.js"

const cwd = Deno.args[0] ?? import.meta.dirname
startLiveServer({hostname: "localhost", port: 5500, context: {cwd}});
