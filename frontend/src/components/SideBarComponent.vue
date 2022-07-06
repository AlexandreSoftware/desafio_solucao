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
    <router-link to="/">
      <img src="../assets/XandrfDevLogo.svg" width="100" height="100">
    </router-link>
    <h1 class="text-primary">Crud Pessoa</h1>
        <router-link to="/">
          <Button label="Primary" class="p-button-text p-button-info" >
            Ver Todas As Pessoas
          </Button>
        </router-link>
      
      <router-link to="/CriarPessoa">
        <Button label="Primary" class="p-button-text p-button-info" >
          Criar uma nova Pessoa
        </Button>
      </router-link>
      
      <router-link to="/VerCidades">
          <Button label="Primary" class="p-button-text p-button-info" >
            Ver todas as Cidades
          </Button>
      </router-link>
      <router-link to="/CriarCidade">
        <Button label="Primary" class="p-button-text p-button-info" >
          Criar uma nova Cidade
        </Button>
      </router-link>
    </Sidebar>
</template>
<style>
a{
  color: white;
  text-decoration: none;
}


</style>