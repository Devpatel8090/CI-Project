
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

