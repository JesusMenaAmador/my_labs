import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

router.beforeEach((to, from, next) => {
  const defaultTitle = 'Aplicación Vue'
  document.title = to.meta.title || defaultTitle
  next()
})

createApp(App).use(router).mount('#app')
