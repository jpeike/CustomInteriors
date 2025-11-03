export interface CustomRouteMeta {
  requiresAuth?: boolean
  roles?: string[]
  layout?: 'default' | 'none'
}
