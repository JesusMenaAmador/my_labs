<template>
  <div>
    <h3>Refrescos disponibles</h3>

    <table class="table table-bordered" v-if="refrescos.length > 0">
      <thead>
        <tr>
          <th>Producto</th>
          <th>Precio (₡)</th>
          <th>Stock</th>
          <th>Cantidad a comprar</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(producto, indice) in refrescos" :key="indice">
          <td>{{ producto.nombre }}</td>
          <td>{{ producto.precio }}</td>
          <td>{{ producto.stock }}</td>
          <td>
            <input
              type="number"
              class="form-control"
              v-model.number="producto.cantidad"
              :min="0"
              :max="producto.stock"
              @change="validarCantidad(indice)"
            />
          </td>
        </tr>
      </tbody>
    </table>

    <div v-else>
      <p>Cargando datos desde el servidor...</p>
    </div>

    <h5>Total parcial: ₡{{ subtotal }}</h5>

    <button class="btn btn-primary" @click="mostrarPago = true" :disabled="subtotal === 0">
      Continuar con el pago
    </button>

    <div v-if="mostrarPago" class="mt-4">
      <!-- Aquí irá la sección de ingreso de dinero -->
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import axios from 'axios';
import { API_BASE } from '../api';

const mostrarPago = ref(false)
const refrescos = ref([])

onMounted(async () => {
  try {
    const respuesta = await axios.get(`${API_BASE}/api/stock/refrescos`)
    console.log('Datos cargados:', respuesta.data)
    refrescos.value = respuesta.data.map(r => ({
      ...r,
      cantidad: 0
    }))
  } catch (error) {
    console.error('Error cargando refrescos:', error)
  }
})

const subtotal = computed(() => {
  return refrescos.value.reduce((acum, r) => acum + (r.cantidad * r.precio), 0)
})

function validarCantidad(indice) {
  const producto = refrescos.value[indice]
  if (producto.cantidad < 0 || producto.cantidad > producto.stock || isNaN(producto.cantidad)) {
    alert(`Ingrese un número válido entre 0 y ${producto.stock} para ${producto.nombre}`)
    producto.cantidad = 0
  }
}
</script>
