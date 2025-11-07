# Arquitectura del Backend

El backend de Booksy está construido con ASP.NET Core, siguiendo una arquitectura por capas que separa responsabilidades de forma clara. Su propósito es manejar la lógica de negocio, coordinar operaciones internas y responder a las solicitudes del frontend.

La comunicación con el frontend se realiza exclusivamente a través de los servicios, que gestionan las peticiones HTTP y responden en formato JSON.

## Estructura de Carpetas

```Bash
src/backend/
├── controllers/
├── models/
├── services/
├── repositories/
├── Properties/
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

### controllers

- Propósito: Actúan como mesoneras del sistema: reciben solicitudes desde los servicios (que representan al cliente externo) y las traducen en acciones del dominio.

- Responsabilidad: Orquestan llamadas a clases de negocio (como modelos o repositorios), sin contener lógica de negocio ni comunicarse directamente con el frontend.

Ejemplo: BooksController.cs podría tener un método GetBooks() que llama a BookService.GetAll() y devuelve el resultado.

### services 

- Propósito: Son el puente entre el frontend y el backend. Gestionan la comunicación HTTP, procesan solicitudes y formatean respuestas.

- Responsabilidad: Validan datos, aplican reglas de negocio y coordinan llamadas a controladores y repositorios.

Ejemplo: BookService.cs podría recibir una solicitud de búsqueda desde el frontend, aplicar filtros y llamar al controlador correspondiente.

### repositories

- Propósito: Encapsulan el acceso a datos.

- Responsabilidad: Consultan, almacenan o modifican datos en la base de datos o en memoria.

Ejemplo: BookRepository.cs podría tener métodos como FindById(int id) o Save(Book book).

### models

- ropósito: Representan las entidades del dominio.

- Responsabilidad: Definen la estructura de los datos que maneja el sistema.

Ejemplo: Book.cs con propiedades como Id, Title, Author, Price

### Properties

- Propósito: Contiene configuraciones de entorno para el desarrollo.

Ejemplo: launchSettings.json define puertos, perfiles de ejecución y variables locales.

### Program.cs

- Propósito: Punto de entrada de la aplicación.

- Responsabilidad: Configura servicios, middleware, CORS, y lanza el servidor web.
