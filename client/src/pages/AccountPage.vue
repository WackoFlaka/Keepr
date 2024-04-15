<template>
  <div class="text-center profile">
    <img :src="account.coverImg" class="coverImg">
    <img :src="account.picture" alt="" class="profile-icon">
    <div class="dropdown">
      <button class="btn btn-white" type="button" data-bs-toggle="dropdown" aria-expanded="false">
        <span class="mdi mdi-dots-horizontal"></span>
      </button>
      <ul class="dropdown-menu">
        <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#editform">Edit Account</a>
        </li>
      </ul>
    </div>
  </div>

  <section class="container-fluid section">
    <h1 class="text-center">{{ account.name }}</h1>
    <p class="text-center fs-3">{{ vaultsCount }} Vaults | {{ keepsCount }} Keeps</p>
  </section>

  <section class="container-fluid section">
    <h1 class="fs-1 mb-3">Vaults</h1>
    <div class="vault-flex">
      <div v-for="vault in vaults" :key="vault.id">
        <div class="position-relative">
          <VaultCard :vault="vault" />
          <button class="btn btn-danger delete-vault" @click="deleteVault(vault.id)">X</button>
        </div>
      </div>
    </div>
  </section>

  <section class="container-fluid section">
    <h1 class="fs-1 mb-3">Keeps</h1>
    <div class="grid">
      <div class="keep" v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
        <button class="btn btn-danger delete" @click="deleteKeep(keep.id)">X</button>
      </div>
    </div>
  </section>



  <div class="modal fade" id="editform" tabindex="-1" aria-labelledby="editform" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-lg">

      <form @submit.prevent="updateAccount()">
        <div class="modal-content">
          <div class="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">Edit Profile</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="row">

              <div class="mb-3">
                <input type="text" v-model="editableAccountData.name" class="form-control" id="name" placeholder="Name">
              </div>
              <div class="mb-3">
                <input type="url" v-model="editableAccountData.picture" class="form-control" id="picture"
                  placeholder="Profile Picture" required>
              </div>
              <div class="mb-3">
                <input type="url" v-model="editableAccountData.coverImg" class="form-control" id="coverImg"
                  placeholder="Cover Image">
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="submit" class="btn btn-primary">Save changes</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
import { keepService } from '../services/KeepService.js';
import { vaultService } from '../services/VaultService.js';
export default {
  setup() {
    const editableAccountData = ref({})
    onMounted(() => {
      getMyVaults()
      getMyKeeps()
    })



    async function getMyVaults() {
      try {
        await accountService.getMyVaults()
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getMyKeeps() {
      try {
        await accountService.GetMyKeeps()
      } catch (error) {
        Pop.error(error)
      }
    }

    return {
      account: computed(() => AppState.account),
      vaultsCount: computed(() => AppState.accountVaults.length),
      keepsCount: computed(() => AppState.accountKeeps.length),
      keeps: computed(() => AppState.accountKeeps),
      vaults: computed(() => AppState.accountVaults),

      async deleteKeep(id) {
        try {
          const confirmation = await Pop.confirm('Are you sure you want to delete this keep?')
          if (!confirmation) {
            return
          }
          await keepService.deleteKeep(id)
          Pop.success('Keep Deleted!')
        } catch (error) {
          Pop.error(error)
        }
      },

      async deleteVault(id) {
        try {
          const confirmation = await Pop.confirm('Are you sure you want to delete this Vault?')
          if (!confirmation) {
            return
          }
          await vaultService.deleteVault(id)
        } catch (error) {
          Pop.error(error)
        }
      },


      editableAccountData,
      async updateAccount() {
        try {
          await accountService.updateAccount(editableAccountData.value)
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
.keep {
  position: relative;
}

.delete {
  position: absolute;
  top: 0;
  right: 0;
  margin: 1rem;
}

.delete-vault {
  position: absolute;
  z-index: 999;
  top: 0;
  left: 0;
  margin: 1rem;
}

.coverImg {
  width: 80% !important;
  max-height: 400px;
  object-fit: cover;
  margin-top: 4rem;
  border-radius: 16px;
}

.profile {
  position: relative;
}

.profile-icon {
  position: absolute;
  width: 150px;
  height: 150px;
  object-fit: cover;
  border-radius: 50%;
  top: 100%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.dropdown {
  position: absolute;
  top: 20%;
  right: 10%;
  transform: translate(-50%, -50%);
}

.section {
  margin-top: 7rem;
  max-width: 80%;
}

.grid {
  --gap: 1em;
  --columns: 4;
  margin: 0 auto;
  display: column;
  columns: var(--columns);
  gap: var(--gap);
  padding-top: 50px;
}

.grid>* {
  break-inside: avoid;
  margin-bottom: var(--gap);
}

@supports (grid-template-rows: masonry) {
  .grid {
    display: grid;
    grid-template-columns: repeat(var(--columns), 1fr);
    grid-template-rows: masonry;
    grid-auto-flow: dense;
    /* align-tracks: stretch; */
  }

  .grid>* {
    margin-bottom: 0em;
  }
}

.vault-flex {
  display: flex;
  flex-wrap: wrap;
  position: relative;
}
</style>
