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
    name:'lineChart',
    components: {
        highcharts: Chart 
    },
    data () {
        return {
            data: [],
            categorias: [],
            chartOptions: {}
        }
    },
    mounted() {
        this.chartOptions = {
            chart: {
                //renderTo: 'container',
                type: 'line',
                height: 50 + "%",
            },                
            title: {
                text: null
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.y}%</b>'
            },
            credits: {
                enabled: false
            },
            yAxis: {
                title: {
                    text: null
                },
                min: 0,
                max: 100,
                visible: false
            },
            xAxis: {
                categories: this.categorias,
                visible: false,
                labels: {
                    enable: false,
                },
                plotLines: [{
                    className: 'lineaA�o',
                    color: '#FF0000',
                    width: 2,
                    value: 5.5,
                }]
            },
            plotOptions: {
                series: {
                    label: {
                        connectorAllowed: false
                    },
                }
            },
            series: this.data,
            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 500
                    },
                    chartOptions: {
                        legend: {
                            layout: 'horizontal',
                            align: 'center',
                            verticalAlign: 'bottom'
                        }
                    }
                }]
            }
        };

        this.$refs.chart.chart.reflow();
        console.log(this.data);
    },
    beforeMount(){
        this.data = [
            {
                name: 'DESARROLLO',
                data: [25,25,25],
                color: 'rgba(14, 152, 25, 0.8)',
                showInLegend: false,
                marker: {
                    symbol: 'circle',
                    radius: 2
                }
            },
            {
                name: 'ATRASADAS',
                data: [10,10,10],
                color: 'rgba(255, 183, 29, 0.9)',
                showInLegend: false,
                marker: {
                    symbol: 'circle',
                    radius: 2
                }
            },
            {
                name: 'VENCIDAS',
                data: [5,5,5],
                color: 'rgba(219, 36, 36, 0.8)',
                showInLegend: false,
                marker: {
                    symbol: 'circle',
                    radius: 2
                }
            },
            {
                name: 'FINALIZADAS',
                data: [60,60,60],
                color: 'rgba(35, 111, 192, 0.9)',
                showInLegend: false,
                marker: {
                    symbol: 'circle',
                    radius: 2
                }
            }
        ];
        this.categorias = [
            'SEP\'2019', 'OCT\'2019', 'NOV\'2019', //'DIC\'2019', 'ENE\'2020', 'FEB\'2020', 'MAR\'2020', 'ABR\'2020', 'MAY\'2020', 'JUN\'2020', 'JUL\'2020', 'AGO\'2020'
        ];
    },
}
</script>