<template>
    <div class="text-center profile" v-if="profile">
        <img :src="profile.coverImg" class="coverImg">
        <img :src="profile.picture" alt="" class="profile-icon">
    </div>

    <section class="container-fluid section" v-if="profile">
        <h1 class="text-center">{{ profile.name }}</h1>
        <p class="text-center fs-3">{{ vaultCount }} Vaults | {{ keepCount }} Keeps</p>
    </section>

    <section class="container-fluid section">
        <h1 class="fs-1 mb-3">Vaults</h1>
        <div class="vault-flex">
            <div v-for="vault in vaults" :key="vault.id">
                <VaultCard :vault="vault" />
            </div>
        </div>
    </section>

    <section class="container-fluid section">
        <h1 class="fs-1 mb-3">Keeps</h1>
        <div class="grid">
            <div class="keep" v-for="keep in keeps" :key="keep.id">
                <KeepCard :keep="keep" />
            </div>
        </div>
    </section>
</template>


<script>
import { useRoute } from 'vue-router';
import { profileService } from '../services/ProfileService.js';
import Pop from '../utils/Pop.js';
import { computed, onMounted, watch } from 'vue';
import { AppState } from '../AppState.js';

export default {
    setup() {

        watch(() => AppState.activeProfile, () => {
            getUserVaults()
            getUserKeeps()
        })

        const route = useRoute()

        async function getUserProfile() {
            try {
                const profileId = route.params.profileId
                await profileService.getUserProfile(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }

        async function getUserVaults() {
            try {
                await profileService.getUserVaults(route.params.profileId)
            } catch (error) {
                Pop.error(error)
            }
        }

        async function getUserKeeps() {
            try {
                await profileService.getUserKeeps(route.params.profileId)
            } catch (error) {
                Pop.error(error)
            }
        }

        onMounted(() => {
            getUserProfile()
        })

        return {
            profile: computed(() => AppState.activeProfile),
            vaults: computed(() => AppState.vaults),
            keeps: computed(() => AppState.keeps),
            vaultCount: computed(() => AppState.vaults.length),
            keepCount: computed(() => AppState.keeps.length)
        }
    }
}
</script>


<style lang="scss" scoped>
.profile {
    position: relative;
}

.coverImg {
    width: 80% !important;
    max-height: 400px;
    object-fit: cover;
    margin-top: 4rem;
    border-radius: 16px;
}

.profile-icon {
    position: absolute;
    width: 150px;
    object-fit: cover;
    border-radius: 50%;
    top: 100%;
    left: 50%;
    transform: translate(-50%, -50%);
}

.section {
    margin-top: 7rem;
    max-width: 80%;
}

.vault-flex {
    display: flex;
    flex-wrap: wrap;
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
</style>