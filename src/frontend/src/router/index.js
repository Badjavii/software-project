import {
    createRouter,
    createWebHistory,
} from 'vue-router';

import BookDetailView from '../views/BookDetailView.vue';
import BooksView from '../views/BooksView.vue';
import RegisterBookView from "../views/RegisterBookView.vue";
import CatalogView from '../views/CatalogView.vue';
import HomeView from '../views/HomeView.vue';
// Importa las vistas del módulo Perfil Comprador
import ProfileView from '../views/perfil-comprador/ProfileView.vue';
import LoginView from '../views/perfil-comprador/LoginView.vue';
import RegisterProfileView from '../views/perfil-comprador/RegisterProfileView.vue';
// Importa las vistas del módulo Perfil Vendedor
import PerfilSellerView from '../views/perfil-vendedor/ProfileView.vue';
import LoginSellerView from '../views/perfil-vendedor/LoginView.vue';
import RegisterProfileSellerView from '../views/perfil-vendedor/RegisterProfileView.vue';
// Importa las vistas del módulo Ventas
import SalesView from '../views/VenderView.vue';
import ManageSalesView from '../views/GestionarVentasView.vue';
// 
import SellerMenuView from "../views/perfil-vendedor/SellerMenuView.vue";

const routes = [

    // Rutas de libro
    { path: '/', component: HomeView },
    { path: '/catalog', component: CatalogView },
    { path: '/books', component: BooksView },
    {
        path: '/book/:id',
        name: 'BookDetail',
        component: BookDetailView
    },

    // Rutas para Perfil de Comprador
    { path: '/comprador/registrar', component: RegisterProfileView },
    { path: '/comprador/login', component: LoginView },
    {
        path: '/comprador/consultar',
        component: ProfileView,
        meta: { requiresAuth: true } // protegida por sesión
    },

    // Rutas para Perfil de Vendedor
    { path: '/vendedor/registrar', component: RegisterProfileSellerView },
    { path: '/vendedor/login', component: LoginSellerView },
    {
        path: '/comprador/consultar',
        component: PerfilSellerView,
        meta: { requiresAuth: true } // protegida por sesión
    },

    // Rutas de Ventas
    { path: '/ventas/', component: SalesView },
    { path: '/ventas/gestionar', component: ManageSalesView },

    { path: '/seller', name: 'SellerMenu', component: SellerMenuView },
    { path: '/seller/registerBook', name: 'RegisterBook', component: RegisterBookView }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

// Protección de rutas con sesión
// Los usuarios solo podran continuar a ciertas vista si esta registrado
router.beforeEach((to, from, next) => {
    const isBuyerLogged = localStorage.getItem("isBuyerLogged") === "true";
    const isSellerLogged = localStorage.getItem("isSellerLogged") === "true";

    if (to.meta.requiresAuth && !isBuyerLogged) {
        alert("Debes iniciar sesión como comprador para acceder.");
        next(false); // cancela navegación
    } else if (isBuyerLogged && isSellerLogged) {
        alert("No puedes estar logeado como comprador y vendedor al mismo tiempo.");
        next(false);
    } else {
        next();
    }
});

export default router;
