function setVerificationStatusComments($btn, commentId, isApproved){
    $.post({
        url: `/Navbar/CheckingComments/?handler=SetVerificationStatus&commentId=${commentId}&isApproved=${isApproved}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result) {
            if(result.isSuccess)
            {
                $("#bg_color_modal_notification").attr("class", "bg-success");
                currentCommentsBeats -= 1;
            }
            else
                $("#bg_color_modal_notification").attr("class", "bg-warning");
            $("#notification_content").text(result.value);
            $btn.parents(".comment").remove();
            $("#modal_with_notification").modal("show");
        }
    });
}