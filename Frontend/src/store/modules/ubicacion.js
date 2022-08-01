import axios from "axios";
import router from '@/router'

export default {
	namespaced: true,
    state: {
        regiones:[],
        provincias:[],
        comunas:[],
		loading: false,
    },
    mutations: {
        ADD_REGIONES(state, payload) {
            state.regiones = payload;
        },
        ADD_PROVINCIAS(state, payload) {
            state.provincias = payload;
        },
        ADD_COMUNAS(state, payload) {
            state.comunas = payload;
        }         
    },
    actions: {
        async fetchRegiones(context){
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/regiones`, {
              headers: {
                "Authorization": `bearer ${context.rootState.token}`
              }
            })
              .then((response) => {
                if (response.data != null) {
                  context.commit('ADD_REGIONES', response.data.data);
                }
                return response;
              })
              .catch((error) => {
                console.log(error);
                if (error.response.status == 401) {
                  console.log("paso por aca")
                  context.commit('LOGOUT', null, { root: true });
                  router.replace({ name: "login" });
                }
                return error;
              })
        },
        async fetchProvinciasByRegionId(context, id_region){
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/regiones/${id_region}/listarprovincia`, {
              headers: {
                "Authorization": `bearer ${context.rootState.token}`
              }
            })
              .then((response) => {
                if (response.data != null) {
                  context.commit('ADD_PROVINCIAS', response.data.data);
                }
                return response;
              })
              .catch((error) => {
                console.log(error);
                context.commit('END_GETTING_DATA');
                if (error.response.status == 401) {
                  console.log("paso por aca")
                  context.commit('LOGOUT', null, { root: true });
                  router.replace({ name: "login" });
                }
                return error;
              })
        },
        async fetchComunasByProvinciaId(context, id_provincia){
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/provincias/${id_provincia}`, {
              headers: {
                "Authorization": `bearer ${context.rootState.token}`
              }
            })
              .then((response) => {
                if (response.data != null) {
                  context.commit('ADD_COMUNAS', response.data.data);
                }
                return response;
              })
              .catch((error) => {
                console.log(error);
                if (error.response.status == 401) {
                  console.log("paso por aca")
                  context.commit('LOGOUT', null, { root: true });
                  router.replace({ name: "login" });
                }
                return error;
              })
        },
        async fetchProvinciaById(context, id_provincia){
          return axios.get(`${process.env.VUE_APP_BASE_URL_API}/provincias/mostrar/${id_provincia}`, {
            headers: {
              "Authorization": `bearer ${context.rootState.token}`
            }
          })
            .then((response) => {
              if (response.data != null) {
                return response.data;
              }
              return response.data;
            })
            .catch((error) => {
              console.log(error);
              if (error.response.status == 401) {
                console.log("paso por aca")
                context.commit('LOGOUT', null, { root: true });
                router.replace({ name: "login" });
              }
              return error;
            })
      },        
    },
    getters: {
		isloading(state){
			return state.loading;
		},
        regiones(state) {
			return state.regiones;
        },
        provincias(state) {
			return state.provincias;
        },
        comunas(state) {
			return state.comunas;
        }         
    }
}