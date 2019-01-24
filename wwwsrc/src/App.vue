<template>
  <div id="app">
    <nav class="navbar navbar-dark bg-dark container-fluid">
      <!-- Navbar content -->
      <router-link :to="{name: 'home'}">Home</router-link>
    </nav>
    <router-view />

  </div>
</template>

<script>
  export default {
    name: "app",
    mounted() {
      //checks for valid session
      this.$store.dispatch("authenticate");
    },
    computed: {
      getUser() {
        return this.$store.state.user
      }
    },
  }
</script>

<style>
  #app {
    font-family: "Avenir", Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

  #nav {
    padding: 30px;
  }

  #nav a {
    font-weight: bold;
    color: #2c3e50;
  }

  #nav a.router-link-exact-active {
    color: #42b983;
  }
</style>