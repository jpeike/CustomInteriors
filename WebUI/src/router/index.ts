import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import HomePage from '../views/HomePage.vue'
import { Roles } from '@/enums/Roles'
import AuthGuard from './guards/AuthGuard'
import RoleGuard from './guards/RoleGuards'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomePage,
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutPage.vue'),
    },
    {
      path: '/users',
      name: 'users',
      component: () => import('../views/UserPage.vue'),
      meta: {
        requiresAuth: true,
        roles: ['FAAAAAKE'],
      },
    },
    // ... your other routes
    {
      path: '/callback',
      name: 'Callback',
      component: () => import('../views/CallbackPage.vue'),
    },
    {
      path: '/customers',
      name: 'customers',
      component: () => import('../views/CustomerPage.vue'),
    },
    {
      path: '/employees',
      name: 'employees',
      component: () => import('../views/EmployeePage.vue'),
    },
    {
      path: '/emails',
      name: 'emails',
      component: () => import('../views/EmailPage.vue'),
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'notfound',
      component: () => import('../views/Error/NotFound.vue'),
      meta: {
        layout: 'none',
      },
    },
  ] satisfies RouteRecordRaw[],
})

router.beforeEach(AuthGuard)
router.beforeEach(RoleGuard)

export default router
