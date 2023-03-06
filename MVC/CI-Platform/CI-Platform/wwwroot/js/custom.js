


let totalMissions = document.getElementsByClassName("total__mission");


let searchBar = document.getElementById("search__bar");

let serchBarSm = document.getElementById("search__bar__sm");

let filterRow = document.getElementById("grid__list__buttons__row")

let cardTitle = document.getElementsByClassName("card-title");

let gridAndListButtonsRow = document.getElementById("grid__list__buttons__row");



let notfound = document.getElementById("noMission__page");

serchBarSm.addEventListener("keyup", search_mission);
searchBar.addEventListener("keyup", search_mission);

function search_mission() {
    let count = 0;
    let input = searchBar.value || serchBarSm.value;
    input = input.toLowerCase();


    for (i = 0; i < totalMissions.length; i++) {
        if (!cardTitle[i].innerHTML.toLowerCase().includes(input)) {
            totalMissions[i].classList.add("hide");


        }
        else {
            totalMissions[i].classList.remove("hide");


        }
    }


    for (i = 0; i < totalMissions.length; i++) {
        if (totalMissions[i].classList.contains("hide")) {
            count++;
        }
        console.log(count);




        console.log(count, totalMissions.length)

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