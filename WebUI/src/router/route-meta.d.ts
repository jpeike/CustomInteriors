import 'vue-router'
import type { CustomRouteMeta } from './types'

declare module 'vue-router' {
  interface RouteMeta extends CustomRouteMeta {}
}
