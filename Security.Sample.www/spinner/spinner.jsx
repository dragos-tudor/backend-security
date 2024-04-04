
export const Spinner = (props) =>
  <>
    <style css={css}></style>
    {props.children[0]}
  </>

const css = `
spinner {
  display: block;
  background-image: url("/images/spinner.svg");
  background-position: center;
  background-size: 3rem;
  background-repeat: no-repeat;
  height: inherit;
}`