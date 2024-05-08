
export const readCertificates = (certPath, keyPath) => Object.freeze({
  cert: Deno.readTextFileSync(certPath),
  key: Deno.readTextFileSync(keyPath)
})