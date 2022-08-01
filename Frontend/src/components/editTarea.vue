<template>
    <v-container>
        <v-card>
            <v-card-title class="headline">{{ formTareaTitle }}</v-card-title>
            <v-card-text>
            <v-container>

                <v-form v-model="validForm" ref="form">
                <v-row>
                    <v-col cols="12" sm="2" md="2">
                    <v-text-field
                        v-model="editedItem.numero"
                        label="Numero"
                    >
                    </v-text-field>
                    </v-col>
                    <v-col cols="12" sm="4" md="4">
                    <v-select
                        :items="tipo_tareas"
                        item-text="descripcion"
                        item-value="id"
                        label="Tipo Tarea"
                        v-model="editedItem.tipO_TAREAID"
                        
                    ></v-select>
                    </v-col>
                    <v-col cols="12" sm="6" md="6">
                    <v-text-field
                        v-model="editedItem.titulo"
                        label="Titulo"
                    ></v-text-field>
                    </v-col>
                </v-row>
                <v-row>
                    
                </v-row>
                <v-row>
                    <v-col cols="12" sm="12" md="12">
                    <v-textarea
                        v-model="editedItem.descripcion"
                        label="Descripcion"
                        auto-grow
                    ></v-textarea>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" sm="6" md="4">
                        <v-autocomplete
                        v-model="editedItem.unidad"
                        :items="unidades"
                        item-text="descripcion"
                        item-value="codigo"
                        label="Unidad Responsable"
                        :hint="`Puede digitar el nombre de la unidad para buscar...`"
                        persistent-hint
                        return-object>
                        </v-autocomplete>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                    <v-menu 
                        ref="menu"
                        v-model="menu"
                        :close-on-content-click="false"
                        :return-value.sync="editedItem.inicio"
                        transition="scale-trasition"
                        offset-y
                        min-width="auto"
                    >
                        <template v-slot:activator="{ on, attrs}">
                            <v-text-field
                            v-model="editedItem.inicio"
                            label="Fecha Inicio"
                            prepend-icon="mdi-calendar"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                            >
                            </v-text-field>
                        </template>
                        <v-date-picker
                            v-model="editedItem.inicio"
                            no-title
                            scrollable
                        >
                            <v-spacer></v-spacer>
                            <v-btn
                            text
                            color="primary"
                            @click="menu = false"
                            >
                            Cancelar
                            </v-btn>
                            <v-btn
                            text
                            color="primary"
                            @click="$refs.menu.save(editedItem.inicio)"
                            >
                            Ok
                            </v-btn>
                        </v-date-picker>
                    </v-menu>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                    <v-menu 
                        ref="menu2"
                        v-model="menu2"
                        :close-on-content-click="false"
                        :return-value.sync="editedItem.plazo"
                        transition="scale-trasition"
                        offset-y
                        min-width="auto"
                    >
                        <template v-slot:activator="{ on, attrs}">
                            <v-text-field
                            v-model="editedItem.plazo"
                            label="Fecha Plazo"
                            prepend-icon="mdi-calendar"
                            readonly
                            v-bind="attrs"
                            v-on="on"
                            >
                            </v-text-field>
                        </template>
                        <v-date-picker
                            v-model="editedItem.plazo"
                            no-title
                            scrollable
                        >
                            <v-spacer></v-spacer>
                            <v-btn
                            text
                            color="primary"
                            @click="menu2 = false"
                            >
                            Cancelar
                            </v-btn>
                            <v-btn
                            text
                            color="primary"
                            @click="$refs.menu2.save(editedItem.plazo)"
                            >
                            Ok
                            </v-btn>
                        </v-date-picker>
                    </v-menu>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" sm="6" md="4">
                    <v-text-field
                        v-model="editedItem.cumplimiento"
                        label="Cumplimiento"
                    ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                    <v-text-field
                        v-model="editedItem.planificado"
                        label="Planificado"
                    ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                    <v-text-field
                        v-model="editedItem.ponderacion"
                        label="Ponderacion"
                    ></v-text-field>
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
            <v-btn color="success darken-1" text @click="save">Guardar</v-btn>
            </v-card-actions>
        </v-card>
    </v-container>
</template>

<script>
import { mapActions } from "vuex";

export default {
    name: 'editTarea',
    components: {
    },
    props: [
        "editedIndex",
        "editedItem",
        "tipo_tareas",
        "unidades",
        "listadoTareas",
        "page",
        "pages"
    ],

    data(){
        return{
            validForm: false,
            menu: false,
            menu2: false,
            defaultItem: {
                numero: 0,
                descripcion: "default",
                ponderacion: 0,
                inicio: "",
                plazo: "",
                cumplimiento: 0,
                planificado: 0,
                tipO_TAREAID: null,
                unidadcodigo: 0
            },
        }
    },
    computed: {
        formTareaTitle () {
            return this.editedIndex > -1 ? 'Editar Tarea' : 'Nueva Tarea'
        },
    },
    methods: {

        
        validate() {
            this.$refs.form.validate();
        },
        async save() {
            this.validate();
            if(this.editedIndex > -1) {
                if (this.validForm) {
                    const res = await this.modificarTarea(this.editedItem);

                    if (res.status == 200) {
                        Object.assign(this.listadoTareas[this.editedIndex], this.editedItem)
                        this.$toastr(
                            "success",
                            "Tarea modificada correctamente",
                            "Éxito!"
                        );
                        //EMITE QUE SE GUARDARON LOS CAMBIOS Y COMO QUEDÓ EL OBJETO
                        const paramRespuesta = {
                            tipo : 'put',
                            data : this.editedItem
                        }
                        this.$emit('save', paramRespuesta);
                    }
                    else {
                        this.$toastr(
                            "error",
                            "Ah ocurrido un error",
                            "Error!"
                        );
                    }
                }
            }
            else {
                if (this.validForm) {
                    const res = await this.nuevaTarea(this.editedItem);
                    if (res.statusCode == 200) {
                        this.listadoTareas.push(this.editedItem);
                        this.$toastr("success", "Tarea creada correctamente", "Éxito!");
                        
                        //EMITE QUE SE GUARDARON LOS CAMBIOS Y COMO QUEDÓ EL OBJETO
                        const paramRespuesta = {
                            tipo : 'post',
                            data : this.editedItem
                        }
                        this.$emit('save', paramRespuesta);
                    }
                    else {
                        this.$toastr(
                        "error",
                        "Ah ocurrido un error",
                        "Error!"
                        );
                    }
                }
            }
        },
        close() {
            this.$refs.form.reset();
            this.$emit('close', true);
        },
        ...mapActions("tareasStore", [
            "nuevaTarea",
            "modificarTarea",
        ]),
    },
}
</script>

<style scoped>
    
</style>