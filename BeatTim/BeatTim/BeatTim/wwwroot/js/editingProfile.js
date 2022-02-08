function outputError(errorMessage){
    $("#error-message-content").text(errorMessage);
    $('#error-message-user-photo').modal('show');
    $("#file-input").val(null);
}

function tryShowImage(e) {
    const file = e.target.files[0];
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
                $("#user-photo").attr("src", this.src);
                $("#input-img").modal('hide');
                let t = document.querySelector("#new-img-user");
                t.files = e.target.files;
            }
        };
        img.src = file.target.result;
    })

    fr.readAsDataURL(file);
}

function changeText(idContent, idInput){
    $(`#${idContent}`).text($(`#${idInput}`).val());
}

function changeHref(idLink, idInput){
    $(`#${idLink}`).attr("href", $(`#${idInput}`).val());
}

$(document).ready(function (){
    $("#file-input").on("change", tryShowImage);
    $("#btn-download-user-photo").on("click",function (){
       $("#file-input").click(); 
    });
    $("#nickname-input").on("input", function (){
        changeText("nickname-content", this.id);
    });
    $("#city-input").on("input", function (){
        changeText("city-content", this.id);
    });
    $("#about-me-input").on("input", function (){
        changeText("about-me-content", this.id);
    });
    $("#vk-input").on("change", function (){
        changeHref("vk-content", this.id);
    });
    $("#tg-input").on("change", function (){
        changeHref("tg-content", this.id);
    });
    $("#inst-input").on("change", function (){
        changeHref("inst-content", this.id);
    });
    $("#twitter-input").on("change", function (){
        changeHref("twitter-content", this.id);
    });
    $("#youtube-input").on("change", function (){
        changeHref("youtube-content", this.id);
    });
    $("#fb-input").on("change", function (){
        changeHref("fb-content", this.id);
    });
})