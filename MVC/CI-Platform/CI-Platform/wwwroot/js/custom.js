﻿



let totalMissions = document.getElementsByClassName("total__mission");


let searchBar = document.getElementById("search__bar");

let serchBarSm = document.getElementById("search__bar__sm");

let sortBy = document.getElementsByClassName("sortByInput");




let cardTitle = document.getElementsByClassName("card-title");

let cardSubtitle = document.getElementsByClassName("card-text");


let gridAndListButtonsRow = document.getElementById("grid__list__buttons__row");



let notfound = document.getElementById("noMission__page");

serchBarSm.addEventListener("input", search_mission);
searchBar.addEventListener("input", search_mission);







function search_mission() {
    let count = 0;
    let input = searchBar.value || serchBarSm.value;
    input = input.toLowerCase();



    /*check for the mission title and subtitle to serch */

    for (i = 0; i < totalMissions.length; i++) {
        if (!cardTitle[i].innerHTML.toLowerCase().includes(input) && !cardSubtitle[i].innerHTML.toLowerCase().includes(input) ) {
            totalMissions[i].classList.add("hide");


        }
        else {
            totalMissions[i].classList.remove("hide");


        }
    }


    /*Check the total hidden missions*/

    for (i = 0; i < totalMissions.length; i++) {
        if (totalMissions[i].classList.contains("hide")) {
            count++;
        }
        console.log(count);




      /*  if hidden mission are equalls to number of total mission then show nofound div*/

        if (count == totalMissions.length) {
            notfound.classList.remove('hide')
            gridAndListButtonsRow.classList.add('hide');

        }
        else {
            notfound.classList.add('hide');
            gridAndListButtonsRow.classList.remove('hide');
        }


    }


}


        //taking the sortby value 

for (var i = 0; i < sortBy.length; i++) {
    sortBy[i].addEventListener('click', sortByMethod);
}

let sortByText;
function sortByMethod() {
    sortByText = event.target.value;
    sendInfo();

}

    // taking the city names list from filter option

let cityName = document.getElementsByClassName("cityNames");
let cityPillsContainer = document.getElementById("newCityPills");
var cityNameList = new Set([]);
for (let i = 0; i < cityName.length; i++) {
    cityName[i].addEventListener("click", displayCityInPill)
}

        let countCheck = -1;

function displayCityInPill() {
  
        let count = 0;
        text = event.target.value;
        for (let i = 0; i < cityName.length; i++) {
            if (cityName[i].value == text) {
                countCheck = i;
                break;
            }
        }
    addFilterTag(text);
    sendInfo();
}

function addFilterTag(text) {
    let temp = "";
    if (!cityNameList.has(text))
        cityNameList.add(text);
    
    else {
        cityNameList.delete(text);
        if (countCheck != -1)
            cityName[countCheck].checked = false;
    }





    for (const item of cityNameList) {
        temp = temp + `<h1 class="badge bg-light text-dark mx-1 rounded-pill">  ${item} <button class="not-selected badge-close border-0 rounded-circle" value="${item}" aria-label="Close" onclick="displayCityInPill()">X</button> </h1>`;

    }



    cityPillsContainer.innerHTML = temp;

}


// taking the skill names list from filter option


let skillName = document.getElementsByClassName("skillNames");
let skillPillsContainer = document.getElementById("newSkillPills");
var skillNameList = new Set([]);
for (let i = 0; i < skillName.length; i++) {
    skillName[i].addEventListener("click", displayskillInPill)
}


let countskillCheck = -1;

function displayskillInPill() {

   
    text = event.target.value;
    for (let i = 0; i < skillName.length; i++) {
        if (skillName[i].value == text) {
            countskillCheck = i;
            break;
        }
    }
    addskillFilterTag(text);
    sendInfo();
}

function addskillFilterTag(text) {
    let temp = "";
    if (!skillNameList.has(text))
        skillNameList.add(text);

    else {
        skillNameList.delete(text);
        if (countskillCheck != -1)
            skillName[countskillCheck].checked = false;
    }


    for (const item of skillNameList) {
        temp = temp + `<h1 class="badge bg-light text-dark mx-1 rounded-pill">  ${item} <button class="not-selected badge-close border-0 rounded-circle" value="${item}" aria-label="Close" onclick="displayskillInPill()">X</button> </h1>`;

    }



    skillPillsContainer.innerHTML = temp;

}







// taking the theme names list from filter option


let themeName = document.getElementsByClassName("themeNames");
let themePillsContainer = document.getElementById("newThemePills");
var themeNameList = new Set([]);

for (let i = 0; i < themeName.length; i++) {
    themeName[i].addEventListener("click", displaythemeInPill)
}


let countthemeCheck = -1;

function displaythemeInPill() {


    text = event.target.value;
    for (let i = 0; i < themeName.length; i++) {
        if (themeName[i].value == text) {
            countthemeCheck = i;
            break;
        }
    }
    addthemeFilterTag(text);
    sendInfo();
}

function addthemeFilterTag(text) {
    let temp = "";
    if (!themeNameList.has(text))
        themeNameList.add(text);

    else {
        themeNameList.delete(text);
        if (countthemeCheck != -1)
            themeName[countthemeCheck].checked = false;
    }


    for (const item of themeNameList) {
        temp = temp + `<h1 class="badge bg-light text-dark mx-1 rounded-pill">  ${item} <button class="not-selected badge-close border-0 rounded-circle" value="${item}" aria-label="Close" onclick="displaythemeInPill()">X</button> </h1>`;

    }



    themePillsContainer.innerHTML = temp;

}





