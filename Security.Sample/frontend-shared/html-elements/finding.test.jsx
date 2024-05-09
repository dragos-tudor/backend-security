import { assertEquals } from "/asserts.ts"
import { findHtmlTreeDescendant } from "./finding.js"
import { render } from "/scripts/rendering.js"


Deno.test("use html element => find html elements", async (t) =>
{
  await t.step("html tree => find html descendant => descendant html element", () =>
  {
    assertEquals(findHtmlTreeDescendant([render(<a></a>)], "A").tagName, "A")
    assertEquals(findHtmlTreeDescendant([render(<a><b></b></a>)], "B").tagName, "B")
    assertEquals(findHtmlTreeDescendant([render(<a><b><c></c></b></a>)], "C").tagName, "C")
    assertEquals(findHtmlTreeDescendant([render(<a><b></b><c></c></a>)], "C").tagName, "C")
    assertEquals(findHtmlTreeDescendant([render(<a><b><c></c></b></a>)], "C").tagName, "C")
    assertEquals(findHtmlTreeDescendant([render(<a><b><c class="level2"></c></b><c class="level1"></c></a>)], "C").className, "level1")
    assertEquals(findHtmlTreeDescendant([render(<a></a>)], "B"), undefined)
  })
})