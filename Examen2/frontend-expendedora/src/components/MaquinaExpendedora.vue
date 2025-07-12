<template>
  <div>
    <div v-if="!mostrarPago">
      <h3 class="subtitulo">Ingrese la cantidad de refrescos que desea adquirir</h3>

      <table class="table table-bordered" v-if="refrescos.length > 0">
        <thead>
          <tr>
            <th>Refresco</th>
            <th>Precio (₡)</th>
            <th>Stock</th>
            <th>Cantidad a comprar</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(producto, indice) in refrescos" :key="indice">
            <td class="nombre-bebida">
              <img
                class="bebida-img"
                :src="obtenerImagen(producto.nombre)"
                :alt="producto.nombre"
              />
              <span>{{ producto.nombre }}</span>
            </td>
            <td>{{ producto.precio }}</td>
            <td>{{ producto.stock }}</td>
            <td>
              <input
                type="number"
                class="form-control cantidad-input"
                v-model.number="producto.cantidad"
                :min="0"
                :max="producto.stock"
                step="1"
                :disabled="inputActivo !== null && inputActivo !== indice"
                @focus="inputActivo = indice"
                @blur="() => manejarFinEdicion(indice)"
                @input="filtrarEntrada($event, indice)"
              />
            </td>
          </tr>
        </tbody>
      </table>

      <div v-else>
        <p>Cargando datos desde el servidor...</p>
      </div>

      <h5>Total parcial: ₡{{ subtotal }}</h5>

      <button class="btn btn-primary" @click="mostrarPago = true" :disabled="!botonHabilitado">
        Continuar con el pago
      </button>
    </div>

    <div v-if="mostrarPago" class="mt-4">
      <h3 class="subtitulo">Ingrese el dinero para completar la compra</h3>

      <div class="form-group">
        <label for="colones1000">₡1000:</label>
        <input type="number" class="form-control" v-model.number="ingreso.colones1000" min="0" />
      </div>
      <div class="form-group">
        <label for="moneda500">₡500:</label>
        <input type="number" class="form-control" v-model.number="ingreso.moneda500" min="0" />
      </div>
      <div class="form-group">
        <label for="moneda100">₡100:</label>
        <input type="number" class="form-control" v-model.number="ingreso.moneda100" min="0" />
      </div>
      <div class="form-group">
        <label for="moneda50">₡50:</label>
        <input type="number" class="form-control" v-model.number="ingreso.moneda50" min="0" />
      </div>
      <div class="form-group">
        <label for="moneda25">₡25:</label>
        <input type="number" class="form-control" v-model.number="ingreso.moneda25" min="0" />
      </div>

      <h5>Monto total a pagar: ₡{{ subtotal }}</h5>
      <h5>Monto ingresado: ₡{{ montoIngresado }}</h5>

      <button class="btn btn-success" :disabled="montoIngresado <= 0">
        Pagar
      </button>
    </div>
  </div>

  <footer class="border-top footer text-muted">
    <div class="container">
      &copy; 2025 - Examen 2 B54291 - <a href="#">Privacy</a>
    </div>
  </footer>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'
import { API_BASE } from '../api'

const mostrarPago = ref(false)
const refrescos = ref([])
const inputActivo = ref(null)

const ingreso = ref({
  colones1000: 0,
  moneda500: 0,
  moneda100: 0,
  moneda50: 0,
  moneda25: 0,
})

const montoIngresado = computed(() => {
  return (
    ingreso.value.colones1000 * 1000 +
    ingreso.value.moneda500 * 500 +
    ingreso.value.moneda100 * 100 +
    ingreso.value.moneda50 * 50 +
    ingreso.value.moneda25 * 25
  )
})

onMounted(async () => {
  try {
    const respuesta = await axios.get(`${API_BASE}/api/stock/refrescos`)
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

const botonHabilitado = computed(() => {
  return inputActivo.value === null && subtotal.value > 0
})

function validarCantidad(indice) {
  const producto = refrescos.value[indice]

  if (producto.cantidad === '' || isNaN(producto.cantidad)) {
    producto.cantidad = 0
  }

  if (producto.cantidad < 0) {
    producto.cantidad = 0
  }

  if (producto.cantidad > producto.stock) {
    alert(`Solo hay ${producto.stock} unidades de ${producto.nombre} disponibles.`)
    producto.cantidad = producto.stock
  }
}

function manejarFinEdicion(indice) {
  validarCantidad(indice)
  inputActivo.value = null
}

function filtrarEntrada(event, indice) {
  const valor = event.target.value
  const numero = parseInt(valor)

  if (isNaN(numero)) {
    refrescos.value[indice].cantidad = 0
  } else {
    refrescos.value[indice].cantidad = numero
  }
}

function obtenerImagen(nombre) {
  const nombreLimpio = nombre.toLowerCase()

  if (nombreLimpio.includes("coca")) {
    return new URL('../assets/refrescos/cocacola.png', import.meta.url).href
  }
  if (nombreLimpio.includes("sprite")) {
    return new URL('../assets/refrescos/sprite.png', import.meta.url).href
  }
  if (nombreLimpio.includes("fanta")) {
    return new URL('../assets/refrescos/fanta.png', import.meta.url).href
  }
  if (nombreLimpio.includes("pepsi")) {
    return new URL('../assets/refrescos/pepsi.png', import.meta.url).href
  }

  return ''
}
</script>

<style scoped>
.cantidad-input {
  width: 80px;
  text-align: center;
  margin: 0 auto;
  display: block;
}

.table {
  width: 100%;
  text-align: center;
}

.table th,
.table td {
  text-align: center;
  vertical-align: middle;
}

.nombre-bebida {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 10px;
}

.bebida-img {
  width: 32px;
  height: 32px;
  object-fit: contain;
}

.subtitulo {
  text-align: center;
  margin-bottom: 20px;
}

.footer {
  margin-top: 40px;
  padding: 20px 0;
  border-top: 1px solid #ddd;
  text-align: center;
  color: #888;
  font-size: 14px;
}
</style>