


/*$('#ContactUsSubmit').on('click',*/
function ContactUsSubmit() {
    
    var subject = $('#ContactusSubject').val();
    var message = $('#ContactUsMessage').val();

    var flag = true;

    if (subject == "") {
        $('#subjectValidation').removeClass('hide');
        flag = false;
    }

    if (message == "") {
        $('#messageValidation').removeClass('hide');
        flag = false;
    }
    if (subject != "") {
        $('#subjectValidation').addClass('hide');
        flag = false;
    }


    if (message != "") {
        $('#messageValidation').addClass('hide');
        flag = false;
    }

    if (flag == true) {
        ContactUsAjaxCalling();
    }
    if (flag == false) {
        return flag;
    }
}

function ContactUsRemoveValidation(){

    var subject = $('#ContactusSubject').val();
    var message = $('#ContactUsMessage').val();

    var flag = true;

    if (subject == "") {
        $('#subjectValidation').removeClass('hide');
        flag = false;
    }

    if (message == "") {
        $('#messageValidation').removeClass('hide');
        flag = false;
    }
    if (subject != "") {
        $('#subjectValidation').addClass('hide');
        flag = false;
    }


    if (message != "") {
        $('#messageValidation').addClass('hide');
        flag = false;
    }
}

function ContactUsAjaxCalling() {
    var userName = $('#UserName').val();
    var Email = $('#UserEmailId').val();
    var subject = $('#ContactusSubject').val();
    var message = $('#ContactUsMessage').val();

    var url = "/ContactUs/ContactUs";

    $.ajax({
        url: url,
        data: {
            Name: userName,
            EmailAddress: Email,
            Subject: subject,
            Message: message
        },
        type: 'POST',
        success: function (data) {
            console.log(data);
            $('#closeButtonContactUs').click();
            toastr.success('Thank you for Contacting us. we will get back to you soon!');

        },
        error: function (error) {
            console.log(error);
        }
    });
}




