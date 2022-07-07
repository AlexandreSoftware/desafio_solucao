<script>
  import SideBarComponent from "./components/SideBarComponent.vue";


  export default {
    components: {
      SideBarComponent
    },
  };
</script>
<template>
  <SideBarComponent>
  </SideBarComponent>
   <router-view ></router-view>
</template>
<style>
body{
  background-color: #1e1e1e;
  color: white;
}
</style>
