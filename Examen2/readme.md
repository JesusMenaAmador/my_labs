# Examen 2 de Ingeniería de Software - Grupo 03

## Estudiante: Jesús Mena Amador

## Carnet: B54291

## Branching strategy

Se trabaja en la rama Examen2 y para la entrega final se hace un pullrequest a main simulando colocar en producción la aplicación.

## Para correr el proyecto

Favor primero revisar los paquetes necesarios para correr cada parte del proyecto:
  * Paquetes necesarios para correr el [front-end](/my_labs/Examen2/FrontEnd/readme.md/#paquetes-necesarios-para-correr-el-proyecto-front-end).

  * Paquetes necesarios para correr el [back-end](/my_labs/Examen2/BackEnd/readme.md/#paquetes-necesarios-para-correr-el-proyecto-back-end).

Una vez verificados los paquetes se puede proceder a correr el proyecto:

  1. [Correr la parte back-end](/my_labs/Examen2/BackEnd/readme.md/#para-correr-el-proyecto-back-end-de-la-planilla) del proyecto.

  2. [Correr la parte front-end](/my_labs/Examen2/FrontEnd/readme.md/#para-correr-el-proyecto) del proyecto.


# Back-end Volagenersteller

## Paquetes necesarios para correr el proyecto back-end

Colocarse en la carpeta raiz del proyecto ".../backend-planilla" y correr los siguientes comandos:
  * `dotnet add package System.Data.SqlClient`
  * `dotnet add package Microsoft.AspNetCore.Mvc.Core`
  * `dotnet add package Microsoft.AspNetCore.Http.Abstractions`
  * `dotnet add package Microsoft.Data.SqlClient`
  * `dotnet add package Moq`

## Volagenersteller Planilla 

Este proyecto de back-end desarrollado con .NET(8.0) se complementa con la parte front-end (más información en [readme.md](/my_labs/Examen2/FrontEnd/readme.md)) desarrollada en la carpeta [FrontEnd](/my_labs/Examen2/FrontEnd).

### Para correr el proyecto back-end de la planilla

Desde Visual Studio Community 2022 abra el [archivo de solución del proyecto en la carpeta backend-planilla](/my_labs/Examen2/BackEnd/) llamado backend-planilla.sln y presionar f5 para correr el proyecto.

# Volagenersteller-Planilla front-end 
Este proyecto de front-end desarrollado con Vue.js se complementa con la parte back-end (más información en [readme.md](/my_labs/Examen2/BackEnd/readme.md)) desarrollada en la carpeta [BackEnd](/my_labs/Examen2/BackEnd).

## Paquetes necesarios para correr el proyecto front-end

Colocarse en la carpeta raiz del proyecto ".../frontend-planilla" y correr los siguientes comandos:
  * `npm install vue-router`
  * `npm install bootstrap`
  * `npm install @fortawesome/fontawesome-free`
  * `npm install html2pdf.js`
  * `npm install chart.js vue-chartjs`


## Para correr el proyecto

Colocarse en la carpeta raíz del proyecto ".../frontend-planilla" y ejecutar el siguiente comando para levantar el servidor:
  * `npm run serve`

Después desde un navegador visitar la página:
  * http://localhost:8080/