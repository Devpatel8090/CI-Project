

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