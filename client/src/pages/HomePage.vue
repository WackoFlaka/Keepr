<template>
  <div class="grid">
    <div class="keep" v-for="keep in keeps" :key="keep.id">
      <KeepCard :keep="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import Pop from '../utils/Pop.js';
import { keepService } from '../services/KeepService.js';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';


export default {
  setup() {
    async function getKeeps() {
      try {
        await keepService.getKeeps()
      } catch (error) {
        Pop.error(error)
      }
    }


    onMounted(() => {
      getKeeps()
    })

    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.grid {
  --gap: 1em;
  --columns: 4;
  max-width: 80%;
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

.art {
  grid-column: span 2;
}
</style>
