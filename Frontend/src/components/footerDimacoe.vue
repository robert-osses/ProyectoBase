<template>
    <v-footer
    padless
    color="black"
    >
    <v-row
        justify="center"
    >
    <v-col cols="2">
        <v-img 
            src="/cot.png"
            height="90"
            contain
        ></v-img>
    </v-col>
    <v-col 
        cols="4"
        class="black py-4 text-justify white--text"
    >
        <!-- OPERACIÓN DEL SISTEMA:<br>
        Comando de Operaciones Terrestres<br>
        Depto VII "Doctrina y Entrenamiento" - <strong><v-icon dark small>mdi-phone</v-icon> 37118</strong><br>
        Av O'Higgins 875 - Concepción -->
    </v-col>
    <v-col 
        cols="2"
    >
        <v-img 
            src="/dimacoe.png"
            height="90"
            contain
        ></v-img>
    </v-col>
    <v-col 
        cols="4"
        class="black py-4 text-justify white--text"
    >
        ADMINISTRACIÓN TÉCNICA:<br>
        Dirección de Mando y Control del Ejército<br>
        Depto V "Telecomunicaciones y TICs" - <strong><v-icon dark small>mdi-phone</v-icon> 32151</strong><br>
        Av Tupper 1725 - Santiago
    </v-col>
    </v-row>
    <v-row justify="center">
        <v-col
        cols="12"
        class="black py-4 text-center white--text"
        >
            <strong>&reg; DIRECCIÓN DE MANDO Y CONTROL DEL EJÉRCITO</strong> - {{new Date().getFullYear()}}
        </v-col>
    </v-row>
    </v-footer>
</template>

<script>

export default {
    name: "footerDimacoe",    
}
</script>