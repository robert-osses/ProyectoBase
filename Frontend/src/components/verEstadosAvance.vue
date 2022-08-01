<template>
  <v-list subheader two-line>
      <v-subheader inset>Estados Avance</v-subheader>

      <v-list-item v-for="estados in verListaEstadosAvance" :key="estados.id">
          <v-list-item-avatar>
              <v-icon class="grey lighten-1" dark>
                  mdi-clipboard-text
              </v-icon>
          </v-list-item-avatar>

          <v-list-item-content>
              <v-list-item-title v-text="estados.descripcion"></v-list-item-title>

              <v-list-item-subtitle v-text="estados.cumplimiento"></v-list-item-subtitle>

              <v-progress-linear v-model="estados.cumplimiento" color="green" height="25">
                  <strong> {{ estados.cumplimiento}}% </strong>
              </v-progress-linear>

          </v-list-item-content>

          <v-list-item-action>
              <v-btn icon>
                  <v-icon color="grey lighten-1">mdi-information</v-icon>
              </v-btn>
          </v-list-item-action>
          
      </v-list-item>
      <v-divider inset ></v-divider>
  </v-list>
</template>

<script>
export default {
    props: [
                "verListaEstadosAvance"
            ],
    data: () => ({
        folders: [
            {
                subtitle: 'Jan 9, 2014',
                title: 'Photos',
            },
            {
                subtitle: 'Jan 17, 2014',
                title: 'Recipes',
            },
            {
                subtitle: 'Jan 28, 2014',
                title: 'Work',
            }
        ]
    }),
    mounted(){
        console.log("mounted");
        console.log(this.listaEstadosAvance);
    }
}
</script>

<style>

</style>