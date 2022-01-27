let $totalAmount = $("#total_amount");

function subtractFromTotalAmount(e){
    let price = $(e.target).parents(".beat").find(".price").attr("data-number");
    let newTotalAmount = $totalAmount.attr("data-sum") - price;
    $totalAmount.attr("data-sum", newTotalAmount);
    $totalAmount.text(`${newTotalAmount} ₽`);
}

function addToTotalAmount(e){
    let price = $(e.target).parents(".beat").find(".price").attr("data-number");
    let newTotalAmount = parseInt($totalAmount.attr("data-sum")) + parseInt(price);
    $totalAmount.attr("data-sum", newTotalAmount);
    $totalAmount.text(`${newTotalAmount} ₽`);
}

$(document).ready(function (){
    $(".remove_beat_to_cart_btn").click(subtractFromTotalAmount);
    $(".add_beat_to_cart_btn").click(addToTotalAmount);
})