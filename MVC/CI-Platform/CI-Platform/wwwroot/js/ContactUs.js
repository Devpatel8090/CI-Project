
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css"></script>

$('#ContactUsSubmit').on('click', function () {
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
});