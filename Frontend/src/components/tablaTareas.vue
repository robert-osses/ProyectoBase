<template>
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

                  <v-list-item @click="details(item)">
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
            <template v-slot:no-data>
              <v-btn color="primary" @click="fetchTareas"
                >Volver a buscar</v-btn
              >
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
</template>

<script>

</script>

<style>

</style>