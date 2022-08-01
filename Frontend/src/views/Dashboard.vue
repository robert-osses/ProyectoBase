<template>
    <v-layout row wrap class="justify-center">
        <v-bottom-navigation>
            <v-btn>
                <span><b>987</b></span>
                <span>Total de Tareas</span>
                <v-icon>mdi-tag-multiple</v-icon>
            </v-btn>

            <v-btn>
                <span><b>987</b></span>
                <span>Total de Tareas</span>
                <v-icon>mdi-chart-pie</v-icon>
            </v-btn>

            <v-btn>
                <span><b>987</b></span>
                <span>Tareas en Gestión</span>
                <v-icon>mdi-chart-pie</v-icon>
            </v-btn>

        </v-bottom-navigation>

        <v-flex v-for="(item, i) in items" :key="i" xs12 sm4 md3 lg2 class="my-3">
            <card :item="item" />
        </v-flex>

        <v-overlay v-if="isLoading">
            <v-progress-circular
                indeterminate
                size="64"
            ></v-progress-circular>
        </v-overlay>

    </v-layout>

</template>

<script>
    import { mapGetters, mapActions } from "vuex";
    import card from '../components/card.vue'

    export default {
        components: {
            card
        },

        data(){
            return{
                items: [],
            }
        },
        async mounted() {
            const resp = await this.fetchTareasEstructuraSuperior();
            this.items = resp.data;
            //console.log(resp);
        },
        computed: {
            ...mapGetters("tareasStore", ["isLoading"])
        },
        methods: {
            ...mapActions("tareasStore", ["fetchTareasEstructuraSuperior"]),
        }
    }
</script>