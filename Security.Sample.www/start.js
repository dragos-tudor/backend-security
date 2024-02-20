import { serveDirWithTs } from "https://deno.land/x/ts_serve@v1.4.4/mod.ts";

const path = `${import.meta.dirname}/wwwroot`
Deno.serve({path}, (request) => serveDirWithTs(request));