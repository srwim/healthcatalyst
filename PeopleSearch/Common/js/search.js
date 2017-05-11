
function SearchPerson(e) {
    $('#blankValidationMsg').html('');
    var name = { 'name': $('#searchTextBox').val(), 'isExactMatchRequested': false };

    if ($('#searchTextBox').val().trim().length > 0) {
        showProgress();
        $.ajax({
            url: "/Person/Result",
            type: "GET",
            data: name,
            success: function (msg) {
                hideProgress();
                $('#peopleDetails').html(msg);
            },
            error: function (f) {
                console.log(f);
                hideProgress();
            }

        });
    } else {
        console.log("hi")
        $('#blankValidationMsg').html('Can not be left empty!');
        console.log($('#blankValidationMsg').text());
    }
   
};

$("#searchTextBox").keyup(function (event) {
    if (event.keyCode == 13) {
        SearchPerson(event);
    }
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




