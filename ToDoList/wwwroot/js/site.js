// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

jQuery(document).ready(function () {
    //Se crean los objetos draggables por cada elementos de las listas
    var drag1 = $("#listaPendientes ul li").draggable({
        revert: true,
        distance: 10
    });
    var drag2 = $("#listaProceso ul li").draggable({
        revert: true,
        distance: 10
    });
    var drag3 = $("#listaTerminados ul li").draggable({
        revert: true,
        distance: 10
    });
    //Se hacen los elementos de la lista sortables para que acepten el drop
    $(".sortable").sortable({
        connectWith: ".sortable",
        dropOnEmpty: true,
        update: function (event, ui) {
            var tarea = ui.item.children().children();
            // Se actualiza el estilo
            var claseActual = tarea.attr("class"); //Se obtiene la clase actual de la tarea
            tarea.removeClass(claseActual); //Se elimina la clase actual
            //Se pregunta en cual columna se paso y aplica el estilo correspondiente
            if (ui.item.parent().attr("id") == "listaPendientes") {
                //Se revisa si la columna esta vacia y se quita el de no elementos
                var children = ui.item.parent().children();
                for (var i = 0; i < children.length; ++i) {
                    var child = children[i];
                    if (child.id == "sinPendientes") {
                        child.style.display = "none";
                    }
                }
                tarea.addClass("col-item-pendiente");
            }
            if (ui.item.parent().attr("id") == "listaProceso") {
                var children = ui.item.parent().children();
                for (var i = 0; i < children.length; ++i) {
                    var child = children[i];
                    if (child.id == "sinProcesos") {
                        child.style.display = "none";
                    }
                }
                tarea.addClass("col-item-proceso");
            }
            if (ui.item.parent().attr("id") == "listaTerminados") {
                var children = ui.item.parent().children();
                for (var i = 0; i < children.length; ++i) {
                    var child = children[i];
                    if (child.id == "sinTerminados") {
                        child.style.display = "none";
                    }
                }
                tarea.addClass("col-item-terminado");
            }
            // *** Se actualizan las cantidades de las columnas ***
            //Se obtiene las columnas
            var colPen = document.getElementById("listaPendientes");
            var colPro = document.getElementById("listaProceso");
            var colTer = document.getElementById("listaTerminados");
            //Se obtienen las etiquetas de los numeros
            var cantPend = document.getElementById("cantidadPendientes");
            var cantProc = document.getElementById("cantidadProcesos");
            var cantTerm = document.getElementById("cantidadTerminados");
            //Se saca la cantidad de nodos en las columnas
            var pen = colPen.children.length;
            var pro = colPro.children.length;
            var ter = colTer.children.length;
            //Se recorre cada nodo de cada columna para ver si existe el 'sin'
            for (var i = 0; i < colPen.children.length; ++i) {
                if (colPen.children[i].id == "sinPendientes") {
                    pen -= 1;
                }
            }
            for (var i = 0; i < colPro.children.length; ++i) {
                if (colPro.children[i].id == "sinProcesos") {
                    pro -= 1;
                }
            }
            for (var i = 0; i < colTer.children.length; ++i) {
                if (colTer.children[i].id == "sinTerminados") {
                    ter -= 1;
                }
            }
            //Se cambian las cantidades en las etiquetas
            cantPend.innerHTML = pen;
            cantProc.innerHTML = pro;
            cantTerm.innerHTML = ter;
            
            //Si alguna cantidad esta en 0, se vuelve a poner el 'sin'
            if (pen == 0) {
                var sinPen = document.getElementById("sinPendientes");
                if (sinPen == null) { //Si no existe, se crea elemento html
                    //Se crea el div
                    var divPen = document.createElement("div");
                    divPen.classList.add("sin-tareas"); //Se agrega la clase
                    divPen.id = "sinPendientes"; //Se agrega el id
                    //Se crea el h5
                    var etq = document.createElement("h5");
                    etq.innerHTML = "No hay tareas pendientes"; //Se agrega el texto
                    divPen.appendChild(etq); //Se agrega la etiqueta al div
                    var colPendientes = document.getElementById("listaPendientes");
                    colPendientes.appendChild(divPen);
                } else {
                    sinPen.style.display = "block";
                }
            }
            if (pro == 0) {
                var sinPro = document.getElementById("sinProcesos");
                if (sinPro == null) { //Si no existe, se crea elemento html
                    //Se crea el div
                    var divPen = document.createElement("div");
                    divPen.classList.add("sin-tareas"); //Se agrega la clase
                    divPen.id = "sinProcesos"; //Se agrega el id
                    //Se crea el h5
                    var etq = document.createElement("h5");
                    etq.innerHTML = "No hay tareas en proceso"; //Se agrega el texto
                    divPen.appendChild(etq); //Se agrega la etiqueta al div
                    var colProceso = document.getElementById("listaProceso");
                    colProceso.appendChild(divPen);
                } else {
                    sinPro.style.display = "block";
                }
            }
            if (ter == 0) {
                var sinTer = document.getElementById("sinTerminados");
                if (sinTer == null) { //Si no existe, se crea elemento html
                    //Se crea el div
                    var divPen = document.createElement("div");
                    divPen.classList.add("sin-tareas"); //Se agrega la clase
                    divPen.id = "sinTerminados"; //Se agrega el id
                    //Se crea el h5
                    var etq = document.createElement("h5");
                    etq.innerHTML = "No hay tareas terminadas"; //Se agrega el texto
                    divPen.appendChild(etq); //Se agrega la etiqueta al div
                    var colTerminados = document.getElementById("listaTerminados");
                    colTerminados.appendChild(divPen);
                } else {
                    sinTer.style.display = "block";
                }
            }
            //Hacer codigo para actualizar a db
        }
    }).disableSelection();
});

function actualizarCantidades() {
    
}
