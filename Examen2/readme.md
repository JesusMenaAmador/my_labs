# Examen 2 de Ingeniería de Software - Grupo 03

## Estudiante: Jesús Mena Amador

## Carnet: B54291

## Descripción del proyecto

Se tiene una máquina expendedora que muestra la cantidad de refrescos disponibles y permite al usuario ingresar la cantidad que desea (puede colocar el número directamente o utilizar los indicadores numéricos para ir aumentando o disminuyendo de 1 en 1 la cantidad).

Desde el frontend se validan entradas permitidas, algunas válidaciones son cantidad máxima, nada diferente a un número, no menor a 0 o mayor a la cantidad en stock, también para que el botón de continuar con el pago se active se debe tener al menos un refresco en el total y no estar seleccionado ningún inputbox. Una vez seleccionada la cantidad valida de refrescos que el usuario desea adquirir se presiona el botón continuar con el pago, el cuál desplegará el menú de ingreso de cantidad de monedas o billetes, se validan un máximo de ingreso por parte del usuario de 999 de cada moneda y/o billete. 

Una vez verificado todo se procede a presionar el botón pagar, el cuál envía al backend el dinero ingresado y los refrescos seleccionados a pagar para que se encargue de realizar los cálculos, y retorne el monto total del vuelto y el monto desglosado, además de actualizar el inventario de refrescos y dinero. Una vez mostrado el resultado de la transacción en pantalla se muestra también la opción para realizar más compras; en caso donde no se ingrese dinero o falte para pagar el total se le indica al usuario y se le reembolsa lo que haya ingresado, de igual forma si la cantidad de dinero insertada es demasiado alta y la máquina no tiene suficientes monedas para dar el cambio procede a indicarle al usuario que la máquina no cuenta con cambio suficiente y le retorna exactamente el mismo dinero que inserto en la máquina.

En los casos donde se paga exacto el monto o la máquina si tiene cambio despliega un mensaje de Pago exitoso junto a los 0 desglosados y el botón de nueva compra, la máquina nunca da billetes como vuelto, sin embargo, si los retorna como devolución, por eso se muestra un campo correspondiente en la parte de Vuelto entregado.

Al final cuando el cambio se muestra aparece la opción para realizar una nueva compra, lo que lleva de nuevo a la máquina expendedora, mientras el backend no deje de correr el inventario se va a ir disminuyendo con cada compra hasta que todo esté en 0 y ya no se pueda comprar más, en cuyo caso hay que cerrar y volver a correr el backend para "reiniciar" o rellenar nuestra máquina expendedora. 

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
