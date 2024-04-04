import { useLabels } from "../support/services/using.js"

export const Info = (_, elem) =>
{
  const labels = useLabels(elem)

  return <>
    <style css={css}></style>
    <h3>{labels["info"]}</h3>
    <span>Dragos Tudor sofware developer.</span>
  </>
}

const css = `
info {
  display: block;
  margin: 3rem;
}`