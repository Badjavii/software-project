
import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import BooksView from '../views/BooksView.vue';

const routes = [
    { path: '/', component: HomeView },
    { path: '/books', component: BooksView }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;