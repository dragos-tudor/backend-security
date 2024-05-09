import { assertEquals } from "/asserts.ts"
import { findHtmlDescendant } from "./finding.js"
import { render } from "/scripts/rendering.js"


Deno.test("use html element => find html elements", async (t) =>
{
  await t.step("html tree => find html descendant => descendant html element", () =>
  {
    assertEquals(findHtmlDescendant(render(<a></a>), "A").tagName, "A")
    assertEquals(findHtmlDescendant(render(<a><b></b></a>), "B").tagName, "B")
    assertEquals(findHtmlDescendant(render(<a><b><c></c></b></a>), "C").tagName, "C")
    assertEquals(findHtmlDescendant(render(<a><b></b><c></c></a>), "C").tagName, "C")
    assertEquals(findHtmlDescendant(render(<a><b><c></c></b></a>), "C").tagName, "C")
    assertEquals(findHtmlDescendant(render(<a><b><c class="level2"></c></b><c class="level1"></c></a>), "C").className, "level1")
    assertEquals(findHtmlDescendant(render(<a></a>), "B"), undefined)
  })
})