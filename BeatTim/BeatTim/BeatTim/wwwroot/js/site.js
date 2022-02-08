$(document).ready(function (){
    $("#registration_checkbox").click(function (){
        if ($(this).val() === "true")
            $(this).val("false");
        else
            $(this).val("true");
    })
    $(".hover_dropdown").mouseenter(function (){
        $(this).children().addClass("show");
    })
    $(".hover_dropdown").mouseleave(function (){
        $(this).children().removeClass("show");
    })
})