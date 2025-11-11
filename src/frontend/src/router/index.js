
import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import BooksView from '../views/BooksView.vue';
import CatalogView from "../views/CatalogView.vue";
import BookDetailView from "../views/BookDetailView.vue";

const routes = [
    { path: '/', component: HomeView },
    { path: '/catalog', component: CatalogView },
    { path: '/books', component: BooksView },
    {
        path: '/book/:id',
        name: 'BookDetail',
        component: BookDetailView
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;