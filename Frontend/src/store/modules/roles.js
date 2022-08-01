import axios from "axios";

export default {
  namespaced: true,
  state: {
    roles: [],
    loading: false,
  },
  mutations: {
    GETTING_DATA(state) {
      state.loading = true;
    },
    END_GETTING_DATA(state) {
      state.loading = false;
    },
    ADD_ROLES(state, roles) {
      state.roles = roles;      
    },
  },
  actions: {
    async fetchRoles(context) {
      context.commit("GETTING_DATA");
      return axios
        .get(`${process.env.VUE_APP_BASE_URL_API}/Roles/getRoles`, {
          headers: {
            Authorization: `bearer ${context.rootState.token}`,
          },
        })
        .then((response) => {
          if (response.data != null) {
            //console.log(response.data);
            context.commit("ADD_ROLES", response.data);
          }
          context.commit("END_GETTING_DATA");
          return response;
        })
        .catch((error) => {
          console.log(error);
          context.commit("END_GETTING_DATA");
          return error;
        });
    },
  },
  getters: {
    isLoading(state) {
      return state.loading;
    },
    roles(state) {
      return state.roles;
    },
  },
};