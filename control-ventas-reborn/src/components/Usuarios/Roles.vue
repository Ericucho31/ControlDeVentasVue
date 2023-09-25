<template>
    <h1 class="text-center ">Roles</h1>
</template>

<template>
  <v-data-table :headers="headers" :items="roles" sort-by="nombreRol" class="elevation-1">
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>Roles</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
              Nuevo Rol
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
                    <v-text-field v-model="nombreRol" label="Nombre Rol"></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="descripcionRol" label="Descripción Rol"></v-text-field>
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
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete">Cancel</v-btn>
              <v-btn color="blue darken-1" text @click="deleteItemConfirm">OK</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon small class="mr-2" @click="editItem(item)">
        mdi-pencil
      </v-icon>
      <v-icon small @click="deleteItem(item)">
        mdi-delete
      </v-icon>
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

    roles: [],
    validarRol() {
      this.valida=0;
      this.ValidaMensajes=[];

  
      if(this.nombreRol.length< 3 || this.nombreRol.length >30) //aqui solamente agregué el .lengh en la segunda condicion
        this.ValidaMensajes.push("El nombre de la categoria debe tener más de 3 caracteres y menos de 30");
      if(this.ValidaMensajes.length)
      this.valida=1;

      return this.valida;
    },

    dialog: false,
    dialogDelete: false,
    headers: [
      { text: 'Nombre Rol', value: 'nombreRol', align:'start', sortable:true },
      { text: 'Descripcion Rol', value: 'descripcionRol',sortable:true },
      { text: 'Estado', value: 'estado',sortable:true },
      { text: 'Accion', value: 'actions', sortable: false },
    ],
    
    editedIndex: -1,
    editedItem: {
      idRol: '',
      nombreRol: '',
      descripcionRol: '',
    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? 'Nuevo Rol' : 'Editar Rol'
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
    this,this.ListadoRoles();
    this.initialize()
  },

  methods: {
    ListadoRoles()
    {
        let Lista = this;
        axios.get('https://localhost:7180/api/Roles/ListarRoles').then(function(response)
        {
            console.log(response);
            Lista.roles = response.data;
        }).catch(function(error)
          {
            console.log(error);
          })
        ;
    },



    initialize() {
     
    },

    editItem(item) {
      this.idRol = item.idRol;
      this.nombreRol = item.nombreRol;
      this.descripcionRol = item.descripcionRol;
      this.editedIndex = 1;
      this.dialog = true
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
      if(this.validarRol() )
      {
        return;
      }
      if (this.editedIndex > -1) {
        let me = this;
        axios.put('api/Roles/ModificarRoles', 
        {
          'idRol': me.idRol,
          'nombreRol': me.nombreRol,
          'descripcionRol': me.descripcionRol
        }).then(function(response){
          me.close();
          me.ListadoRoles();
          me.LimpiarModal();
        }).catch(function(error)
        {
          console.log(error);
        });

        //Sección para editar los datos 
      } else {
        //Sección para Guardar los datos de una nueva categoria
        let me = this;
        axios.post('api/Roles/InsertarRoles', 
        {
          'nombreRol': me.nombreRol,
          'descripcionRol': me.descripcionRol
        }).then(function(response){
          me.close();
          me.ListadoRoles();
          me.LimpiarModal();
        }).catch(function(error)
        {
          console.log(error);
        });
      }
      this.close()
    },

    LimpiarModal() {
      this.idRol= '';
      this.nombreRol='';
      this.descripcionRol='';
    },

  },
}
</script>