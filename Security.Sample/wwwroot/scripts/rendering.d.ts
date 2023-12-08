// deno-lint-ignore-file no-explicit-any

type Action = {
  type: string,
  payload: any
}

export enum JSXType {
  factory,
  element,
  text,
  fragment
}
export type JSX = JSXFactory | JSXText | JSXElement
export type JSXReturn = JSXFactory | JSXElement | JSXFragment

export type JSXFactory = {
  build: (props: Record<string, any>, children: JSX[] | JSX, elem: HTMLElement) => JSXReturn[] | JSXReturn,
  props?: Record<string, any>,
  children: JSX[] | JSX
  type: JSXType,
  name: string
}

export type JSXElement = {
  props?: Record<string, any>,
  children: JSX[] | JSX,
  type: JSXType,
  name: string
}

export type JSXText = {
  text?: string,
  children: JSX[] | JSX,
  type: JSXType
}

export type JSXFragment = {
  name: string,
  children: JSX[] | JSX,
  type: JSXType
}

export function render(elem: JSXFactory | JSXElement, $parent: HTMLElement): HTMLElement
export function update($elem: HTMLElement, elem: JSXFactory | JSXElement): HTMLElement
export function unrender($elem: HTMLElement): HTMLElement
export function getCurrentElement($elem?: HTMLElement): HTMLElement

export function Context(props: {name: string, value: any}, children: JSX[] | JSX): JSXReturn[] | JSXReturn
export function ErrorBoundary(props: {error: string, path?: string}, children: JSX[] | JSX, elem: HTMLElement): JSXReturn[] | JSXReturn
export function Portal(props: Record<string, any>, children: JSX[] | JSX, elem: HTMLElement): JSXReturn[] | JSXReturn
export function Suspense(props: {suspending: boolean, fallback: JSX}, children: JSX[] | JSX): JSXReturn[] | JSXReturn
export type Lazy = (props: {suspending: boolean, fallback: JSX}, children: JSX[] | JSX) => JSXReturn[] | JSXReturn
export function lazy(name: string, resolver: () => Promise<JSX>): Lazy
export function Services(props: Record<string, any>, children: JSX[] | JSX, elem: HTMLElement): JSXReturn[] | JSXReturn

export function useContext<T>(name: string, fallbackValue: T): [T, (value: T) => void, () => HTMLElement]
export function useService<T>(name: string, fallbackValue: T): T
export function useEffect(func: () => Promise<any>, deps?: any[]): void
export function useLayoutEffect(func: () => any, deps?: any[]): void
export function useMemo<T>(func: () => T, deps?: any[]): T
export function useCallback<T extends () => any>(func: T, deps?: any[]): T
export function useReducer<T>(reducer: () => T, initState?: T, init?: () => T): [T, (action: Action) => any]
export function useState<T>(value: T): [T, (value: T) => void]
export function useStates<T>(states: T): [T, () => T]