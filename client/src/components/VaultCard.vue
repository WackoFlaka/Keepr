<template>
    <div class="image-container">
        <div class="vault-container">
            <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
                <h1 class="vault-name">{{ vault.name }}</h1>
                <img :src="vault.img" alt="vault picture">
                <span class="mdi mdi-lock lock fs-2" v-if="vault.isPrivate"></span>
            </router-link>
        </div>
    </div>
</template>


<script>
import { computed } from 'vue';
import { Vault } from '../models/Vault.js';
import { AppState } from '../AppState.js';

export default {
    props: { vault: { type: Vault, required: true } },
    setup() {
        return {
            account: computed(() => AppState.account)
        }
    }
}
</script>


<style lang="scss" scoped>
img {
    width: 300px;
    height: 300px;
    object-fit: cover;
    margin-right: 2em;
    margin-bottom: 3em;
    padding: 0;
}

.vault-container {
    position: relative;
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
    width: 90%;
    height: 87%;
    background: linear-gradient(0deg, rgba(2, 0, 36, 1) 0%, rgba(0, 0, 0, 0.8239670868347339) 10%, rgba(0, 212, 255, 0) 100%);
    z-index: 1;
    pointer-events: none;
}

.lock {
    position: absolute;
    bottom: 15%;
    right: 15%;
    color: white;
    z-index: 999;
}

.vault-name {
    position: absolute;
    color: white;
    bottom: 15%;
    padding: 0.3em;
    font-size: 22px;
    z-index: 999;
    margin: auto 15px 15px;
}
</style>