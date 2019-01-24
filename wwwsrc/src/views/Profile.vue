<template>
  <div>
    <h1>Welcome to your profile {{User.username}}</h1>
    <div class="container-fluid">
      <div class="row">
        <div class="col-12 keeps">
          <div class="card" v-for="keep in getPrivateKeeps">
            <h4>{{keep.name}}</h4>
            <img class="card-img" :src="keep.img" alt="card img">
            <i class="fas fa-eye"> Views: {{keep.views}}</i>

          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
  export default {
    name: "profile",
    mounted() {
      // blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
      this.$store.dispatch("getPrivateKeeps");
      this.$store.dispatch("getVaults");
    },

    data() {
      return {
      }
    },
    computed: {
      getPrivateKeeps() {
        return this.$store.state.privatekeeps
      },
      getVaults() {
        return this.$store.state.vaults
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