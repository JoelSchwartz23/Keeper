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
    userkeeps: [],
    vaults: [],
    vaultkeep: [],
    activevault: 0,
    activekeep: 0
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    setPublicKeeps(state, keeps) {
      state.publickeeps = keeps
    },
    setUserKeeps(state, userkeeps) {
      state.userkeeps = userkeeps
    },
    setVaults(state, uservaults) {
      state.vaults = uservaults
    },
    setActiveVault(state, vaultId) {
      state.activevault = vaultId
    },
    setVaultKeep(state, vaultkeep) {
      state.vaultkeep = vaultkeep
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
          router.push({ name: 'login' })
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
    getUserKeeps({ commit, dispatch }) {
      api.get('keeps/user')
        .then(res => {
          commit('setUserKeeps', res.data)
        })
        .catch(e => {
          console.log('Cannot retrieve user keeps')
        })
    },
    updateUserKeep({ commit, dispatch }, keep) {
      api.put('keeps/' + keep.id, keep)
        .then(res => {
          dispatch('getUserKeeps')
        })
        .catch(e => {
          console.log('Cannot update keep')
        })
    },
    // updatePublicKeep({ commit, dispatch }, keep) {
    //   api.put('keeps/' + keep.id, keep)
    //     .then(res => {
    //       dispatch('getPublicKeeps')
    //     })
    //     .catch(e => {
    //       console.log('Cannot update keep')
    //     })
    // },
    addKeep({ commit, dispatch }, keep) {
      api.post('keeps', keep)
        .then(res => {
          dispatch('getUserKeeps', res.data)
        })
        .catch(e => {
          console.log('Cannot create keep')
        })
    },
    deleteKeep({ commit, dispatch }, id) {
      api.delete('keeps/' + id)
        .then(res => {
          dispatch('getUserKeeps')
        })
        .catch(e => {
          console.log('Cannot delete keep')
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
    addVault({ commit, dispatch }, vault) {
      api.post('vaults', vault)
        .then(res => {
          dispatch("getVaults")
        })
        .catch(e => {
          console.log('Cannot add vaults')
        })
    },
    deleteVault({ commit, dispatch }, id) {
      api.delete('vaults/' + id)
        .then(res => {
          dispatch('getVaults')
        })
        .catch(e => {
          console.log('Cannot delete vault')
        })
    },
    getVaultKeep({ commit, dispatch }, vaultId) {
      api.get('vaultkeeps/' + vaultId)
        .then(res => {
          commit("setVaultKeep", res.data)
          commit("setActiveVault", vaultId)
          router.push('/vault')
        })
        .catch(e => {
          console.log("cannot get vaultkeep")
        })
    },
    addKeepToVault({ commit, dispatch }, payload) {
      api.post('vaultkeeps/', payload)
        .then(res => {
          dispatch("getVaultKeep", payload.vaultId)
        })
        .catch(e => {
          console.log("cannot add keep to vault")
        })
    },
    removeVaultKeep({ commit, dispatch }, payload) {
      api.put("vaultkeeps/", payload)
        .then(res => {
          dispatch("getVaultKeep", payload.vaultId)
        })
        .catch(e => {
          console.log("cannot remove keep from vault")
        })
    }

  }
})