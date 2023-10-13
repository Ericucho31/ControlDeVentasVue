
<template>
  <v-data-table :headers="headers" :items="usuarios" :search="search" sort-by="nombreUsuario" class="elevation-1">
    <template v-slot:top>
      <v-toolbar flat>
        <!---->
        <v-toolbar-title class="text-center ">Usuarios</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>

          <!--Búsqueda de roles-->
          <v-text-field class="text-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>

        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
              Nuevo Usuario
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{formTitle}}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                    <v-select v-model="idRol" :items="roles" label="Rol"></v-select>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="nombreUsuario" label="Nombre Usuario"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-select v-model="tipoDocumento" :items="lstDocumento" label="Tipo Documento"></v-select>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="numeroDocumento" label="Numero de Documento"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="direccion" label="Dirección"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="email" label="Email"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="telefono" label="Teléfono"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="password" type="password" label="Contraseña"></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="close">
                Cancelar
              </v-btn>
              <v-btn color="blue darken-1" text @click="Grabar">
                Grabar
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>

        <v-dialog v-model="adModal" max-width="350px">
          <v-card>
            <v-card-title v-if="adAccion==1">¿Activar Usuario?</v-card-title>
            <v-card-title v-if="adAccion==2">¿Desactivar Usuario?</v-card-title>

            <v-card-text>
              Vas a 
                <span v-if="adAccion==1"> Activar </span>
                <span v-if="adAccion==2"> Desactivar </span>
                el Usuario {{ adNombreUsuario }},
            </v-card-text>

            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="dark darken-1" @click="ActivarDesactivarCerrar"> Cerrar </v-btn>
                <v-btn v-if="adAccion==1" color="success darken-1" @click="activar"> Activar </v-btn>
                <v-btn v-if="adAccion==2" class="white--text" color="red darken-1" @click="desactivar"> Desactivar </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>

      </v-toolbar>
      
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon small class="mr-2" @click="editItem(item)">
        mdi-pencil
      </v-icon>

      <!--Íconos de ESTADO-->
      <template v-if="item.estado">
        <v-icon medium color="green darker-2" class="mr-2" @click="modalActivarDesactivar(2,item)"> check_circle</v-icon>
      </template>
      <template v-else="item.estado">
        <v-icon medium color="red darker-2" class="mr-2" @click="modalActivarDesactivar(1,item)"> cancel</v-icon>
      </template>

    </template>
  </v-data-table>
</template>


<script>
import axios from 'axios'

