import { mount } from '@vue/test-utils'
import { describe, it, expect, vi } from 'vitest'
import RegisterSale from '../src/components/RegisterSale.vue'
import * as api from '../src/services/api'

describe('RegisterSale', () => {
  it('envía datos y muestra success', async () => {
    const fake = vi.spyOn(api, 'registrarVenta').mockResolvedValue({})
    const wrapper = mount(RegisterSale)

    await wrapper.find('input[required]').setValue(123)
    // set other inputs
    const inputs = wrapper.findAll('input')
    await inputs[0].setValue(123)
    await inputs[1].setValue(1)
    await inputs[2].setValue(1)
    await inputs[3].setValue(1)

    await wrapper.find('form').trigger('submit.prevent')
    // wait a tick
    await wrapper.vm.$nextTick()

    expect(fake).toHaveBeenCalled()
    expect(wrapper.text()).toContain('Venta registrada')
    fake.mockRestore()
  })
})
