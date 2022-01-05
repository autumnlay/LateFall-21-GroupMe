<template>
  <div class="container-fluid">
    <div class="row">
      This is where the groups will go
      <Group v-for="g in myGroups" :key="g.id" :group="g" />
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account),
      myGroups: computed(() => AppState.myGroups)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
