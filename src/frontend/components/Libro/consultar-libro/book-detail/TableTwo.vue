<script setup lang="ts">

import '../../../../assets/styles/components-style/table-book-detail.css';

defineProps({
  book: {type: Object, required: true}
})

</script>

<template>
  <table class="table" v-if="book">
    <caption class="table-caption">Otros</caption>
    <tbody class="table-body">
    <tr class="color-gray">
      <th class="table-head top-table-left" scope="row">Formato del libro</th>
      <td class="table-data top-table-right">{{ book._typeBook }}</td>
    </tr>
    <tr class="color-white">
      <th class="table-head" scope="row">Género del libro</th>
      <td class="table-data">{{ book?._categoryList?.[0] ?? 'Sin categoría' }}</td>
    </tr>
    <tr class="color-gray bottom-table">
      <th class="table-head bottom-table-left" scope="row">Subgéneros del libro</th>
      <td class="table-data bottom-table-right"> {{ book?._categoryList?.slice(1).join(', ') ?? 'Sin categoría' }}</td>
    </tr>
    </tbody>
  </table>
</template>

<style scoped>

.table {
  margin: 1.5rem 0 0 0;
  height: 5vh;
}

</style>