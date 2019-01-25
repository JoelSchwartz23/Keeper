<template>
  <div>
    <div>
      <div class="container-fluid">
        <h1>Welcome to Your Profile {{User.username}}</h1>
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addvault">
          Create Vault
        </button>
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addkeep">
          Create Keep
        </button>
      </div>
      <div class="row">
        <h1 class="mx-4">Your Vaults</h1>
        <div class=" col-12 vault" v-for="vault in getVaults">
          <button @click="getVaultKeep(vault.id)" class="btn">{{vault.description}}</button>
          <i @click="deleteVault(vault.id)" class="fas fa-trash-alt mt-4"></i>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <!-- ADD KEEP MODAL -->
          <div class="modal fade" id="addkeep" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLabel">Create a keep</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <form @submit.prevent="createKeep()">
                    <div><input type="text" placeholder="Name" v-model="addKeep.name"></div>
                    <div><input type="text" placeholder="Description" v-model="addKeep.description"></div>
                    <div><input type="text" placeholder="Image Url" v-model="addKeep.img"></div>
                    <div><input type="checkbox" v-model="addKeep.isPrivate">Private</div>
                    <button type="submit" class="btn btn-primary">Create Keep</button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <!-- ADD Vault Modal -->
          <div class="modal fade" id="addvault" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <h5 class="modal-title" id="exampleModalLabel">Create a Vault</h5>
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <form @submit.prevent="createVault()">
                    <div><input type="text" placeholder="Name" v-model="addVault.name"></div>
                    <div><input type="text" placeholder="Description" v-model="addVault.description"></div>
                    <button type="submit" class="btn btn-primary">Create Vault</button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <h1 class="mx-4">Your Keeps</h1>
      <div class="col-12 keeps">
        <div class="card" v-for="keep in getUserKeeps">
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
                <button data-dismiss="modal" type="button" @click="deleteKeep(keep.id)" class="btn">delete</button>
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
    name: "profile",

    data() {
      return {
        addKeep: {
          name: "",
          description: "",
          img: "",
          isPrivate: 0
        },
        addVault: {
          name: "",
          description: ""
        },
        activeKeep: 0
      }
    },
    mounted() {
      // blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
      this.$store.dispatch("getUserKeeps");
      this.$store.dispatch("getVaults");
    },
    computed: {
      getUserKeeps() {
        return this.$store.state.userkeeps
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
      },
      createKeep() {
        this.$store.dispatch('addKeep', this.addKeep);
      },
      createVault() {
        this.$store.dispatch('addVault', this.addVault);
      },
      deleteKeep(id) {
        this.$store.dispatch('deleteKeep', id)
      },
      addView(keep) {
        keep.views++
        this.$store.dispatch('updateUserKeep', keep)
      },
      addtoKeeps(keep) {
        keep.keeps++
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
      getVaultKeep(vaultId) {
        this.$store.dispatch("getVaultKeep", vaultId)
      },
      deleteVault(id) {
        this.$store.dispatch('deleteVault', id)
      },
    }
  }
</script>

<style>
  .vault {
    display: flex;
    justify-content: flex-start;
  }

  .modal-body {
    background-image: linear-gradient(to right, black, red);
  }

  .modal-header {
    background-image: linear-gradient(to right, black, red);
  }

  .modal-footer {
    background-image: linear-gradient(to right, black, red);
  }

  .modal-content {
    background-image: linear-gradient(to right, black, red);
  }
</style>