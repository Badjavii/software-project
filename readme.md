# Booksy: El Mercado de Libros Online de la Comunidad Ucabista

## Introducción

**Booksy** es la plataforma digital diseñada para la comunidad de la **Universidad Católica Andrés Bello (UCAB)**, que tiene como misión principal ofrecer un **espacio seguro y confiable** donde los estudiantes puedan **comprar y vender libros** de forma eficiente.

## Funcionalidades Clave

La plataforma está pensada para ser intuitiva y funcional:

- **Acceso Visitante:** Los usuarios pueden explorar la oferta completa de libros disponibles sin necesidad de registrarse.
- **Registro de comprador:** El registro permite a los estudiantes comprar libros, gestionar sus datos y consultar el historial de compras.
- **Registro de vendedor:** El registro permite a los estudiantes gestionar publicaciones de venta de libros, gestionar sus datos y consultar el historial de ventas.
- **Búsqueda Avanzada:** Permite buscar libros específicos por título, autor o genero.
- **Tendencias y Valoraciones:** Visualización de los libros más populares y la posibilidad de calificar las transacciones y los ejemplares comprados.

Nuestra **máxima prioridad** es desarrollar un software **estable, seguro y de alta calidad** que sirva efectivamente a la comunidad, sentando las bases para futuras innovaciones.

## Tecnologías Empleadas

Booksy es una aplicación web que sigue una arquitectura **Cliente-Servidor (Frontend/Backend)**. Las siguientes tecnologías conforman las herramientas de desarrollo:

### Frontend
- **Vue.js:** Framework progresivo de JavaScript utilizado para construir la interfaz de usuario de manera reactiva y eficiente.

### Backend y Servidor
- **ASP.NET Core:** Framework basado en **C#**, utilizado para construir la lógica de negocio y las APIs del lado del servidor.

### Gestión y Persistencia
- **Git:** Sistema de control de versiones, esencial para la colaboración en equipo y el manejo de código. El alojamiento y flujo de trabajo se gestiona a través de **GitHub**.
- **JSON (JavaScript Object Notation):** Se empleará como formato de intercambio de datos para la comunicación **Cliente-Servidor** (a través de APIs REST). JSON también se utiliza para el almacenamiento en local de los datos.

## Arquitectura del Proyecto

Booksy está estructurado bajo un enfoque Cliente-Servidor, dividiendo claramente la lógica de negocio (backend) de la interfaz de usuario (frontend). Esta separación permite un desarrollo modular, escalable y mantenible, facilitando la colaboración entre equipos y la evolución del sistema.

Ambos componentes se comunican a través de peticiones HTTP utilizando JSON como formato de intercambio de datos, siguiendo el estilo de arquitectura RESTful.

### Estructura General

```Bash
src/
├── backend/     # Lógica del servidor, controladores, modelos, servicios, repositorios
└── frontend/    # Interfaz de usuario, componentes Vue, servicios, recursos estáticos
```

Cada una de estas capas está documentada en profundidad en los siguientes archivos:

- [backend/docs/architecture-backend.md](URL del enlace) - Detalles sobre la estructura, responsabilidades y flujo de datos del backend.

- [frontend/docs/architecture-frontend.md]() - Detalles sobre la estructura, responsabilidades y flujo de datos del backend

## Contexto del Proyecto

Este proyecto está siendo desarrollado en el marco de la asignatura **Ingeniería de Software** de la **Universidad Católica Andrés Bello**.

- **Docente Supervisor:** María Angélica Pérez
- **Equipo de Desarrollo:**
    - Freddy Fernandez
    - Carmelo Moschella
    - Marco Di Mare
    - Javier Otero

## Licencia

Este software se distribuye bajo la licencia **GNU General Public License v3.0 (GPL-3.0)**, clasificándolo como un proyecto de software libre y de código abierto.

Puedes consultar los términos completos de la licencia [aquí](LICENSE)..

## Artefactos

A continuación se presentarán todos los artefactos que dan soporte a este proyecto como contratos entre cliente-desarrollador. 

- Brief
- ...
