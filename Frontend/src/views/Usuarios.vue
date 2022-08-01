<template>
  <div>
    <v-container fluid>
      <v-card class="elevation-0">
        <v-card-title>
          Listado de Usuarios
          <v-spacer></v-spacer>
          <v-text-field
            v-model="search"
            append-icon="mdi-magnify"
            label="Buscar usuario en el listado"
            single-line
            hide-details
          ></v-text-field>         
        </v-card-title>

        <v-container fluid>
          <v-data-table
            :headers="headers"
            :loading="isLoading"
            loading-text="Cargando... por favor espere"
            :items="usuarios"
            :search="search"
          >
            <template v-slot:top>
              <v-toolbar
                flat
              >
                <v-spacer></v-spacer>
                <v-dialog
                  v-model="dialog"
                  max-width="500px"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn
                      color="primary"
                      dark
                      class="mb-2"
                      v-bind="attrs"
                      v-on="on"
                    >
                      NUEVO USUARIO
                    </v-btn>
                  </template>
                  <v-card>
                    <v-card-title>
                      <span class="text-h5">{{ formTitle }}</span>
                    </v-card-title>

                    <v-card-text>
                      <v-form v-model="validForm" ref="form">
                        <v-container>
                          <v-row>
                            <v-col
                              cols="12"
                            >
                              <v-text-field
                                v-model="editedItem.run"
                                label="RUN"
                                :hint="`RUN sin puntos, guión ni dígito verificador`"
                                :readonly="editedIndex === -1 ? false : true"
                                :append-icon="editedIndex === -1 ? 'mdi-magnify' : ''"
                                @click:append="buscarPersonal"
                                persistent-hint
                                :rules="rules.rut"
                                :counter="8"
                                maxlength="8"
                              ></v-text-field>
                            </v-col>
                            <v-col
                              cols="2"
                            >
                              <v-text-field
                                v-model="editedItem.persona.grado"
                                label="GRA"
                                readonly
                                :disabled="editItemRunValidated ? false : true"
                                :rules="rules.required"
                              ></v-text-field>
                            </v-col>
                            <v-col
                              cols="5"
                            >
                              <v-text-field
                                v-model="editedItem.persona.nombre"
                                label="NOMBRE"
                                readonly
                                :disabled="editItemRunValidated ? false : true"
                                :rules="rules.required"
                              ></v-text-field>
                            </v-col>
                            <v-col
                              cols="5"
                            >
                              <v-text-field
                                v-model="editedItem.persona.apellidO_PATERNO"
                                label="APELLIDO"
                                readonly
                                :disabled="editItemRunValidated ? false : true"
                                :rules="rules.required"
                              ></v-text-field>
                            </v-col>
                            <v-col
                              cols="12"
                            >
                              <v-select
                                v-model="editedItem.rol"
                                :hint="`${editedItem.rol.descripcion}`"
                                :items="roles"
                                item-text="nombre"
                                item-value="id"
                                label="Rol del Usuario"
                                persistent-hint
                                return-object
                                :disabled="editItemRunValidated ? false : true"
                                :rules="rules.required"
                              ></v-select>
                            </v-col>
                            
                            <v-col
                              cols="12"
                            >
                              <v-autocomplete
                                v-model="editedItem.unidad"
                                :items="unidades"
                                item-text="descripcion"
                                item-value="codigo"
                                label="Unidad"
                                :hint="`Puede digitar el nombre de la unidad para buscar...`"
                                persistent-hint
                                return-object
                                :disabled="editItemRunValidated ? false : true"
                                :rules="rules.required"
                              ></v-autocomplete>
                            </v-col>

                            <v-col
                              cols="12"
                            >
                              <v-select
                                v-model="editedItem.estado"
                                :hint="`Estado del registro`"
                                :items="estados"
                                item-text="nombre"
                                item-value="id"
                                label="Estado"
                                persistent-hint
                                :disabled="editItemRunValidated ? false : true"
                              ></v-select>
                            </v-col>

                          </v-row>
                        </v-container>
                      </v-form>
                    </v-card-text>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="blue darken-1"
                        text
                        @click="close"
                      >
                        Cancelar
                      </v-btn>
                      <v-btn
                        color="blue darken-1"
                        text
                        @click="save"
                      >
                        Guardar
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </v-toolbar>
            </template>

            <template v-slot:item.persona.nombre="{ item }">
              {{ item.persona.nombre}} {{ item.persona.apellidO_PATERNO }} {{ item.persona.apellidO_MATERNO}}
            </template>

            <template v-slot:item.creacion="{ item }">
              {{ getFormatFecha(item.creacion) }}
            </template>

            <template v-slot:item.estado="{ item }">
              {{ item.estado === 1 ? 'ACTIVO' : '' }}
              {{ item.estado === 0 ? 'BORRADO' : '' }}
            </template>

            <template v-slot:item.acciones="{ item }">
              
              <v-tooltip top>
                <template v-slot:activator="{ on }">
                  <v-icon small class="mr-2" v-on="on" @click="editItem(item)"
                    >mdi-pencil</v-icon
                  >
                </template>
                <span>Editar</span>
              </v-tooltip>
              
            </template>
            <template v-slot:no-data>
              <v-btn color="primary" @click="fetchUsuarios"
                >Volver a buscar</v-btn
              >
            </template>
          </v-data-table>
        </v-container>

      </v-card>
    </v-container>

  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "usuarios",
  data: () => ({
    search: "",
    dialog: false,
    editedItem:{
      run: null,
      persona: {},
      rol: {},
      unidad: {},
      creacion: Date.now(),
      estado: 1,
    },
    editItemRunValidated: false,
    defaultItem:{
      run: null,
      persona: {},
      rol: {},
      unidad: {},      
      creacion: Date.now(),
      estado: 1,
    },
    editedIndex: -1,
    validForm: false,
    rules: {
      rut: [
        (v) => !!v || "El RUN del usuario es requerido",
        (v) => (v && isNaN(v) == false) || "Debe ingresar solo números",
      ],
      required:[
        (v) => !!v || "Este campo es requerido",
      ],
    },
    headers: [
      { text: "Grado", value: "persona.grado", groupable: false, sortable: false},
      { text: "Nombre Completo", value: "persona.nombre", groupable: false, sortable: false},      
      { text: "paterno", value: "persona.apellidO_PATERNO", groupable: false, sortable: false, align: ' d-none'},//d-none hide the column but keeps the search ability
      { text: "materno", value: "persona.apellidO_MATERNO", groupable: false, sortable: false, align: ' d-none'},//d-none hide the column but keeps the search ability
      { text: "Unidad", value: "unidad.sigla" },
      { text: "Rol", value: "rol.nombre" },
      { text: "Creación", value: "creacion" },
      { text: "Estado", value: "estado" },
      { text: "Acciones", value: "acciones", sortable: false, align: 'right'},
    ],
  }),
  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'Nuevo Usuario' : 'Editar Usuario'
    },
    
    ...mapGetters(["currentUser"]),
    ...mapGetters("rolesStore", ["roles"]),
    ...mapGetters("unidadesStore", ["unidades"]),
    ...mapGetters("personasStore", ["persona"]),
    ...mapGetters("usuariosStore", ["usuarios", "isLoading"]),
    ...mapGetters("estadosStore", ["estados"]),
  },
  watch: {
    dialog (val) {
      val || this.close()
    },
  },
  mounted() {
    this.fetchUsuarios();
    this.fetchUnidades();
    this.fetchRoles();
  },
  methods: {
    getFormatFecha(f) {
      let fechaToISO = new Date(f).toISOString().substring(0,10);
      let fecha = `${fechaToISO.substring(8,10)}/${fechaToISO.substring(5,7)}/${fechaToISO.substring(0,4)}`;
      return fecha;
    },
    buscarPersonal() {
      if(this.editedItem.run) {
        this.fetchPersonaRun(this.editedItem.run).then(() => {
          if(this.persona) {
            this.editedItem.persona = {
              grado: this.persona.grado,
              nombre: this.persona.nombre,
              apellidO_PATERNO: this.persona.apellidO_PATERNO
            }
            this.editItemRunValidated = true;
          }
          else {
            this.$toastr("warning", "No se ha encontrado una persona con ese RUN", "Éxito!");
          }
        });
      }
    },
    editItem (item) {
      //console.log(this.editedIndex);
      //console.log(item);
      if(this.$refs.form) 
        this.$refs.form.resetValidation();//limpia los mensajes de validación de formulario abiertos previamente

      //deshabilita el formulario cuando no se ha ingresado un run en la creación de usuario
      this.editItemRunValidated = this.editedItem === -1 ?  false : true;
      
      this.editedIndex = this.usuarios.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    close () {
      this.editItemRunValidated = false;

      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      })
    },

    async save () {
      this.validate();
      if (this.validForm) {
        if (this.editedIndex > -1) {
          //debe ser un llamado asincrono para poder ver el resultado en vue sin utilizar promesas.
          const res = await this.putUsuario(this.editedItem);

          if (res.statusCode == 200) {
            this.$toastr("success", res.message, "Éxito!");
            Object.assign(this.usuarios[this.editedIndex], this.editedItem)
          }
          else {
            this.$toastr("error", res.message, "Error!");
            return;
          }          
        } else {
          const res = await this.postUsuario(this.editedItem);
          
          if (res.statusCode == 200) {
            this.$toastr("success", res.message, "Éxito!");
            this.usuarios.push(this.editedItem)
          }
          else {
            console.log("error");
            this.$toastr("error", res.message, "Error!");
            return;
          }
        }
        this.close()
      }
    },
    
    validate() {
      this.$refs.form.validate();
    },
    ...mapActions("rolesStore", ["fetchRoles"]),
    ...mapActions("unidadesStore", ["fetchUnidades"]),
    ...mapActions("usuariosStore", ["fetchUsuarios","putUsuario","postUsuario",]),
    ...mapActions("personasStore", ["fetchPersonaRun"]),
  },
};
</script>

<style></style>
