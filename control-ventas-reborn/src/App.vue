<template>
  <v-app id="inspire">



    <v-navigation-drawer app>
      <template>
        <v-list-item :to="{ name: 'home' }">
          <v-list-item-action>
            <v-icon>home</v-icon>
          </v-list-item-action> 

          <v-list-item-title>
            Inicio
          </v-list-item-title>
        </v-list-item>
      </template>

      <template>
        <v-list-group>
          <v-list-item slot="activator">
            <v-list-item-content>
              <v-list-item-title>
                Almacén
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'categorias' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Gestión de categorías
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'articulos' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Gestión de artículos
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

        </v-list-group>
      </template>



      <template>
        <v-list-group>
          <v-list-item slot="activator">
            <v-list-item-content>
              <v-list-item-title>
                Usuarios
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'roles' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Gestión de roles
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'usuarios' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Gestión de usuarios
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

        </v-list-group>
      </template>



      <template>
        <v-list-group>
          <v-list-item slot="activator">
            <v-list-item-content>
              <v-list-item-title>
                Ventas
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'ventas' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Ventas
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'clientes' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Gestión de clientes
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-group>
      </template>



      <!--COMPRAS -->
      <template>
        <v-list-group>
          <v-list-item slot="activator">
            <v-list-item-content>
              <v-list-item-title>
                Compras
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'ingresos' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Ingresos
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'proveedor' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Gestión de proveedores
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-group>
      </template>

      <!--CONSULTAS -->
      <template>
        <v-list-group>
          <v-list-item slot="activator">
            <v-list-item-content>
              <v-list-item-title>
                Consultas
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'consultarVentas' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Consultar compras
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>

          <v-list-item :to="{ name: 'consultarCompras' }">
            <v-list-item-action>
              <v-icon>table_chart</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                Consultar ventas
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-group>
      </template>

    </v-navigation-drawer>

    <v-content>
      <v-card height="100" width="100%" class="text-center white--text" color="purple
lighten-1">
        <h3 class="pt-10"> Sistema para el control de Ventas </h3>
      </v-card>
    </v-content>

    <v-content>
      <h1 class="text-center success--text mt-10">Zona para trabajar el proyecto</h1>
      <router-view /> <!-- Etiqueta para mostrar los componentes del ruteo -->
    </v-content>

    <v-footer>
      <v-card height="30" width="100%" class="text-center white--text" color="purple
lighten-1">
        <h3> ISyTE - FIAD-UABC - {{ new Date().getFullYear() }} </h3>
      </v-card>
    </v-footer>



  </v-app>
</template>
