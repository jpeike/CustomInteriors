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
      path: '/:pathMatch(.*)*',
      name: 'notfound',
      component: () => import('../views/Error/NotFound.vue'),
    },
  ],
})

export default router
