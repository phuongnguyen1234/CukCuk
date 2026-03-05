import { createRouter, createWebHistory } from 'vue-router'
import Menu from '@/views/menu/Menu.vue'
import NotFound from '@/views/not-found/NotFound.vue'
import TheLayout from '@/layouts/TheLayout.vue'

const routes = [
  {
    path: '/',
    component: TheLayout,
    children: [
      {
        path: '',
        redirect: '/menu',
      },
      {
        path: 'menu',
        name: 'menu',
        component: Menu,
      },
    ],
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'not-found',
    component: NotFound,
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
