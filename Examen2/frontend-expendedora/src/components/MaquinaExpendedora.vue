<template>
  <div>
    <div v-if="!mostrarPago && !mostrarVuelto">
      <h3 class="subtitulo">Ingrese la cantidad de refrescos que desea adquirir</h3>

      <p v-if="cargando">Cargando datos desde el servidor...</p>

      <table class="table table-bordered" v-if="!cargando && refrescos.length > 0">
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
              <img class="bebida-img" :src="obtenerImagen(producto.nombre)" :alt="producto.nombre" />
              <span>{{ producto.nombre }}</span>
            </td>
            <td>{{ producto.precio }}</td>
            <td>{{ producto.stock }}</td>
            <td>
              <input type="number" class="form-control cantidad-input" v-model.number="producto.cantidad"
                :min="0" :max="producto.stock" step="1" :disabled="inputActivo !== null && inputActivo !== indice"
                @focus="inputActivo = indice" @blur="() => manejarFinEdicion(indice)"
                @input="filtrarEntrada($event, indice)" />
            </td>
          </tr>
        </tbody>
      </table>

      <p v-if="mensajeErrorCantidad" class="text-danger text-center mt-2">
        {{ mensajeErrorCantidad }}
      </p>

      <h5>Total parcial: ₡{{ subtotal }}</h5>
      <button class="btn btn-primary" @click="mostrarPago = true" :disabled="!botonHabilitado">Continuar con el pago</button>
    </div>

    <div v-if="mostrarPago && !mostrarVuelto" class="mt-4">
      <h4 class="subtitulo">Ingrese el dinero</h4>
      <div class="row justify-content-center">
        <div class="col-auto" v-for="(denominacion, index) in denominaciones" :key="index">
          <label>
            {{ denominacion.tipo === 'billete' ? `Cantidad de billetes de ₡${denominacion.valor}` : `Cantidad de monedas de ₡${denominacion.valor}` }}
          </label>
          <input type="number" class="form-control dinero-input" v-model.number="denominacion.cantidad"
            min="0" max="999" @input="validarIngreso(index, $event)"
            :disabled="indiceActivo !== null && indiceActivo !== index"
            @focus="indiceActivo = index" @blur="indiceActivo = null" />
        </div>
      </div>
      <h5>Monto ingresado: ₡{{ montoIngresado }}</h5>
      <button class="btn btn-primary mt-3" :disabled="!botonPagoHabilitado" @click="confirmarPago">
        Confirmar pago
      </button>
    </div>

    <div v-if="mostrarVuelto">
      <h4 class="subtitulo">Vuelto entregado</h4>

      <p class="mensaje-vuelto">{{ mensajeVuelto }}</p>

      <div class="row justify-content-center">
        <div v-for="denominacion in denominaciones" :key="denominacion.valor" class="col-auto">
          <label>
            {{ denominacion.tipo === 'billete' ? `Billetes de ₡${denominacion.valor}` : `Monedas de ₡${denominacion.valor}` }}
          </label>
          <input 
            type="text" 
            class="form-control vuelto-input" 
            :value="vuelto[denominacion.valor] || 0" 
            readonly 
            disabled 
          />
        </div>
      </div>

      <h5>Total vuelto entregado: ₡{{ totalVuelto }}</h5>

      <button class="btn btn-primary" @click="resetearCompra">Nueva compra</button>
    </div>

    <footer class="border-top footer text-muted">
      <div class="container">&copy; 2025 - Examen 2 B54291 - <a href="#">Privacy</a></div>
    </footer>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'
import { API_BASE } from '../api'

const mostrarPago = ref(false)
const mostrarVuelto = ref(false)
const refrescos = ref([])
const cargando = ref(true)
const vuelto = ref({})
const mensajeVuelto = ref("")
const mensajeErrorCantidad = ref("")
const inputActivo = ref(null)
const indiceActivo = ref(null)

onMounted(async () => {
  await cargarRefrescos()
})

async function cargarRefrescos() {
  cargando.value = true
  try {
    const res = await axios.get(`${API_BASE}/api/stock/refrescos`)
    refrescos.value = res.data.map(r => ({ ...r, cantidad: 0 }))
  } catch (error) {
    console.error("Error cargando refrescos:", error)
  } finally {
    cargando.value = false
  }
}

