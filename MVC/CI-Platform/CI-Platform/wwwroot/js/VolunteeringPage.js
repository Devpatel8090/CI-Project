let addToFavourite = document.getElementsByClassName("addToFavdiv");




//for (var i = 0; i < addToFavourite.length; i++) {
//    addToFavourite[i].addEventListener("click", addToFavouritefun);
//}


function addToFavouritefun() {
    var text = event.target.getAttribute("value");
    var favarray = text.split(" ");
    let obj = {
        missionId: favarray[0],
        userId: favarray[1]
    };
    var url = "/Mission/AddToFavourite?favObj=" + JSON.stringify(obj);

    $.ajax({
        url: url,
        success: function (data) {
            window.location.reload();
        },
        error: function (error) {
            console.log(error);
        }
    });



}
