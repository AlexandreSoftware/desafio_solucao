<script>
  import SideBarComponent from "./components/SideBarComponent.vue";
  export default {
    components: {
      SideBarComponent
    },
  };
</script>
<template>
  <SideBarComponent :isOpen="isOpen">
  </SideBarComponent>

</template>
<style>
body{
  background-color: #121212;
}
</style>
