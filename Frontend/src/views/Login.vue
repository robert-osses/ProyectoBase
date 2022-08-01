<template>
    <v-app id="inspire">
        <v-main color="primary">
            <v-container class="contentHeight" fluid>
                <v-row>
                    <v-col cols="4">
                        <!-- Contenedor en blanco -->
                    </v-col>
                    <v-col cols="4">

                        <v-col cols="12" class="centrarCont">
                            <v-img
                                src="/sicadeLogo.svg"
                                :class="existLastUser ? 'logoSmall' : 'logoBig'"
                            ></v-img>
                        </v-col>

                        <v-col cols="12" class="centrarCont">
                            <v-avatar
                                v-if="existLastUser"
                                size="230"
                                class="Avatar"                                          
                            >
                                <v-img contain :src="avatar"></v-img>
                            </v-avatar>
                        </v-col>

                        <v-col cols="12">
                            <p 
                                v-if="existLastUser"
                                class="text-center subtitle"
                            >
                                {{nombreUsuario}}
                            </p>
                        </v-col>

                        <v-col cols="12">
                            <v-form @submit.prevent="authenticate" ref="loginform" v-model="formValid">
                            
                                <v-text-field
                                    v-show="!existLastUser"
                                    label="Rut sin puntos ni dígito verificador"
                                    v-model="credentials.usuario"
                                    :rules="userRule"
                                    type="text"
                                />

                                <v-text-field
                                    id="password"
                                    label="Contraseña SIAP"
                                    v-model="credentials.password"
                                    :append-icon="mostrar ? 'mdi-eye' : 'mdi-eye-off'"
                                    :type="mostrar ? 'text' : 'password'"
                                    :rules="passRule"
                                    @click:append="mostrar = !mostrar"
                                />
                                <p v-if="existLastUser" @click="changeUserLogin" class="text-right" style="cursor:pointer">Cambiar de usuario</p>
                                <p class="text-right red--text text--lighten-1">{{authError}}</p>
                                
                                <v-btn
                                    type="submit"
                                    class="ma-2"
                                    style="float: right;"
                                    title="INGRESAR A SICADE"
                                    :loading="isLoading"
                                    :disabled="isLoading"
                                    color="primary"
                                    @click="authenticate"
                                >
                                    INGRESAR
                                    <v-icon right x-large>mdi-chevron-right</v-icon>
                                </v-btn>

                            </v-form>
                        </v-col>

                    </v-col>
                    <v-col cols="4">
                        <!-- Contenedor en blanco -->
                    </v-col>
                </v-row>
            </v-container>
            <footerDimacoe />
        </v-main>
    </v-app>
</template>

<script>
import { mapGetters, mapActions, mapState } from "vuex";
import footerDimacoe from '@/components/footerDimacoe.vue';

export default {
    components:{
        footerDimacoe,
    },
    data() {
        return {
            existLastUser: false,
            avatar: null,
            hayCredencialesAnteriores: false,
            mostrar: false,
            formValid: false,
            nombreUsuario: '',
            userRule:[
                v => !!v || "El rut es requerido",
                v => v? v.length <= 8 || "El rut debe ser menor a 8 digitos": "ingrese un valor",
            ],
            passRule:[
                v => !!v || "Contraseña requerida"
            ],
            credentials: {
                usuario: "",
                password: ""
            }
        };
    },
    methods: {
        validate(){
            this.$refs.loginform.validate();
        },
        authenticate() {
            this.validate();
            if(this.formValid){
                //console.log("logeando");
                console.log(this.credentials.usuario);
                this.login(this.credentials);
            }
        },
        changeUserLogin(){
            this.deleteLocalUser();
            this.credentials.usuario = '';
        },
        deleteLocalUser(){
            localStorage.removeItem("sicadeAvatar");
            localStorage.removeItem("sicadeRun");
            localStorage.removeItem("sicadeNombre");
            localStorage.removeItem("sicadeExpira");

            this.credentials.usuario = null;
            this.nombreUsuario = null;
            this.avatar = null;
            this.existLastUser = false;
        },
        ...mapActions(["login"]),
    },
    computed: {
        ...mapState(['currentUser']),
        ...mapGetters(["isLoading", "authError"])
    },
    created() {
        this.$store.commit("SET_LAYOUT", "login-layout");
        this.credentials.usuario = localStorage.getItem("sicadeRun");
        this.nombreUsuario = localStorage.getItem("sicadeNombre");
        this.avatar = localStorage.getItem("sicadeAvatar");
        if(this.nombreUsuario){
            this.existLastUser = true;
        }
    },
};
</script>

<style scoped>
    .logoBig{
        height: 35vh;
        width: 35vh;
    }

    .logoSmall{
        height: 15vh;
        width: 15vh;
    }

    .Avatar {
        border: 3px solid rgb(230, 230, 230);
    }

    .centrarCont {
        text-align: -webkit-center;
    }

    .contentHeight {
        height: 95vh;    
        display: table;
    }
</style>