

$('#SelectMissionTime').on('change', function () {
    var missionId = $("#SelectMissionTime").find(":selected").val();
    var date = $('#DateTimeMission').val();
    var hour = $('#HourWorkTime').val();
    var minutes = $('#MinutesWorkTime').val();
    var time = hour + ":" + minutes;
    var message = $('#MessageTime').val();

    var url = "/VolunteeringTimeSheet/SavedReport"

    $.ajax({
        url: url,
        data: {
           missionId: missionId,
           /* date: date,
            hour: hour,
            minutes: minutes,
            message: message*/
        },
        type:'POST',
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(error);
        }

    })
});
/*
$("#")*/

$(".EditbuttonTime").on('click', function () {
    var timesheetId = $(this).val();
    console.log(timesheetId);

    var url = "/VolunteeringTimeSheet/EditTimesheetTime";

    $.ajax({
        url: url,
        type: 'POST',
        data: {
            timesheetId: timesheetId
        },
        success: function (data) {
            console.log(data);
            
            $('#TimeBaseTimeSheetModalEdit').html(data);
            $('#TimeBaseTimeSheetEdit').modal("show");


           


        },
        error: function (error) {
            console.log(error);
        }

    })

})



$(".EditbuttonGoal").on('click', function () {
    var timesheetId = $(this).val();
    console.log(timesheetId);

    var url = "/VolunteeringTimeSheet/EditTimesheetTime";

    $.ajax({
        url: url,
        type: 'POST',
        data: {
            timesheetId: timesheetId
        },
        success: function (data) {
            console.log(data);

            $('#GoalBaseTimeSheetModalEdit').html(data);
            $('#GoalBaseTimeSheetEdit').modal("show");





        },
        error: function (error) {
            console.log(error);
        }

    })

});

$('.deletebutton').on('click', function () {
    var timesheetId = $(this).val();
    console.log(timesheetId);
    var url = "/VolunteeringTimeSheet/deleteTimesheet";

    $.ajax({
        url: url,
        data: {
            timesheetId: timesheetId
        },
        type: 'POST',
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(error);
        }
    })
});
