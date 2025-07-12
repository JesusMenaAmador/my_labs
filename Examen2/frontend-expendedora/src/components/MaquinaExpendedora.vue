<template>
  <div>
    <!-- Sección de selección de refrescos -->
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

    <!-- Sección de ingreso de dinero -->
    <div v-if="mostrarPago" class="mt-4">
      <h4 class="subtitulo">Ingrese el dinero</h4>

      <div class="row justify-content-center">
        <div class="col-auto" v-for="(denominacion, index) in denominaciones" :key="index">
          <label :for="'den-' + denominacion.valor">
            {{ denominacion.tipo === 'billete' ? `Cantidad de billetes de ₡${denominacion.valor}` : `Cantidad de monedas de ₡${denominacion.valor}` }}
          </label>
          <input
            type="number"
            class="form-control dinero-input"
            v-model.number="denominacion.cantidad"
            min="0"
            max="999"
            @input="validarIngreso(index, $event)"
            :disabled="indiceActivo !== null && indiceActivo !== index"
            @focus="indiceActivo = index"
            @blur="indiceActivo = null"
          />
        </div>
      </div>

      <h5>Monto ingresado: ₡{{ montoIngresado }}</h5>

      <button class="btn btn-primary mt-3" :disabled="!botonPagoHabilitado" @click="confirmarPago">
        Confirmar pago
      </button>
    </div>

    <footer class="border-top footer text-muted">
      <div class="container">
        &copy; 2025 - Examen 2 B54291 - <a href="#">Privacy</a>
      </div>
    </footer>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'
import { API_BASE } from '../api'

const mostrarPago = ref(false)
const refrescos = ref([])
const inputActivo = ref(null)
const indiceActivo = ref(null)

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

  if (isNaN(numero) || numero < 0) {
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

const denominaciones = ref([
  { valor: 1000, tipo: 'billete', cantidad: 0 },
  { valor: 500, tipo: 'moneda', cantidad: 0 },
  { valor: 100, tipo: 'moneda', cantidad: 0 },
  { valor: 50, tipo: 'moneda', cantidad: 0 },
  { valor: 25, tipo: 'moneda', cantidad: 0 },
])

const montoIngresado = computed(() => {
  return denominaciones.value.reduce((total, d) => total + d.valor * d.cantidad, 0)
})

const botonPagoHabilitado = computed(() => {
  const todosValidos = denominaciones.value.every(d => d.cantidad >= 0 && d.cantidad <= 999)
  const sinCampoActivo = indiceActivo.value === null
  return todosValidos && sinCampoActivo
})

function validarIngreso(indice, event) {
  const valor = event.target.value
  const numero = parseInt(valor)

  if (isNaN(numero) || numero < 0 || numero > 999) {
    denominaciones.value[indice].cantidad = 0
  } else {
    denominaciones.value[indice].cantidad = numero
  }

  denominaciones.value.forEach((d, i) => {
    if (i !== indice) d.cantidad = 0
  })
}

async function confirmarPago() {
  const refrescosSeleccionados = {}
  refrescos.value.forEach(r => {
    if (r.cantidad > 0) {
      refrescosSeleccionados[r.nombre] = r.cantidad
    }
  })

  const dineroIngresado = {}
  denominaciones.value.forEach(d => {
    if (d.cantidad > 0) {
      dineroIngresado[d.valor] = d.cantidad
    }
  })

  try {
    const respuesta = await axios.post(`${API_BASE}/api/stock/confirmar-pago`, {
      refrescosSeleccionados,
      dineroIngresado
    })
    alert(respuesta.data.mensaje)

    mostrarPago.value = false
    refrescos.value.forEach(r => r.cantidad = 0)
    denominaciones.value.forEach(d => d.cantidad = 0)
  } catch (error) {
    alert(error.response?.data?.mensaje || "Error procesando el pago.")
  }
}
</script>

<style scoped>
.cantidad-input,
.dinero-input {
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
