import { createApp } from 'vue';
import App from './App.vue';
import router from "./router/index.js";
import 'vuetify/lib/components/VChip/VChip.css'
import 'vuetify/lib/components/VChipGroup/VChipGroup.css'
import 'vuetify/lib/components/VCombobox/VCombobox.css'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import {createVuetify} from 'vuetify';

const vuetify = createVuetify({
    components,
    directives,
})
const app = createApp(App);
app.use(router);
app.use(vuetify)
app.mount('#app');