import { startHmrServer } from "./serving.js"

const cwd = `${import.meta.dirname}`
startHmrServer({hostname: "localhost", port: 5500, context: {cwd}});
