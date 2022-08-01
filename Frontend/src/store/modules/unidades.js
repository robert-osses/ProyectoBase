import axios from "axios";
import router from "@/router";

export default {
  namespaced: true,
  state: {
    unidad: [],
    unidades: [],
    estructuras_superiores: [],
    estructura_superior: [],
    loading: false,
  },
  mutations: {
    GETTING_DATA(state) {
      state.loading = true;
    },
    END_GETTING_DATA(state) {
      state.loading = false;
    },
    ADD_UNIDAD(state, unidad) {
      state.unidad = unidad;
    },
    ADD_UNIDADES(state, unidades) {
      state.unidades = unidades;      
    },
    ADD_ESTRUCTURAS_SUPERIORES(state, data) {
      state.estructuras_superiores = data;
    },
    ADD_ESTRUCTURA_SUPERIOR(state, data) {
      state.estructura_superior = data;
    }
  },
  actions: {
    async fetchUnidades(context) {
      context.commit("GETTING_DATA");
      return axios
        .get(`${process.env.VUE_APP_BASE_URL_API}/Unidades/getUnidades`, {
          headers: {
            Authorization: `bearer ${context.rootState.token}`,
          },
        })
        .then((response) => {
          if (response.data != null) {
            //console.log(response.data);
            context.commit("ADD_UNIDADES", response.data);
          }
          context.commit("END_GETTING_DATA");
          return response;
        })
        .catch((error) => {
          console.log(error);
          context.commit("END_GETTING_DATA");
          if (error.response.status == 401) {
            context.commit("LOGOUT", null, { root: true });
            router.replace({ name: "401" });
          }
          return error;
        });
    },
    async fetchUnidad(context, id) {
      //console.log(id);
      context.commit("GETTING_DATA");
      return axios
        .get(`${process.env.VUE_APP_BASE_URL_API}/Unidades/getUnidadesId?id=${id}`, {
          headers: {
            Authorization: `bearer ${context.rootState.token}`,
          },
        })
        .then((response) => {
          if (response.data != null) {
            //console.log(response.data[0]);
            context.commit("ADD_UNIDAD", response.data[0]);
          }
          context.commit("END_GETTING_DATA");
          return response.data;
        })
        .catch((error) => {
          context.commit("END_GETTING_DATA");
          if (error.response.status == 401) {
            context.commit("LOGOUT", null, { root: true });
            router.replace({ name: "401" });
          }
          return error.request;
        });
    },
    async fetchUnidadesESE(context, id) {
      //console.log(id);
      context.commit("GETTING_DATA");
      return axios
        .get(`${process.env.VUE_APP_BASE_URL_API}/Unidades/getUnidadesESE?id=${id}`, {
          headers: {
            Authorization: `bearer ${context.rootState.token}`,
          },
        })
        .then((response) => {
          if (response.data != null) {
            //console.log(response.data);
            context.commit("ADD_UNIDADES", response.data);
          }
          context.commit("END_GETTING_DATA");
          return response.data;
        })
        .catch((error) => {
          context.commit("END_GETTING_DATA");
          if (error.response.status == 401) {
            context.commit("LOGOUT", null, { root: true });
            router.replace({ name: "401" });
          }
          return error.request;
        });
    },
    async fetchEstructurasSuperiores(context) {
      context.commit("GETTING_DATA");
      return axios
        .get(`${process.env.VUE_APP_BASE_URL_API}/EstructurasSuperiores/getEstructurasSuperiores`, {
          headers: {
            Authorization: `bearer ${context.rootState.token}`,
          },
        })
        .then((response) => {
          if (response.data != null) {
            //console.log(response.data);
            context.commit("ADD_ESTRUCTURAS_SUPERIORES", response.data);
          }
          context.commit("END_GETTING_DATA");
          return response;
        })
        .catch((error) => {
          console.log(error);
          context.commit("END_GETTING_DATA");
          if (error.response.status == 401) {
            context.commit("LOGOUT", null, { root: true });
            router.replace({ name: "401" });
          }
          return error;
        });
    },
    async fetchEstructuraSuperior(context, id) {
      context.commit("GETTING_DATA");
      return axios
        .get(`${process.env.VUE_APP_BASE_URL_API}/EstructurasSuperiores/getEstructuraSuperior?id=${id}`, {
          headers: {
            Authorization: `bearer ${context.rootState.token}`,
          },
        })
        .then((response) => {
          if (response.data != null) {
            console.log(response.data);
            context.commit("ADD_ESTRUCTURA_SUPERIOR", response.data[0]);
          }
          context.commit("END_GETTING_DATA");
          return response;
        })
        .catch((error) => {
          console.log(error);
          context.commit("END_GETTING_DATA");
          if (error.response.status == 401) {
            context.commit("LOGOUT", null, { root: true });
            router.replace({ name: "401" });
          }
          return error;
        });
    },
  },
  getters: {
    isLoading(state) {
      return state.loading;
    },
    unidades(state) {
      return state.unidades;
    },
    unidad(state) {
      return state.unidad;
    },
    estructura_superior(state) {
      return state.estructura_superior;
    },
    estructuras_superiores(state) {
      return state.estructuras_superiores;
    },
  },
};
