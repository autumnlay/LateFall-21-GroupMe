<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">groups</div>
      <Group v-for="g in groups" :key="g.id" :group="g" />
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core'
import { groupsService } from '../services/GroupsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await groupsService.getGroups()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      groups: computed(() => AppState.groups),
    }
  }

}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
