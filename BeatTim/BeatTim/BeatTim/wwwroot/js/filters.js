function sortBeatTitle() {
    let allBeats = $("#container_beats").children().sort(function (firstBeat, secondBeat){
        let firstRating = $(".title_beat", firstBeat).text();
        let secondRating = $(".title_beat", secondBeat).text();
        return firstRating < secondRating ? 1 : firstRating === secondRating ? 0 : -1;
    });
    $("#container_beats").append(allBeats);
}

function sortBeatByNumber(idElementForSort) {
    let allBeats = $("#container_beats").children().sort(function (firstBeat, secondBeat){
        let firstRating = $(`#${idElementForSort}`, firstBeat).attr("data-number");
        let secondRating = $(`#${idElementForSort}`, secondBeat).attr("data-number");
        return secondRating - firstRating;
    });
    $("#container_beats").append(allBeats);
}

function sortBeatByDate() {
    let allBeats = $("#container_beats").children().sort(function (firstBeat, secondBeat){
        let firstRating = new Date($(`.publication_date`, firstBeat).text());
        let secondRating = new Date($(`.publication_date`, secondBeat).text());
        return firstRating < secondRating ? 1 : firstRating === secondRating ? 0 : -1;
    });
    $("#container_beats").append(allBeats);
}

function setActiveInDropdown($newActiveElement){
    $(".dropdown-item").removeClass("active");
    $newActiveElement.addClass("active");
}

$(document).ready(function (){
    $("#sort-title").on("click", function (){
        sortBeatTitle();
        setActiveInDropdown($(this));
    })
    $(".sort-num").on("click", function (){
        sortBeatByNumber($(this).attr("data-sort-class"));
        setActiveInDropdown($(this));
    })
    $("#sort-date").on("click", function (){
        sortBeatByDate();
        setActiveInDropdown($(this));
    })
})