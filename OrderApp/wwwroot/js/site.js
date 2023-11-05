$("#add-order-button").click(function () {
    $.ajax({
        type: "POST",
        url: "/Order/Create",
        data: { "orderId": 0 },
        success: function (response) {
            $("#partialModal").find(".modal-body").html(response);
            $("#partialModal").modal('show');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
});