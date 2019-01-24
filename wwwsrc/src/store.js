import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let auth = Axios.create({
  baseURL: "http://localhost:5000/account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: "http://localhost:5000/api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    publickeeps: [],
    privatekeeps: [],
    vaults: [],
    activevault: 0
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    setPublicKeeps(state, keeps) {
      state.publickeeps = keeps
    },
    setPrivateKeeps(state, userkeeps) {
      state.privatekeeps = userkeeps
    },
    setVaults(state, uservaults) {
      state.vaults = uservaults
    },
    SetActiveVault(state, vaultId) {
      state.activevault = vaultId
    }
  },
  actions: {
    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {
      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },
    logout({ commit, dispatch }) {
      auth.delete('logout')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'login' })
        })
    },
    getPublicKeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setPublicKeeps', res.data)
        })
        .catch(e => {
          console.log('Cannot retrieve public keeps')
        })
    },
    getPrivateKeeps({ commit, dispatch }) {
      api.get('keeps/user')
        .then(res => {
          commit('setPrivateKeeps', res.data)
        })
        .catch(e => {
          console.log('Cannot retrieve private keeps')
        })
    },
    getVaults({ commit, dispatch }) {
      api.get('vaults')
        .then(res => {
          commit('setVaults', res.data)
        })
        .catch(e => {
          console.log('Cannot retrieve vaults')
        })
    },
    activeVault({ commit, dispatch }, vaultId) {
      api.get('vaults/' + vaultId)
        .then(res => {
          let vault = res.data
          api.get('vaultkeeps/' + vaultId)
            .then(res => {
              vault.keeps = res.data
              commit("setActiveVault", vaultId)
            })
        })
        .catch(e => {
          console.log('Cannot retrieve vaults')
        })
    }
  }
})