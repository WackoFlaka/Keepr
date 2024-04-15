<template>
  <nav class="navbar navbar-expand-lg" style="padding: 20px; background-color: #FEF6F0; height: auto;">
    <div class="container-fluid">
      <div class="navbar-nav mr-auto">
        <router-link :to="{ name: 'Home' }">
          <button class="btn border border-dark me-4">Home</button>
        </router-link>
        <div class="dropdown">
          <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false"
            v-if="account.id">
            Create
          </button>
          <ul class="dropdown-menu">
            <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#vaultModal">new vault</a></li>
            <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#keepModal">new keep</a></li>
          </ul>
        </div>
      </div>

      <div class="navbar-brand mx-auto border border-dark px-4">
        <h3>the <br> keepr <br> co.</h3>
      </div>

      <div class="navbar-nav ml-auto">
        <Login />
      </div>
    </div>
  </nav>

  <div class="modal fade" id="vaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <h1 class="text-center pt-3">Add your vault</h1>
        <form class="p-5" @submit.prevent="createVault()">
          <div class="mb-3">
            <label for="exampleInputEmail1" class="form-label">Name</label>
            <input type="text" class="form-control" v-model="editableVaultData.name" required>
          </div>
          <div class="mb-3">
            <label for="exampleInputPassword1" class="form-label">Image URL</label>
            <input type="url" class="form-control" v-model="editableVaultData.img" required>
          </div>
          <div class="mb-3 form-check">
            <input type="checkbox" class="form-check-input" v-model="editableVaultData.isPrivate">
            <label class="form-check-label" for="exampleCheck1">Make Vault Private?</label>
          </div>
          <button type="submit" class="btn btn-primary">Create</button>
        </form>
      </div>
    </div>
  </div>

  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <h1 class="text-center pt-3">Add your keep</h1>
        <form class="p-5" form v-if="account.id" @submit.prevent="createKeep()">
          <div class="mb-3">
            <label for="exampleInputEmail1" class="form-label">Name</label>
            <input type="text" class="form-control" name="name" v-model="editableKeepData.name" required>
          </div>
          <div class="mb-3">
            <label class="form-label">Image URL</label>
            <input type="url" class="form-control" name="img" v-model="editableKeepData.img" required>
          </div>
          <div class="mb-3">
            <label for="exampleInputPassword1" class="form-label">Description</label>
            <textarea name="description" id="" cols="30" rows="10" class="form-control"
              v-model="editableKeepData.description" required></textarea>
          </div>
          <button type="submit" class="btn btn-primary">Create</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import Pop from '../utils/Pop.js';
import { keepService } from '../services/KeepService.js';
import { AppState } from '../AppState.js';
import { vaultService } from '../services/VaultService.js';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')
    const editableKeepData = ref({})
    const editableVaultData = ref({})

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      editableKeepData,
      editableVaultData,
      theme,
      account: computed(() => AppState.account),

      async createKeep() {
        try {
          await keepService.createKeep(editableKeepData.value)
          editableKeepData.value = []
          Pop.success("Keep has been posted.")
        } catch (error) {
          Pop.error(error)
        }
      },

      async createVault() {
        try {
          await vaultService.createVault(editableVaultData.value)
          editableVaultData.value = []
          Pop.success("Vault has been created.")
        } catch (error) {
          Pop.error(error)
        }
      },

      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

nav {
  text-align: center;
  box-shadow: rgba(50, 50, 93, 0.25) 0px 13px 27px -5px, rgba(0, 0, 0, 0.3) 0px 8px 16px -8px;
  max-width: 80%;
  display: block;
  margin-right: auto;
  margin-left: auto;
  margin-top: 20px;
  border-radius: 16px;
}

.navbar-nav .router-link-exact-active {
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
