# VentasApi (mock JSON-backed)

Este servicio es un mock para las historias de "Venta" del sprint. Usa archivos JSON como base de datos en `data/`.

Endpoints principales:

- POST /api/ventas
  - Registrar una venta.
  - Body (application/json):

```json
{
  "codigoDeCompra": 1001,
  "fechaDeVenta": "2025-11-07T12:00:00Z", // opcional, si no se envía se usa UTC now
  "libroId": 1,
  "compradorId": 1,
  "vendedorId": 1
}
```

  - Respuestas:
    - 201 Created: devuelve la venta registrada (DTO VentaResponse).
    - 409 Conflict: si `codigoDeCompra` ya existe.
    - 404 NotFound: si libro/comprador/vendedor no existen.

- DELETE /api/ventas/{codigoDeCompra}?vendedorId={vendedorId}
  - Soft-delete de la venta. Como aún no hay autenticación, el endpoint requiere `vendedorId` como query param para validar que quien elimina sea el vendedor de la venta.
  - Respuestas:
    - 204 No Content: eliminado.
    - 400 Bad Request: vendedorId faltante o venta ya eliminada.
    - 403 Forbid: vendedorId distinto al vendedor asociado a la venta.
    - 404 Not Found: venta / vendedor no encontrados.

Datos iniciales en `data/`:
- `libros.json` contiene algunos libros con `Precio`.
- `compradores.json`, `vendedores.json` con personas de ejemplo.
- `ventas.json` empieza vacío.

Nota sobre autenticación y autorización:
- Requisito funcional: "solo usuarios autenticados pueden registrar ventas" y "solo el vendedor puede eliminar".
- Como no hay auth aún, se realiza la validación de eliminación con `vendedorId` en query. Cuando se implemente auth, sustituir por la identidad del usuario autenticado y roles.

Formato de fecha: se usa ISO 8601 (UTC recomendable). `FechaDeVenta` se almacena como DateTime.
