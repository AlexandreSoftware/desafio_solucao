
<script>
export default {
    data() {
        return {
            products: new Array(4)
        }
    }
}
</script>

<template>
    <div class="TodasAsPessoasContainer">
        <DataTable :value="products" responsiveLayout="scroll">
            <Column field="id" header="Id">
                <template #body>
                    <Skeleton></Skeleton>
                </template>
            </Column>
            <Column field="uf" header="UF">
                <template #body>
                    <Skeleton></Skeleton>
                </template>
            </Column>
            <Column field="verMais" header="Ver Mais">
                <template #body>
                    <Skeleton></Skeleton>
                </template>
            </Column>
            <Column field="excluir" header="Excluir">
                <template #body>
                    <Skeleton></Skeleton>
                </template>
            </Column>
        </DataTable>
    </div>
</template>
<style scoped>
    .TodasAsPessoasContainer{
        margin-left: 5vh;
        margin-right: 5vh;
    }
</style>