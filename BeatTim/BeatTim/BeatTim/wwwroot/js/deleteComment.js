function deleteComment($btn, commentId){
    $.post({
        url: `/Account/UserAccount/${$("#page_id").attr("page-id")}?handler=DeleteComment&commentId=${commentId}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result) {
            if(result.isSuccess)
            {
                $("#bg_color_modal_notification").attr("class", "bg-success");
                currentCommentsAmount--;
                $btn.parents(".comment").remove();
            }
            else
                $("#bg_color_modal_notification").attr("class", "bg-warning");
            $("#notification_content").text(result.value);
            $("#modal_with_notification").modal("show");
        }
    });
}