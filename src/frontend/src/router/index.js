import {
  createRouter,
  createWebHistory,
} from 'vue-router';

import BookDetailView from '../views/BookDetailView.vue';
import BooksView from '../views/BooksView.vue';
import CatalogView from '../views/CatalogView.vue';
import HomeView from '../views/HomeView.vue';
// Importa las vistas del módulo Perfil Comprador
import ConsultarPerfilView
  from '../views/perfil-comprador/ConsultarPerfilView.vue';
import LoginView from '../views/perfil-comprador/LoginView.vue';
import RegistrarPerfilView
  from '../views/perfil-comprador/RegisterProfileView.vue';

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
    { path: '/comprador/registrar', component: RegistrarPerfilView },
    { path: '/comprador/login', component: LoginView },
    {
        path: '/comprador/consultar',
        component: ConsultarPerfilView,
        meta: { requiresAuth: true } // protegida por sesión
    }
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
