<template>
  <div class="home">
    <h1>Home</h1>
    <div class="container-fluid">
      <div class="row">
        <div class="col-12 keeps">
          <div class="card" v-for="keep in getPublicKeeps">
            <h4>{{keep.name}}</h4>
            <img class="card-img" :src="keep.img" alt="card img">
            <i class="fas fa-eye"> Views: {{keep.views}}</i>
          </div>
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
  .card {
    max-width: 16rem;
    box-shadow: 0px 0px 2px grey;
    transition: .5s linear;
    background-color: black;
  }

  .card:hover {
    box-shadow: 5px 5px black;
    transform: scale(.95);
  }

  .card-border {
    border-width: 5px;
  }

  .keeps {
    display: flex;
    align-content: flex-end;
    flex-wrap: wrap;
  }
</style>