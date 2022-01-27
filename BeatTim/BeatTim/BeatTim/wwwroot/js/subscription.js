function subscribe(){
    $.post({
        url: `/Account/UserAccount/${$("#page_id").attr("page-id")}?handler=Subscribe`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result){
            if(!result.isSuccess)
                $("#bg_color_modal_notification").attr("class", "bg-danger");
            else {
                $("#bg_color_modal_notification").attr("class", "bg-success");
                $("#subscribe_btn").hide();
                $("#unsubscribe_btn").show();
            }
            $("#notification_content").text(result.value);
            $("#modal_with_notification").modal("show");
        }
    })
}

function unsubscribe(){
    $.post({
        url: `/Account/UserAccount/${$("#page_id").attr("page-id")}?handler=Unsubscribe`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result){
            if(!result.isSuccess)
                $("#bg_color_modal_notification").attr("class", "bg-danger");
            else {
                $("#bg_color_modal_notification").attr("class", "bg-success");
                $("#unsubscribe_btn").hide();
                $("#subscribe_btn").show();
            }
            $("#notification_content").text(result.value);
            $("#modal_with_notification").modal("show");
        }
    })
}

$(document).ready(function (){
    $("#subscribe_btn").click(subscribe);
    $("#unsubscribe_btn").click(unsubscribe);
});