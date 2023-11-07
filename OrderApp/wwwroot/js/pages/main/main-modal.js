$('body').on('click', '.column-remove', function () {
    $(this).parent("tr:first").remove();
})