export default {
  data: () => ({
    search:'',
    usuarios: [], /* se creo un arreglo vacío */
    
    idUsuario: '',
    idRol: '',
    roles: [],
    nombreUsuario: '',
    lstDocumento:  ["INE", "CURP", "PASAPORTE", "CÉDULA PROFESIONAL"],
    tipoDocumento: '',
    
    numeroDocumento: '',
    direccion: '',
    telefono:'',
    email:'',
    password:'',
    estado: false,
    actualizarPassword: false,
    passwordAnterior: '',

    adModal: 0,
    adAccion: 0,
    adNombreUsuario: '',
    adIdUsuario: '',

    dialog: false,
    dialogDelete: false,
    headers: [
      { text: 'Nombre Usuario', value: 'nombreUsuario', align:'start', sortable:true },
      { text: 'Rol', value: 'rol', align:'start', sortable:true },
      { text: 'Tipo Documento', value: 'tipoDocumento', align:'start', sortable:true },
      { text: 'Numero de Documento', value: 'numeroDocumento', align:'start' },
      { text: 'Email', value: 'email' },
      { text: 'Teléfono', value: 'telefono' },
      { text: 'Acciones', value: 'actions', sortable: false },
    ],

    validar() {
      this.valida=0;
      this.ValidaMensajes=[];

      if(this.nombreUsuario.length< 3 || this.nombreUsuario.length >150) //aqui solamente agregué el .lengh en la segunda condicion
        this.ValidaMensajes.push("El nombre del artículo debe tener más de 3 caracteres y menos de 150");
      
      if(!this.idRol)
      this.ValidaMensajes.push("Seleccione un rol");

      if(!this.email)
      this.ValidaMensajes.push("Debe capturar un email");
      
      if(!this.password)
      this.ValidaMensajes.push("Debe capturar una contraseña");

      if(this.ValidaMensajes.length)
      this.valida=1;

      return this.valida;
    },

    modalActivarDesactivar(accion, item) {
      this.adModal = 1;
      this.adIdUsuario = item.idUsuario;
      this.adNombreUsuario=item.nombreUsuario;

      if(accion==1) {
        this.adAccion=1;
      } else if(accion==2) {
        this.adAccion=2;
      } else {
        this.adAccion=0;
      }
      
    },
    
    editedIndex: -1,
    editedItem: {

      idUsuario: '',
      idRol: 0,
      nombreUsuario: '',
      tipoDocumento: '',
      numeroDocumento: '',
      direccion: '',
      telefono:'',
      email:'',
      password:'',
      estado: false,
      actualizarPassword: false,
      passwordAnterior: '',

    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? 'Nuevo Artículo' : 'Editar Artículo'
    },
  },

  watch: {
    dialog(val) {
      val || this.close()
    },
    dialogDelete(val) {
      val || this.closeDelete()
    },
  },

  created() {
    this,this.ListadoUsuarios();
    this.SeleccionarCategoria();
    this.initialize()
  },

  methods: {
    SeleccionarCategoria() {
      let me = this;
      var Lstroles = [];

      axios.get('https://localhost:7180/api/roles/SeleccionarRol').then(function(response) {
        Lstroles = response.data;
        Lstroles.map(function(c) {
          me.roles.push({text: c.nombreRol, value:c.idRol});
        });

      
      }).catch( function(error) {
        console.log(error);
      })
    },

    ListadoUsuarios()
    {
        let Lista = this;
        axios.get('https://localhost:7180/api/E_Usuarios/ListarUsuarios').then(function(response)
        {
            console.log(response);
            Lista.usuarios = response.data;
        }).catch(function(error)
          {
            console.log(error);
          })
        ;
    },

    activar () {
      let me = this;
      axios.put('api/E_Usuarios/ActivarUsuarios/'+this.adIdUsuario,{}).then(function(response) {
        me.adModal=0;
        me.adAccion=0;
        me.adNombreUsuario='';
        me.adIdUsuario=0;
        me.close();
        me.ListadoUsuarios();
      }).catch(function(error) {
        console.log(error);
      });
    },

    desactivar () {
      let me = this;
      axios.put('api/E_Usuarios/DesactivarUsuarios/'+this.adIdUsuario,{}).then(function(response) {
        me.adModal=0;
        me.adAccion=0;
        me.adNombreUsuario='';
        me.adIdUsuario=0;
        me.close();
        me.ListadoUsuarios();
      }).catch(function(error) {
        console.log(error);
      });
    },

    ActivarDesactivarCerrar () {
      this.adModal=0;
    },



    initialize() {
     
    },

    editItem(item) {
      this.idUsuario = item.idUsuario;
      this.idRol = item.idRol;
      this.nombreUsuario = item.nombreUsuario;
      this.tipoDocumento = item.tipoDocumento;
      this.numeroDocumento = item.numeroDocumento;
     
      this.direccion = item.direccion;
      this.email = item.email;
      this.telefono = item.telefono;
      this.estado = item.estado;

      this.editedIndex = 1;
      this.dialog = true
    },


    close() {
      this.dialog = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    closeDelete() {
      this.dialogDelete = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    Grabar() {
      if(this.validar() )
      {
        return;
      }
      if (this.editedIndex > -1) {
        let me = this;
        axios.put('api/E_Usuarios/ModificarUsuarios', 
        {
          'idUsuario': me.idUsuario,
          'idRol': me.idRol,
          'nombreUsuario': me.nombreUsuario,
          'tipoDocumento': me.tipoDocumento,
          'numeroDocumento': me.numeroDocumento,
          'direccion': me.direccion,
          'email': me.email,
          'telefono': me.telefono,
          'password': me.password,
          'estado': true

        }).then(function(response){
          me.close();
          me.ListadoUsuarios();
          me.LimpiarModal();
        }).catch(function(error)
        {
          console.log(error);
        });

        //Sección para editar los datos 
      } else {
        //Sección para Guardar los datos de una nueva categoria
        let me = this;
        axios.post('api/E_Usuarios/InsertarUsuarios', 
        {

          'idRol': me.idRol,
          'nombreUsuario': me.nombreUsuario,
          'tipoDocumento': me.tipoDocumento,
          'numeroDocumento': me.numeroDocumento,
          'direccion': me.direccion,
          'email': me.email,
          'telefono': me.telefono,
          'password': me.password,
          'estado': true

        }).then(function(response){
          me.close();
          me.ListadoUsuarios();
          me.LimpiarModal();
        }).catch(function(error)
        {
          console.log(error);
        });
      }
      this.close()
    },

    LimpiarModal() {
      this.idUsuario= '';
      this.idRol= '';

      this.tipoDocumento='';
      this.numeroDocumento='';
      this.direccion='';
      this.email='';
      this.telefono='';
      this.password='';
    },

  },
}
</script>