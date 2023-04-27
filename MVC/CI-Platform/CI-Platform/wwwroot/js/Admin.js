
let uploadBtn = document.getElementById("UploadFiles");
let chosenImage = document.getElementById("ImageChooseFile");
var files = [];
let imagecount = 0;
let missionfiles = [];
let documentfiles = [];

function chooseImageBanner() {
    $('#UploadFiles').click();
    imagecount = 1;
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
            $("#particularMissiontype").css('pointer-events', 'none');
            $("#particularMissiontype").css('background-color', '#e9ecef');
            GoalToTime();
            var oldImgs = document.getElementsByClassName('oldImgs');
            for (let i = 0; i < oldImgs.length; i++) {
                img = oldImgs[i];
                let path = img.src.substr(img.src.lastIndexOf("/") + 1)
                fetch(img.src)
                    .then(res => res.blob())
                    .then(blob => {
                        const file = new File([blob], path, blob)
                        console.log(file)
                        missionfiles.push(file)
                        showImages();
                        showMissionImgcount();
                    })

            }
            var oldDocument = document.getElementsByClassName('oldDocs');
            for (let i = 0; i < oldDocument.length; i++) {
                doc = oldDocument[i];
                let path = doc.getAttribute('href').substr(doc.getAttribute('href').lastIndexOf("/") + 1)
                fetch(doc.getAttribute('href'))
                    .then(res => res.blob())
                    .then(blob => {
                        const file = new File([blob], path, blob)
                        console.log(file)
                        documentfiles.push(file)
                        showDocuments();
                        showMissionDocumentcount();
                    })

            }
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
            imagecount = 1;
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
                <img width="100" height="100" src="${URL.createObjectURL(curr)}" />
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

    // if user select no Document
    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[1] == "docx" || file[i].type.split("/")[1] == "pdf" || file[i].type.split("/")[1] == "xlsx" || file[i].type.split("/")[0] == "application" || file[i].type.split("/")[0] == "text") {
            if (!documentfiles.some((e) => e.name == file[i].name))
                documentfiles.push(file[i]);
        }
        else {
            continue;
        }
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
                 <a target = "_blank" href = "${URL.createObjectURL(curr)}" class="docpill mx-1 oldDocs" > ${curr.name}</a >
                <button class="mx-2 pillbtn" type="button" onclick="delDocument(${index})">X</button>
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
    documentfiles.splice(index, 1);
    showDocuments();
    showMissionDocumentcount();
}

$("#CountryId").on('change', function () {
    let countryId = $("#CountryId").find(":selected").val();
    $.ajax({
        url: "/Mission/GetCityByCountry?CountryId=" + countryId,
        type: "GET",
        success: function (data) {
            console.log(data);
            var items = "";
            for (let i = 0; i < data.length; i++) {
                items += `<option value="${data[i].cityId}" selected>${data[i].name}</option>`
            }
            $('#CityId').html(items);
        },
        error: function (error) {
            console.log(error);
        }
    })
})



