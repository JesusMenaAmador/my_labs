let fondoBlanco = true; /*Indica si el color de fondo actual blanco o no*/

function agregar() {
    let lista = document.getElementById("lista"); /*Se obtiene la referencia al elemento <ul> con id 'lista' */
    let elementos = lista.getElementsByTagName("li"); /*Obtiene todos los elementos <li> dentro de la lista */
    let nuevoElemento = document.createElement("li"); /*Crea un nuevo elemento <li> */
    nuevoElemento.textContent = "Elemento" + (elementos.length + 1); /*Asigna el texto 'Elemento siguiente número disponible' */
    lista.appendChild(nuevoElemento); /*Añade el nuevo elemento <li> al final de la lista */
}

function cambiarFondo() {
    document.body.style.backgroundColor = fondoBlanco ? "green" : "white"; /*Alterna el color en función de cual está activo */
    fondoBlanco = !fondoBlanco; /*Indica que el color de fondo fue cambiado */
}

function borrar() {
    let lista = document.getElementById("lista"); /*Se obtiene la referencia al elemento <ul> con id 'lista' */
    let elementos = lista.getElementsByTagName("li"); /*Obtiene todos los elementos <li> dentro de la lista */
    /*Pregunta si hay elementos en lista antes de eliminar */
    if (elementos.length > 0) {
        lista.removeChild(elementos[elementos.length - 1]); /*Elimina el último elemento de la lista si hay al menos uno presente */
    }
}