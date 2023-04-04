
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

$(function () {

    $('body').on('click', '.list-group .list-group-item', function () {
        $(this).toggleClass('active');
    });
    $('.list-arrows button').click(function () {
        var $button = $(this), actives = '';
        if ($button.hasClass('move-left')) {
            actives = $('.list-right ul li.active');
            actives.clone().appendTo('.list-left ul');
            actives.remove();
        } else if ($button.hasClass('move-right')) {
            actives = $('.list-left ul li.active');
            actives.clone().appendTo('.list-right ul');
            actives.remove();
        }
    });
    $('.dual-list .selector').click(function () {
        var $checkBox = $(this);
        if (!$checkBox.hasClass('selected')) {
            $checkBox.addClass('selected').closest('.well').find('ul li:not(.active)').addClass('active');
            $checkBox.children('i').removeClass('glyphicon-unchecked').addClass('glyphicon-check');
        } else {
            $checkBox.removeClass('selected').closest('.well').find('ul li.active').removeClass('active');
            $checkBox.children('i').removeClass('glyphicon-check').addClass('glyphicon-unchecked');
        }
    });
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
