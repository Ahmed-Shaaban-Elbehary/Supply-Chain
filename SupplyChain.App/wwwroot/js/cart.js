var cart = cart || {}
var quantity = $("#ProductQuantity");
var actual_amount = $("#ProductAmount").val();
var count = 0;
cart.items = {
    add: function () {
        if (count < actual_amount) {
            count++;
            quantity.val(count);
        } else {
            $("#warningMessage").text("insufficient amount, please contact admin!");
        }
    },
    remove: function () {
        $("#warningMessage").text("");

        if (count > 0) {
            count--;
            quantity.val(count);
        }
    },
    check_amount: function () {
        if (quantity.val() > actual_amount) {
            $("#warningMessage").text("insufficient amount, stock only have (" + actual_amount + ").");
        } else {
            $("#warningMessage").text("");
        }
    }
}