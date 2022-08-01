<template>
    <v-container>
        <v-card>
            <v-card-title class="headline">{{ formTareaTitle }}</v-card-title>
            <v-card-text>
            <v-container>

                <v-form v-model="validForm" ref="form">
                <v-row>
                    <v-col cols="12" sm="12" md="12">
                    <v-textarea
                        v-model="estadoAvanceItem.descripcion"
                        label="Descripcion"
                        auto-grow
                    ></v-textarea>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" sm="12" md="12">
                        <v-subheader class="pl-0">
                            Cumplimiento
                        </v-subheader>
                        <v-divider></v-divider>
                        <v-slider
                        v-model="zoom"
                        :thumb-size="24"
                        thumb-label="always"
                        append-icon="mdi-magnify-plus-outline"
                        prepend-icon="mdi-magnify-minus-outline"
                        @click:append="zoomIn"
                        @click:prepend="zoomOut"
                        >
                            
                        </v-slider>
                    </v-col>
                </v-row>
                </v-form>

            </v-container>
            </v-card-text>
            <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary darken-1" text @click="close"
                >Salir</v-btn
            >
            <v-btn color="success darken-1" text @click="guardarEstadoAvance">Guardar</v-btn>
            </v-card-actions>
        </v-card>
    </v-container>
</template>


<script>
import { mapGetters, mapActions } from "vuex";

export default {
    name: 'estadoAvance',
    components: {
    },
    props: [
            "estadoAvanceItem",
            "paginaActual",
            "editedIndex",
            "editedItem",
            "listadoTareas"
    ],

    data(){
        return{
            listaEstadoAvance: [],
            validForm: false,
            menu: false,
            menu2: false,
            zoom : this.estadoAvanceItem.cumplimiento
        }
    },

    computed: {
        formTareaTitle () {
            return this.editedIndex > -1 ? 'Editar Estado Avance' : 'Nuevo Estado Avance'
        },
        ...mapGetters("estadoAvancesStore", ["listaEstadosAvance"]),
    },

    mounted() {
        //this.getEstadoAvanceTarea(8307);
    },
    methods: {
        
        zoomOut() {
            this.zoom = (this.zoom - 1) || 0
        },

        zoomIn(){
            this.zoom = (this.zoom + 1) || 100
        },
        
        validate() {
            this.$refs.form.validate();
        },
        async guardarEstadoAvance() {
            this.validate();

            this.estadoAvanceItem.cumplimiento = this.zoom

            if(this.validForm){

                const res = await this.nuevoEstadoAvance(this.estadoAvanceItem);

                if (res.statusCode == 200) {
                    this.$toastr(
                    "success",
                    "Estado de Avance creado correctamente",
                    "Éxito!"
                    );
                    this.editedItem.cumplimiento= this.zoom

                    Object.assign(this.listadoTareas[this.editedIndex], this.editedItem)
                    this.estadoAvanceItem.descripcion = ""
                    this.$emit('save', true);
                    this.$emit('guardarEstadoAvance', {valorCumplimiento: this.zoom, pagina: this.paginaActual});
                }
                else {
                    this.$toastr(
                    "error",
                    "Ah ocurrido un error", "Ups!",
                    "Error!"
                    );
                }
        //});
            }
        },
        close() {
            this.$refs.form.reset();
            this.$emit('close', true);
        },
        ...mapActions("estadoAvancesStore", ["nuevoEstadoAvance","getEstadoAvanceTarea"]),
    },
}
</script>