function isValidUserAdmin() {
    flag = 1;
    var FirstName = $('#FirstName').val();
    var LastName = $('#LastName').val();
    var Email = $('#Email').val();
    var PhoneNumber = $('#PhoneNumber').val();
    var EmployeeId = $('#EmployeeId').val();
    var Department = $('#DepartmentName').val();
    var Country = $('#CountryId').find(":selected").val();
    var City = $('#city').val();
    var EmailRegex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var PhoneRegex = /^[0-9]{10}$/;

  
    if (FirstName == "") {
        $('#FirstNameValidation').text("FirstName is Required");
        flag = 0;
    }
    else {
        $('#FirstNameValidation').text("");
    }
    if (LastName == "") {
        $('#LastNameValidation').text("LastName is Required");
        flag = 0;
    }
    if (Email == "") {
        $('#EmailValidation').text("Email is Required");
        flag = 0;
    }
    if (PhoneNumber == "") {
        $('#PhoneNumberValidation').text("PhoneNumber is Required");
        flag = 0;
    }
    if (EmployeeId == "") {
        $('#EmployeeIdValidation').text("EmployeedId is Required");
        flag = 0;
    }
    if (Department == "") {
        $('#DepartmentNameValidation').text("Department is Required");
        flag = 0;
    }
    if (Country == -1) {

        $('#CountryIdValidation').text("Select The Country Please");
        flag = 0;
    }
    if (City == -1) {

        $('#CityIdValidation').text("Select the City Please");
        flag = 0;
    }

    if (FirstName.length <= 2 || FirstName.length >= 25) {
        $('#FirstNameValidation').text("Length of First Name must between 2 to 25 characters");
       
        flag = 0;
    }

    if (LastName.length <= 2 || LastName.length >= 25) {
        $('#LastNameValidation').text("Length of First Name must between 2 to 25 characters");
       
        flag = 0;
    }

    if (!Email.match(EmailRegex)) {
        $('#EmailValidation').text("Please Enter Correct Email Id ");
     
        flag = 0;
    }

    if (!PhoneNumber.match(PhoneRegex)) {
        
        $('#PhoneNumberValidation').text("Phone number must be of 10 digits.");
        flag = 0;
    }

    if (flag == 0) {
        return false;
    }




}

function removeUserValidation() {
     
        var data = event.target.getAttribute("id");
        $('#' + data + "Validation").html('');
    
}

function isValidCMSPageAdmin() {
    flag = 1;
    var CMSTitle = $('#CMSTitle').val();
    var CMSDescription = $('#CMSText').val();
    var CMSSlug = $('#CMSSlug').val();

    if (CMSTitle == "") {
        $('#CMSTitleValidation').text("Title is required");
        flag = 0;
    }
    if (CMSDescription == "") {
        $('#CMSTextValidation').text("Description is required");
        flag = 0;
    }
    if (CMSSlug == "") {
        $('#CMSSlugValidation').text("slug is required");
        flag = 0;
    }

    if (flag == 0) {
        return false;
    }
}

function RemoveCmsPageValidation() {
    var input = event.target.getAttribute("id");
    $('#' + input + "Validation").html('');

}

function isValidBannerAdmin() {
    flag = 1;
    var BannerTitle = $('#BannerText').val();
    var BannerSortOrder = $('#BannerSortOrder').val();
    

    if (BannerTitle == "") {
        $('#BannerTextValidation').text("Title is required so kindly enter it");
        flag = 0;
    }
    if (BannerSortOrder == "") {
        $('#BannerSortOrderValidation').text("Please Enter SortValue and Accept only Number");
        flag = 0;
    }
    if (imagecount == 0) {
        $('#BannerImagesValidaion').text("Please Add Image");
        flag = 0;
    }
    if (flag == 0) {
        return false;
    }
}

function RemoveValidationBannerAdmin() {
    var input = event.target.getAttribute("id");
    $('#' + input + "Validation").html('');
}



function isValidMissionAdmin() {

 var regex = /^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube(-nocookie)?\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$/gm;
 var flag = 0;
var data = $('#youtubeurl').val();
    if (data != "")
    {
        if (data.includes(" ")) {
            var arr = data.split(" ");
        }
        else if (data.includes(",")) {
            var arr = data.split(",")
        }
        else {
            var arr = data.split("\n");
        }
        if (arr.length > 20) {
            toastr.error('maximum 20 urls are allowed')
            return false;
        }
        for (var url of arr) {

            if (url.match(regex) == null) {
                toastr.error('Only Youtube URLs are allowed')
                flag = 1;
            }
        }

    }
    var missionTitle = $('#particularMissiontitle').val();
    if (missionTitle == "") {
        $('#particularMissiontitlespan').text("Title is required");
        flag = 1;
    }
    

    if (flag == 1) {
        return false;
    }
}


$('.ViewStoryButton').on('click', function () {
    var storyId = $(this).val();

    $.ajax({
        url: "/Admin/ViewStoryAdmin?StoryId=" + storyId,
        success: function (data) {
            console.log(data);
            $('#StoryRightSide').html(data);
        },
        error: function (error) {
            console.log(error);
        }
    });

});

