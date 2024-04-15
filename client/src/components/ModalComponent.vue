<template>
    <div class="modal fade" id="activeKeepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content" style="background-color: #FEF6F0;" v-if="activeKeep">
                <div class="row">
                    <div class="col-6">
                        <img :src="activeKeep.img" alt="" class="img-fluid activeKeepImage">
                    </div>
                    <div class="col-6">
                        <div class="d-flex justify-content-evenly py-5">
                            <div class="d-flex align-items-start">
                                <span class="mdi mdi-eye fs-4 me-2"></span>
                                <p class="fs-4">{{ activeKeep.views }}</p>
                            </div>
                            <div class="d-flex align-items-start">
                                <span class="mdi mdi-alpha-k-box-outline fs-4 me-2"></span>
                                <p class="fs-4">{{ activeKeep.kept }}</p>
                            </div>
                        </div>
                        <h1 class="text-center">{{ activeKeep.name }}</h1>
                        <p class="text-center mx-4 mt-5">{{ activeKeep.description }}</p>

                        <div class="d-flex align-items-center justify-content-around form">
                            <form @submit.prevent="createVaultKeep()">
                                <select class="select" v-model="saveOption.vaultId">
                                    <option v-for=" vault in myVaults" :key="vault.id" :value="vault.id">
                                        {{ vault.name }}
                                    </option>
                                </select>
                                <button class="btn ms-3 save-button">Save</button>
                            </form>
                            <div class="d-flex align-items-center">
                                <img :src="activeKeep.creator.picture" alt="" class="keep-profile me-2">
                                <h1 class="fs-5">{{ activeKeep.creator.name }}</h1>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import { vaultkeepService } from '../services/VaultKeepService.js';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop.js';

export default {
    setup() {

        const saveOption = ref({ keepId: '' })

        return {
            activeKeep: computed(() => AppState.activeKeep),
            myVaults: computed(() => AppState.accountVaults),
            saveOption,

            async createVaultKeep() {
                try {
                    const vaultkeepData = saveOption.value
                    const keepId = AppState.activeKeep.id
                    vaultkeepData.keepId = keepId.toString()

                    await vaultkeepService.createVaultKeep(vaultkeepData)
                    logger.log(saveOption.value)
                    Modal.getOrCreateInstance('#activeKeepModal').hide()
                    Pop.success('Keep saved in Vault.')
                } catch (error) {
                    Pop.error(error)
                }
            },
        }
    }
}
</script>


<style lang="scss" scoped>
.form {
    margin-top: 200px;
}

.keep-profile {
    width: 50px;
    height: 50px;
    border-radius: 50%;
}

.info-container {
    z-index: 2;
}

.activeKeepImage {
    max-width: 100%;
    height: 40rem;
    object-fit: cover;
}

.select {
    background-color: transparent;
    outline: 0 !important;
    border: none;
    border-bottom: 2px solid black;
}

.save-button {
    border: 2px solid black;
}
</style>