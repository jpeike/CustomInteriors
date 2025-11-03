// src/router/guards/authGuard.ts
import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

export default function authGuard(
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext,
) {
  const auth = useAuthStore()
  console.log('user:', auth.user?.profile)

  if (to.meta.requiresAuth && !auth.user) {
    auth.login()
    return
  }

  next()
}
