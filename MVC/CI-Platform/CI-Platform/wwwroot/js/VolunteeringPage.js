

let addToFavourite = document.getElementsByClassName("addToFavdiv");



for (var i = 0; i < addToFavourite.length; i++) {
    addToFavourite[i].addEventListener("click", addToFavouritefun);
}


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

//var recommedationButton = document.getElementsByClassName("RecommendationButton");

//for (var i = 0; i < recommedationButton.length; i++) {
//    recommedationButton[i].addEventListener('click', RecommendationToCoWorker);
//}

//function RecommendationToCoWorker() {
//    var 

//}
/*$('#RecommendationDiv button').on("click",*/
function recomendtoyourfriend() {

    var userDetails = event.target.getAttribute("value");
   
    var userarray = userDetails.split(" ");
    let inviteObj = {
        missionId: userarray[0],
        userId: userarray[1],
        toUserId: userarray[2],
        toUserMail: userarray[3]
    };
    console.log(inviteObj);
    var url = "/Mission/Recommendation?inviteObj=" + JSON.stringify(inviteObj);

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

let RatingStars = document.getElementsByClassName("ratingStar");

for (var i = 0; i < RatingStars.length; i++) {
    RatingStars[i].addEventListener('click', StarRatingFunction)
}

function StarRatingFunction() {
    let input = event.target.getAttribute("value");
    let userRatingArray = input.split(" ");
    let userRatingObj = {
        rating: userRatingArray[0],
        missionId: userRatingArray[1],
        userId: userRatingArray[2]
    };
    var url = "/Mission/userStarRating?userRatingObj=" + JSON.stringify(userRatingObj);

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
