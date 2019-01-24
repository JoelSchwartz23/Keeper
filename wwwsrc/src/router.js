import Vue from 'vue'
import Router from 'vue-router'
// @ts-ignore
import Home from './views/Home.vue'
// @ts-ignore
import Login from './views/Login.vue'
// @ts-ignore
import Profile from './views/Profile.vue'

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
    }
  ]
})
// router.beforeEach((to, from, next) => {
//   // to and from are both route objects
//   if (to.path == '/login' || to.path == '/register') {
//     next()
//   } else if (to.matched.length == 0) {
//     next("/")
//   } else {
//     next()
//   }
// })

export default router
