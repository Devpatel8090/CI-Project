

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


/* Recommendation to Co-worker */


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


    /*Rating the mission */


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



/*comment section */


/*var commentText = document.getElementById("WriteCommentText").value;*/


let commentButton = document.getElementById("postCommentButton");

commentButton.addEventListener("click", postcommentfunction);
/*$('#postCommentButton').on("click",*/ function postcommentfunction () {
    var commentText = document.getElementById("WriteCommentText").value;
    var input = event.target.getAttribute("value");
    let commentArray = input.split(" ");
 
    let commentObj = {
        missionId: commentArray[0],
        userId: commentArray[1],
        commenttext: commentText
    };
    var url = "/Mission/postComment?commentObj=" + JSON.stringify(commentObj);

    $.ajax({
        url: url,
        success: function (data) {
            $('#commentbyuser').html(data);
            $('#WriteCommentText').val('');
        },
        error: function (error) {
            console.log(error);
        }
    });
};