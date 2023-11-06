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

$("#submit-order-filter-button").click(function () {

    var data = $("#order-number-filter").val();
    var values = $("#order-number-filter").chosen().val();

    let params = {
        OrderDateStart: $("#order-start-date-filter").datepicker().val(),
        OrderDateEnd: $("#order-end-date-filter").datepicker().val(),
        OrderNumbers: $("#order-number-filter").val(),
        OrderProviderIds: $("#order-provider-id-filter").val(),
        OrderItemNames: $("#order-item-name-filter").val(),
        OrderItemUnits: $("#order-item-unit-filter").val(),
        ProviderNames: $("#provider-name-filter").val()
    };

    $.ajax({
        type: "POST",
        url: "/Form/GetFilteredOrders",
        data: params,
        success: function (response) {
            $("#orders-table-container").html(response);
            //$("#partialModal").modal('show');
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
});