<template>
    <div>
    <v-container fluid>
      <v-card class="elevation-0">
          <v-card-title>
            Listado de Tareas..
            <v-spacer></v-spacer>
            <v-text-field
              v-model="search"
              append-icon="mdi-magnify"
              label="Buscar tarea"
              @click:append="buscar"
              @change="buscar"
              single-line
              hint="Digíte su criterio de búsqueda y presione la tecla ENTER"
              persistent-hint
            ></v-text-field>
          </v-card-title>
              <!-- MODAL TAREA -->
        <v-dialog scrollable v-model="ItemEditModal" max-width="900">
          <template v-slot:activator="{ on }">
            <v-btn color="primary" dark class="mb-2" v-on="on"
              >Nueva Tarea</v-btn
            >
          </template>
          <editTarea 
            v-if="ItemEditModal" 
            :editedItem="editedItem" 
            :editedIndex="editedIndex"
            :tipo_tareas="tipo_tareas"
            :unidades="unidades"
            :listadoTareas="listadoTareas"
            @close="close"
            @save="save"
          />
        </v-dialog>

                  <!-- MODAL TAREA Detalle-->
        <v-dialog scrollable v-model="ItemDetailModal" max-width="900">
          <v-card>
            <v-card-title class="headline">Detalle Tarea</v-card-title>
            <v-card-text>
              <v-container>

                <v-form v-model="validForm" ref="form">
                  <v-row>
                    <v-col cols="12" sm="4" md="4">
                      <v-text-field
                        v-model="detailsItem.id"
                        label="Id"
                        readonly
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="4" md="4">
                      <v-text-field
                        v-model="detailsItem.numero"
                        label="Numero"
                        :readonly="ItemEdit ? false : true"
                      >
                      </v-text-field>
                    </v-col>
                    <v-col cols="12" sm="4" md="4">
                      <v-autocomplete
                          :items="tipo_tareas"
                          item-text="descripcion"
                          item-value="id"
                          readonly
                          label="Tipo Tarea"
                          v-model="detailsItem.tipO_TAREAID"
                          persistent-hint
                          return-object
                      ></v-autocomplete>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" sm="12" md="12">
                      <v-text-field
                        v-model="detailsItem.titulo"
                        label="Titulo"
                        readonly
                      ></v-text-field>
                    </v-col>
                  </v-row> 
                  <v-row>
                    <v-col cols="12" sm="12" md="12">
                      <v-textarea
                        v-model="detailsItem.descripcion"
                        label="Descripcion"
                        auto-grow
                        :readonly="ItemEdit ? false : true"
                      ></v-textarea>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" sm="6" md="4">
                        <v-autocomplete
                          v-model="detailsItem.unidadcodigo"
                          :items="unidades"
                          item-text="descripcion"
                          item-value="codigo"
                          label="Unidad Responsable"
                          readonly
                          persistent-hint
                          return-object>
                        </v-autocomplete>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="detailsItem.inicio"
                        label="Fecha Inicio"
                        prepend-icon="mdi-calendar"
                        readonly
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="detailsItem.plazo"
                        label="Fecha Plazo"
                        prepend-icon="mdi-calendar"
                        readonly
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="detailsItem.cumplimiento"
                        label="Cumplimiento"
                        :readonly="ItemEdit ? false : true"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="detailsItem.planificado"
                        label="Planificado"
                        :readonly="ItemEdit ? false : true"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="detailsItem.ponderacion"
                        label="Ponderacion"
                        :readonly="ItemEdit ? false : true"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-form>

              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary darken-1" text @click="ItemDetailModal = false"
                >Salir</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>

        <v-dialog scrollable v-model="PlanificarEstadoAvanceModal" max-width="900">
          <v-card>
            <v-card-title class="headline">Planificar</v-card-title>
            <v-card-text>
              <v-container>

                <v-form v-model="validForm" ref="form">

                    <resumen-tarea
                        :resumenTareaItems="resumenTareaItems"
                    />
                    <v-spacer></v-spacer>

                    <ver-estados-avance
                    :verListaEstadosAvance="verListaEstadosAvance"
                    />

                </v-form>

              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary darken-1" text @click="PlanificarEstadoAvanceModal = false"
                >Salir</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>

        <v-dialog scrollable v-model="ItemEstadoAvancelModal" max-width="900">
          <estado-avance
            v-if="ItemEstadoAvancelModal"
            :estadoAvanceItem="estadoAvanceItem"
            :paginaActual="currentPage"
            :editedIndex="editedIndex"
            :editedItem="editedItem" 
            :listadoTareas="listadoTareas"
            @close="close"
            @save="save"
            @guardarEstadoAvance="actualizarLista"
          />
        </v-dialog>

        <v-container fluid>
          <v-data-table
            :headers="headers"
            :items="listadoTareas"
            :loading="isLoading"
            loading-text="Cargando... favor espere"
            item-key="id"
            sort-by="inicio"
            hide-default-footer
            disable-pagination
            dense
          >

            <template v-slot:item.inicio="{ item }">
              {{ item.inicio }}
            </template>

            <template v-slot:item.plazo="{ item }">
              {{ item.plazo }}
            </template>

            <template v-slot:item.descripcion="{ item }">
              <v-icon :color="getColorTarea(item)" small>mdi-circle</v-icon>
              {{ item.descripcion }}
            </template>

            <template v-slot:item.cumplimiento="{ item }">
              {{ item.cumplimiento }} %
            </template>

            <template v-slot:item.actions="{ item }">
              
              <v-menu
                bottom
                left
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    icon
                    v-bind="attrs"
                    v-on="on"
                  >
                    <v-icon>mdi-dots-vertical</v-icon>
                  </v-btn>
                </template>

                <v-list>

                  <v-list-item>
                    <v-list-item-content>
                      <v-list-item-title><b>TAREA</b></v-list-item-title>
                    </v-list-item-content>
                  </v-list-item>

                  <v-list-item @click="details(item)">
                    <v-icon small class="mr-2">
                      mdi-magnify
                    </v-icon> Ver Detalle de la Tarea
                  </v-list-item>

                  <v-list-item @click="editItem(item)">
                    <v-icon small class="mr-2">
                      mdi-pencil
                    </v-icon> Modificar Tarea
                  </v-list-item>

                  <v-list-item @click="planificar(item)">
                    <v-icon small class="mr-2">
                      mdi-format-list-numbered
                    </v-icon> Planificar Tarea
                  </v-list-item>

                  <v-list-item @click="deleteItem(item)">
                    <v-icon small class="mr-2">
                      mdi-delete
                    </v-icon> Eliminar Tarea
                  </v-list-item>

                  <v-divider></v-divider>
                  <v-list-item>
                    <v-list-item-content>
                      <v-list-item-title><b>ESTADOS DE AVANCE</b></v-list-item-title>
                    </v-list-item-content>
                  </v-list-item>

                  <v-list-item @click="verEstadosAvance(item)">
                    <v-icon small class="mr-2">
                      mdi-clipboard-text
                    </v-icon> Ver Estado de Avance
                  </v-list-item>

                  <v-list-item @click="crearEstadoAvance(item)">
                    <v-icon small class="mr-2">
                      mdi-clipboard-plus
                    </v-icon> Ingresar Estado de Avance
                  </v-list-item>

                  <v-list-item @click="details(item)">
                    <v-icon small class="mr-2">
                      mdi-clipboard-check
                    </v-icon> Validar Estado de Avance
                  </v-list-item>

                  <v-divider></v-divider>
                  <v-list-item>
                    <v-list-item-content>
                      <v-list-item-title><b>PROBLEMAS</b></v-list-item-title>
                    </v-list-item-content>
                  </v-list-item>

                  <v-list-item @click="details(item)">
                    <v-icon small class="mr-2">
                      mdi-clipboard-alert
                    </v-icon> Ver Problemas
                  </v-list-item>

                  <v-list-item @click="details(item)">
                    <v-icon small class="mr-2">
                      mdi-alert-octagram
                    </v-icon> Ingresar un Problema
                  </v-list-item>

                </v-list>
              </v-menu>
              
            </template>
            <template v-slot:item.siguiente="{ item }">
              <v-btn
                icon
                @click="recargarComponente(item)"
              >
                <v-icon>mdi-chevron-right</v-icon>
              </v-btn>
            </template>
          </v-data-table>
            <template>
                <div class="text-center">
                    <v-pagination
                        v-model="currentPage"
                        :length="pages"
                        :total-visible="10"
                        :disabled = "isLoading"
                    ></v-pagination>
                </div>
            </template>
        </v-container>


