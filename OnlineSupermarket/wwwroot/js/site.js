$(".basket-form").on('submit', function (e) {
    e.preventDefault()

    var form = $(this);
    var url = form.attr("action")

    $.ajax({
        type: 'POST',
        url: url,
        success: function (result) {
            $("#checkout-number").text(result)
        }
    })
})

$(".quantity-form").on('submit', function (e) {
    e.preventDefault();

    var form = $(this);
    var url = form.attr('action');

    $.ajax({
        type: 'POST',
        url: url,
        data: form.serialize(),
        success: function (result) {
            $("#subtotal-value").text(result)
        }
    })
})

$(".quantity-input").change(function (e) {
    e.preventDefault();
    $(this).closest('form').submit();
})

$(".remove-form").on('submit', function(e) {
    e.preventDefault();

    var form = $(this);
    var url = form.attr('action');

    $.ajax({
        type: 'POST',
        url: url,
        success: function () {
            form.closest('tr').remove();
        }
    })
})
