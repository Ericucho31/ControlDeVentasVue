import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'

import Categorias from "../components/Almacen/Categoria.vue"
import Articulos from "../components/Almacen/Articulos.vue"
import Roles from "../components/Usuarios/Roles.vue"
import Usuarios from "../components/Usuarios/Usuarios.vue"
import Ventas from "../components/Ventas/Ventas.vue"
import Clientes from "../components/Ventas/Clientes.vue"
import Ingresos from "../components/Compras/Ingresos.vue"
import Proveedor from "../components/Compras/Proveedor.vue"
import GestionCompras from "../components/Consultas/GestionCompras.vue"
import GestionVentas from "../components/Consultas/GestionVentas.vue"

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  {
    path: '/categorias',
    name: 'categorias',
    component: Categorias
  },
  {
    path: '/articulos',
    name: 'articulos',
    component: Articulos
  },
  {
    path: '/roles',
    name: 'roles',
    component: Roles
  },
  {
    path: '/usuarios',
    name: 'usuarios',
    component: Usuarios
  },
  {
    path: '/ventas',
    name: 'ventas',
    component: Ventas
  },
  {
    path: '/clientes',
    name: 'clientes',
    component: Clientes
  },
  {
    path: '/ingresos',
    name: 'ingresos',
    component: Ingresos
  },
  {
    path: '/proveedor',
    name: 'proveedor',
    component: Proveedor
  },
  {
    path: '/gestionventas',
    name: 'consultarVentas',
    component: GestionVentas
  },
  {
    path: '/gestioncompras',
    name: 'consultarCompras',
    component: GestionCompras
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
