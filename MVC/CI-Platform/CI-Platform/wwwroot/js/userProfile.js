
//$("#CountryId").on('change', function () {
//    var countryId = $("#CountryId").find(':selected').val();
//    $.ajax({
//        url: '/Mission/GetCityByCountry',
//        type: 'GET',
//        data: {
//            CountryId : countryId
//        },
//        dataType: 'Json',
//        success: function (data) {
//            console.log(data);
//            var citiList = $("#CityList");
//            citiList.empty();
//            var items = "";
//            $(data).each(function (i, item) {
//                items += `<option value=${item.cityId}>` + item.name + `</option>`
//            });
//            citiList.html(items);

//        },
//        error: function (error) {
//            console.log(error);
//        }

//    });
//});

$('#CountrySelect').on('change', function () {
    var countryId = $('#CountrySelect').find(":selected").val();
    $.ajax({
        url: '/Mission/GetCityByCountry',
        type: 'GET',
        data: { CountryId: countryId },
        dataType: 'json',
        success: function (data) {
            console.log(data.length);
            var cities = $('#Citylistings');
            cities.empty();
            var items = "";
            cities.empty();
            if (data.length == 0) {
                var items = "";
                cities.empty();
            }
            $(data).each(function (i, item) {
                items += `<option value=${item.cityId}>` + item.name + `</option>`

                console.log(item);

            });
            cities.html(items);
        }
    });
});

/*var skillIds = new Array();*//*
var element = {};

*//*$('#SkillNames').on('click',*//* function addSkillsIdIntoArray(skillId) {
    *//*var skillId = $('#skillNames').val();*//*
    var skillidName = skillId.value;

    element['skillidName' + skillidName] = skillidName;
    console.log(element);
   *//* skillIds.push(element);
    console.log(skillIds);*//*
*/
/*}*/
const points = new Array();
const skillids = new Array();
/*const skillidsrightside = new Array();*/

$(function () {

    $('body').on('click', '.list-group .list-group-item', function () {
        $(this).toggleClass('active');
    });
    points.length = 0;
    skillids.length = 0;
    $('.list-arrows button').click(function () {
        
        var $button = $(this), actives = '';
        if ($button.hasClass('move-left')) {
            actives = $('.list-right ul li.active');
            actives.clone().appendTo('.list-left ul');
            
            
            for (var i = 0; i < actives.length; i++) {
                var OldArrayIndex = skillids.indexOf(actives[i].attributes[0].value);
                skillids.splice(OldArrayIndex, 1);
            }
            console.log(points);
            console.log(skillids);
            actives.remove();
        }
                
        else if ($button.hasClass('move-right')) {
           
            actives = $('.list-left ul li.active');
            actives.clone().appendTo('.list-right ul');
          
            
           
            for (var i = 0; i < actives.length; i++) {
                let elementExistsright = skillids.includes(actives[i].attributes[0].value);
                if (!elementExistsright) {
                    points.push(actives[i].innerText);
                    skillids.push(actives[i].attributes[0].value);
                }
            }
           /* console.log(actives[0].innerHTML);
            console.log(actives[0].attributes[0].value);*/
            console.log(points);
            console.log(skillids);
            
           
           /* console.log(actives[0].outerHTML.val);
            console.log(actives[0].innerHTML.value);
            console.log(actives.html);
            console.log(actives.value);*/
            actives.remove();
        }
    });
  /*  $('.dual-list .selector').click(function () {
        var $checkBox = $(this);s
        if (!$checkBox.hasClass('selected')) {
            $checkBox.addClass('selected').closest('.well').find('ul li:not(.active)').addClass('active');
            $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
        } else {
            $checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
            $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
        }
    });*/
    $('[name="SearchDualList"]').keyup(function (e) {
        var code = e.keyCode || e.which;
        if (code == '9') return;
        if (code == '27') $(this).val(null);
        var $rows = $(this).closest('.dual-list').find('.list-group li');
        var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
        $rows.show().filter(function () {
            var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            return !~text.indexOf(val);
        }).hide();
    });

});

$('#AddSkillButtonModal').on('click', function () {

    var url = "/Registration/AddSkills";

    $.ajax({

        url: url,
        type: 'POST',
        data: {
            SkillIDs :skillids
        },
        traditional: true,
        success: function (data) {
            console.log(data);
            items =""
            $(data).each(function (i, item) {
                items += ` <li> ` + item + `<li>  `;
            });

            
            $('#SkillsTextArea').html(items);
            $('#closeBtnOfModal').click();
        },
        error: function (error) {
            console.log(error);
        }

    })
})



