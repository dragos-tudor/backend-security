import { github, linkedin, youtube } from "../images/icons.jsx"

export const Footer = () =>
  <>
    <style css={css}></style>
    <div class="social-icons">
      <a class="social-icon" href="https://github.com/dragos-tudor" target="_blank">
        {github}
      </a>
      <a class="social-icon" href="https://linkedin.com/in/dragos-tudor-marian" target="_blank">
        {linkedin}
      </a>
      <a class="social-icon" href="https://youtube.com/@dragos-tudor" target="_blank">
        {youtube}
      </a>
    </div>
    <span class="me">dragos.tudor [2024]</span>
  </>

const css = `
footer {
  padding: 1em;
  text-align: center;
  vertical-align: middle;
  background-color: var(--dark-primary-color);
}

footer .social-icons {
  display: inline-block;
}

footer .social-icon {
  height: 2em;
  padding: 0.1em;
  border-color: transparent;
}

footer .me {
  color: var(--light-primary-color);
}
`