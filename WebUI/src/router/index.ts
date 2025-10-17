import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '../views/HomePage.vue'

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
    },
    {
      path: '/forbidden',
      name: 'forbidden',
      component: () => import('../views/Error/Forbidden.vue'),
    },
    {
      path: '/unauthorized',
      name: 'unauthorized',
      component: () => import('../views/Error/Unauthorized.vue'),
    },
    {
      path: '/generalError',
      name: 'generalError',
      component: () => import('../views/Error/GeneralError.vue'),
    },
    {
      path: '/internalServerError',
      name: 'internalServerError',
      component: () => import('../views/Error/InternalServerError.vue'),
    },
    {
      path: '/badGateway',
      name: 'badGateway',
      component: () => import('../views/Error/BadGateway.vue'),
    },
    {
      path: '/requestTimeout',
      name: 'requestTimeout',
      component: () => import('../views/Error/RequestTimeout.vue'),
    },
    {
      path: '/serviceUnavailable',
      name: 'serviceUnavailable',
      component: () => import('../views/Error/ServiceUnavailable.vue'),
    },
  ],
})

export default router
