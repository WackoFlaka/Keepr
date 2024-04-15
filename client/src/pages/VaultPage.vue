<template>
    <div class="vault-container text-center" v-if="vault">
        <img :src="vault.img" alt="" class="vault-img">
        <h2 class="vault-title">{{ vault.name }}</h2>
        <p class="vault-creator">By: {{ vault.creator.name }}</p>
    </div>

    <h1 class="text-center mt-5">{{ keepCount }} Keeps</h1>
    <section class="section">
        <div class="grid" v-if="keeps">
            <div class="keep" v-for="keep in keeps" :key="keep.vaultKeepId">
                <VaultKeepCard :keep="keep" />
                <button class="btn btn-danger delete-vault" @click="deleteVaultKeep(keep.vaultKeepId)"
                    v-if="keep.creatorId == account.id">X</button>
            </div>
        </div>
    </section>
</template>


<script>
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop.js';
import { vaultService } from '../services/VaultService.js';
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import VaultKeepCard from '../components/VaultKeepCard.vue';
import { vaultkeepService } from '../services/VaultKeepService.js';
import { router } from '../router.js';

export default {
    setup() {
        const route = useRoute()

        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId
                await vaultService.GetVaultById(vaultId)

            } catch (error) {
                Pop.error(error)
                router.push({ name: 'Home' })
            }
        }

        async function getKeepsInVault() {
            try {
                const vaultId = route.params.vaultId
                await vaultService.getKeepsInVault(vaultId)
            } catch (error) {
                Pop.error(error)
            }
        }

        onMounted(() => {
            getVaultById()
            getKeepsInVault()
        })

        return {
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.vaultkeeps),
            keepCount: computed(() => AppState.vaultkeeps.length),
            account: computed(() => AppState.account),

            async deleteVaultKeep(id) {
                try {
                    const confirmation = await Pop.confirm('Are you sure you want to remove this keep?')
                    if (!confirmation) {
                        return
                    }
                    await vaultkeepService.deleteVaultKeep(id)
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    },
    components: { VaultKeepCard }
}
</script>


<style lang="scss" scoped>
.vault-container {
    position: relative;
    margin-top: 20px;
}


.vault-img {
    width: 700px;
    height: 300px;
    object-fit: cover;
}

.vault-title {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    color: white;
}

.vault-creator {
    position: absolute;
    top: 70%;
    left: 50%;
    transform: translate(-50%, -50%);
    color: white;
}

.grid {
    --gap: 1em;
    --columns: 4;
    margin: 0 auto;
    display: column;
    columns: var(--columns);
    gap: var(--gap);
    padding-top: 50px;
    max-width: 80%;
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

.keep {
    position: relative;
}

.delete-vault {
    position: absolute;
    z-index: 999;
    top: 0;
    left: 0;
    margin: 1rem;
}
</style>