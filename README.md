Documentación del Sistema de Pedidos para Restaurante

Este proyecto consiste en un sistema de gestión de pedidos para un restaurante, implementado mediante una arquitectura
de microservicios en .NET. Los microservicios se comunican entre sí utilizando HTTP y un API Gateway basado en YARP. 
Cada servicio está desacoplado y posee su propia base de datos en PostgreSQL.

#############################################################################################

TECNOLOGIAS UTILIZADAS 

Lenguaje principal: C#

Framework: .NET (ASP.NET Core)

API Gateway: YARP (Yet Another Reverse Proxy)

Bases de datos: PostgreSQL (una por microservicio)

ORM: Entity Framework Core

###############################################################################################

MICROSERVICIOS 


1. 📦Microservicio de Pedidos (PedidosService)
Responsabilidad: Crear y gestionar pedidos de comida.

Endpoints clave:

POST /api/pedidos: Crear un nuevo pedido.

GET /api/pedidos/{id}: Obtener información de un pedido.

PUT /api/pedidos/{id}/estado: Actualizar estado del pedido (ej. "entregado").
Integraciones:

Llama al microservicio de Mesa para saber si está disponible.

Llama al microservicio de Menú para validar los ítems del pedido.

Al marcar como "entregado", libera la mesa correspondiente.

2. 📋 Microservicio de Menú (MenuService)
Responsabilidad: Gestionar los menús del restaurante.

Endpoints clave:

POST /api/menu: Crear un nuevo ítem de menú.

GET /api/menu: Listar los ítems del menú.

GET /api/menu/{id}: Consultar detalle de un ítem.

3. 🍽 Microservicio de Mesa (MesaService)
Responsabilidad: Gestionar la disponibilidad de mesas.

Endpoints clave:

GET /api/mesa/disponible/{id}: Consultar si una mesa está libre.

PUT /api/mesa/ocupar/{id}: Ocupar una mesa.

PUT /api/mesa/liberar/{id}: Liberar una mesa 

###################################################################################################


Comunicación entre Microservicios
Utiliza HTTP REST entre servicios.

YARP funciona como API Gateway, dirigiendo las peticiones hacia el microservicio correspondiente según la ruta.

Estructura típica del yarp.json

Ej :
{
  "ReverseProxy": {
    "Routes": [
      {
        "RouteId": "pedidos",
        "ClusterId": "pedidosCluster",
        "Match": { "Path": "/api/pedidos/{**catch-all}" }
      }
    ],
    "Clusters": {
      "pedidosCluster": {
        "Destinations": {
          "pedidosApp": { "Address": "https://localhost:5001" }
        }
      }
    }
  }
}



#########################################################################################################################


Requisitos de Instalación


.NET SDK 9.0 o superior.
PostgreSQL para las bases de datos.
Visual Studio 2022+ o VS Code con extensiones C# y PostgreSQL.


#########################################################################################################################


Cómo Ejecutar el Proyecto


Clonar el repositorio.

Entrar a cada carpeta de microservicio y ejecutar:

dotnet ef database update
dotnet run

Ejecutar el API Gateway (yarp) para enrutar peticiones

Consumir los Endpoints via swagger,  Postman o bruno 

##########################################################################################################################