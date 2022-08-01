<template>
    <v-container>
        <v-row justify="space-around">
            <v-card hover>
                <v-app-bar
                    flat
                    dark
                    color="#b71c1c"
                >
                    <v-toolbar-title :title="item.nombre" class="text-h6 pl-0">
                    {{item.nombre}}
                    </v-toolbar-title>

                    <v-spacer></v-spacer>

                    <v-btn
                    icon
                    >
                    <v-icon>mdi-chevron-right</v-icon>
                    </v-btn>
                </v-app-bar>
                <v-card-actions>

                    <v-icon title="Tareas en Gestión (gráfico de torta)">mdi-chart-pie</v-icon> {{item.tareasengestion}}

                    <v-spacer></v-spacer>

                    <v-icon title="Tareas Finalizadas" color="rgba(35, 111, 192, 0.9)"> mdi-checkbox-marked-circle</v-icon> {{item.tareasterminadas}}
                </v-card-actions>

                <pieChart :data="[item.tareasdesarrollo, item.tareasatrasadas, item.tareasvencidas]" />
                <lineChart />

                <v-card-actions>

                    <v-icon title="Problemas en Gestión">mdi-alert-circle</v-icon> {{item.problemasdesarrollo}}
                    
                    <v-spacer></v-spacer>

                    <v-icon title="Problemas Solucionados">mdi-close-circle</v-icon> {{item.problemassolucionados}}

                    <v-spacer></v-spacer>

                    <v-icon title="Tareas Reprogramadas">mdi-clock</v-icon> {{item.tareasmodificadas}}
                </v-card-actions>
            </v-card>
        </v-row>
    </v-container>
</template>

<script>
import pieChart from '../components/Charts/pieChart.vue'
import lineChart from '../components/Charts/lineChart.vue'


export default {
    components: {
        pieChart,
        lineChart
    },
    props: [
        "item"
    ],

    data(){
        return{
            
        }
    },

    mounted() {
        console.log(this.item);
    }
}
</script>

<style scoped>
    .v-card{
        width: 80%;
    }
</style>