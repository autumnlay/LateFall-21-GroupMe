<template>
  <div
    class="col-md-6 group border border-primary"
    :class="{
      'bg-dark': group.groupMemberCreatorId == account.id,
      'bg-primary': !group.groupMemberCreatorId == account.id,
    }"
  >
    <div>group name: {{ group.name }}</div>
    <img :src="group.picture" class="img-fluid" alt="" />
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  props: { group: { type: Object, required: true } },
  setup(props) {


    return {
      account: computed(() => AppState.account),
      inGroup: computed(() => AppState.myGroups.find(g => g.id == props.group.id))
    }
  }
};
</script>


<style scoped>
</style>