<!-- COMPONENTE QUE CONTIENE SOLO LA LISTA DE LAS TAREAS, PERO NO LOGRO HACER FUNCIONAR LA PAGINACION -->
        <!-- <tabla-tareas
        :headers="headers"
        :listadoTareas="listadoTareas"
        :isLoading="isLoading"
        :pages="pages"
        :getColorTarea="getColorTarea"
        :currentPage="currentPage"
        /> -->

      </v-card>
    </v-container>

    <!-- MODAL TAREA FIN -->
    <v-dialog v-model="dialogDelete" max-width="290">
      <v-card>
        <v-card-title class="headline">Eliminar</v-card-title>
        <v-card-text
          >Desea eliminar la tarea {{ detailsItem.id }} ?</v-card-text
        >
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="red darken-1" text @click="eliminar">Eliminar</v-btn>
          <v-btn color="primary darken-1" text @click="dialogDelete = false"
            >Salir</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- MODAL VER ESTADOS DE AVANCE -->

    <v-dialog scrollable v-model="VerEstadoAvanceModal" max-width="900">
      <v-card>
        <v-card-title class="headline">Estados de Avance</v-card-title>
        <v-card-text>
          <v-container>

            <v-form v-model="validForm" ref="form">

                <ver-estados-avance
                :verListaEstadosAvance="verListaEstadosAvance"
                />
                <v-spacer></v-spacer>

            </v-form>

          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary darken-1" text @click="VerEstadoAvanceModal = false"
            >Salir</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>

  </div>
