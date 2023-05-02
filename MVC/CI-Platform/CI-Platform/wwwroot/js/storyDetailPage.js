////let slideIndex = 1;
////let item = document.querySelectorAll(".carousel .carousel-item");
////showSlides(slideIndex);

////// Thumbnail image controls
////function currentSlide(n) {
////    showSlides(slideIndex = n);
////}

////function showSlides(n) {
////    let slides = document.getElementsByClassName("mySlides");
////    for (i = 0; i < slides.length; i++) {
////        slides[i].classList.replace("d-sm-block", "d-none");
////    }
////    slides[slideIndex - 1].classList.add("d-sm-block");
////}



////item.forEach((el) => {
////    const minPerSlide = 4;
////    let next = el.nextElementSibling;
////    for (var i = 1; i < minPerSlide; i++) {
////        if (!next) {
////            // wrap carousel by using first child
////            next = item[0];
////        }
////        let cloneChild = next.cloneNode(true);
////        el.appendChild(cloneChild.children[0]);
////        next = next.nextElementSibling;
////    }
////});


/* Recommendation to Co-worker */

function recomendtoyourfriend() {

    var userDetails = event.target.getAttribute("value");

    var userarray = userDetails.split(" ");
    let inviteObj = {
        storyId: userarray[0],
        userId: userarray[1],
        toUserId: userarray[2],
        toUserMail: userarray[3]
    };
    console.log(inviteObj);
    var url = "/Story/Recommendation?inviteObj=" + JSON.stringify(inviteObj);

    $.ajax({
        url: url,
        success: function (data) {
            toastr.success("Email Sent Successfully");
            window.location.reload();
        },
        error: function (error) {
            console.log(error);
        }
    });

}