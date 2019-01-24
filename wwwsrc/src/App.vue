<template>
  <div id="app">
    <nav class="navbar navbar-dark bg-dark container-fluid">
      <!-- Navbar content -->
      <div class="col-4">
        <router-link to='/home'>Home</router-link>
      </div>
      <div class="col-4">
        <router-link class="home-font" to="/profile">Profile</router-link>
      </div>
      <div class="col-4">
        <router-link @click.native='logout()' to="/login">Logout</router-link>
      </div>
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
    methods: {
      logout() {
        this.$store.dispatch("logout")
      }
    }
  }
</script>

<style>
  #app {
    font-family: "Avenir", Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: orange;
  }

  body {
    background-image: linear-gradient(to right, black, purple);
    height: 100%;
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