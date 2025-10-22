import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import HomePage from '../views/HomePage.vue'
import { Roles } from '@/enums/Roles'
import AuthGuard from './guards/AuthGuard'
import RoleGuard from './guards/RoleGuards'
import { RouteNames } from '@/enums/RouteNames'
import type { RouteLocation } from 'vue-router'
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
        requiresAuth: true,
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
        requiresAuth: true,
      },
    },
    {
      path: RoutePaths.EMPLOYEES,
      name: RouteNames.EMPLOYEES,
      component: () => import('../views/EmployeePage.vue'),
      meta: {
        requiresAuth: true,
        roles: ['FAKE ROLE NO ONE WILL HAVE'],
      },
    },
    {
      path: RoutePaths.EMAILS,
      name: RouteNames.EMAILS,
      component: () => import('../views/EmailPage.vue'),
    },
    { 
      path: RoutePaths.ERROR_PAGE, 
      name: RouteNames.ERROR_PAGE, 
      component: () => import('../views/Error/ErrorPage.vue'),
    },
    { 
    path: RoutePaths.NOT_FOUND, 
    name: RouteNames.NOT_FOUND, 
    redirect: (to: RouteLocation) => ({
      name: 'ErrorPage',
      params: { code: '404' },
      query: { routeName: to.fullPath }
  })
  },
  ] satisfies RouteRecordRaw[],
})


export default router
