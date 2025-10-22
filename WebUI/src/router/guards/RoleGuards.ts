// src/router/guards/roleGuard.ts
import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import { RouteNames } from '@/enums/RouteNames'

function extractRoles(user: any): string[] {
  const raw = user?.profile?.['cognito:groups'] ?? []

  return raw
}

export default function roleGuard(
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext,
) {
  if (!to.meta.roles?.length) return next()

  const auth = useAuthStore()
  const roles = extractRoles(auth.user)
  const hasRole = roles.some((r) => to.meta.roles!.includes(r))

  // TODO: We should reroute to forbidded instead
  if (!hasRole) return next({ name: RouteNames.ERROR_PAGE, params: { code: '403' }, query: { routeName: to.fullPath } })
  next()
}
