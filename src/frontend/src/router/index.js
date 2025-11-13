
import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import BooksView from '../views/BooksView.vue';
import CatalogView from "../views/CatalogView.vue";
import BookDetailView from "../views/BookDetailView.vue";
import SellerMenuView from "../views/SellerMenuView.vue";
import RegisterBookView from "../views/RegisterBookView.vue";

const routes = [
    { path: '/', component: HomeView },
    { path: '/catalog', component: CatalogView },
    { path: '/books', component: BooksView },
    { path: '/book/:id', name: 'BookDetail', component: BookDetailView },
    { path: '/seller', name: 'SellerMenu', component: SellerMenuView },
    { path: '/seller/registerBook', name: 'RegisterBook', component: RegisterBookView }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;