import axios from 'axios';

export default {
  namespaced: true,
  state:{
      usuarios: [],
      loading: false,
  },
  mutations:{
      GETTING_DATA(state){
          state.loading = true;
      },
      END_GETTING_DATA(state){
          state.loading = false;
      },
      ADD_USUARIOS(state, usuarios){
          state.usuarios = usuarios;
      },
  },
  actions:{
      async fetchUsuarios(context){
          context.commit('GETTING_DATA');
          return axios.get(`${process.env.VUE_APP_BASE_URL_API}/Usuarios/getUsuarios`,{
              headers: {
                  "Authorization": `bearer ${context.rootState.token}`
              }
          }).then((res) => {
              if (res.data != null) {
                  console.log(res.data);
                  context.commit('ADD_USUARIOS', res.data);
              }
              context.commit('END_GETTING_DATA');
              return res;
          })
          .catch((error) => {
              console.log(error);
              console.log('Se produjo un error al cargar el listado de usuarios');
              context.commit('END_GETTING_DATA');
              return error;
          })
      },

      async postUsuario(context, u){
          let usuario = {
              run: parseInt(u.run),
              rolid: u.rol.id,
              unidadcodigo: u.unidad.codigo,
              estado: parseInt(u.estado),
          }
          return axios.post(`${process.env.VUE_APP_BASE_URL_API}/Usuarios/postUsuario`, usuario, {
              headers: {
                  "Authorization": `bearer ${context.rootState.token}`
              }
          })
          .then((res) => {
              if (res.status == 200) {
                  return res.data;
          }
          }).catch((error) => {
              return error;
          });
      },

      async putUsuario(context, u){
          let usuario = {
              run: parseInt(u.run),
              rolid: u.rol.id,
              unidadcodigo: u.unidad.codigo,
              estado: parseInt(u.estado),
          }
          return axios.put(`${process.env.VUE_APP_BASE_URL_API}/Usuarios/putUsuario`, usuario, {
              headers: {
                  "Authorization": `bearer ${context.rootState.token}`
              }
          })
          .then((res) => {
              if (res.status == 200) {
                  return res.data;
              }
          }).catch((error) => {
              return error;
          });
      },
  },
  getters:{
      isLoading(state) {
          return state.loading;
      },
      usuarios(state) {
          return state.usuarios;
      },
  }
}