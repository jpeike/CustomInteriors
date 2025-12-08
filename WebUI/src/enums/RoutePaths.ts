export enum RoutePaths {
  HOME = '/',
  ABOUT = '/about',
  USERS = '/users',
  EMPLOYEES = '/employees',
  CUSTOMERS = '/customers',
  JOBS = '/jobs',
  EMAILS = '/emails',
  CALLBACK = '/callback',
  ERROR_PAGE = '/error/:code?',
  NOT_FOUND = '/:pathMatch(.*)*', // Vue 3 catch-all route
}
