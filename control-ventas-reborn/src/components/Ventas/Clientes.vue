<template>
  <v-data-table :headers="headers" :items="Personas" :search="search" sort-by="nombrePersona" class="elevation-1">
    <template v-slot:top>
      <v-toolbar flat>
        <!---->
        <v-toolbar-title class="text-center ">Clientes</v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>

         
          <v-text-field class="text-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>

        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
              Nuevo Cliente
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{formTitle}}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row >
                  <v-col cols="6" xs="12" sm="12" md="12">
                      <v-text-field v-model="nombrePersona" label="Nombre del Cliente"></v-text-field>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="6" xs="12" sm="12" md="6">
                      <v-select v-model="tipodocumento" :items="lstDocumentos" label="Tipo de documento"></v-select>
                  </v-col>
                  <v-col cols="6" xs="12" sm="12" md="6">
                      <v-text-field v-model="numeroDocumento" label="Numero de documento"></v-text-field>
                  </v-col>
              </v-row>
              <v-row>
                  <v-col cols="6" xs="12" sm="12" md="6">
                      <v-text-field v-model="emailPersona" label="Email"></v-text-field>
                  </v-col>
                  <v-col cols="6" xs="12" sm="12" md="6">
                      <v-text-field v-model="direccionPersona" label="Direccion"></v-text-field>
                  </v-col>
              </v-row>
              <v-row>
                  <v-col cols="12" xs="12" sm="12" md="12">
                      <v-text-field v-model="telefonoPersona" label="Telefono"></v-text-field>
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

      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
     <v-icon medium color="primary darken-1" class="mr-2" size="x-large" @click="editItem(item)">mdi-pencil</v-icon>


    </template>


    
    <template v-slot:no-data>
      <v-btn color="primary" @click="initialize">
        Reset
      </v-btn>
    </template>
  </v-data-table>
</template>


<script>
import axios from 'axios'

export default {
  data: () => ({
    search:'',

    idPersona:'',
    nombrePersona:'',
    tipoPersona:'',
    tipodocumento:'',
    lstDocumentos:["INE","CURP","PASAPORTE","CEDULA PRODESIONAL"],
    numeroDocumento:'',
    direccionPersona:'',
    telefonoPersona:'',
    emailPersona:'',
    Personas:[],

    
    adModal: 0,
    adAccion: 0,
    adNombre: '',
   

    dialog: false,
    dialogDelete: false,
    headers: [
      { text: 'Nombre', value: 'nombrePersona', align:'start', sortable:true },
      { text: 'tipo de persona', value: 'tipoPersona', align:'start', sortable:true },
      { text: 'Email', value: 'emailPersona', align:'start', sortable:true },
      { text: 'Tipo Documento', value: 'tipodocumento',align:'start', sortable:true  },
      { text: 'No. Documento', value: 'numeroDocumento',align:'start', sortable:true  },
      { text: 'Telefono', value: 'telefonoPersona', align:'start', sortable:true  },
      { text: 'Acciones', value: 'actions', sortable: false },
    ],

    validar() {
      this.valida=0;
      this.ValidaMensajes=[];

      if(this.nombrePersona.length< 3 || this.nombrePersona.length >150) //aqui solamente agregué el .lengh en la segunda condicion
        this.ValidaMensajes.push("El nombre del Persona debe tener más de 3 caracteres y menos de 150");
      


      if(!this.emailPersona)
      this.ValidaMensajes.push("Debe capturar un email");


      if(this.ValidaMensajes.length)
      this.valida=1;
      console.log(this.ValidaMensajes);

      return this.valida;
    },

 
    
    editedIndex: -1,
    editedItem: {
    idPersona:'',
    nombrePersona:'',
    tipoPersona:'',
    tipodocumento:'',
    numeroDocumento:'',
    direccionPersona:'',
    telefonoPersona:'',
    emailPersona:'',

    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? 'Nuevo Cliente' : 'Editar Cliente'
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
    this,this.ListadoCliente();
    this.initialize();
  },

  methods: {

    ListadoCliente()
    {
        let Lista = this;
        axios.get('api/Personas/ListarCliente').then(function(response)
        {
            console.log(response);
            Lista.Personas = response.data;
            console.log(Lista.Personas,"Hola")
        }).catch(function(error)
          {
            console.log(error);
          })
        
    },


    initialize() {
     
    },

    editItem(item) {
      this.idPersona = item.idPersona;
      this.tipoPersona=item.tipoPersona;
      this.nombrePersona = item.nombrePersona;
      this.tipodocumento=item.tipodocumento;
      this.numeroDocumento=item.numeroDocumento;
      this.direccionPersona=item.direccionPersona;
      this.telefonoPersona=item.telefonoPersona;
      this.emailPersona=item.emailPersona;
      this.editedIndex=2;
      this.dialog=true;
   
    },

    deleteItem(item) {
      this.editedIndex = this.desserts.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true
    },

    deleteItemConfirm() {
      this.desserts.splice(this.editedIndex, 1)
      this.closeDelete()
    },

    close() {
      this.dialog = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
        this.LimpiarModal()
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
          console.log("no le sabe pete")
        return;
      }
      if (this.editedIndex > -1) {
        let me = this;
        axios.put('api/Personas/ModificarPersona', 
        {
          'idPersona':me.idPersona,
          'nombrePersona':me.nombrePersona,
          'tipoPersona':me.tipoPersona,
          'tipodocumento':me.tipodocumento,
          'numeroDocumento':me.numeroDocumento,
          'direccionPersona': me.direccionPersona,
          'telefonoPersona': me.telefonoPersona,
          'emailPersona':me.emailPersona,



        }).then(function(response){
          me.close();
          me.ListadoCliente();
          me.LimpiarModal();
        }).catch(function(error)
        {
          console.log(error);
        });

        //Sección para editar los datos 
      } else {
      
        let me = this;
        axios.post('api/Personas/InsertarPersona', 
        {
          'nombrePersona':me.nombrePersona,
          'tipoPersona':'Cliente',
          'tipodocumento':me.tipodocumento,
          'numeroDocumento':me.numeroDocumento,
          'direccionPersona': me.direccionPersona,
          'telefonoPersona': me.telefonoPersona,
          'emailPersona':me.emailPersona,

        }).then(function(response){
          me.close();
          me.ListadoCliente();
          me.LimpiarModal();
        }).catch(function(error)
        {
          console.log(error);
        });
      }
      this.close()
    },

    LimpiarModal() {
      this.idPersona="",
      this.nombrePersona="",
      this.tipodocumento="",
      this.numeroDocumento="",
      this.direccionPersona="",
      this.telefonoPersona="",
      this.emailPersona="",
      this.valida=0,
      this.ValidaMensaje=[]
      
    },

  },
}
</script>