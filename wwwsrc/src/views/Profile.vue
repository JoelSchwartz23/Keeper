<template>
  <div>
    <div>
      <div class="container-fluid">
        <h1>Welcome to your profile {{User.username}}</h1>
      </div>
      <div class="row">
        <div class="col-12 card" v-for="vault in getVaults">
          <h4>{{vault.description}}</h4>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addkeep">
            create keep
          </button>
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
    </div>
    <div class="row">
      <div class="col-12 keeps">
        <div class="card" v-for="keep in getUserKeeps">
          <h4>{{keep.name}}</h4>
          <img class="card-img" :src="keep.img" alt="card img">
          <i class="fas fa-eye"> Views: {{keep.views}}</i>
          <button type="button" class="btn" data-toggle="modal" @click="addtoKeeps(keep)" data-target="#addtovault">
            add to vault
          </button>
          <button type="button" class="btn" data-toggle="modal" @click="addView(keep)" :data-target="'#'+keep.id">
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
                  <button type="button" @click="deleteKeep(keep.id)" class="btn btn-danger">delete</button>
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
    name: "profile",

    data() {
      return {
        addKeep: {
          name: "",
          description: "",
          img: "",
          isPrivate: 0
        }
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
    }
  }
</script>

<style>

</style>