

let files = [],
    dragArea = document.querySelector(".drag-area"),
    input = document.querySelector(".drag-area input"),
    button = document.querySelector(".drag-card button"),
    select = document.querySelector(".drag-area .select"),
    container = document.querySelector(".container-img");

var oldimages = document.getElementById("oldImages");

/* CLICK LISTENER */
select.addEventListener("click", () => input.click());

/* INPUT CHANGE EVENT */
input.addEventListener("change", () => {
    let file = input.files;

    // if user select no image
    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[0] != "image") continue;
        if (!files.some((e) => e.name == file[i].name)) files.push(file[i]);
    }

    showImages();
});

/* SHOW IMAGES */
function showImages() {
    container.innerHTML = files.reduce((prev, curr, index) => {
        return `${prev}
                <div class="image">
                <span onclick="delImage(${index})">&times;</span>
                <img src="${URL.createObjectURL(curr)}" />
                </div>`;
                    }, "");
    for (let i = 0; i < files.length; i++) {
        console.log(files[i].name);
    }
}

/* DELETE IMAGE */
function delImage(index) {
    files.splice(index, 1);
    showImages();
}

/* DRAG & DROP */
dragArea.addEventListener("dragover", (e) => {
    e.preventDefault();
    dragArea.classList.add("dragover");
});

/* DRAG LEAVE */
dragArea.addEventListener("dragleave", (e) => {
    e.preventDefault();
    dragArea.classList.remove("dragover");
});

/* DROP EVENT */
dragArea.addEventListener("drop", (e) => {
    e.preventDefault();
    dragArea.classList.remove("dragover");

    let file = e.dataTransfer.files;
    input.files = e.dataTransfer.files;
    for (let i = 0; i < file.length; i++) {
        /* Check selected file is image */

        if (file[i].type.split("/")[0] != "image") continue;

        if (!files.some((e) => e.name == file[i].name)) files.push(file[i]);
    }
    showImages();
});

function textareaval() {
    const dt = new DataTransfer();
    for (let i = 0; i < files.length; i++) {
        dt.items.add(files[i]);
    }

    input.files = dt.files;
}

// Adding  Story To db

/*$('#StorySaveButton').on('click',*/
function StorySave() {
    var missionId = $("#selectMission").find(":selected").val();
    var storytitle = $("#storyTitle").val();
    var storydate = $("#StoryDate").val();
    /*  var storyDetails = CKEDITOR.instances['Content'].getData();*/
    var storyDetails = CKEDITOR.instances["Content"].getData();
    var storyurl = $("#storyVideoUrl").val();

    var formphotos = new FormData();

    var uploadedfiles = /*$('#filesName')[0].files*/ files;

    for (var i = 0, j = 0; i < uploadedfiles.length && j < 20; i++) {
        var filesfromdata = uploadedfiles[i];
        formphotos.append("totalfiles", filesfromdata);
    }

    if (missionId == -1) {
        $('#selectMissionValidation').text("Please Select Mission");
        return false;
    }

    if (storytitle == "") {
        $('#storyTitleValidation').text("Please Enter  Story Tilte");
        return false;
    }

    if (storytitle.trim().length < 5 || storytitle.trim().length > 255) {
        $('#storyTitleValidation').text("Length of title must between 5 to 255 characters");
        return false;
    }
    var regex = /^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube(-nocookie)?\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$/gm;
    
    
        if (storyurl != "") {
            if (storyurl.includes(" "))
            {
                var arr = storyurl.split(" ");
            }
            else if (storyurl.includes(","))
            {
                var arr = storyurl.split(",")
            }
            else
            {
                var arr = storyurl.split("\n");
            }
            if (arr.length > 20) {
                toastr.error('maximum 20 urls are allowed')
                return false;
            }
            for (var url of arr) {

                if (url.match(regex) == null) {
                    toastr.error('Only Youtube URLs are allowed')
                    return false;
                }
            }
    }

    let addStoryObj = {
        MissionId: missionId,
        StoryTitle: storytitle,
        StoryDetails: storyDetails,
        StoryVideoUrl: storyurl,
        storyImages: PreviousImages,
    };

    formphotos.append("addStoryObj", JSON.stringify(addStoryObj));

    var url = "/Story/saveStory";

    $.ajax({
        url: url,
        data: formphotos,
        type: "POST",
        contentType: false,
        processData: false,
        success: function (data) {
            $('#StorySubmitButton').removeAttr("disabled");
            $('#StorySubmitButton').removeAttr("style");
            $('#StoryPreviewButton').removeAttr("disabled");
            $('#StoryPreviewButton').removeAttr("style");
            var link = "/Story/storyDetailPage?storyId=" + data;
            $('#StoryPreviewButton').attr("href",link);
            console.log(data);
        },
        error: function (error) {
            consol.log(data);
        },
    });
}

