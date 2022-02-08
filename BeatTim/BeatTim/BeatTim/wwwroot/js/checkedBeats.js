let currentAmountBeats = 0;
let amountOutputBeats = 2;

function downloadMoreBeats(){
    $.post({
        url: `/Navbar/CheckingBeats/?handler=DownloadCheckedBeats&currentAmountBeats=${currentAmountBeats}&amountOutputBeats=${amountOutputBeats}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (beats) {
            $("#checked_beats_container").append(beats)
            currentAmountBeats += amountOutputBeats;
            if (beats.length < 5)
                $("#more_beats_button").remove();
        }
    });
}

function sendVerificationResult($btn, beatId, isApproved){
    $.post({
        url: `/Navbar/CheckingBeats/?handler=SetVerificationStatus&beatId=${beatId}&isApproved=${isApproved}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result) {
            if(result.isSuccess)
            {
                $("#bg_color_modal_notification").attr("class", "bg-success");
                currentAmountBeats -= 1;
            }
            else
                $("#bg_color_modal_notification").attr("class", "bg-warning");
            $("#notification_content").text(result.value);
            $btn.parents(".beat").remove();
            $("#modal_with_notification").modal("show");
        }
    });
}

$(document).ready(function (){
    downloadMoreBeats();
    $("#more_beats_button").click(downloadMoreBeats);
})