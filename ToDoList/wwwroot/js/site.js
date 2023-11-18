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
            var elementos = ui.item.children();
            //Hacer codigo para actualizar a db
        }
    }).disableSelection();
});
