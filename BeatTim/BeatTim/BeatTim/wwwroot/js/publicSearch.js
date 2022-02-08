let $checkbox_beats = $("#checkbox_beats"),
    $checkbox_beatmakers = $("#checkbox_beatmakers"),
    $input = $("#global_search_input"),
    $messageError = $("#message_error"),
    $container =  $("#container_result_search");

function globalSearch(searchValue){
    $.post({
        url: `/Navbar/Search/?handler=SearchBeatsAndBeatmakers&searchValue=${searchValue}&searchBeats=${$checkbox_beats.prop("checked")}&searchBeatmakers=${$checkbox_beatmakers.prop("checked")}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result){
            $container.empty();
            $container.append(result);
        }
    })
}

$(document).ready(function (){
    $(document).keydown(function (e){
        if(e.keyCode === 13)
            $("#global_search_btn").click();
    });
    $("#global_search_btn").click(function (){
        if($input.val().length > 0) {
            $messageError.text("");
            globalSearch($("#global_search_input").val());
        }
        else 
            $messageError.text("Необходимо ввести хотя бы один символ");
    });
})