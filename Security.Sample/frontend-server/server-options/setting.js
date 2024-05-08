import { readCertificates } from "../certificates/reading.js"

export const setServerOptionsContextOptions = (serverOptions, contextOptions) => Object.assign(serverOptions, contextOptions)

export const setServerOptionsCertificates = (serverOptions) => Object.assign(serverOptions, readCertificates(serverOptions.certPath, serverOptions.keyPath))