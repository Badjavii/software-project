# Frontend (Vue 3)

Este esqueleto contiene una aplicación Vue 3 con Vite, un servicio para llamar al backend mock y pruebas unitarias básicas con Vitest.

Preparación rápida:

1. Instalar dependencias:

```bash
cd frontend
npm install
```

2. Ejecutar en modo desarrollo:

```bash
npm run dev
```

3. Ejecutar pruebas unitarias:

```bash
npm test
```

Notas:
- El `api.baseURL` en `src/services/api.js` apunta por defecto a `http://localhost:5000/api`. Ajusta `VITE_API_BASE_URL` si tu backend corre en otro puerto.
- `listarVentas` intenta leer `/data/ventas.json` desde el backend; si el servidor no sirve la carpeta `data/`, la lista quedará vacía. Para desarrollo rápido, puedes configurar el backend para exponer `data/` como contenido estático o añadir un endpoint de listado.
