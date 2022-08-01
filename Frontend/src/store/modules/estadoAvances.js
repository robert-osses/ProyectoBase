import axios from 'axios';

export default {
    namespaced: true,
    state:{
        listaEstadosAvance: [],
        loading: false,
    },
    mutations:{
        GETTING_DATA(state) {
            state.loading = true;
          },
          END_GETTING_DATA(state) {
            state.loading = false;
          },
        ADD_ESTADOSAVANCE(state, listaEstadosAvance){
            state.listaEstadosAvance = listaEstadosAvance;
        },
    },
    actions:{
        async nuevoEstadoAvance(context, estadoAvance){
            console.log(estadoAvance);
            console.log(`${process.env.VUE_APP_BASE_URL_API}/EstadosAvance/crearEstadoAvance`);
            return axios.post(`${process.env.VUE_APP_BASE_URL_API}/EstadosAvance/crearEstadoAvance`, estadoAvance
            , {
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

        async getEstadoAvanceTarea(context, tarea){
            console.log(tarea);
            console.log(context);
            context.commit("GETTING_DATA");
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/EstadosAvance/getEstadoAvancesTarea/${tarea}`
            , {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                if (res.status == 200) {
                    context.commit("ADD_ESTADOSAVANCE", res.data);
                    console.log(res);
                    context.commit("END_GETTING_DATA");
                    return res.data;
                }
            }).catch((error) => {
                context.commit("END_GETTING_DATA");
                return error;
            });
        }

    },
    getters:{
        estadosAvance(state){
            return state.listaEstadosAvance;
        },
        isLoading(state) {
            return state.loading;
        }
    }
}