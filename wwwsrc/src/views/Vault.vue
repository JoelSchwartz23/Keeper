<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 keeps">
        <div class="card" v-for="keep in getVaultKeep">
          <h4>{{keep.name}}</h4>
          <i @click="removeKeep(getActiveVaultId.id, keep.id)" class="fas fa-trash-alt d-flex justify-content-start"></i>
          <img class="card-img" :src="keep.img" alt="card img">
          <i class="fas fa-eye"> Views: {{keep.views}}</i>
          <i class="fab fa-jenkins"> Keeps:{{keep.keeps}}</i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "vault",
    computed: {
      getVaultKeep() {
        return this.$store.state.vaultkeep
      },
      getActiveVaultId() {
        return this.$store.state.activevault
      },
    },
    methods: {
      removeKeep(vaultId, keepId) {
        let payload = {
          vaultId: this.getActiveVaultId,
          keepId: keepId
        }
        this.$store.dispatch('removeVaultKeep', payload)
      }
    }
  }
</script>

<style>
</style>