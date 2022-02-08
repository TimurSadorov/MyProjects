function outputError(errorMessage){
    $("#error_message_cover").text(errorMessage);
    $("#input_cover").val(null);
}

function tryShowImage(e) {
    const file = e.files[0];
    if (!file.type.match('image.(jpeg|jpg|pjpeg|png)')) {
        outputError("Фотография может иметь только следующие форматы: .jpeg, .jpg, .pjpeg, .png");
        return;
    }
    const fr = new FileReader();

    fr.onload = (function(file) {
        const img = document.createElement('img');

        img.onload = function () {
            if (!(this.naturalWidth === this.naturalHeight))
                outputError("Фотография должна иметь соотношение сторон 1:1");
            else if(this.naturalWidth < 300)
                outputError("Размер фотографии по высоте и ширине должен быть не меньше 300 пикселей")
            else {
                $("#preview_cover").attr("src", this.src);
                $("#error_message_cover").text("");
                $("#input_cover_final").prop("files", e.files);
            }
        };
        img.src = file.target.result;
    })

    fr.readAsDataURL(file);
}

function checkOnCorrectAudio(input){
    const file = input.files[0];
    if (!file.type.match('audio.(wav|mpeg)')) {
        $("#error_message_audio").text("Неккоректный формат файла");
        return false;
    }
    else {
        $("#error_message_audio").text("");
        return true;
    }
}

function loadAudioFile(files){
    $("#input_audio_final").prop("files", files);
    $("#unloaded_img").hide();
    $("#loaded_img").show();
}

$(document).ready(function (){
    $("#add_cover_button").on("click", function (){
       $("#input_cover").click(); 
       return false;
    });
    $("#input_cover").on("change", function (){
        tryShowImage(this);
    })
    $("#add_audio_button").on("click", function (){
        $("#input_audio").click();
        return false;
    });
    $("#input_audio").on("change", function (){
        if(checkOnCorrectAudio(this))
            loadAudioFile(this.files)
    })
    $("#form_adding_beat").on("submit", function (){
        if($("#input_audio_final").val() === "")
        {
            $("#error_message_audio").text("Необходимо загрузить бит")
            return false;
        }
        $("#form_adding_beat").submit();
    })
})