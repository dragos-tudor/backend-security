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
  padding: 1.5rem;
  text-align: center;
  background-color: var(--dark-neutral-color);
}

footer .social-icons {
  margin: 1rem;
}

footer .social-icons .social-icon {
  border-color: transparent;
}

.social-icon * {
  height: 2.5rem;
}
`