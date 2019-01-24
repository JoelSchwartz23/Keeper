<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 keeps">
        <div class="card" v-for="keep in getVaultKeep">
          <h4>{{keep.name}}</h4>
          <img class="card-img" :src="keep.img" alt="card img">
          <i class="fas fa-eye"> Views: {{keep.views}}</i>
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
      }
    }
  }
</script>

<style>
</style>