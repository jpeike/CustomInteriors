import { createRouter, createWebHistory } from 'vue-router'
import { RoutePaths } from '@/enums/RoutePaths'
import { RouteNames } from '@/enums/RouteNames'

import HomePage from '@/views/HomePage.vue'
import ErrorPage from '@/views/Error/ErrorPage.vue'

const routes = [
  { path: RoutePaths.HOME, name: RouteNames.HOME, component: HomePage },
  {
    path: RoutePaths.ABOUT,
    name: RouteNames.ABOUT,
    component: () => import('@/views/AboutPage.vue'),
  },
  {
    path: RoutePaths.USERS,
    name: RouteNames.USERS,
    component: () => import('@/views/UserPage.vue'),
  },
  {
    path: RoutePaths.EMPLOYEES,
    name: RouteNames.EMPLOYEES,
    component: () => import('@/views/EmployeePage.vue'),
  },
  {
    path: RoutePaths.CUSTOMERS,
    name: RouteNames.CUSTOMERS,
    component: () => import('@/views/CustomerPage.vue'),
  },
  {
    path: RoutePaths.EMAILS,
    name: RouteNames.EMAILS,
    component: () => import('@/views/EmailPage.vue'),
  },
  {
    path: RoutePaths.CALLBACK,
    name: RouteNames.CALLBACK,
    component: () => import('@/views/CallbackPage.vue'),
  },
  { path: RoutePaths.ERROR_PAGE, name: RouteNames.ERROR_PAGE, component: ErrorPage, props: true },
  { path: RoutePaths.NOT_FOUND, name: RouteNames.NOT_FOUND, component: ErrorPage, props: { code: '404' } },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

export default router
