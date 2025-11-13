<script setup lang="ts">

import '../../../assets/styles/components-style/table-book-detail.css';

defineProps({
  book: {type: Object, required: true}
})

</script>

<template>
  <table class="table">
    <caption class="table-caption " >Peso y dimensiones</caption>
    <tbody class="table-body">
    <tr class="color-gray">
      <th class="table-head top-table-left" scope="row">Altura</th>
      <td class="table-data top-table-right" >{{ book._bookHeight }}cm</td>
    </tr>
    <tr class="color-white">
      <th class="table-head" scope="row">Ancho</th>
      <td class="table-data">{{ book._bookWidth }}cm</td>
    </tr>
    <tr class="color-gray">
      <th class="table-head bottom-table-left" scope="row">Peso</th>
      <td class="table-data bottom-table-right">{{ book._bookWeight }}</td>
    </tr>
    </tbody>
  </table>
</template>

<style scoped>

.table{
  height: 5vh;
}

</style>