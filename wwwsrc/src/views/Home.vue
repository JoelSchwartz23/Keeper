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
            <i class="fab fa-jenkins"> Keeps:{{keep.keeps}}</i>
            <button type="button" class="btn" data-toggle="modal" @click="addView(keep)" :data-target="'#'+keep.id">
              View Keep
            </button>
            <!-- - Add to Vault/View a Keep --->
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
                  <h4>Click to add to Vault</h4>
                  <div class="modal-footer d-flex justify-content-center" v-for="vault in getVaults">
                    <button data-dismiss="modal" @click="addKeepToVault(vault.id, keep.id);addtoKeeps(keep)" class="btn">{{vault.description}}</button>
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
      this.$store.dispatch("getVaults");
    },

    data() {
      return {
        activekeep: 0
      }
    },
    computed: {
      getPublicKeeps() {
        return this.$store.state.publickeeps
      },
      User() {
        return this.$store.state.user
      },
      getVaults() {
        return this.$store.state.vaults
      },
    },
    methods: {
      logout() {
        this.$store.dispatch("logout")
      },
      addView(keep) {
        keep.views++
        this.$store.dispatch('updateUserKeep', keep)
      },
      addKeepToVault(vault, keep) {
        let payload = {
          vaultId: vault,
          keepId: keep,
          user: this.User.id
        }
        this.$store.dispatch('addKeepToVault', payload)
      },
      addtoKeeps(keep) {
        keep.keeps++
        this.$store.dispatch('updateUserKeep', keep)
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
    background-image: linear-gradient(to right, black, red);
    color: whitesmoke;
    margin: 10px;
    border: double;
  }

  .keeps {
    display: flex;
    align-content: flex-end;
    flex-wrap: wrap;
  }

  .fa-jenkins {
    font-size: 20px;
  }
</style>