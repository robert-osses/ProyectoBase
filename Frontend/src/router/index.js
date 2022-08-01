import Vue from "vue";
import VueRouter from "vue-router";
import Login from "../views/Login.vue";

import Contacto from "../views/Contacto.vue";
import Usuarios from "../views/Usuarios.vue";

import Dashboard from "../views/Dashboard.vue";
import Predios from "../views/Predios.vue";

import LoginLayout from '@/views/layouts/Login';
import MainLayout from '@/views/layouts/Main';
import NotFound from "@/views/NotFound.vue";

import store from "@/store/index";

//import store from "@/store/index";

Vue.use(VueRouter);

const routes = [
  {
    path: "/login",
    name: "login",
    component: Login,
    meta: {
      requiresAuth: false,
      whoCan: [0],
      layout: LoginLayout
    }
  },
  {
    path: "/",
    name: "login",
    component: Login,
    meta: {
      requiresAuth: false,
      whoCan: [0],
      layout: LoginLayout
    }
  },
  {
    path: "/contacto",
    name: "contacto",
    component: Contacto,
    meta: {
      requiresAuth: false,
      whoCan: [0],
      layout: LoginLayout
    }
  },
  {
    path: "/usuarios",
    name: "usuarios",
    component: Usuarios,
    meta: {
      requiresAuth: true,
      whoCan: [141],
      layout: MainLayout
    }
  },  
  {
    path: "/dashboard",
    name: "dashboard",
    component: Dashboard,
    meta: {
      requiresAuth: true,
      whoCan: [141],
      layout: MainLayout
    }
  },
  {
    path: "/predios",
    name: "predios",
    component: Predios,
    meta: {
      requiresAuth: true,
      whoCan: [141],
      layout: MainLayout
    }
  },
  {
    path: "/404",
    name: "404",
    component: NotFound,
    meta: {
      requiresAuth: false,
      whoCan: [0],
      layout: LoginLayout,
    },
  },
  {
    path: "*",
    redirect: "/404",
  },      
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});


router.beforeEach((to, from, next) => {
  const whoCan = to.meta.whoCan;
  const currentUser = store.state.currentUser;

  if(to.meta.requiresAuth) {
      if(currentUser) {
          if(whoCan.includes(parseInt(currentUser.Rol))) {
              //Tiene acceso a la vista
              return next();
          }
          else {
              //No tiene acceso
              return next({path: '/404'});
          }
      }
      else {
          //Si no está logeado, expulsa al login.
          return next({path: '/'});
      }
  }
  else {
      //Si no requiere autenticación, continúa.
      return next();
  }

});

export default router;
