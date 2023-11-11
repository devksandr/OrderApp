$('body').on('click', '.column-remove', function () {
    $(this).parent("tr:first").remove();
})

$('body').on('click', '#add-order-item-button', function () {
    addEmptyRowToOrderItemsTable();
})

function addEmptyRowToOrderItemsTable() {
    let OrderItemsDefaultName = 'OrderData.Items[0]';
    $("#order-items-table").find('tbody')
        .append($('<tr>')

            .append($('<td>')
                .attr('class', 'column-id d-none')
                .append($('<input>')
                    .attr('name', `${OrderItemsDefaultName}.Id`)
                    .text('0')))

            .append($('<td>')
                .append($('<input>')
                    .attr('name', `${OrderItemsDefaultName}.Name`)))

            .append($('<td>')
                .append($('<input>')
                    .attr('name', `${OrderItemsDefaultName}.Quantity`)))

            .append($('<td>')
                .append($('<input>')
                    .attr('name', `${OrderItemsDefaultName}.Unit`)))

            .append($('<td>')
                .attr('class', 'column-remove')
                .append($('<button>')
                    .text('Remove')))
        );
}

$('body').on('click', '#save-order-button', function (e) {
    e.preventDefault();
    var params = $("#save-order-form").serializeArray();
    setOrderItemsArrayIndexes(params);

    var orderId = $('#order-id-input').val();
    let requestParams = {
        type: orderId > 0 ? "PUT" : "POST",
        url: orderId > 0 ? "/Order/Update" : "/Order/Create"
    };

    $.ajax({
        type: requestParams.type,
        url: requestParams.url,
        data: params,
        success: function (response) {
            // data: { "orderId": orderId },
            //$("#order-madal").find(".modal-content").html(response);
            //$("#order-madal").modal('show');
            location.reload();  

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

    //return false;
})

function setOrderItemsArrayIndexes(dataArray) {

    let orderItemsDefaultIndex = 0;
    let orderItemsPropsCount = 4;
    let orderItemsName = 'OrderData.Items';
    let orderItemsStartIndex = dataArray.findIndex(e => e.name.includes(orderItemsName));

    if (orderItemsStartIndex < 0) {
        return;
    }

    for (var i = orderItemsStartIndex; i < dataArray.length; i++) {
        let orderItemIndex = div(i - orderItemsStartIndex, orderItemsPropsCount);
        dataArray[i].name = dataArray[i].name.replace(orderItemsDefaultIndex, orderItemIndex);
    }
}

function div(a, b) {
    return Math.floor(a / b);
}

$('body').on('click', '#edit-order-button', function () {
    $("#save-order-form :input").removeAttr("disabled");
    $("#edit-order-button").addClass("d-none");
    $("#remove-order-button").addClass("d-none");
    $("#add-order-item-button").removeClass("d-none");
    $("#order-items-table tbody td:last-child").removeClass("d-none");
    $("#save-order-button").removeClass("d-none");
})

$('body').on('click', '#remove-order-button', function (e) {
    var orderId = $('#order-id-input').val();
    $.ajax({
        type: 'DELETE',
        url: "/Order/Delete",
        data: orderId,
        success: function (response) {
            // data: { "orderId": orderId },
            //$("#order-madal").find(".modal-content").html(response);
            //$("#order-madal").modal('show');

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
})

