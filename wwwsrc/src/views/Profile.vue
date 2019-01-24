<template>
  <div>

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
      this.$store.dispatch("getPublicKeeps");
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