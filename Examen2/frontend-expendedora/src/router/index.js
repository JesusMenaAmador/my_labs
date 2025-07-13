import { createRouter, createWebHistory } from 'vue-router'
import MaquinaExpendedora from '../components/MaquinaExpendedora.vue'

const routes = [
  {
    path: '/',
    name: 'ExpendedoraDeRefrescos',
    component: MaquinaExpendedora,
    meta: {
      title: 'Expendedora de Refrescos'
    }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router