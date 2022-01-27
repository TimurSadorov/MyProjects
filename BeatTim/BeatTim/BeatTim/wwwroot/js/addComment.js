let heightToTop = $(window).height() * 0.25;

function showFormForAddComment(){
    $("#add_comment_line_container").hide(200, "swing");
    $("#comment_adding_form").show(400, "swing");
    $("#comment_input").focus();
}

function hideFormForAddComment(){
    $("#comment_adding_form").hide(200, "swing");
    $("#add_comment_line_container").show(200, "swing");
    $("#comment_input").val("");
}

function scrollToNewComment(){
    $('html, body').animate({
        scrollTop: $("#comments_container").offset().top - heightToTop
    }, {
        duration: 200,
        easing: "swing"
    });
}

function checkCorrectComment() {
    let text = $("#comment_input").val();
    if(text.length === 0) {
        $("#error_message_comment").text("Комментароий не может быть пустым");
        $("#add_comment_btn").prop("disabled", true);
    } else {
        $("#error_message_comment").text("");
        $("#add_comment_btn").prop("disabled", false);
        return true;
    }
    return false;
}

function addComment(){
    $.post({
        url: `/Account/UserAccount/${$("#page_id").attr("page-id")}?handler=AddComment`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        }, 
        data: { commentContent: $("#comment_input").val()},
        success: function (result){
            if(result.hasOwnProperty("isSuccess")){
                $("#bg_color_modal_notification").attr("class", "bg-danger");
                $("#notification_content").text(result.value);
            }
            else {
                currentCommentsAmount++;
                $("#bg_color_modal_notification").attr("class", "bg-success");
                $("#notification_content").text("Комментарий успешно добавлен");
                $("#comments_container").prepend(result);
                hideFormForAddComment();
                scrollToNewComment();
            }
            $("#modal_with_notification").modal("show");
        }
    });
}

$(document).ready(function (){
    $("#add_comment_line").click(showFormForAddComment);
    $("#comment_input").on("input", checkCorrectComment);
    $("#add_comment_btn").click(function (){
        if(checkCorrectComment())
            addComment();
    });
})