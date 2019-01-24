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
            <button type="button" class="btn" data-toggle="modal" data-target="#addtovault">
              add to vault
            </button>
            <button type="button" class="btn" data-toggle="modal" @click="addView(keep.views ++)" :data-target="'#'+keep.id">
              View Keep
            </button>
            <!-- VIEWING A SINGLE KEEP -->
            <div class="modal fade" :id="keep.id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
              aria-hidden="true">
              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">{{keep.name}}</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body">
                    <img class="img-responsive" :src="keep.img" style="max-height:250px" alt=" keep">
                  </div>
                  {{keep.description}}
                  <i class="fas fa-eye"> Views: {{keep.views}}</i>
                  <div class="modal-footer">
                  </div>
                </div>
              </div>
            </div>
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
      },
      addView(id) {
        keep.views++
        this.$store.dispatch('updateUserKeep', id)
      },

    }
  }
</script>

<style>
  .card {
    max-width: 16rem;
    box-shadow: 0px 0px 2px black;
    transition: .5s linear;
    background-color: grey;
  }

  /* .card:hover {
    box-shadow: 5px 5px black;
    transform: scale(.95);
  }

  .card-border {
    border-width: 5px;
  }  */
  .btn {
    cursor: pointer;
    background-image: linear-gradient(to right, rgb(145, 12, 168), rgb(8, 8, 8));
    color: whitesmoke;
    margin: 10px;
    border: double;
  }

  .keeps {
    display: flex;
    align-content: flex-end;
    flex-wrap: wrap;
  }
</style>