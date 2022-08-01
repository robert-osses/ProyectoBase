<template>
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
</template>

<script>
export default {
    props:[
        "item"
    ]
}
</script>

<style>

</style>