// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
 $(function () {
        $('table').DataTable();
    $('[data-toggle="tooltip"]').tooltip();
 });

$(function () {
    $(".modal").on("hidden.bs.modal", function () {
        $("#modal_content").html("Loading....");
        $(".modal-footer input[type='submit']").remove();
    });
    $(".modal").on("show.bs.modal", function () {
        $('[data-toggle="tooltip"]').tooltip('hide');
        $("#modal_content input[type='submit']").prependTo(".modal-footer");

    });
});

