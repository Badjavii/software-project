# Arquitectura del Frontend

El frontend de Booksy está construido con Vue.js, siguiendo una estructura modular que separa componentes visuales, lógica de cliente y recursos estáticos. Su propósito es ofrecer una experiencia de usuario fluida y reactiva.

La comunicación con el backend se realiza exclusivamente a través de los servicios del frontend, que envían peticiones HTTP y procesan respuestas en formato JSON.

## Estructura de Carpetas

```Bash
src/frontend/
├── components/
├── services/
├── assets/
├── public/
├── main.js
└── index.html
```

### components/

- Propósito: Representan las vistas con las que interactúa el usuario (el cliente en la analogía).

- Responsabilidad: Renderizan datos, manejan eventos y comunican acciones del usuario.

Ejemplo: BookCard.vue muestra información de un libro, SearchBar.vue permite buscar por título o autor.

### services/

- Propósito: Son el puente entre el frontend y el backend. Gestionan la comunicación HTTP.

- Responsabilidad: Realizan peticiones usando Axios y exponen funciones reutilizables para los componentes.

Ejemplo: api.ts podría tener getBooks(), createBook(book) o deleteBook(id).

### assets/

- Propósito: Contiene recursos estáticos como imágenes, íconos y estilos.

Ejemplo: logo.svg, booksy-banner.png, global.css.

### public/

- Propósito: Archivos servidos directamente sin pasar por el sistema de módulos.

Ejemplo: favicon.ico, robots.txt, vite.svg.

### main.js

- Propósito: Punto de entrada de la aplicación Vue.

- Responsabilidad: Monta el componente raíz y configura la instancia de Vue.

### index.html

- Propósito: Documento HTML base.

- Responsabilidad: Define el contenedor principal (#app) y carga el script de Vue.

