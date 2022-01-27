function search(value, $container, nameClassWithContent){
    let beats = $container.children();
    beats.each(function (){
        let element = $(this);
        if (element.find(`.${nameClassWithContent}`).text().toLowerCase().indexOf(value) !== 0)
            $(this).prop("hidden", "true");
        else
            $(this).prop("hidden", "");
    });
}