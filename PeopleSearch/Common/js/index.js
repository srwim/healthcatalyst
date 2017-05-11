
$("#isCustomInstance").change(function () {
    console.log(this.checked)
    if (this.checked) {
        $('#SqlServerInstanceValue').attr("disabled", false);
    } else {
        $('#SqlServerInstanceValue').attr("disabled", true);
    }
});

$("#dropDown").change(function () {
    console.log($('#dropDown option:selected').val());

    $('#SqlServerInstanceValue').val($('#dropDown option:selected').val());
});


$('#btnSubmit').click(function () {
    $('#SqlServerInstanceValue').attr("disabled", false);
    $('#IntegratedSecurityValue').attr("disabled", false);
    $('.name').attr('disabled', false);
    $('.providerName').attr('disabled', false);

})

$('#btnSubmit1').on("click", function (event) {
    showProgress();
    $('#ConfigurationResultDiv1').html('');
    $('#ConfigurationResultDiv2').html('');
    var form = $(this);

    var conString = {
        'Name': $('.name').val(),
        'ServerNameValue': $('#ServerNameValue').val(),
        'SqlServerInstanceValue': $('#SqlServerInstanceValue').val(),
        'DatabaseValue': $('#DatabaseValue').val(),
        'IntegratedSecurityValue': $('#IntegratedSecurityValue').val(),
        'FinalConfigurationString': $('#FinalConfigurationString').val(),
        'Status': $('#Status').val(),
        'Message': $('#Message').val(),
        'ProviderName': $('#ProviderName').val(),
    }

    if (form.valid()) {
        $.ajax({
            url: "/Person/ConfigurationResult",
            type: "GET",
            data: name,
            data: conString,
            success: function (msg) {
                hideProgress();
                $('#ConfigurationResultDiv1').html(msg);

                console.log(msg);
            },
            error: function (err) {
                hideProgress();
                console.log("Error occured while setting connection string.");
                $('#ConfigurationResultDiv1').html(err)

            }
        });
    }

    event.preventDefault();
});

$('#btnSubmit2').on("click", function (event) {
    showProgress();
    $('#ConfigurationResultDiv1').html('');
    $('#ConfigurationResultDiv2').html('');
    var form = $(this);
    var conString = {
        'Name': $('.name').val(),
        'ServerNameValue': '',
        'SqlServerInstanceValue': '',
        'DatabaseValue': '',
        'IntegratedSecurityValue': '',
        'FinalConfigurationString': $('#FinalConfigurationString').val(),
        'Status': '',
        'Message': '',
        'ProviderName': $('#ProviderName2').val(),
    }

    if (form.valid()) {
        $.ajax({
            url: "/Person/ConfigurationResult",
            type: "GET",
            data: conString,
            success: function (msg) {
                hideProgress();
                $('#ConfigurationResultDiv2').html(msg);
                console.log(msg);
            },
            error: function (err) {
                hideProgress();
                $('#ConfigurationResultDiv2').html(err);
                console.log("Error occured while setting connection string.");
            }
        });
    }

    event.preventDefault();
});
// code for loader

var spinnerVisible = false;

function showProgress() {
    if (!spinnerVisible) {
        $("div#spinner").fadeIn("fast");
        spinnerVisible = true;
    }
};

function hideProgress() {
    if (spinnerVisible) {
        var spinner = $("div#spinner");
        spinner.stop();
        spinner.fadeOut("fast");
        spinnerVisible = false;
    }
};


