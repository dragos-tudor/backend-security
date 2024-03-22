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
    <div class="me">dragos.tudor - 2024</div>
  </>

const css = `
footer {
  text-align: center;
  vertical-align: middle;
  background-color: var(--dark-primary-color);
}

footer .social-icons {
  margin: 0.5rem;
}

footer .social-icons .social-icon {
  border-color: transparent;
}

footer .me {
  margin: 0.5rem;
  color: var(--light-primary-color);
}
`