import { startLiveServer } from "./serving.js"

const cwd = `${import.meta.dirname}`
startLiveServer({hostname: "localhost", port: 5500, context: {cwd, logEnabled: true}});
