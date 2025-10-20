import { createRouter, createWebHistory } from 'vue-router'
import { RoutePaths } from '@/enums/RoutePaths'
import { RouteNames } from '@/enums/RouteNames'

import HomePage from '@/views/HomePage.vue'
import NotFoundPage from '@/views/Error/NotFound.vue'

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
  { path: RoutePaths.NOT_FOUND, name: RouteNames.NOT_FOUND, component: NotFoundPage },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

export default router
