function addBeatToCart(beatId, $btn){
    $.ajax({
        type: "POST",
        url: `/Beats/MoreUserBeats/0?handler=AddBeatToCart&beatId=${beatId}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result) {
            if(result.isSuccess) {
                $btn.hide(200, "swing");
                $btn.parent().children(".remove_beat_to_cart_btn").show(200, "swing");
            }
            else {
                $(`#bg_color_modal_notification`).attr("class", "bg-danger");
                $("#notification_content").text(result.value);
                $("#modal_with_notification").modal("show");
            }
        }
    });
}

function removeBeatToCart(beatId, $btn){
    $.ajax({
        type: "POST",
        url: `/Beats/MoreUserBeats/0?handler=RemoveBeatToCart&beatId=${beatId}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result) {
            if(result.isSuccess) {
                $btn.hide(200, "swing");
                $btn.parent().children(".add_beat_to_cart_btn").show(200, "swing");
            }
            else {
                $(`#bg_color_modal_notification`).attr("class", "bg-danger");
                $("#notification_content").text(result.value);
                $("#modal_with_notification").modal("show");
            }
        }
    });
}

function tryAddBeatToCart($addBtn){
    if($addBtn.attr("data-is-registered") === "True")
        addBeatToCart($addBtn.attr("data-beat-id"), $addBtn);
    else
    {
        $("#bg_color_modal_notification").attr("class", "bg-danger");
        $("#notification_content").text("Покупать биты могут только авторизованные пользователи");
        $("#modal_with_notification").modal("show");
    }
}

function tryRemoveBeatToCart($removeBtn){
    removeBeatToCart($removeBtn.attr("data-beat-id"), $removeBtn);
}

$(document).ready(function (){
    $(".add_beat_to_cart_btn").click(function (){
        tryAddBeatToCart($(this));
    })

    $(".remove_beat_to_cart_btn").click(function (){
        tryRemoveBeatToCart($(this));
    })
})