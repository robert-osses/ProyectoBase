import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import unidadesStore from "@/store/modules/unidades";
import ubicacionStore from "@/store/modules/ubicacion";
import usuariosStore from "@/store/modules/usuarios";
import tareasStore from "@/store/modules/tareas";
import rolesStore from "@/store/modules/roles";
import estadosStore from "@/store/modules/estados";
import personasStore from "@/store/modules/personas";
import estadoAvancesStore from "@/store/modules/estadoAvances";
import planesStore from "@/store/modules/planes";


var jwtDecode = require('jwt-decode');

import router from '@/router'

Vue.use(Vuex);

function getLocalUser() {
  const user = localStorage.getItem("sicadeToken");
  if (!user) {
    return null;
  }

  return decodeJwt(user);
}

function getToken() {
  return localStorage.getItem("sicadeToken") || null;
}

function decodeJwt(jwt){
  const token = jwtDecode(jwt);
  return token;
}

function setCookies(token){
  localStorage.setItem("sicadeToken", token);
  localStorage.setItem("sicadeNombre", decodeJwt(token).Nombres);
  localStorage.setItem("sicadeRun", decodeJwt(token).Run);
  localStorage.setItem("sicadeAvatar", decodeJwt(token).Foto);
  localStorage.setItem("sicadeExpira", formatNumericDate(decodeJwt(token).exp));
}

function setInitPage(token){
  let usuarioConectado = decodeJwt(token);
  console.log(usuarioConectado);
  //aca puedes pooner las paginas de inicio por rol
  if(usuarioConectado.Rol == 141){
    router.push({ path: "/dashboard" });
  }
  else {
    //si no encuantra el rol se conectará a esta página
    router.push({ path: "/404" });
  }
}

function formatNumericDate(numericDate){
  //Hora de expiración token
  var myObj = JSON.parse('{"date_created":"' + numericDate + '"}'),
  myDate = new Date(1000*myObj.date_created);

  return myDate.toLocaleString();
}

export default new Vuex.Store({
  state: {
    token: getToken(),
    currentUser: getLocalUser(),
    isLoggedIn: false,//!!user,
    loading: false,
    auth_error: null,
    layout: 'principal-layout',
    drawerLeft: true,
  },
  mutations: {

    LOGIN(state) {
      state.loading = true;
      state.auth_error = null;
    },

    LOGIN_SUCCESS(state, token) {
      state.auth_error = null;
      state.isLoggedIn = true;
      state.loading = false;
      state.currentUser = decodeJwt(token);
      state.token = token;      
      setCookies(token);//guarda cookies

      //console.log(state.currentUser);
    },

    LOGIN_FAILED(state, mensaje) {
      state.loading = false;
      state.auth_error = mensaje;
    },

    LOGOUT(state, mensaje) {
      //console.log('elimina cookies de la sesión');
      localStorage.removeItem("sicadeExpira");
      localStorage.removeItem("sicadeToken");
      state.isLoggedIn = false;
      state.currentUser = null;
      state.token = null;
      state.auth_error = mensaje;
    },

    SET_LAYOUT(state, newLayout) {
      state.layout = newLayout
    },

    toogleDrawer(state) {
      state.drawerLeft = !state.drawerLeft;
    },
  },
  getters: {
    layout(state) {
      return state.layout;
    },
    isLoading(state) {
      return state.loading;
    },
    isLoggedIn(state) {
      return state.isLoggedIn;
    },
    currentUser(state) {
      return state.currentUser;
    },
    authError(state) {
      return state.auth_error;
    },
    getToken(state) {
      return state.token;
    },
  },  
  actions: {

    async login(context, credentials) {  
      //console.log(credentials);
      context.commit('LOGIN');
      return axios.post(`${process.env.VUE_APP_BASE_URL_API}/auth/login`,{
        "RUT": credentials.usuario,
        "PASSWORD": credentials.password
      },{
        headers: {
            "Access-Control-Allow-Control": "*"
        }
      }).then((response) => {
        //console.log(response);
        if (response.data.token != null) {
          context.commit('LOGIN_SUCCESS', response.data.token); 
          setInitPage(response.data.token);
        }
        else {
          context.commit('LOGIN_FAILED','Credenciales incorrectas.')
        }
      }).catch((error) => {
        console.log("error");
        context.commit('LOGIN_FAILED','Error al tratar de conectar, por favor contacte a un administrador.')
        return error;
      })
    },

    async refreshToken({state, commit}){
      if(state.token) {
        return axios.post(`${process.env.VUE_APP_BASE_URL_API}/auth/refresh-token`,{
          "tokenExp": state.token,
        }).then((response) => {
          console.log('refresh-token');
          if (response.data.token != null) {
            commit('LOGIN_SUCCESS', response.data.token)
          }else {
            commit('LOGOUT');
          }
        }).catch((error) => {
          commit('LOGOUT');
          return error;
        })
      }
    },

    logout(context, mensaje) {
      //console.log('entra a logout');
      context.commit('LOGOUT', mensaje);
      router.replace({ name: "login" });
    },
  },
  modules: {
    unidadesStore,
    ubicacionStore,
    usuariosStore,
    tareasStore,
    rolesStore,
    estadosStore,
    personasStore,
    estadoAvancesStore,
    planesStore
  }
});
