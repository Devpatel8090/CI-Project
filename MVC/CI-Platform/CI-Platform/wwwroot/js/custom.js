


let totalMissions = document.getElementsByClassName("total__mission");


let searchBar = document.getElementById("search__bar");

let serchBarSm = document.getElementById("search__bar__sm");

let sortBy = document.getElementsByClassName("sortByInput");

let PaginationFooter = document.getElementById("Pagination");




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
            PaginationFooter.classList.add('hide');

        }
        else {
            notfound.classList.add('hide');
            gridAndListButtonsRow.classList.remove('hide');
            PaginationFooter.classList.remove('hide');
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

    
    
    //$("#countryDropdown").text();

    //$("#countryDropdown").append($(`<svg xmlns="http://www.w3.org/2000/svg" class="ms-lg-4" width="16" height="16" fill="currentColor"
    //    class= "bi bi-chevron-down" viewBox = "0 0 16 16" >
    //    <path fill-rule="evenodd"
    //        d="M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z" />
    //                        </svg >`)
    //);




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
    



    if (sort == null && cityNameList.size == 0 && skillNameList.size == 0 && themeNameList.size == 0 && pageNo == 0) {

        var url = "/Mission/LandingPage/";
        window.location.reload();
    }

    else if (cityNameList.size == 0 && themeNameList.size == 0 && skillNameList.size == 0 && sort == null) {
        var url = "/Mission/LandingPage?page=" + pageNo;
    }





    else if (cityNameList.size == 0 && skillNameList.size == 0 && themeNameList.size == 0) {

        var url = "/Mission/LandingPage?sort=" + sort + "&page=" + pageNo;
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

        var url = "/Mission/LandingPage?filter=" + JSON.stringify(obj) + "&page=" + pageNo;
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

        var url = "/Mission/LandingPage?filter=" + JSON.stringify(obj) + "&sort=" + sort + "&page=" + pageNo;;
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

            
            
            $('#GridCardContainer').html(data);
        },
        error: function (err) {
            console.error(err);
        }
    })




}

//pagination function
var pageNo = 1;
function AddPagination() {
    pageNo = event.target.innerHTML;
    pageNo = Number.parseInt(pageNo);
    sendInfo();
}


//nextpointer

function NextPointer() {
    pageNo = Number.parseInt(pageNo);
    pageNo = pageNo + 1;
    sendInfo();
}

//prevpointer

function PrevPointer() {
    pageNo = Number.parseInt(pageNo);
    pageNo = pageNo - 1;
    sendInfo();
}




