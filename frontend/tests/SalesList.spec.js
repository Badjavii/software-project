import { mount } from '@vue/test-utils'
import { describe, it, expect, vi } from 'vitest'

describe('SalesList', () => {
  it('muestra ventas y permite eliminar', async () => {
    // Mock the api module before importing the component so the component uses the mocked functions
    vi.mock('../src/services/api', () => ({
      listarVentas: vi.fn().mockResolvedValue([ { CodigoDeCompra: 1, FechaDeVenta: new Date().toISOString(), MontoTotal: 10.5, LibroId:1, CompradorId:1, VendedorId:1 } ]),
      eliminarVenta: vi.fn().mockResolvedValue(true),
    }))

    const SalesList = (await import('../src/components/SalesList.vue')).default
    const api = await import('../src/services/api')

    const wrapper = mount(SalesList)
    // wait for mounted fetch
  await wrapper.vm.$nextTick()
  await wrapper.vm.$nextTick()
  // flush microtasks to allow promises to resolve
  await new Promise((r) => setTimeout(r, 0))

    expect(wrapper.text()).toContain('Consultar ventas')
    expect(wrapper.html()).toContain('Código')

    // trigger delete by calling method directly
    await wrapper.vm.eliminar(1,1)
    expect(api.eliminarVenta).toHaveBeenCalledWith(1,1)
  })
})
