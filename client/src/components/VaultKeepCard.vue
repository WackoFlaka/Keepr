<template>
    <div class="image-container" v-if="keep">
        <img :src="keep.img" alt="keep image" @click="getActiveKeep" data-bs-toggle="modal"
            data-bs-target="#activeKeepModal" class="selectable">
        <div class="d-flex info-container">
            <h1 class="keep-name">{{ keep.name }}</h1>
            <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                <img :src="keep.creator?.picture" alt="" class="profile">
            </router-link>
        </div>
    </div>

    <!-- <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content" v-if="activeKeep" style="background-color: #FEF6F0;">
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
                        <div class="text-center" style="margin-top: 30%;">
                            <button class="btn btn-danger" @click="removeVaultKeep(activeKeep)">Remove</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div> -->
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { Vaultkeep } from '../models/VaultKeep.js';
import { keepService } from '../services/KeepService.js';
import Pop from '../utils/Pop.js';
import { vaultkeepService } from '../services/VaultKeepService.js';
export default {

    props: { keep: { type: Vaultkeep, required: true } },

    setup(props) {
        return {

            async getActiveKeep() {
                try {
                    await keepService.GetActiveVaultKeep(props.keep.id)
                } catch (error) {
                    Pop.error(error)
                }
            },

            async removeVaultKeep() {
                try {
                    await vaultkeepService.removeVaultKeep(props.keep.vaultKeepId)
                } catch (error) {
                    Pop.error(error)
                }
            },

            activeKeep: computed(() => AppState.activeKeep),

        }
    }
}
</script>


<style lang="scss" scoped>
img {
    width: 100%;
    box-shadow: rgba(0, 0, 0, 0.25) 0px 0.0625em 0.0625em, rgba(0, 0, 0, 0.25) 0px 0.125em 0.5em, rgba(255, 255, 255, 0.1) 0px 0px 10px 10px inset #000;
}

.image-container {
    position: relative;
    display: inline-block;
}

.image-container::after {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(0deg, rgba(2, 0, 36, 1) 0%, rgba(0, 0, 0, 0.8239670868347339) 10%, rgba(0, 212, 255, 0) 100%);
    z-index: 1;
    pointer-events: none;
}

.keep-name {
    position: absolute;
    color: white;
    bottom: 0;
    padding: 0.3em;
    font-size: 16px;
    z-index: 5;
    margin: auto 15px 15px;
}

.profile {
    width: 50px;
    border-radius: 50%;
    z-index: 5;
    position: absolute;
    bottom: 0;
    right: 0;
    margin: auto 15px 15px;
}

.form {
    margin-top: 200px;
}

.keep-profile {
    width: 50px;
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