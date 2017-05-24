$(function () {
    $('#addItem').on('click', function () {
        var actualItems = [];
        $("table").find(".itemNumber").each(function () {
            actualItems.push($(this).text().trim());
        });

        $.get("/Shippings/AddOrderItem",
            $.param({ 'productIds': actualItems }, true), function (partialView) {
            var tableBody = $('table tbody');
            tableBody.fadeIn(400,
                function () {
                    tableBody.append(partialView);
                });
            });
        (actualItems.length === 3) ? $('#addItem').removeClass('show').hide() : $('#addItem').show();
    });

    $("table").on("click", ".delete-button", function(event) {
        event.preventDefault();
        var tr = $(this).closest('tr');
        tr.fadeOut(400,
            function() {
                tr.remove();
                $('#addItem').removeClass('hide').show();
            });
    });
});
