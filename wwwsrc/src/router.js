import Vue from 'vue'
import Router from 'vue-router'
// @ts-ignore
import Home from './views/Home.vue'
// @ts-ignore
import Login from './views/Login.vue'
// @ts-ignore
import Profile from './views/Profile.vue'
// @ts-ignore
import Vault from './views/Vault.vue'

Vue.use(Router)

let router = new Router({
  routes: [
    {
      path: '/home',
      name: 'home',
      component: Home
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/profile',
      name: 'profile',
      component: Profile
    },
    {
      path: '/vault',
      name: 'vault',
      component: Vault
    }
  ]
})

export default router
