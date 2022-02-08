let currentAmountBeats = 0;
let amountOutputBeats = 2;

function downloadMoreBeats(){
    $.post({
        url: `/Navbar/ChartBeats/?handler=DownloadMoreBeats&currentAmountBeats=${currentAmountBeats}&amountOutputBeats=${amountOutputBeats}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (beats) {
            $("#container_beats").append(beats)
            currentAmountBeats += amountOutputBeats;
            if (beats.length < 5) 
                $("#more_beats_button").remove();
        }
    });
}

$(document).ready(function (){
    downloadMoreBeats();
    $("#more_beats_button").click(downloadMoreBeats);
})