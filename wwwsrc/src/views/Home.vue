<template>
  <div class="home">
    <h1>Home</h1>
    <div class="container-fluid">
      <button @click='logout()'>Logout</button>
      <div class="row">
        <div v-for="keep in getPublicKeeps">
          <div class="card">
            <img class="card-img" :src="keep.img" alt="card img">


          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script>
  export default {
    name: "home",
    mounted() {
      this.$store.dispatch("getPublicKeeps");
      // blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
    },

    data() {
      return {
      }
    },
    computed: {
      getPublicKeeps() {
        return this.$store.state.publickeeps
      },
      User() {
        return this.$store.state.user
      }
    },
    methods: {
      logout() {
        this.$store.dispatch("logout")
      }
    }
  }
</script>

<style>

</style>