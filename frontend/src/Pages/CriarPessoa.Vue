<script lang="ts">
import { defineComponent } from "vue";
import { useConfirm } from "primevue/useconfirm";
import { useToast } from "primevue/usetoast";
export default {
    setup()
    {
        const confirm = useConfirm();
        const toast = useToast();

        const confirmarInsercao = () => {
            console.log("passed")
            confirm.require({
                message: 'Voce tem certeza que quer inserir essa cidade nas cidades?',
                header: 'Confirmation',
                icon: 'pi pi-exclamation-triangle',
                accept: () => {
                    toast.add({severity:'info', summary:'Confirmed', detail:'You have accepted', life: 3000});
                },
                reject: () => {
                    toast.add({severity:'error', summary:'Rejected', detail:'You have rejected', life: 3000});
                }
            });
        }
        return {confirmarInsercao};
    }
}
</script>
<template>
    <div class="CriarCidadeFormContainer">
        <Toast />
        <ConfirmDialog></ConfirmDialog>
        <ConfirmDialog group="positionDialog"></ConfirmDialog>
        <form class="FormCidade">
            <span class="p-float-label my-1">
                <InputText id="username" type="text" v-model="NomePessoa" />
                <label for="username">Nome da Pessoa</label>
            </span>
            <span class="p-float-label my-1">
                <InputNumber id="minmax" v-model="IdadePessoa" mode="decimal" :min="0" :max="150" />
                <label for="username">Idade da Pessoa</label>
            </span>
            <span class="p-float-label my-1">
                <InputMask mask="999.999.999.99" v-model="CpfPessoa"  />
                <label for="username">Cpf da Pessoa</label>
            </span>
            
            <div class="BotoesSubmit">
                <Button  @click="confirmarInsercao()" label="Criar" icon="pi pi-check" class="p-button m-1"  />
                <Button label="Limpar" icon="pi pi-times" class="p-button-sm m-1 p-button-danger" />
            </div>
        </form>
    </div>
</template>
<style scoped>
    .CriarCidadeFormContainer{
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        min-height: 100vh;
        height: 100%;
        width: 100%;
    }
    .FormCidade{
        background-color: #121212;
        height: 40vh;
        padding-left: 5vh;
        padding-top: 5vh;
        padding-right: 10vh;
        padding-bottom: 5vh;
        border-radius: 5%;
        display: flex;
        justify-content: start;
        flex-direction: column;
    }
    .BotoesSubmit{
        margin-top: auto;
        margin-bottom: 0;
    }
</style>