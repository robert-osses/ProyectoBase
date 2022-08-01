<template>
    <v-row>
        <v-col col-md-2 col-sm-4 col-xs-6>
            <v-card >
                <v-card-text>
                    <div>Número</div>
                    <p class="text-subtitle-2 text--primary">
                        {{resumenTareaItems.numero}}
                    </p>
                </v-card-text>
            </v-card>
        </v-col>
        
        <v-col cols="12" sm="6" md="6">
            <v-card >
                <v-card-text>
                    <div>Descripción</div>
                    <p class="text-subtitle-2 text--primary">
                        {{resumenTareaItems.descripcion}}
                    </p>
                </v-card-text>
            </v-card>
        </v-col>

        <v-col col-md-2 col-sm-4 col-xs-6>
            <v-card >
                <v-card-text>
                    <div>Cumplimiento</div>
                    <p class="text-subtitle-2 text--primary">
                        {{resumenTareaItems.cumplimiento}}
                    </p>
                </v-card-text>
            </v-card>
        </v-col>

        <v-col col-md-2 col-sm-4 col-xs-6>
            <v-card >
                <v-card-text>
                    <div>Plazo</div>
                    <p class="text-subtitle-2 text--primary">
                        {{resumenTareaItems.plazo}}
                    </p>
                </v-card-text>
            </v-card>
        </v-col>
        <v-spacer></v-spacer>
            <v-row>
                <!-- <listado-tareas
                :estructuraID="estructuraID"
                :tareaID="tareaID"
                /> -->
            </v-row>
    </v-row>

    
    
    
</template>

<script>
//import ListadoTareas from '../components/listaTareas.vue';

export default {
    components:{
    //editTarea,
    //ListadoTareas,
  },
    props: [
                "resumenTareaItems"
            ],
    data () {
        return {}
        
    }
}
</script>

<style>

</style>