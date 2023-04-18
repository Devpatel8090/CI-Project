



$('#AddSkillButton').on('click', function () {
    var skillName = $('#skillName').val();


    url = "/Admin/AddSkill"
    $.ajax({
        url: url,
        data: {
            skillName: skillName
        },
        type: "POST",
        success: function (data) {
            console.log(data);
            location.reload();
            
        },
        error: function (error) {
            console.log(error);
        }

    });
});

$('#AddMissionThemeButton').on('click', function () {
    var MissionThemeName = $('#MissionThemeName').val();


    url = "/Admin/AddMissionThemeName"
    $.ajax({
        url: url,
        data: {
            MissionThemeName: MissionThemeName
        },
        type: "POST",
        success: function (data) {
            console.log(data);
            location.reload();

        },
        error: function (error) {
            console.log(error);
        }

    });
});

$('#AddCMSPageButton').on('click', function () {
    var url = "/Admin/AddCMSPage";

    $.ajax({
        url: url,
        success: function (data) {
            console.log(data);
            $('#CMSPageRightPart').html(data);
           
            

        },
        error: function (error) {
            console.log(error);
        }

    });
    
})

let cmspages = document.getElementsByClassName("cmsPage");

for (let i = 0; i < cmspages.length; i++) {
    cmspages[i].addEventListener('click', CMSPageEditFunction);
}

function CMSPageEditFunction() {
    var data = event.target.getAttribute("value");
    var url = "/Admin/EditCMSPage?cmspageID="+data;

    $.ajax({
        url: url,
        success: function (data) {
            console.log(data);
            $('#CMSPageRightPart').html(data);


        },
        error: function (error) {
            console.log(error);
        }

    });

}


$('#AddUserButton').on('click', function () {

    $.ajax({
        url: "/Admin/AddUser",
        success: function (data) {
            console.log(data);
            $('#UserTabAdd').html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
});

$('.EditUserButton').on('click', function (event) {
    // To Stop Bubbling and prevent to create the multiple click & stop the clicks to other parents and children elements
    event.stopPropagation();
    event.stopImmediatePropagation();
    var userid = event.target.getAttribute("value");
    var username = userid;
    $.ajax({
        url: "/Admin/EditUser?UserId=" + userid,
        type: "GET",
        success: function (data) {
            console.log(data);
            $('#UserTabAdd').html(data);
        },
        error: function (error) {
            console.log(error);
        }

    })
});


$('#AddMissionButton').on('click', function () {
    $.ajax({
        url: "/Admin/AddMission",
        success: function (data) {
            console.log(data);
            $('#MissionTabAdd').html(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
});

$('.missionEdit').on('click', function (event) {
    // To Stop Bubbling and prevent to create the multiple click & stop the clicks to other parents and children elements
    event.stopPropagation();
    event.stopImmediatePropagation();
    var missionId = event.target.getAttribute("value");

    $.ajax({
        url: "/Admin/EditMission?MissionId=" + missionId,
        type: "GET",
        success: function (data) {
            console.log(data);
            $('#MissionTabAdd').html(data);
        },
        error: function (error) {
            console.log(error);
        }

    })
})