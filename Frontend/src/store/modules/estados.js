export default {
    namespaced: true,
    state:{
        estados: [
            {
                id: 1,
                nombre: 'Activo'
            },
            {
                id: 0,
                nombre: 'Borrado'
            },
        ],
    },
    getters:{
        estados(state) {
            return state.estados;
        },
    }
}