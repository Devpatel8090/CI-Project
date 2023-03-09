// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('#country li').on('click', function () {
    var CountryId = $(this).attr('value');
    $.ajax({
        url: '/Mission/GetCityByCountry',
        type: 'GET',
        data: { CountryId: CountryId },
        dataType: 'json',
        success: function (data) {
            var cities = $('#cities');
            cities.empty();
            var items = "";
            console.log("Data is: ", data);
            
            //$.each(data, function (i, item) {

            // items += `<li class="ms-2"><input type="checkbox" class="form-check-input me-3" id="exampleCheck1" value=` + item.cityId + `>
            // <label class="form-check-label" for="exampleCheck1" >` + item.name + `</label></li>`
            // console.log(item.CountryId);

            //});
            $(data).each(function (i, item) {


                items += `<li class=""><input type="checkbox" class="form-check-input me-1" id="exampleCheck1" value=` + item.cityId + `>
<label class="form-check-label" for="exampleCheck1" >` + item.name + `</label></li>`
                console.log(item);

            });
            cities.html(items);
        }
    });

});


$('#SortBy button').on('click', function () {
    var SortValue = $(this).attr('value');
    console.log("value is:", SortValue);
    $.ajax({
        url: '/Mission/DateSort?sort=' + SortValue,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);

            var missions = $('#gridmission__row');

            missions.empty();

            var items = "";
            $(data).each(function (i, item) {

                items += ` <div class="col  mb-3 total__mission" >
                <div class="card" style="max-width: 26rem;">
                    <div style="position: relative;">
                        <img class="card-img-top"
                            src="/images/Grow-Trees-On-the-path-to-environment-sustainability-1.png"
                            alt="Card image cap">
                        <div style="position: absolute;bottom: 0;width: 100%;">
                            <p class="m-0 bg-white border rounded-pill text-center w-50"
                                style="transform: translate(50%, 50%) ;">`+ item.value.theme + `</p>
                        </div>
                        <div class="heart_image_container rounded-circle ">
                            <img src="/images/heart.png" class=" heart__image">
                        </div>
                        <div class="rounded-circle add__person__image__container">
                            <img src="/images/user.png" class="add__person__image">
                        </div>
                        <div class="city__container rounded-pill p-2">
                            <img src="/images/pin.png" class="locatin__image">
                            <span class="text-white ">`+ item.value.name + `</span>
                        </div>
                    </div>
                    <div class="card-body">
                        <h3 class="card-title  text-start mt-2">` + item.value.title + `
                        </h3>
                        <p class="card-text text-start">`+ item.value.shortDescription +`</p>
                        <div class="d-flex justify-content-between mb-1 ">
                            <span>`+ item.value.organizationName + `</span>
                            <div class="star__container">
                                <img src=" /images/selected-star.png">
                                <img src=" /images/selected-star.png">
                                <img src=" /images/selected-star.png">
                                <img src=" /images/selected-star.png">
                                <img src=" /images/star.png">
                            </div>
                        </div>
                        <br />
                        <hr>
                        <div class="time__text border rounded-pill w-75 mx-auto ">
                            <span>  From `+item.value.startDate+` until `+item.value.endDate+`</span>
                        </div>


                        <div class="row mt-3 ms-2 mb-3">
                            <div class="d-flex flex-row gap-3 align-items-center justify-content-center col-auto">
                                <img src=" /images/Seats-left.png" class="" width="30px" height="30px">
                                <div class="d-flex flex-column ">
                                    <h5 class="mb-0">10</h5>
                                    <span>Seats left</span>
                                </div>
                            </div>
                            
                              
                                    <div class="d-flex flex-row gap-3 align-items-center justify-content-center col-auto">
                                        <img src=" /images/deadline.png" width="40px" height="40px">
                                        <div class="d-flex flex-column">
                                            <h5 class="mb-0">`+ item.value.endDate + `</h5>
                                            <span>Deadline</span>
                                        </div>
                                    </div>
                               
                                 


                        </div>
                        <hr>
                        <div class="mt-2 apply__button    text-center apply__button">
                            <a href="#" class="btn rounded-pill   text-center apply__text">Apply <img
                                    src=" /images/right-arrow.png" class="ms-2"></a>

                        </div>
                    </div>
                </div>
            </div>`

            

        });

    missions.html(items);

           






        }
    });
});


//var sortByInput = document.getElementsByClassName("sortByInput");

//for (let i = 0; i < sortByInput.length; i++) {
//    sortByInput[i].addEventListener("click", sortMissions);

//}

//function sortMissions() {


//}





//function handleChange(checkbox) {
//    if (checkbox.checked == true) {

//        let filter = document.getElementById("filteritems");
//        filter.empty();
//        filter.innerHTML = filter.innerHTML + `<div class="rounded border p-1 ps-2 mx-2 custom-badge rounded-pill" id=` + checkbox.name + `>` + checkbox.name + `<img src="/images/cancel.png"  class="p-1" id="` + checkbox.name + `"/></div>`;

//    }
//    else {
//        let badge = document.getElementById(checkbox.name);
//        badge.remove();
//    }



