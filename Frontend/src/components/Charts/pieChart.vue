<template>
    <highcharts ref="chart" :options="chartOptions"></highcharts>
</template>

<script>
import {Chart} from 'highcharts-vue'

import Vue from "vue";
import Highcharts from "highcharts";
import Highcharts3d from "highcharts/highcharts-3d";
import HighchartsVue from "highcharts-vue";

Highcharts3d(Highcharts);
Vue.use(HighchartsVue);

export default {
    name:'pieChart',
    components: {
        highcharts: Chart 
    },
    props: [
        "data"
    ],
    data () {
        return {
            chartOptions: {
                chart: {
                    //renderTo: 'container',
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0
                    },
                    height: 80 + "%",
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: null
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 45,
                        dataLabels: {
                            enabled: false,
                            format: '{point.name}'
                        },
                        point: {
                            events: {
                                click: function (e) {
                                    console.log(e);
                                    //if (tipo == 1)
                                        //modalTareas(e.point.name, e.point.color, id);                                        
                                    //if (tipo == 2)
                                        //modalTareasPlan(e.point.name, e.point.color, id, control);
                                }
                            },
                        },
                        showInLegend: false
                    },
                },
                series: [{
                    data: this.data // sample data
                }]
            }
        }
    },
    mounted() {
        this.$refs.chart.chart.reflow();
    },
}
</script>