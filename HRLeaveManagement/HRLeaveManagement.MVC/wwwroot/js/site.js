$(window).on('beforeunload', function () {
    $("#loader").modal('show')
    $(".spinner-border").removeClass("d-none");
});
