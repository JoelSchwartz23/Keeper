<template>
  <div id="app">
    <nav class="navbar navbar-dark bg-dark container-fluid">
      <!-- Navbar content -->
      <h1 class="d-flex justify-content-start mt-2">Keeper <i class="fas fa-fire"></i></h1>
      <div class="col-12 nav-style">
        <router-link class="nav-style mb-1" to='/home'>Home</router-link>
        <router-link class="nav-style" to="/profile">Profile</router-link>
        <router-link v-if="getUser.id" class="nav-style" @click.native='logout()' to="/login">Logout</router-link>
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
    color: red;
  }

  body {
    background-image: linear-gradient(to right, black, red);
    height: 100%;
  }

  .nav-style {
    color: white;
    display: flex;
    justify-content: flex-end;
    margin-left: 10px;
    font-size: 20px;
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