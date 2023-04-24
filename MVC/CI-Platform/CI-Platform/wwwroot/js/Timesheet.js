

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

function IsValideTimesheetAdd() {

var missionIds = $('#SelectMissionTime').find(":selected").val();
var DateVolunteered = $('#DateTimeMission').val();
var TotalHours = $('#HourWorkTime').val();
var TotalMinutes = $('#MinutesWorkTime').val();

    var flag = 0;
    

if (missionIds == -1) {
    $('#SelectMissionTimeValidation').text("Please Select Mission");
    flag = 1;
}

if (DateVolunteered == "") {
    $('#DateTimeMissionValidation').text("Please Select Date You Volunteer");
    flag = 1;
}

if (TotalHours == 0) {
    $('#HourWorkTimeValidation').text("Please Select Hour between 1 to 24");
    flag = 1;
}

if (parseInt(TotalHours) <= -1 || parseInt(TotalHours) > 24) {    
    $('#HourWorkTimeValidation').text("Hours Must between 0 to 24");
    flag = 1;
}

if (TotalMinutes == 0) {
    $('#MinutesWorkTimeValidation').text("Please Select Minute between 0 to 60");
    flag = 1;
}

if (parseInt(TotalMinutes) < -1 || parseInt(TotalMinutes) > 60) {   
    $('#AddTimeMinutesValidation').text("Minutes Must between 0 to 60");
    flag = 1;
}

    if (flag == 1) {
        return false;
    }
}

function IsValideGoalsheetAdd() {
    var flag = 0;
    var GoalmissionIds = $('#GoalMissionId').find(":selected").val();
    var GoalDateVolunteered = $('#GoalDate').val();
    var TotalAction = $('#GoalAction').val();

    if (GoalmissionIds == -1) {
        $('#GoalMissionIdValidation').text("Please Select the goal");
        var flag = 1;
    }

    if (GoalDateVolunteered == "") {
        $('#GoalDateValidation').text("Please Select the Date");
        var flag = 1;
    }

    if (TotalAction == "") {
        $('#GoalActionValidation').text("Please insert Action and it should be in Postive numbers only");
        var flag = 1;
    }
    if (TotalAction < 0) {
        $('#GoalActionValidation').text("Please insert Action and it should be in Postive numbers only");
        var flag = 1;
    }

    
    if (flag == 1) {
        return false;
    }
}

function IsValideTimesheetEdit() {
    var DateVolunteered = $('#DateTimeMission').val();
    var TotalHours = $('#HourWorkTime').val();
    var TotalMinutes = $('#MinutesWorkTime').val();

    flag = 0;

    if (DateVolunteered == "") {
        $('#DateTimeMissionValidation').removeClass("hide");
        flag = 1;
    }

    if (TotalHours == "") {
        $('#HourWorkTimeValidation').removeClass("hide");
        flag = 1;
    }

    if (parseInt(TotalHours) <= -1 || parseInt(TotalHours) > 24) {        
        $('#HourWorkTimeValidation').text("Hours Must between 0 to 24");
        flag = 1;
    }

    if (TotalMinutes == "") {
        $('#MinutesWorkTimeValidation').removeClass("hide");
        flag = 1;
    }
    if (flag == 1) {
        return false;
    }

}
function IsValideGoalsheetEdit() {
    flag = 0;
    var GoalDateVolunteered = $('#GoalDate').val();
    var TotalAction = $('#GoalAction').val();

    if (GoalDateVolunteered == "") {
        $('#GoalDateValidation').text("Please Select the Date");
        var flag = 1;
    }

    if (TotalAction == "") {
        $('#GoalActionValidation').text("Please insert Action and it should be in Postive numbers only");
        var flag = 1;
    }
    if (TotalAction < 0) {
        $('#GoalActionValidation').text("Please insert Action and it should be in Postive numbers only");
        var flag = 1;
    }

    if (flag == 1) {
        return false;
    }
}

function removeIsValideTimesheet() {
    var input = event.target.getAttribute("id");
    $('#' + input + "Validation").html('');
}


