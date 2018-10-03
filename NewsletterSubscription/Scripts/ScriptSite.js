// Confirm before removing a subscriptor.
function confirmRemove(email) {
    return confirm("Are you sure you want to remove '" + email + "' subscriptor?");
}


// Show MessageBox when necessary.
function showMessageBoxTimer() {
    $("#messageBox").fadeTo(3000, 500).slideUp(500, function () {
        $("#messageBox").slideUp(500);
    }); 
}


// Print on messageBox depending of situation.
function printMessageBox(code, message) {
    // Codes:
    // 0 init, 1 successKO, 2 successOK, 3 error

    switch (code) {
        case 0:
                showMessageBoxTimer();
            break;
        case 1:
                $("#messageBox").html(message);
                $("#messageBox").removeClass("alert alert-info alert-success alert-warning alert-danger hidden");
                $("#messageBox").addClass("alert alert-danger");
                showMessageBoxTimer();
            break;
        case 2:
                $("#messageBox").html(message);
                $("#messageBox").removeClass("alert alert-info alert-success alert-warning alert-danger hidden");
                $("#messageBox").addClass("alert alert-success");
                showMessageBoxTimer();
            break;
        case 3:
                $("#messageBox").html(message);
                $("#messageBox").removeClass("alert alert-info alert-success alert-warning alert-danger hidden");
                $("#messageBox").addClass("alert alert-warning");
                showMessageBoxTimer();
            break;
        default:
                $("#messageBox").html();
                $("#messageBox").removeClass("alert alert-info alert-success alert-warning alert-danger hidden");
            break;
    }
}


// Add all event associated to IDs.
$(document).ready(function () {

    // Check with AJAX if email already exists in DB and shows a message.
    $("#Email").focusout(function () {
        var regex = /^([A-Za-z0-9_\-.+])+@([A-Za-z0-9_\-.])+\.([A-Za-z]{2,})$/;

        // If email format is properly...
        if ($('#Email-error').val() === undefined && regex.test($("#Email").val())) {
            // Check if it exists at DB
            if ($("#Email").val().trim() !== "") {
                $.ajax({
                    type: "POST",
                    url: "/Subscriptions/AJAXCheckEmailExists",
                    contentType: "application/json; charset=utf-8",
                    data: '{"email":"' + $("#Email").val() + '"}',
                    dataType: "html",
                    success: function (result) {
                        if (result.toLowerCase() === "true") {
                            printMessageBox(1, "Ooops! This email is already registered.");
                        }
                        else {
                            printMessageBox(2, "Hooray! This email is not in use (yet)");
                        }
                    },
                    error: function (error) {
                        printMessageBox(3, "Error! There was a problem when calling the server.");
                    }
                });
            }
            // Else just clean messageBox
            else {
                printMessageBox();
            }
        }
        // Else just clean messageBox
        else {
            printMessageBox();
        }
    });

    // When loading the page, if has some message (set up from server) shows it for a few time.
    if ($("#messageBox").html() !== "") {
        printMessageBox(0);
    }
});
