import { createRouter, createWebHistory, type RouteRecordRaw, type RouteLocation } from 'vue-router'
import Dashboard from '../views/Dashboard.vue'
import { Roles } from '@/enums/Roles'
import AuthGuard from './guards/AuthGuard'
import RoleGuard from './guards/RoleGuards'
import LandingPage from '@/views/LandingPage.vue'
import { RouteNames } from '@/enums/RouteNames'
import { RoutePaths } from '@/enums/RoutePaths'
import { useAuthStore } from '@/stores/auth'
import QuoteRequest from '@/views/QuoteRequest.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: RoutePaths.LANDING_REDIRECT,
      name: RouteNames.LANDING_REDIRECT,
      component: { template: '<div></div>' },
      beforeEnter: async () => {
        const auth = useAuthStore()
        const roles = extractRoles(auth.user)
        if (roles.includes(Roles.ADMIN)) {
          return RoutePaths.DASHBOARD
        }
        if (roles.includes(Roles.CUSTOMER)) {
          return RoutePaths.QUOTE_REQUEST
        }
        if (!auth.isLoggedIn) {
          return RoutePaths.LANDING
        }
        return RoutePaths.ABOUT
      },
    },
    {
      path: RoutePaths.DASHBOARD,
      name: RouteNames.DASHBOARD,
      component: Dashboard,
      meta: {
        requiresAuth: true,
        roles: [Roles.ADMIN],
      },
    },
    {
      path: RoutePaths.LANDING,
      name: RouteNames.LANDING,
      component: LandingPage,
      meta: {
        layout: 'none',
      },
    },
    {
      path: RoutePaths.QUOTE_REQUEST,
      name: RouteNames.QUOTE_REQUEST,
      component: QuoteRequest,
      meta: {
        layout: 'none',
        requiresAuth: true,
      },
    },
    {
      path: RoutePaths.ABOUT,
      name: RouteNames.ABOUT,
      component: () => import('../views/AboutPage.vue'),
    },
    {
      path: RoutePaths.USERS,
      name: RouteNames.USERS,
      component: () => import('../views/UserPage.vue'),
      meta: {
        requiresAuth: true,
        roles: [Roles.ADMIN],
      },
    },
    {
      path: RoutePaths.CALLBACK,
      name: RouteNames.CALLBACK,
      component: () => import('../views/CallbackPage.vue'),
    },
    {
      path: RoutePaths.CUSTOMERS,
      name: RouteNames.CUSTOMERS,
      component: () => import('../views/CustomerPage.vue'),
      meta: {
        requiresAuth: true,
        roles: [Roles.ADMIN, Roles.EMPLOYEE],
      },
    },
    {
      path: RoutePaths.JOBS,
      name: RouteNames.JOBS,
      component: () => import('../views/JobsPage.vue'),
      meta: {
        requiresAuth: false,
      },
    },
    {
      path: RoutePaths.EMPLOYEES,
      name: RouteNames.EMPLOYEES,
      component: () => import('../views/EmployeePage.vue'),
      meta: {
        requiresAuth: true,
        roles: [Roles.ADMIN],
      },
    },
    {
      path: RoutePaths.ERROR_PAGE,
      name: RouteNames.ERROR_PAGE,
      component: () => import('../views/Error/ErrorPage.vue'),
      meta: {
        layout: 'none',
      },
    },
    {
      path: RoutePaths.NOT_FOUND,
      name: RouteNames.NOT_FOUND,
      redirect: (to: RouteLocation) => ({
        name: 'ErrorPage',
        params: { code: '404' },
        query: { routeName: to.fullPath },
      }),
      meta: {
        layout: 'none',
      },
    },
  ] satisfies RouteRecordRaw[],
})

router.beforeEach(AuthGuard)
router.beforeEach(RoleGuard)

export default router

function extractRoles(user: any): string[] {
  const raw = user?.profile?.['cognito:groups'] ?? []

  return raw
}