var PreviousImages = [];

function delImageSelect(id) {
    var divId = id;
    PreviousImages.splice(id, 1);
    DraftedImages();
    console.log("new images: ", PreviousImages);
}

console.log(PreviousImages);

function showDraftedImgs() {
    $('#oldImages').html(PreviousImages.reduce((prev, curr, index) => {
        return `${prev}
                <div class="imageDraft">
                <span class="CloseSpan" onclick="delImageSelect(${index})">&times;</span>
                <img class="ImgDraft" src="${curr}" />
                </div>`
    }, ''));
    for (let i = 0; i < PreviousImages.length; i++) {
        console.log(PreviousImages[i].name)
    }
}

$("#selectMission").on("change", function () {
    var missionId = $("#selectMission").find(":selected").val();
    var url = "/Story/storyByMissionID";

    $.ajax({
        url: url,
        data: {
            missionID: missionId,
        },
        type: "POST",
        success: function (data) {
            console.log(data);
            if (data != "EmptyStory") {
                $("#storyTitle").val(data.title);
                CKEDITOR.instances["Content"].setData(data.description);
                $("#StoryDate").val(data.createAt);
                $("#storyVideoUrl").val(data.videos);
                var items = "";
                for (var i = 0; i < data.images.length; i++) {
                    console.log(data.images[i]);
                    items += `<div class="imageDraft" id="${i}">
                                <span class="CloseSpan" onclick="delImageSelect(${i})">&times;</span>
                                <img class="ImgDraft" src="${data.images[i]}" />
                              </div>`;
                    PreviousImages.push(data.images[i]);

                    console.log(PreviousImages);
                }
               
                $("#oldImages").html(items);
               
            }
            else {
                $("#storyTitle").val("");
                CKEDITOR.instances["Content"].setData('');
                $("#oldImages").html('');
              
            }
        },
        error: function (error) {
            console.log(error);
        },
    });
});


function AddyourStoryValidation() {
    var flag = 0;
    var missionIds = $('#selectMission').find(":selected").val();
    var tittle = $('#storyTitle').val();
    var description = CKEDITOR.instances['content'].getData();
    var youtubeurl = $('#storyVideoUrl').val();
    var urlRegex = /^(?:https?:\/\/)?(?:m\.|www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$/;



    if (missionIds == -1) {
        $('#selectMissionValidation').text("Please Select Mission");
        flag = 1;
    }
    if (tittle == "") {
        $('#storyTitleValidation').text("Please Enter  Story Tilte");
        flag = 1;
    }

    if (tittle.length <= 5 && tittle.length >= 255) {
        $('#storyTitleValidation').text("Length of title must between 5 to 255 characters");
        flag = 1;
    }

    if (description == "") {
        $('#ContentValidation').text("Please Enter Discription");
        flag = 1;
    }

    if (!youtubeurl.match(urlRegex) && !youtubeurl == "") {
        $('#storyVideoUrlValidation').text("Please Enter Correct youtube Url");
        flag = 1;
    }

    if (flag == 1) {
        return false;
    }
  
}

function RemoveAddyourStoryValidation() {
    var input = event.target.getAttribute("id");
    $('#' + input + "Validation").html('');
}



/*
$('#StoryPreviewButton').on('click', function ()
{

    var storyId = $('#StoryPreviewButton').val();

    var url = "/Story/PreviewStory"

    $.ajax({
        url: url,
        data: {
            storyID: storyId
        },
        type: "POST",
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(error);
        }

    })

});*/
