<script setup lang="ts">

import '../../../../assets/styles/components-style/table-book-detail.css';

defineProps({
  book: {type: Object, required: true }
})
</script>

<template>
  <table class="table">
    <caption class="table-caption">Características principales</caption>
    <tbody class="table-body">
    <tr class="color-gray">
      <th class="table-head top-table-left" scope="row">Subtítulo del libro</th>
      <td class="table-data top-table-right">{{ book._subtitle }}</td>
    </tr>
    <tr class="color-white">
      <th class="table-head" scope="row">Serie</th>
      <td class="table-data">{{ book._series }}</td>
    </tr>
    <tr class="color-gray">
      <th class="table-head" scope="row">Autor</th>
      <td class="table-data">{{ book._author }}</td>
    </tr>
    <tr class="color-white">
      <th class="table-head" scope="row">Idioma</th>
      <td class="table-data">{{ book._language }}</td>
    </tr>
    <tr class="color-gray">
      <th class="table-head" scope="row">Editorial del libro</th>
      <td class="table-data">{{ book._publisher }}</td>
    </tr>
    <tr class="color-white">
      <th class="table-head" scope="row">Tapa del libro</th>
      <td class="table-data">{{ book._bookCover }}</td>
    </tr>
    <tr class="color-gray">
      <th class="table-head bottom-table-left" scope="row">Volumen del libro</th>
      <td class="table-data bottom-table-right">{{ book._bookVolume }}</td>
    </tr>
    </tbody>
  </table>

</template>

<style scoped>
</style>