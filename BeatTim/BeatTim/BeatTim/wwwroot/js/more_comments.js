const commentOutput = 3;
var currentCommentsAmount = 0;
let isMaxComments = false;

function sendReport(commentId){
    $.post({
        url: `/Account/UserAccount/${$("#page_id").attr("page-id")}?handler=ReportComment&commentId=${commentId}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result){
            if(result.isSuccess)
                $("#bg_color_modal_notification").attr("class", "bg-success");
            else 
                $("#bg_color_modal_notification").attr("class", "bg-danger");
            $("#notification_content").text(result.value);
            $("#modal_with_notification").modal("show");
        }
    })
}

function downloadNewComments(){
    $.ajax({
        type: "POST",
        url: `/Account/UserAccount/${$("#page_id").attr("page-id")}?handler=MoreComments&currentCommentsAmount=${currentCommentsAmount}&commentOutput=${commentOutput}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (comments) {
            $("#comments_container").append(comments)
            currentCommentsAmount += commentOutput;
            if (comments.length < 5) {
                isMaxComments = true;
                $("#more_comments_button").hide();
            }
        }
    });
}

$(document).ready(function (){
    downloadNewComments();
    $("#more_comments_button").click(function () {
        if (!isMaxComments) 
            downloadNewComments();
    });
})