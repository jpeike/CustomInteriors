export enum RoutePaths {
  DASHBOARD = '/dashboard',
  LANDING = '/landing',
  QUOTE_REQUEST = '/quoterequest',
  LANDING_REDIRECT = '/',
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
