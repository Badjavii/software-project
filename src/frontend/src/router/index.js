
import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import BooksView from '../views/BooksView.vue';
import CatalogView from "../views/CatalogView.vue";
import BookDetailView from "../views/BookDetailView.vue";
import SellerMenuView from "../views/SellerMenuView.vue";
import RegisterBookView from "../views/RegisterBookView.vue";
import LoginBuyerView from "../views/perfil-comprador/LoginBuyerView.vue";
import LoginView from "../views/perfil-vendedor/LoginView.vue";
import VenderView from "../views/VenderView.vue";
import GestionarVentasView from "../views/GestionarVentasView.vue";
import RegisterProfileView from "../views/perfil-vendedor/RegisterProfileView.vue";
import RegisterProfileBuyerView from "../views/perfil-comprador/RegisterProfileBuyerView.vue";

const routes = [
    { path: '/', component: HomeView },
    { path: '/catalog', component: CatalogView },
    { path: '/books', component: BooksView },
    { path: '/book/:id', name: 'BookDetail', component: BookDetailView },
    { path: '/seller', name: 'SellerMenu', component: SellerMenuView },
    { path: '/seller/registerBook', name: 'RegisterBook', component: RegisterBookView },
    { path: '/beginSection', component: LoginBuyerView },
    { path: '/registerBuyerSection', component: RegisterProfileBuyerView },
    { path: '/beginSellerSection', component: LoginView },
    { path: '/registerSellerSection', component: RegisterProfileView },
    { path: '/seller/sale', component: VenderView},
    { path: '/gestionarVenta/sale', component: GestionarVentasView }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;