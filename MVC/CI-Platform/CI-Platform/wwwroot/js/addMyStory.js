

let files = [],
dragArea = document.querySelector('.drag-area'),
input = document.querySelector('.drag-area input'),
button = document.querySelector('.drag-card button'),
select = document.querySelector('.drag-area .select'),
    container = document.querySelector('.container-img');
oldImgs = document.getElementsByClassName('.oldImgs');

/* CLICK LISTENER */
select.addEventListener('click', () => input.click());

/* INPUT CHANGE EVENT */
input.addEventListener('change', () => {
let file = input.files;

// if user select no image
if (file.length == 0) return;

for (let i = 0; i < file.length; i++) {
if (file[i].type.split("/")[0] != 'image') continue;
if (!files.some(e => e.name == file[i].name)) files.push(file[i])
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
</div>`
}, '');
for (let i = 0; i < files.length; i++) {
console.log(files[i].name)
}
}

/* DELETE IMAGE */
function delImage(index) {
files.splice(index, 1);
showImages();
}

/* DRAG & DROP */
dragArea.addEventListener('dragover', e => {
e.preventDefault()
dragArea.classList.add('dragover')
})

/* DRAG LEAVE */
dragArea.addEventListener('dragleave', e => {
e.preventDefault()
dragArea.classList.remove('dragover')
});

    /* DROP EVENT */
    dragArea.addEventListener('drop', e => {
    e.preventDefault()
    dragArea.classList.remove('dragover');

    let file = e.dataTransfer.files;
    input.files = e.dataTransfer.files;
    for (let i = 0; i < file.length; i++) {
    /* Check selected file is image */

        if (file[i].type.split("/")[0] != 'image') continue;

        if (!files.some(e => e.name == file[i].name)) files.push(file[i])
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
   
    var missionId = $('.MissionIdFromTitle').val();
    var storytitle = $('#storyTitle').val();
    var storydate = $('#StoryDate').val();
  /*  var storyDetails = CKEDITOR.instances['Content'].getData();*/
    var storyDetails = CKEDITOR.instances['Content'].getData();
    var storyurl = $('#storyVideoUrl').val();

    var formphotos = new FormData();

    var uploadedfiles = /*$('#filesName')[0].files*/ files;

    for (var i = 0, j = 0; i < uploadedfiles.length && j < 20; i++) {
        var filesfromdata = uploadedfiles[i];
        formphotos.append('totalfiles', filesfromdata);
    }


    let addStoryObj = {
        MissionId: missionId,
        StoryTitle: storytitle,
        StoryDetails: storyDetails,

    };

    formphotos.append('addStoryObj', JSON.stringify(addStoryObj));

    var url = "/Story/saveStory";

    $.ajax({
        url: url,
        data: formphotos,
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            consol.log(data);
        }
    })


}

$('#selectMission').on('change', function () {

    var missionId = $('#selectMission').find(':selected').val();
    var url = "/Story/storyByMissionID"
    $.ajax({
        url: url,
        data: {
            missionID: missionId
        },
        type: 'POST',
        success: function (data) {
            console.log(data);
            if (data != "EmptyStory") {
                $('#storyTitle').val(data.title);
                CKEDITOR.instances['Content'].setData(data.description);
                $('#StoryDate').val(data.createAt);
            }
            else {
                $('#storyTitle').val('');
                CKEDITOR.instances['Content'].setData();

            }
            window.location.reload();
        },
        error: function (error) {
            console.log(error);
        }


    });
})



        window.addEventListener('load', () => {
    console.log(oldImgs);
    
    for (let i = 0; i < oldImgs.length; i++) {
            img = oldImgs[i];
            fetch(img.src)
            .then(res => res.blob())
            .then(blob => {
            const file = new File([blob], 'dot.png', blob)
            console.log(file)
            files.push(file)
            showImages();
        })
    
    }
    console.log(files);
    })