const subtotal = computed(() =>
  refrescos.value.reduce((acum, r) => acum + (r.cantidad * r.precio), 0)
)

const botonHabilitado = computed(() => inputActivo.value === null && subtotal.value > 0)

function validarCantidad(indice) {
  const producto = refrescos.value[indice]
  const cantidad = producto.cantidad

  if (cantidad > producto.stock) {
    mensajeErrorCantidad.value = `La cantidad ingresada de ${producto.nombre} supera el stock disponible. Se ajustó al máximo permitido.`
    producto.cantidad = producto.stock
  } else {
    mensajeErrorCantidad.value = ""
    producto.cantidad = Math.max(0, cantidad || 0)
  }
}

function manejarFinEdicion(indice) {
  validarCantidad(indice)
  inputActivo.value = null
}

function filtrarEntrada(event, indice) {
  const numero = parseInt(event.target.value)
  refrescos.value[indice].cantidad = isNaN(numero) ? 0 : numero
}

function obtenerImagen(nombre) {
  const lower = nombre.toLowerCase()
  if (lower.includes("coca")) return new URL('../assets/refrescos/cocacola.png', import.meta.url).href
  if (lower.includes("sprite")) return new URL('../assets/refrescos/sprite.png', import.meta.url).href
  if (lower.includes("fanta")) return new URL('../assets/refrescos/fanta.png', import.meta.url).href
  if (lower.includes("pepsi")) return new URL('../assets/refrescos/pepsi.png', import.meta.url).href
  return ''
}

const denominaciones = ref([
  { valor: 1000, tipo: 'billete', cantidad: 0 },
  { valor: 500, tipo: 'moneda', cantidad: 0 },
  { valor: 100, tipo: 'moneda', cantidad: 0 },
  { valor: 50, tipo: 'moneda', cantidad: 0 },
  { valor: 25, tipo: 'moneda', cantidad: 0 },
])

const montoIngresado = computed(() =>
  denominaciones.value.reduce((total, d) => total + d.valor * d.cantidad, 0)
)

const botonPagoHabilitado = computed(() =>
  denominaciones.value.every(d => d.cantidad >= 0 && d.cantidad <= 999) &&
  indiceActivo.value === null
)

function validarIngreso(indice, event) {
  const numero = parseInt(event.target.value)
  denominaciones.value[indice].cantidad = isNaN(numero) || numero < 0 || numero > 999 ? 0 : numero
}

async function confirmarPago() {
  const refrescosSeleccionados = {}
  refrescos.value.forEach(r => {
    if (r.cantidad > 0) refrescosSeleccionados[r.nombre] = r.cantidad
  })

  const dineroIngresado = {}
  denominaciones.value.forEach(d => {
    if (d.cantidad > 0) dineroIngresado[d.valor] = d.cantidad
  })

  try {
    const res = await axios.post(`${API_BASE}/api/stock/confirmar-pago`, {
      refrescosSeleccionados,
      dineroIngresado
    })

    mensajeVuelto.value = res.data.mensaje || ""
    vuelto.value = res.data.vuelto || {}
    mostrarPago.value = false
    mostrarVuelto.value = true

    await cargarRefrescos()
  } catch (err) {
    mensajeVuelto.value = err.response?.data?.mensaje || "Error procesando el pago"
    mostrarPago.value = false
    mostrarVuelto.value = true
    vuelto.value = { 0: 0 }
  }
}

function resetearCompra() {
  refrescos.value.forEach(r => r.cantidad = 0)
  denominaciones.value.forEach(d => d.cantidad = 0)
  mensajeVuelto.value = ""
  mensajeErrorCantidad.value = ""
  mostrarVuelto.value = false
}

const totalVuelto = computed(() =>
  Object.entries(vuelto.value)
    .reduce((total, [valor, cantidad]) => total + parseInt(valor) * cantidad, 0)
)
</script>

<style scoped>
.cantidad-input, .dinero-input, .vuelto-input {
  width: 80px;
  text-align: center;
  margin: 0 auto;
  display: block;
}

.table {
  width: 100%;
  text-align: center;
}

.table th, .table td {
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

.vuelto-input {
  background-color: #f1f1f1;
  cursor: not-allowed;
}

.mensaje-vuelto {
  text-align: center;
  font-size: 16px;
  color: #444;
  margin: 15px 0;
}
</style>
