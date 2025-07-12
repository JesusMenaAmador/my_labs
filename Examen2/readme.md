# Examen 2 de Ingeniería de Software - Grupo 03

## Estudiante: Jesús Mena Amador

## Carnet: B54291

## Descripción del proyecto

Se tiene una máquina expendedora que muestra la cantidad de refrescos disponibles y permite al usuario ingresar la cantidad que desea, desde el frontend se validan entradas permitidas únicamente; una vez seleccionada la cantidad valida de refrescos que el usuario desea adquirir se presiona el botón continuar con el pago, el cuál desplegará el menú de ingreso de cantidad de monedas o billetes, se validan un máximo de ingreso por parte del usuario de 999 de cada moneda y/o billete. Una vez verificado todo se procede a presionar el botón pagar, el cuál envía al backend el dinero ingresado y el total a pagar para que se encargue de realizar los cálculos, y retorne el monto del vuelto desglozado, además de actualizar el inventario de refrescos y monedas. Una vez mostrado el resultado de la transacción en pantalla se muestra también la opción para realizar más compras. 

## Branching strategy

Se trabaja en la rama Examen2 y para la entrega final se hace un pullrequest a main simulando colocar en producción la aplicación.

## Para correr el proyecto

Favor primero revisar los paquetes necesarios para correr cada parte del proyecto:
  * Paquetes necesarios para correr el proyecto front-end

    Colocarse en la carpeta [/my_labs/Examen2/frontend-expendedora](/my_labs/Examen2/frontend-expendedora), abrir una terminal y correr los siguientes comandos:
      * `npm install vue-router`
      * `npm install bootstrap`
      * `npm install axios`

  * Paquetes necesarios para correr el proyecto back-end

    Colocarse en la carpeta [/my_labs/Examen2/backend-expendedora](/my_labs/Examen2/backend-expendedora), abrir una terminal y correr los siguientes comandos:
      * ``
      * ``
      * ``

Una vez verificados los paquetes se puede proceder a correr el proyecto backend y después el frontend:

  1. **Para correr el proyecto back-end:**

      Desde Visual Studio Community 2022 abra el archivo de solución del proyecto en la carpeta backend-expendedora llamado [backend-expendedora.sln](/my_labs/Examen2/backend-expendedora/backend-expendedora.sln) y presionar f5 para correr el proyecto.

  2. **Para correr el proyecto front-end:**

      Colocarse en la carpeta [/my_labs/Examen2/frontend-expendedora](/my_labs/Examen2/frontend-expendedora) y ejecutar el siguiente comando para levantar el servidor:
      * `npm run serve`

      Después desde un navegador visitar la página:
      * http://localhost:8080/
