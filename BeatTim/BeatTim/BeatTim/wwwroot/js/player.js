const audioPlayer = $("#audio_player"),
    imgPlayer = $("#img_player"),
    titlePlayer = $("#title_player"), 
    playButton= $("#play_svg"),
    pauseButton= $("#pause_svg"),
    progressbarPlayer = $("#progressbar_player"),
    timeScrolling = $("#time_scrolling");

let currentBeatId = 0,
    currentTime = 0,
    totalTimeListeningTrack = 0,
    timeAddListening = 100;

function playBeat(){
    audioPlayer[0].play();
    playButton.hide();
    pauseButton.show();
}

function pauseBeat(){
    audioPlayer[0].pause();
    pauseButton.hide();
    playButton.show();
}

function showPlayer(){
    $("#opened_player").show("fast", "swing");
}

function loadBeat(beat){
    imgPlayer.attr("src", beat.find(".beat_cover").attr("src"));
    titlePlayer.text(beat.find(".title_beat").text());
    let newAudio = beat.find("audio");
    audioPlayer.attr("src", newAudio.attr("src"));
    currentBeatId = beat.attr("data-beat-id");
    currentTime = 0;
    totalTimeListeningTrack = 0;
    timeAddListening = newAudio[0].duration * 0.1;
}

function isThisBeatPlayed(audio){
    return audioPlayer.attr("src") === audio.attr("src");
}

function updateProgressbar(audio){
    let completionPercentage = audio.currentTime * 100 / audio.duration
    progressbarPlayer.css("width", `${completionPercentage}%`);
}

function setProgressbar(e){
    let allWidth = timeScrolling.innerWidth();
    let newCompletionPercentage = e.offsetX / allWidth;
    audioPlayer[0].currentTime = audioPlayer[0].duration * newCompletionPercentage;
}

function playNewBeat($playBtn){
    let currentBeat = $playBtn.parents(".beat").eq(0);
    let currentAudio = currentBeat.find("audio");
    if (!isThisBeatPlayed(currentAudio)) {
        loadBeat(currentBeat);
        showPlayer();
        playBeat(currentAudio)
    } else if(audioPlayer[0].paused)
        playBeat();
    else
        pauseBeat();
}

function addAuditions(){
    $.post({
        url: `/Beats/Listening/?handler=AddAuditions&beatId=${currentBeatId}`,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        }
    });
}

function addListeningTime(){
    let listenTime = audioPlayer[0].currentTime - currentTime;
    if(listenTime > 0 && listenTime < 0.5)
        totalTimeListeningTrack += listenTime;
    if(totalTimeListeningTrack >= timeAddListening)
    {
        addAuditions();
        totalTimeListeningTrack = 0;
    }
    currentTime = audioPlayer[0].currentTime;
}

$(document).ready(function (){
    $(".play_button").on("click", function (){
        playNewBeat($(this));
    });
    playButton.on("click", playBeat);
    pauseButton.on("click", pauseBeat);
    audioPlayer.on("timeupdate", function() {
        updateProgressbar(this);
        addListeningTime();
    });
    timeScrolling.on("click", setProgressbar);
})