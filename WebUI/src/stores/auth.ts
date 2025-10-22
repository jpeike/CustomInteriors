// src/stores/auth.ts
import { defineStore } from 'pinia'
import { UserManager, User } from 'oidc-client-ts'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null as User | null,
    userManager: null as UserManager | null,
  }),
  getters: {
    isLoggedIn: (state) => !!state.user,
    accessToken: (state) => state.user?.access_token,
  },
  actions: {
    init() {
      this.userManager = new UserManager({
        authority: 'https://us-east-1jpmu56ifb.auth.us-east-1.amazoncognito.com',
        client_id: '574mvan5pjeoifpt063t473se6',
        redirect_uri: window.location.origin + '/callback',
        response_type: 'code',
        scope: 'openid profile email',
        post_logout_redirect_uri: window.location.origin,
        metadata: {
          issuer: 'https://us-east-1jpmu56ifb.auth.us-east-1.amazoncognito.com',
          authorization_endpoint:
            'https://us-east-1jpmu56ifb.auth.us-east-1.amazoncognito.com/oauth2/authorize',
          end_session_endpoint:
            'https://us-east-1jpmu56ifb.auth.us-east-1.amazoncognito.com/logout',
          token_endpoint:
            'https://us-east-1jpmu56ifb.auth.us-east-1.amazoncognito.com/oauth2/token',
          userinfo_endpoint:
            'https://us-east-1jpmu56ifb.auth.us-east-1.amazoncognito.com/oauth2/userInfo',
          jwks_uri:
            'https://cognito-idp.us-east-1.amazonaws.com/us-east-1_JPMU56Ifb/.well-known/jwks.json',
        },
      })

      // Subscribe to events
      this.userManager.events.addUserLoaded((user) => {
        this.user = user
      })

      this.userManager.events.addUserUnloaded(() => {
        this.user = null
      })

      // Try to load user from storage
      this.userManager.getUser().then((user) => {
        this.user = user
      })
    },

    login() {
      if (!this.userManager) return
      return this.userManager.signinRedirect()
    },

    async handleCallback() {
      if (!this.userManager) return
      this.user = await this.userManager.signinRedirectCallback()
    },

    async logout() {
      if (!this.userManager) return

      // clear local session
      this.user = null

      // clear oidc-client-ts cached user
      await this.userManager.removeUser() // removes user from storage
      this.userManager.clearStaleState() // cleans any pending state

      // redirect to Cognito logout
      const domain = 'https://us-east-1jpmu56ifb.auth.us-east-1.amazoncognito.com'
      const clientId = '574mvan5pjeoifpt063t473se6'
      const logoutUri = encodeURIComponent(window.location.origin)

      window.location.href = `${domain}/logout?client_id=${clientId}&logout_uri=${logoutUri}`
    },
  },
})