function sendInfo() {
    filtercitystr = "";
    filterthemestr = "";
    filterSkillstr = "";
    let sort = sortByText;



    if (sort == null && cityNameList.size == 0 && skillNameList.size == 0 && themeNameList.size == 0) {

        var url = "/Mission/LandingPage/";
        window.location.reload();
    }

    //else if (filterList.size == 0 && filterTheme.size == 0 && filterSkill.size == 0 && sort == null) {
    //    var url = "/Mission/LandingPage?page=" + pageNo;
    //}





    else if (cityNameList.size == 0 && skillNameList.size == 0 && themeNameList.size == 0) {

        var url = "/Mission/LandingPage?sort=" + sort /*+ "&page=" + pageNo*/;
    }

    else if (sort == null) {
        for (const item of cityNameList) {
            filtercitystr += item + ",";
        }
        for (const item of themeNameList) {
            filterthemestr += item + ",";
        }
        for (const item of skillNameList) {
            filterSkillstr += item + ",";
        }
        let obj = { city: filtercitystr, theme: filterthemestr, skill: filterSkillstr }

        var url = "/Mission/LandingPage?filter=" + JSON.stringify(obj) /*+ "&page=" + pageNo*/;
    }
    else {
        for (const item of cityNameList) {
            filtercitystr += item + ",";
        }
        for (const item of themeNameList) {
            filterthemestr += item + ",";
        }
        for (const item of skillNameList) {
            filterSkillstr += item + ",";
        }
        let obj = { city: filtercitystr, theme: filterthemestr, skill: filterSkillstr }

        var url = "/Mission/LandingPage?filter=" + JSON.stringify(obj)/* + "&sort=" + sort + "&page=" + pageNo;*/;
    }




    $.ajax({
        url: url,
        success: function (data) {
            if (data.length == 0) {
                notfound.classList.remove('hide');
            }
            else {
                notfound.classList.add('hide')
            }
            console.log(data);
            var items = "";
            var listitems = "";

            var totalCountedMissions = data.length;
           
            $("#totalCountedMissions").text(totalCountedMissions);

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
                        <p class="card-text text-start">`+ item.value.shortDescription + `</p>
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
                            <span>  From `+ item.value.startDate + ` until ` + item.value.endDate + `</span>
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



                listitems += ` <div class="card mb-3 mt-3 total__mission" style="max-width: 100%;">
                           <div class="row no-gutters">



                                <div class="col-md-4 ">
                                    <div style="position: relative;height: 100%;">
                                        <img class="card-img-top h-100"
                                            src="/images/Grow-Trees-On-the-path-to-environment-sustainability-1.png" alt="Card image cap">
                                        <div style="position: absolute;bottom: 0;width: 100%;">
                                            <p class="m-0 bg-white border rounded-pill text-center w-50"
                                                style="transform: translate(50%, 50%) ;">`+ item.value.theme + `</p>
                                        </div>
                                        <div class="heart__image__container__listView rounded-circle ">
                                            <img src="/images/heart.png" class=" heart__image">
                                        </div>
                                        <div class="rounded-circle add__person__image__container__listView">
                                            <img src="/images/user.png" class="add__person__image">
                                        </div>
                                        <div class="city__container__list rounded-pill">
                                            <img src="/images/pin.png" class="locatin__image">
                                            <span class="text-white">${item.value.name}</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-8  ">
                                    <div class="card-body">
                                        <h3 class="card-title mt-2">${item.value.title}</h3>
                                        <p class="card-text">${item.value.shortDescription}</p>
                                        <div class="row  justify-content-between ">


                                            <div class="col d-flex flex-row align-items-center justify-content-start">
                                                <span class="">${ item.value.organizationName}</span>
                                                <div class="star__container ms-3 ">
                                                    <img src="/images/selected-star.png">
                                                    <img src="/images/selected-star.png">
                                                    <img src="/images/selected-star.png">
                                                    <img src="/images/selected-star.png">
                                                    <img src="/images/star.png">
                                                </div>
                                            </div>

                                            <div class="col">
                                                <hr class="">
                                                <div class="time__text__list w-75 ms-5 text-center border rounded-pill ">
                                                    <span>From ${item.value.startDate}  until ${item.value.endDate}</span>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row justify-content-between  mt-3">
                                            <div class="col mt-2 ml-0 apply__button__list     ">

                                                <a href="#" class="btn rounded-pill   text-center apply__text__list">Apply <img
                                                        src="/images/right-arrow.png" class="ms-2"></a>


                                            </div>
                                            <div class="col">



                                                <div class="row  mb-3">


                                                  <div class="d-flex flex-row gap-1 align-items-center justify-content-center col">
                                                        <img src="/images/Seats-left.png" class="" width="30px" height="30px">
                                                        <div class="d-flex flex-column ">
                                                            <h5 class="mb-0">10</h5>
                                                            <span>Seats left</span>
                                                        </div>
                                                   </div>


                                                  
                                                    <div class="d-flex flex-row gap-3 align-items-center justify-content-center col-auto">
                                                           <img src="/images/deadline.png" width="40px" height="40px">
                                                           <div class="d-flex flex-column">
                                                                 <h5 class="mb-0">${item.value.endDate}</h5>
                                                                 <span>Deadline</span>
                                                           </div>
                                                    </div>
                                                    
                                                  

                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                      </div>`






            });
            var view = `<div class="row row-cols-1 row-cols-lg-2 row-cols-xl-3  g-4" >
                ${items}
            </div>`
      
            $("#grid__view__container").html(view);
            var listview = $('#list__view__container');
            listview.empty();
            listview.html(listitems);
        },
        error: function (err) {
            console.error(err);
        }
    })




}




