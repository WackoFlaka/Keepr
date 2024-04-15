<template>
    <div class="image-container" v-if="keep">
        <img :src="keep.img" alt="keep image" @click="setActiveKeep" data-bs-toggle="modal"
            data-bs-target="#activeKeepModal" class="selectable">
        <div class="d-flex info-container">
            <h1 class="keep-name">{{ keep.name }}</h1>
            <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                <img :src="keep.creator.picture" alt="" class="profile">
            </router-link>
        </div>
    </div>
</template>


<script>
import { computed, ref } from 'vue';
import { Keep } from '../models/Keep.js';
import { keepService } from '../services/KeepService.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
import { vaultkeepService } from '../services/VaultKeepService.js';

export default {

    props: { keep: { type: Keep, required: true } },

    setup(props) {

        const saveOption = ref({ keepId: '' })

        return {
            async createVaultKeep() {
                try {
                    const vaultkeepData = saveOption.value
                    const keepId = AppState.activeKeep.id
                    vaultkeepData.keepId = keepId.toString()

                    await vaultkeepService.createVaultKeep(vaultkeepData)
                    logger.log(saveOption.value)
                    Modal.getOrCreateInstance('#exampleModal').hide()
                    Pop.success('Keep saved in Vault.')
                } catch (error) {
                    Pop.error(error)
                }
            },

            setActiveKeep() {
                keepService.setActiveKeep(props.keep.id)
            },

            account: computed(() => AppState.account),
            activeKeep: computed(() => AppState.activeKeep),
            saveOption,
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
    height: 50px;
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