import axios from 'axios';

export default {
    namespaced: true,
    state:{
        tareas: [],
        iniciativa: [],
        loading: false,
        page: 0,
        pages: 0,
    },
    mutations:{
        GETTING_DATA(state){
            state.loading = true;
        },
        END_GETTING_DATA(state){
            state.loading = false;
        },
        ADD_TAREAS(state, tareas){
            state.tareas = tareas;
        },
        SET_TAREAS(state, tarea){
            state.tarea = tarea;
        },
        DELETE_TAREA(state, tarea){
            const index = state.tareas.indexOf(tarea);
            state.tareas.splice(index, 1);
        },
        ADD_INICIATIVA(state, iniciativa) {
            state.iniciativa = iniciativa;
        },
        CHANGE_PAGE(state, page) {
            state.page = page
        },
        ADD_PAGES(state, pages) {
            state.pages = pages
        },
    },
    actions:{
        async fetchTareas(context, params){
            console.log(params);
            context.commit('GETTING_DATA');
            console.log('Cargando tareas...');
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/tareas/getTareas?page=${params.page}&planid=${params.planId}&tipo_tareaid=${params.tipoTareaId}&busqueda=${params.busqueda}`, {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                if (res.data != null) {
                    context.commit('ADD_PAGES', res.data.pages);
                    context.commit('CHANGE_PAGE', res.data.currentPage);
                    context.commit('ADD_TAREAS', res.data.data);
                }
                context.commit('END_GETTING_DATA');
                console.log('Tareas cargadas!');
                console.log(res.data);
                return res;
            })
            .catch((error) => {
                console.log(error);
                console.log('Se produjo un error al cargar las tareas');
                context.commit('END_GETTING_DATA');
                return error;
            })
        },
        async buscarTarea(context, id){
            return axios.post(`${process.env.VUE_APP_BASE_URL_API}/tareas/${id}`,
            {
                "ID": id
            },
            {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                return res;
            })
            .catch((error) => {
                return error;
            })
        },
        async nuevaTarea(context, tarea){

            delete tarea.id;
            console.log(tarea);

            let newTarea = {
                numero: tarea.numero,
                titulo: tarea.titulo,
                descripcion: tarea.descripcion,
                inicio: tarea.inicio,
                plazo: tarea.plazo,
                cumplimiento: tarea.cumplimiento,
                planificado: tarea.planificado,
                tipO_TAREAID: tarea.tipO_TAREAID,
                unidadcodigo: tarea.unidadcodigo.codigo
                }

                console.log(newTarea);

            return axios.post(`${process.env.VUE_APP_BASE_URL_API}/tareas/crearTarea`, newTarea
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
        async modificarTarea(context, tarea){
            console.log(tarea);
            let editTarea = {
                id: tarea.id,
                numero: tarea.numero,
                titulo: tarea.titulo,
                descripcion: tarea.descripcion,
                inicio: tarea.inicio,
                plazo: tarea.plazo,
                cumplimiento: tarea.cumplimiento,
                planificado: tarea.planificado,
                tipO_TAREAID: tarea.tipO_TAREAID,
                unidadcodigo: tarea.unidadcodigo.codigo
                }
            console.log(`${process.env.VUE_APP_BASE_URL_API}/tarea/${tarea.id}`);
            return axios.put(`${process.env.VUE_APP_BASE_URL_API}/tareas/actualizarTarea/${tarea.id}`, editTarea,
            {
            headers: {
                "Authorization": `bearer ${context.rootState.token}`
            }
            })
            .then((res) => {
            if (res.status == 200) {

                return res;
            }
            }).catch((error) => {
                return error;
            });
        }, 
        async eliminarTarea(context, tarea){
            console.log(context);
            console.log(tarea);
            console.log(`${process.env.VUE_APP_BASE_URL_API}/tarea/eliminarTarea/${tarea.id}`);
            return axios.put(`${process.env.VUE_APP_BASE_URL_API}/tareas/eliminarTarea/${tarea.id}`, tarea.id,
            {
            headers: {
                "Authorization": `bearer ${context.rootState.token}`
            }
            })
            .then((res) => {
            if (res.status == 200) {

                return res;
            }
            }).catch((error) => {
                return error;
            });
        },
        async fetchTareasEstructuraSuperior(context){
            context.commit('GETTING_DATA');
            return axios.get(`${process.env.VUE_APP_BASE_URL_API}/tareas/GetTareasEstructuraSuperior`, {
                headers: {
                    "Authorization": `bearer ${context.rootState.token}`
                }
            }).then((res) => {
                //console.log(res.data);
                context.commit('END_GETTING_DATA');
                return res;
            })
            .catch((error) => {
                console.log(error);
                console.log('Se produjo un error al cargar las tareas');
                context.commit('END_GETTING_DATA');
                return error;
            })
        },    
    },
    getters:{
        isLoading(state){
            return state.loading;
        },
        tareas(state){
            return state.tareas;
        },
        iniciativa(state) {
			return state.iniciativa;
        },
        page(state) {
            return state.page;
        },
        pages(state) {
            return state.pages;
        },
    }
}