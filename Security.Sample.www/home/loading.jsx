
export const loadHome = async () => {
  const { Home } = await import("./home.jsx")
  return <Home></Home>;
}