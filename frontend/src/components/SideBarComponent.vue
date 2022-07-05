<script lang="ts">
export default {
  data() {
    return {
      isOpen: false
    }
  }
}
</script>
<template>
    <Button  icon="pi pi-bars" class="p-button-lg p-button-primary px-4  " @click="isOpen =true" />
    <Sidebar v-model:visible="isOpen" position="left">
    </Sidebar>
</template>