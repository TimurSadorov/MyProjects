function deleteBeat(id) {
    $.ajax({
        type: "POST",
        url: `/Beats/EditingBeats?handler=DeleteBeat&id=${id}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (isSuccess) {
            if(isSuccess) {
                $(`#beat-${id}`).detach();
                $(`#bg_color_modal_notification`).attr("class", "bg-success");
                $("#notification_content").text("Бит успешно удален");
            }
            else {
                $(`#bg_color_modal_notification`).attr("class", "bg-danger");
                $("#notification_content").text("Во время удаления произошла ошибка, попробуйте перезагрузить страницу и повторить операцию снова");
            }
            $("#modal_with_notification").modal("show");
        }
    });
}

function setPrice(price, beatId)
{
    $.ajax({
        type: "POST",
        url: `/Beats/EditingBeats?handler=SetPrice&newPrice=${price}&beatId=${beatId}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (isSuccess) {
            if(isSuccess) {
                let priceElement = $(`#beat-${beatId}`).find("#price");
                priceElement.text(`${price}₽`);
                priceElement.attr("data-number", price);
                $("#modal_change_price").modal("hide");
                $(`#bg_color_modal_notification`).attr("class", "bg-success");
                $("#notification_content").text("Цена успешно сменина");
            }
            else {
                $(`#bg_color_modal_notification`).attr("class", "bg-danger");
                $("#notification_content").text("Во время смены цены произошла ошибка, попробуйте перезагрузить страницу и повторить операцию снова");
            }
            $("#modal_with_notification").modal("show");
        }
    });
}

function checkOnCorrectPrice() {
    let priceInStr = $("#input_price").val();
    if(priceInStr.length === 0) {
        $("#error_message_for_price").text("Поле должно быть заполнено целым числом");
        $("#confirm_price").prop("disabled", true);
    }
    else {
        let price = parseInt(priceInStr);
        if (isNaN(price)) {
            $("#error_message_for_price").text("Значение должно быть целым числом");
            $("#confirm_price").prop("disabled", true);
        } else if (price < 0) {
            $("#error_message_for_price").text("Значение должно быть больше 0");
            $("#confirm_price").prop("disabled", true);
        } else {
            $("#error_message_for_price").text("");
            $("#confirm_price").prop("disabled", false);
            return true;
        }
    }
    return false;
}


function checkOnCorrectTitle() {
    let priceInStr = $("#input_title").val();
    if(priceInStr.length === 0) {
        $("#error_message_for_title").text("Это поле должно быть заполнено");
        $("#confirm_title").prop("disabled", true);
    }
    else if(priceInStr.length > 100) {
        $("#error_message_for_title").text("Название не должно быть больше ста символов");
        $("#confirm_title").prop("disabled", true);
    }
    else {
        $("#error_message_for_title").text("");
        $("#confirm_title").prop("disabled", false);
        return true;
    }
    return false;
}

function setTitle(newTitle, beatId)
{
    $.ajax({
        type: "POST",
        url: `/Beats/EditingBeats?handler=SetTitle&newTitle=${newTitle}&beatId=${beatId}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (isSuccess) {
            if(isSuccess) {
                let priceElement = $(`#beat-${beatId}`).find(".title_beat");
                priceElement.text(newTitle);
                $("#modal_change_title").modal("hide");
                $(`#bg_color_modal_notification`).attr("class", "bg-success");
                $("#notification_content").text("Название бита успешно сменино");
            }
            else {
                $(`#bg_color_modal_notification`).attr("class", "bg-danger");
                $("#notification_content").text("Во время смены названия бита произошла ошибка, попробуйте перезагрузить страницу и повторить операцию снова");
            }
            $("#modal_with_notification").modal("show");
        }
    });
}

$(document).ready(function (){
    $(".delete_beat_button").on("click", function () {
        $("#yes").attr("beat-id", $(this).attr("beat-id"));
        $("#confirmation").modal("show");
    });
    $("#yes").on("click", function (){
        deleteBeat($(this).attr("beat-id"));
    });
    $(".price").on("click", function (){
        $("#confirm_price").attr("beat-id", $(this).attr("beat-id"));
        $("#input_price").val($(this).attr("data-number"));
        $("#modal_change_price").modal("show");
    })
    $("#confirm_price").on("click", function (){
        if(checkOnCorrectPrice()) 
            setPrice($("#input_price").val(), $(this).attr("beat-id"));
    })
    $("#input_price").on("input", function (){
       checkOnCorrectPrice();
    })
    $(".title_beat").on("click", function () {
        $("#confirm_title").attr("beat-id", $(this).attr("beat-id"));
        let t = $(this).text();
        $("#input_title").val($(this).text());
        $("#modal_change_title").modal("show");
    });
    $("#input_title").on("input", function (){
        checkOnCorrectTitle();
    });
    $("#confirm_title").on("click", function (){
        if(checkOnCorrectTitle())
            setTitle($("#input_title").val(), $(this).attr("beat-id"));
    })
})