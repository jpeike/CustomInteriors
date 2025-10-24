export enum RoutePaths {
  HOME = '/',
  ABOUT = '/about',
  USERS = '/users',
  EMPLOYEES = '/employees',
  CUSTOMERS = '/customers',
  EMAILS = '/emails',
  CALLBACK = '/callback',
  NOT_FOUND = '/:pathMatch(.*)*', // Vue 3 catch-all route
}
