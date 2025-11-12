import { createRouter, createWebHistory, type RouteRecordRaw, type RouteLocation } from 'vue-router'
import HomePage from '../views/HomePage.vue'
import { Roles } from '@/enums/Roles'
import AuthGuard from './guards/AuthGuard'
import RoleGuard from './guards/RoleGuards'
import { RouteNames } from '@/enums/RouteNames'
import { RoutePaths } from '@/enums/RoutePaths'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: RoutePaths.HOME,
      name: RouteNames.HOME,
      component: HomePage,
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
        requiresAuth: false,
        roles: [Roles.ADMIN],
      },
    },
    // ... your other routes
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
        requiresAuth: false,
      },
    },
    {
      path: RoutePaths.EMPLOYEES,
      name: RouteNames.EMPLOYEES,
      component: () => import('../views/EmployeePage.vue'),
      meta: {
        requiresAuth: false,
        roles: ['FAKE ROLE NO ONE WILL HAVE'],
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
    },
  ] satisfies RouteRecordRaw[],
})

router.beforeEach(AuthGuard)
router.beforeEach(RoleGuard)

export default router
