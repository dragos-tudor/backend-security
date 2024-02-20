export type JSX = JSXFactory | JSXElement | JSXValue
export type JSXReturn = JSXFactory | JSXElement | JSXFragment
export type JSXFactoryFunc = (props: Record<string, any>, elem: HTMLElement) => JSXReturn

export type JSXFactory = {
  $$typeof: string,
  type: JSXFactoryFunc,
  props: Record<string, any> & {children: JSX[] | JSX},
  key: string | undefined,
  ref: JSXFactory | JSXElement | undefined,
  _owner: JSXElement | undefined
}

export type JSXElement = {
  $$typeof: string,
  type: string,
  props: Record<string, any> & {children: JSX[] | JSX}
  key: string | undefined,
  ref: JSXFactory | JSXElement | undefined,
  _owner: JSXElement | undefined
}

export type JSXValue = any

export type JSXFragment = JSXElement

export function render(elem: JSXFactory | JSXElement, $parent: HTMLElement): HTMLElement
export function update($elem: HTMLElement, elem: JSXFactory | JSXElement): HTMLElement
export function unrender($elem: HTMLElement): HTMLElement

export function Context(props: {name: string, value: any, children: JSX[] | JSX}, elem: HTMLElement): JSXReturn[] | JSXReturn
export function ErrorBoundary(props: {error?: string, path?: string, children: JSX[] | JSX}, elem: HTMLElement): JSXReturn[] | JSXReturn
export function Lazy(props: Record<string, any> & {loader: () => Promise<JSX>, children: JSX[] | JSX}, elem: HTMLElement): JSXReturn
export function Suspense(props: {suspending: boolean, fallback: JSX, children: JSX[] | JSX}, elem: HTMLElement): JSXReturn[] | JSXReturn
export function Services(props: Record<string, any> & { children: JSX[] | JSX }, elem: HTMLElement): JSXReturn[] | JSXReturn

export function jsx(type: string| JSXFactoryFunc| undefined, props: Record<string, any> & {children: any | any[]}, key: any | undefined): JSXReturn
export function jsxs(type: string| JSXFactoryFunc| undefined, props: Record<string, any> & {children: any | any[]}, key: any | undefined): JSXReturn
export function createElement(type: string| JSXFactoryFunc| undefined, props: Record<string, any>, children: any | any[]): JSXReturn