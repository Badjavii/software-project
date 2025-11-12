import { mount } from '@vue/test-utils'
import { describe, it, expect, vi } from 'vitest'
import RegisterSale from '../src/components/RegisterSale.vue'
import * as api from '../src/services/api'

describe('RegisterSale', () => {
  it('envía datos y muestra success', async () => {
    const fake = vi.spyOn(api, 'registrarVenta').mockResolvedValue({})
    const wrapper = mount(RegisterSale)

    const inputs = wrapper.findAll('input')
    expect(inputs.length).toBe(3)
    await inputs[0].setValue(12)
    await inputs[1].setValue(1)
    await inputs[2].setValue(1)

    await wrapper.find('form').trigger('submit.prevent')
    // wait a tick
    await wrapper.vm.$nextTick()

    expect(fake).toHaveBeenCalled()
    expect(wrapper.text()).toContain('Venta registrada')
    fake.mockRestore()
  })
})
