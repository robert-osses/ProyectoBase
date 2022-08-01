import axios from 'axios';

export default {
    namespaced: true,
    state:{
        listaTipoPlanes: [],
        tipo_plan: [],
        planes: [],
        planEstructura: [],
        loading: false,
    },
    mutations:{
        GETTING_DATA(state) {
            state.loading = true;
          },
          END_GETTING_DATA(state) {
            state.loading = false;
          },
        ADD_TIPOPLANES(state, listaTipoPlanes){
            state.listaTipoPlanes = listaTipoPlanes;
        },
        ADD_TIPOPLAN(state, data){
            state.tipo_plan = data;
        },
        ADD_PLANES(state, planes){
            state.planes = planes;
        },
        ADD_PLANESTRUCTURA(state, planEstructura){
            state.planes = planEstructura;
        },
    },
    actions:{

        async getTipoPlanes(context){
            context.commit("GETTING_DATA");
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/Tipo_Plan/GetListarTipos_Planes`
            , {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                //console.log(res);
                if (res.status == 200) {
                    context.commit("ADD_TIPOPLANES", res.data);
                    context.commit("END_GETTING_DATA");
                    return res.data;
                }
            }).catch((error) => {
                context.commit("END_GETTING_DATA");
                return error;
            });
        },
        async getTipoPlan(context, id){
            //console.log(id);
            context.commit("GETTING_DATA");
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/Tipo_Plan/GetTipoPlan/${id}`
            , {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                //console.log(res.data.data);
                if (res.status == 200) {
                    context.commit("ADD_TIPOPLAN", res.data.data);
                    context.commit("END_GETTING_DATA");
                    return res.data;
                }
            }).catch((error) => {
                context.commit("END_GETTING_DATA");
                return error;
            });
        },
        async getPlanesTipo(context, id){
            context.commit("GETTING_DATA");
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/Planes/GetPlanesTipo/${id}`
            , {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                console.log(res);
                if (res.status == 200) {
                    context.commit("ADD_PLANES", res.data);
                    context.commit("END_GETTING_DATA");
                    return res.data;
                }
            }).catch((error) => {
                context.commit("END_GETTING_DATA");
                return error;
            });
        },

        async nuevoPlan(context, plan){
            console.log(plan);
            console.log(`${process.env.VUE_APP_BASE_URL_API}/Planes/crearPlan`);
            return axios.post(`${process.env.VUE_APP_BASE_URL_API}/Planes/crearPlan`, plan
            , {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            })
            .then((res) => {
                console.log(res);
                if (res.status == 200) {
                    return res.data;
                }
            }).catch((error) => {
                return error;
            });
        },
        async actualizarPlan(context, plan){
            console.log(plan);
            console.log(`${process.env.VUE_APP_BASE_URL_API}/Planes/actualizarPlan`);
            return axios.put(`${process.env.VUE_APP_BASE_URL_API}/Planes/actualizarPlan`, plan
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
        async getPlanEstructura(context, params){
            console.log(params);
            context.commit("GETTING_DATA");
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/PlanEstructura/getPlanEstructura?id=${params.tipoPlan}&orden=${params.orden}`
            , {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                //console.log(res.data);
                if (res.status == 200) {
                    context.commit("ADD_PLANESTRUCTURA", res.data);
                    context.commit("END_GETTING_DATA");
                    return res.data;
                }
            }).catch((error) => {
                context.commit("END_GETTING_DATA");
                return error;
            });
        },

    },
    getters:{
        listaTipoPlanes(state){
            return state.listaTipoPlanes;
        },
        planes(state){
            return state.planes;
        },
        isLoading(state) {
            return state.loading;
        },
        tipo_plan(state){
            return state.tipo_plan;
        },
        planEstructura(state){
            return state.planEstructura;
        },
        
    }
}