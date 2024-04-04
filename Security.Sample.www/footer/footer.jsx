import { github, linkedin, youtube } from "../images/icons.jsx"

export const Footer = () =>
  <>
    <style css={css}></style>
    <section class="footer social-icons">
      <a class="footer-social-icon" href="https://github.com/dragos-tudor" target="_blank">
        {github}
      </a>
      <a class="footer-social-icon" href="https://linkedin.com/in/dragos-tudor-marian" target="_blank">
        {linkedin}
      </a>
      <a class="footer-social-icon" href="https://youtube.com/@dragos-tudor" target="_blank">
        {youtube}
      </a>
    </section>
    <div class="footer-me">dragos.tudor - 2024</div>
  </>

const css = `
footer {
  padding-block: 2rem 3rem;
  text-align: center;
  background-color: var(--dark-neutral-color);
}

.footer-social-icon {
  margin-inline: 0.5rem 0.5rem;
  font-size: 2.5rem;
}

.footer-me {
  margin-top: 1rem;
}`