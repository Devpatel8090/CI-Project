
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

let commentButton = document.getElementById("postCommentButton");

commentButton.addEventListener("click", postcommentfunction);

function postcommentfunction () {
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

$('#ApplyTomission').on('click', function () {
    var input = $(this).attr('value');
    var applyArray = input.split(" ");

    var applyObj = {
        missionId: applyArray[0],
        userId: applyArray[1]
    };
    var url = "/Mission/ApplyToMission?applyObj=" + JSON.stringify(applyObj);

    $.ajax({
        url: url,
        success: function (data) {
            var temp = "";
            var applyToMissionText = $('#applyToMissionText');
            temp +=` <a class="btn rounded-pill text-center border-1 border-success text-success btn_no py-3 px-5   apply__text__list"> Already Applied 
            </a>`;
            applyToMissionText.html(temp);
        },
        error: function (error) {
            console.log(error);
        }
    });
});

let recent_vol = document.getElementsByClassName("recent-vol");
let prev_vol = document.getElementById("prev-vol");
let next_vol = document.getElementById("next-vol");
let page = 1;
let pageSize = 9;
let maxpages = Math.ceil(recent_vol.length / 9);
let recentvolpagenumber = document.getElementById("recentvolpagenumber");



recentpagination();

prev_vol.addEventListener("click", () => {
    if (page > 1) {
        page = page - 1;
    }
    recentpagination();
});

next_vol.addEventListener("click", () => {
    if (page != maxpages) {
        page = page + 1;
    }
    recentpagination();
});

function recentpagination() {
    for (i = 0; i < recent_vol.length; i++) {
        if (i < (page * pageSize) && i > (((page - 1) *pageSize) - 1)) {
            recent_vol[i].classList.remove("d-none");
        }
        else {
            recent_vol[i].classList.add("d-none");
        }
    }
    recentvolpagenumber.innerHTML = `<a class="page-link"   style="color:black">${((page - 1)*  9) + 1
} - ${ (page) * 9 < recent_vol.length ? (page) * 9 : recent_vol.length} of ${ recent_vol.length } Recent Volunteers</a > `
}