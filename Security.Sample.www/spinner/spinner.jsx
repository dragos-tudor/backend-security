
export const Spinner = (props) =>
  <>
    <style css={css}></style>
    {...props.children}
  </>

const css = `
spinner {
  display: block;
  background-image: none;
  background-position: center;
  background-size: 3rem;
  background-repeat: no-repeat;
  height: inherit;
}`