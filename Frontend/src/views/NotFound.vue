<template>
  <v-app id="inspire">
    <v-content>
      <v-container
        class="fill-height"
        fluid
      >
        <v-row
          align="center"
          justify="center">
          <v-col
            cols="12"
            sm="8"
            md="4">
            <h1>No hay nada que mostrar :(</h1>
          </v-col>
        </v-row>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
    export default {
        created () {
            this.$store.commit('SET_LAYOUT', 'login-layout')
        }
    }
</script>