</template>

<script>

import { mapGetters, mapActions } from "vuex";
import editTarea from '@/components/editTarea.vue'
import estadoAvance from './estadoAvance.vue';

import resumenTarea from './resumenTarea.vue';
import verEstadosAvance from './verEstadosAvance.vue';
//import TablaTareas from './tablaTareas.vue';

export default {
  name: "Tareas",
  components:{
    editTarea,
    estadoAvance,
    resumenTarea,
    verEstadosAvance,
    //TablaTareas,
  },

  props:[
      "planId",
      "tipoTareaId",
      "orden"
  ],
  data: () => ({
    currentPage: 1,
    listadoTareas: [],
    verListaEstadosAvance: [],
    item: [
      {acciones: ""}
    ],
      tipo_tareas: [
      {descripcion: 'Tarea', id: 1},
      {descripcion: 'Estudio', id: 2},
      {descripcion: 'Auditoria', id: 4},
      {descripcion: 'Atencion Referencia', id: 53},
      {descripcion: 'Investigacion', id: 54},
      {descripcion: 'Pronunciamiento Juridico', id: 55},
      ],
    date: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    menu: false,
    menu2: false,
    ItemDetailModal: false,
    ItemEditModal: false,
    ItemEstadoAvancelModal: false,
    PlanificarEstadoAvanceModal: false,
    VerEstadoAvanceModal: false,
    ItemEdit: false,
    search: "",
    dialog: false,
    dialogDelete: false,
    search_rut: false,
    editedIndex: -1,
    detailsItem: {
      id: 0,
      numero: 0,
      titulo: "",
      descripcion: "details",
      ponderacion: 0,
      inicio: "",
      plazo: "",
      cumplimiento: 0,
      planificado: 0,
      tipO_TAREAID: null,
      unidadcodigo: 0
    },
    editedItem: {
      id: 0,
      numero: 0,
      titulo: "",
      descripcion: "",
      ponderacion: 0,
      inicio: "",
      plazo: "",
      cumplimiento: 0,
      planificado: 0,
      tipO_TAREAID: null,
      unidad: {},
      unidadcodigo: 0
    },
    estadoAvanceItem: {
                tareaID: 0,
                descripcion: "",
                cumplimiento: 0,
            },
    resumenTareaItems: {
        numero: 0,
        descripcion: "",
        cumplimiento: 0,
        plazo: ""
    },
    deleteUsuario: {
      nombre: "",
    },
    deleteTarea: {
      id: "",
    },
    validForm: false,
    rules: {
    },
    headers: [
      { text: "Id", value: "id", width: "1%", sortable: false, groupable: false},
      { text: "Titulo", value: "titulo", align: 'justify', width: "10%", sortable: false, groupable: false},
      { text: "Descripcion", value: "descripcion", align: 'justify', width: "50%", sortable: false, groupable: false},
      { text: "Unidad", value: "unidad.sigla", align: 'right', width: "6%", sortable: false, groupable: false},
      { text: "Inicio", value: "inicio", align: 'right', width: "6%", sortable: false, groupable: false},
      { text: "Plazo", value: "plazo", align: 'right', width: "6%", sortable: false, groupable: false},
      { text: "Cumplimiento", value: "cumplimiento", align: 'right', width: "6%", sortable: false, groupable: false},
      { text: "Planificado", value: "planificado", align: 'right', width: "6%", sortable: false, groupable: false},
      { text: 'Acciones', value: 'actions', align: 'right', width: "5%", sortable: false, groupable: false},
      { text: 'Siguiente', value: 'siguiente', align: 'right', width: "1%", sortable: false, groupable: false},
    ],
  }),

  computed: {
    ...mapGetters("usuariosStore", ["currentUser"]),
    ...mapGetters("tareasStore", ["tareas", "page","pages","isLoading"]),
    ...mapGetters("unidadesStore", ["unidades"]),
    ...mapGetters("estadoAvancesStore", ["listaEstadosAvance"]),
  },
  mounted() {
    console.log(this.tipoTareaId);
    this.fetchTareas({page : this.currentPage, planId: this.planId, tipoTareaId: this.tipoTareaId, busqueda : ""});
  },
  watch: {
    async tareas(res) {
      await this.$nextTick();
      this.formatTareas(res);
      this.fetchUnidades();
    },
    page(res) {
                this.currentPage = res;
            },
    currentPage(res) {
                this.fetchTareas({ page : res, planId: this.planId, tipoTareaId: this.tipoTareaId, busqueda: this.search});
            },
    
  },
  methods: {
    recargarComponente(item){
      console.log(item);
      this.$emit('valorOrden', true);
    },
    buscar(){
            if(this.search != "")
              this.currentPage = 1;

              this.fetchTareas({ page : this.currentPage, planId: this.planId, tipoTareaId: this.tipoTareaId, busqueda: this.search});
            },

    formatTareas(resp) {
      this.respuesta = [];
      resp.forEach(val => {
          let tareaTemp = {};
          let tareaTemp2 = Object.assign({}, val);
        tareaTemp = {
          inicio : this.getFormatFecha(val.inicio),
          plazo : this.getFormatFecha(val.plazo),
        }
        tareaTemp2 = Object.assign(tareaTemp2, tareaTemp);
        this.respuesta.push(tareaTemp2);
      })
      
      this.listadoTareas = this.respuesta
    },
    getFormatFecha(f) {
      var fecha = new Date(f).toISOString().substring(0,10);
      return fecha;
    },
    getColorTarea(tarea) {
      var color = "green";
      if ((!tarea.cumplimiento || !tarea.planificado) && tarea.cumplimiento != 0 && tarea.planificado != 0) {
        color = "black"
      }
      else {
        if (tarea.cumplimiento === 100)
          color = "blue";
        if (tarea.planificado === 100 && tarea.cumplimiento < 100)
          color = "red";
        if ((tarea.planificado > tarea.cumplimiento) && tarea.planificado < 100)
          color = "yellow";
      }

      return color;
    },
    validate() {
      this.$refs.form.validate();
    },
    editItem(item) {
      this.ItemEdit = true;
      this.editedIndex = this.listadoTareas.indexOf(item)
      this.editedItem = Object.assign({}, item);
      this.ItemEditModal = !this.ItemEditModal;
    },
    details(item) {
      this.ItemEdit = false;
      this.detailsItem = Object.assign({}, item);
      this.ItemDetailModal = !this.ItemDetailModal;
    },
    async planificar(item) {
      this.resumenTareaItems.numero = item.numero
      this.resumenTareaItems.descripcion = item.descripcion
      this.resumenTareaItems.cumplimiento = item.cumplimiento
      this.resumenTareaItems.plazo = item.plazo
      this.PlanificarEstadoAvanceModal = !this.PlanificarEstadoAvanceModal;
      const res = await this.getEstadoAvanceTarea(item.id);
      this.verListaEstadosAvance = res.data
    },
    async verEstadosAvance(item) {
        this.VerEstadoAvanceModal = !this.VerEstadoAvanceModal;
        const res = await this.getEstadoAvanceTarea(item.id);
        this.verListaEstadosAvance = res.data
        //this.getEstadoAvanceTarea(item.id)
    },
    crearEstadoAvance(item) {
      this.editedIndex = this.listadoTareas.indexOf(item)
      this.estadoAvanceItem.tareaID = item.id
      this.estadoAvanceItem.cumplimiento = item.cumplimiento
      this.editedItem = Object.assign({}, item);
      this.ItemEstadoAvancelModal = !this.ItemEstadoAvancelModal;
    },
    close() {
      this.ItemEditModal = false
      this.ItemEstadoAvancelModal = false
      this.dialog = false;
      this.editedIndex = -1;
      
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
      }, 300);
    },
    async save() {
        this.close();
    },

    async actualizarLista(message){
        //hacer fetch
        //this.fetchTareas({ page : message.pagina, busqueda: ""});
        console.log("valor desde el emmit: "+message.valorCumplimiento + " y valor de pagina: "+ message.pagina);

    },

    deleteItem(item) {
      this.detailsItem = Object.assign({}, item);
      this.dialogDelete = true;
    },
    eliminar() {
        this.eliminarTarea(this.detailsItem).then((res) => {
          if (res.status == 200) {
            this.fetchTareas({page : this.currentPage, planId: this.planId, tipoTareaId: this.tipoTareaId, busqueda : ""});
            this.$toastr(
              "success",
              "Tarea eliminada correctamente",
              "Éxito!"
            );
            this.dialogDelete = false;
          }
          else {
            this.$toastr(
              "error",
              "Tarea no pudo ser eliminada", "Ups!",
              "Error!"
            );
            this.dialogDelete = false;
          }
        });

    },
    limpiarForm() {
      this.editedItem = Object.assign({}, this.defaultItem);
    },
     
    ...mapActions("tareasStore", [
      "fetchTareas",
      //"fetchTareasSinStore",
      "buscarTarea",
      "eliminarTarea",
    ]),
        ...mapActions("unidadesStore", ["fetchUnidades"]),
        ...mapActions("estadoAvancesStore", ["nuevoEstadoAvance","getEstadoAvanceTarea"]),
  },

};

</script>