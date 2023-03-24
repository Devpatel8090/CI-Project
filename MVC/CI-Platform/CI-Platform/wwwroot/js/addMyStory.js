

let files = [],
dragArea = document.querySelector('.drag-area'),
input = document.querySelector('.drag-area input'),
button = document.querySelector('.drag-card button'),
select = document.querySelector('.drag-area .select'),
container = document.querySelector('.container-img');

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
    

    let addStoryObj = {
        MissionId: missionId,
        StoryTitle: storytitle,
        StoryDetails: storyDetails,
    };

    var url = "/Story/saveStory";

    $.ajax({
        url: url,
        data: addStoryObj,
        type: 'POST',
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            consol.log(data);
        }
    })


}