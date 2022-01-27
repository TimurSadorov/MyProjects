let currentCommentsBeats = 0;
let amountOutputComments = 10;

function downloadMoreComments(){
    $.post({
        url: `/Navbar/CheckingComments/?handler=DownloadCheckedComments&currentAmountComments=${currentCommentsBeats}&amountOutputComments=${amountOutputComments}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (comments) {
            $("#checked_comments_container").append(comments)
            currentCommentsBeats += amountOutputComments;
            if (comments.length < 5)
                $("#more_comments_button").remove();
        }
    });
}

$(document).ready(function (){
    downloadMoreComments();
    $("#more_comments_button").click(downloadMoreComments);
})