import axios from 'axios';
//import router from '@/router'

export default {
    namespaced: true,
    state:{
        personas: [],
        persona: [],
        loading: false,
    },
    mutations:{
        GETTING_DATA(state){
            state.loading = true;
        },
        END_GETTING_DATA(state){
            state.loading = false;
        },
        ADD_PERSONAS(state, personas){
            state.personas = personas;
        },
        ADD_PERSONA(state, persona){
            state.persona = persona;
        },
    },
    actions:{

        async fetchPersonasUnidad(context,codigo){
            //console.log(codigo);
            context.commit('GETTING_DATA');
            //console.log('Cargando personas...');
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/personas/getPersonasUnidad?codigo=${codigo}`, {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                if (res.data != null) {
                    //console.log(res.data);
                    context.commit('ADD_PERSONAS', res.data);
                }
                context.commit('END_GETTING_DATA');
                //console.log('Personal cargado!');
                return res;
            })
            .catch((error) => {
                console.log('Se produjo un error al cargar las personas');
                context.commit('END_GETTING_DATA');
                return error;
            })
        },
        
        async fetchPersonaRun(context, run){
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/Personas/getPersonasRun?run=${run}`, {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                if (res.data != null) {
                    //console.log(res.data);
                    context.commit('ADD_PERSONA', res.data);
                }
                context.commit('END_GETTING_DATA');
                //console.log('Persona cargada!');
                return res;
            })
            .catch((error) => {
                return error;
            })
        },

    },
    getters:{
        isLoading(state){
            return state.loading;
        },
        personas(state){
            return state.personas;
        },
        persona(state){
            return state.persona;
        },
    }
}