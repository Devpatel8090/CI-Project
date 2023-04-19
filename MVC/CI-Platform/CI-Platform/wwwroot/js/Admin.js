
let uploadBtn = document.getElementById("UploadFiles");
let chosenImage = document.getElementById("ImageChooseFile");
var files = [];
let missionfiles = [];
let documentfiles = [];

function chooseImageBanner() {
    $('#UploadFiles').click();
}


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

    });
});

$('#AddBannerManagementPageButton').on('click', function () {
    $.ajax({
        url: "/Admin/AddBanner",
        success: function (data) {
            console.log(data);
            $('#BannerManagementPageAdd').html(data);
            uploadBtn = document.getElementById("UploadFiles");
            chosenImage = document.getElementById("ImageChooseFile");

            uploadBtn.onchange = () => {
                let reader = new FileReader();
                reader.readAsDataURL(uploadBtn.files[0]);
                files.push(uploadBtn.files[0]);
                chosenImage.setAttribute("src", URL.createObjectURL(uploadBtn.files[0]));
                console.log(files);
            }
        },
        error: function (error) {
            console.log(error);
        }

    });
});

$('.bannerEditButton').on('click', function (event) {
    // To Stop Bubbling and prevent to create the multiple click & stop the clicks to other parents and children elements
    event.stopPropagation();
    event.stopImmediatePropagation();
    var bannerId = event.target.getAttribute("value");

    $.ajax({
        url: "/Admin/EditBanner?BannerId=" + bannerId,
        success: function (data) {
            console.log(data);
            $('#BannerManagementPageAdd').html(data);
            var image = $('#ShowImageInEdit').val();
            uploadBtn = document.getElementById("UploadFiles");
            chosenImage = document.getElementById("ImageChooseFile");

            uploadBtn.onchange = () => {
                let reader = new FileReader();
                reader.readAsDataURL(uploadBtn.files[0]);
                files.push(uploadBtn.files[0]);
                chosenImage.setAttribute("src", URL.createObjectURL(uploadBtn.files[0]));
                console.log(files);
            }
            $('#ImageChooseFile').attr('src', image);
        },
        error: function (error) {
            console.log(error);
        }
    })
})



function displayImages() {
    let file = event.target.files;

    // if user select no image
    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[0] != "image") continue;
        if (!missionfiles.some((e) => e.name == file[i].name))
            missionfiles.push(file[i]);
    }

    showImages();
    showMissionImgcount();
};


/* SHOW IMAGES */
function showImages() {
    let container = document.getElementById("image-container");
    container.innerHTML = missionfiles.reduce((prev, curr, index) => {
        return `${prev}
                <div class="image">
                <span onclick="delImage(${index})">&times;</span>
                <img src="${URL.createObjectURL(curr)}" />
                </div>`;
    }, "");
    for (let i = 0; i < missionfiles.length; i++) {
        console.log(missionfiles[i].name);
    }
}

/* DELETE IMAGE */
function delImage(index) {
    missionfiles.splice(index, 1);
    showImages();
    showMissionImgcount();
}

function showMissionImgcount() {
    let img = document.getElementById('img');
    const dt = new DataTransfer();
    for (let i = 0; i < missionfiles.length; i++) {
        dt.items.add(missionfiles[i]);
    }

    img.files = dt.files;
}


function displayDocuments() {
    let file = event.target.files;

    // if user select no image
    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[0] != "doc" || file[i].type.split("/")[0] != "docx") continue;
        if (!documentfiles.some((e) => e.name == file[i].name))
            documentfiles.push(file[i]);
    }

    showDocuments();
    showMissionDocumentcount();
};
/* SHOW documents */
function showDocuments() {
    let containerDoc = document.getElementById("documents-container");
    containerDoc.innerHTML = documentfiles.reduce((prev, curr, index) => {
        return `${prev}
                <div class="image">
                <span onclick="delImage(${index})">&times;</span>
                <img src="${URL.createObjectURL(curr)}" />
                </div>`;
    }, "");
    for (let i = 0; i < documentfiles.length; i++) {
        console.log(documentfiles[i].name);
    }
}

function showMissionDocumentcount() {
    let doc = document.getElementById('document');
    const dt = new DataTransfer();
    for (let i = 0; i < documentfiles.length; i++) {
        dt.items.add(documentfiles[i]);
    }

    doc.files = dt.files;
}

/* DELETE Documents */
function delDocument(index) {
    missionfiles.splice(index, 1);
    showDocuments();
    showMissionDocumentcount();
}
