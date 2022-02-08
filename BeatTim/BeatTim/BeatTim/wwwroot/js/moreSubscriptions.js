let currentAmountSubscriptions = 0;
let amountOutputSubscriptions = 4;

function downloadMoreSubscriptions(){
    $.post({
        url: `/Navbar/Subscriptions/?handler=GetMoreSubscriptions&currentAmountSubscriptions=${currentAmountSubscriptions}&amountOutputSubscriptions=${amountOutputSubscriptions}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (beats) {
            $("#container_subscriptions").append(beats);
            search($("#search_subscriptions").val().toLowerCase(), $('#container_subscriptions'), 'username')
            currentAmountSubscriptions += amountOutputSubscriptions;
            if (beats.length < 5)
                $("#more_subscriptions_button").remove();
        }
    });
}

$(document).ready(function (){
    downloadMoreSubscriptions();
    $("#more_subscriptions_button").click(downloadMoreSubscriptions);
})