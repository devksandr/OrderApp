$('body').on('click', '.column-remove', function () {
    $(this).parent("tr:first").remove();
})

$('body').on('click', '#add-order-item-button', function () {
    addEmptyRowToOrderItemsTable();
})

function addEmptyRowToOrderItemsTable() {
    $("#order-items-table").find('tbody')
        .append($('<tr>')

            .append($('<td>')
                .attr('class', 'column-id d-none')
                .text('0'))

            .append($('<td>')
                .append($('<input>')
                    .attr('id', 'orderItem_Name')
                    .attr('name', 'orderItem.Name')))

            .append($('<td>')
                .append($('<input>')
                    .attr('id', 'orderItem_Quantity')
                    .attr('name', 'orderItem.Quantity')))

            .append($('<td>')
                .append($('<input>')
                    .attr('id', 'orderItem_Unit')
                    .attr('name', 'orderItem.Unit')))

            .append($('<td>')
                .attr('class', 'column-remove')
                .text('remove'))
        );
}