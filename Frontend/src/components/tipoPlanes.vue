<template>
  <div>
      <v-container fluid>
          <div>Tipo Planes</div>
          <v-card class="elevation-0">
              <v-card-title>
                  Listado tipo Planes...
                  <v-spacer></v-spacer>
              </v-card-title>
          </v-card>

          <v-container fluid>
              <v-col cols="6">
                <v-data-table
                :headers="headers"
                :items="listaTipoPlanes"
                :loading="isLoading"
                item-key="id"
              >
                <template v-slot:item.acciones="{ item }">

                <v-tooltip top>
                  <template v-slot:activator="{ on }">
                    <v-btn icon @click="goRoute(item)">
                      <v-icon v-on="on">mdi-chevron-right</v-icon>
                    </v-btn>
                  </template>
                  <span>Ver Planes</span>
                </v-tooltip>
                
              </template>
              </v-data-table>
              </v-col>

          </v-container>
      </v-container>
  </div>
</template>

<script>
export default {
      props:[
      "headers",
      "listaTipoPlanes",
      "isLoading",
  ],

  methods: {
    goRoute(item) {
        //console.log(item);
        this.$router.replace({
          
        path: `/planes/listaPlanes/${item.id}`,
      });
    },
  },

}
</script>

<style>

</style>