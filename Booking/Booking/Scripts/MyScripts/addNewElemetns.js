$(document).ready(function () {
    var dropDownLists = [];
    $.each($("[id^=personTypes]"), function () {
        dropDownLists.push($(this).attr('id'));
    });

    for (var i = 0; i < dropDownLists.length; i++) {
        var id = "#" + dropDownLists[i];
        $(id).on('change',
            function () {
                var id = "#" + event.target.id;
                var disabledID = "#disabled" + event.target.id;
                var studentID = "#student" + event.target.id;
                var pensionerID = "#pensioner" + event.target.id;
                var childID = "#child" + event.target.id;
                var dropDownListVal = $(id).val();
                if ($(disabledID)[0]) {
                    $(disabledID).next("input").remove();
                    $(disabledID).remove();
                }
                else if ($(studentID)[0]) {
                    $(studentID).next("input").remove();
                    $(studentID).remove();
                }
                else if ($(pensionerID)[0]) {
                    $(pensionerID).next("input").remove();
                    $(pensionerID).remove();
                }
                else if ($(childID)[0]) {
                    $(childID).next("input").remove();
                    $(childID).remove();
                }
                if (dropDownListVal == "Disabled") {
                    $("<label for='DisabledLabel' id='" + disabledID.substring(1) + "' class='labelMini'>Please, enter your number of certificate invalidity: </label>").insertAfter($(id));
                    $("<input type='text' required='required' class='textBoxMini' name='NumbOfCertificateInvalidity' />").insertAfter($(disabledID));
                }
                else if (dropDownListVal == "Student") {
                    $("<label for='StudentLabel' id='" + studentID.substring(1) + "' class='labelMini'>Please, enter your student card id: </label>").insertAfter($(id));
                    $("<input type='text' required='required' class='textBoxMini' name='StudentCardID' />").insertAfter($(studentID));
                }
                else if (dropDownListVal == "Pensioner") {
                    $("<label for='PensionerLabel' id='" + pensionerID.substring(1) + "' class='labelMini'>Please, enter your number of pension certificate: </label>").insertAfter($(id));
                    $("<input type='text' required='required' class='textBoxMini' name='NumbOFPensionCertificate' />").insertAfter($(pensionerID));
                }
                else if (dropDownListVal == "Child") {
                    $("<label for='ChildLabel' id='" + childID.substring(1) + "' class='labelMini'>Please, enter your school ticket number: </label>").insertAfter($(id));
                    $("<input type='text' required='required' class='textBoxMini' name='SchoolTicket' />").insertAfter($(childID));
                }
                else {
                    if ($(disabledID)[0]) {
                        $(disabledID).next("input").remove();
                        $(disabledID).remove();
                    }
                    else if ($(studentID)[0]) {
                        $(studentID).next("input").remove();
                        $(studentID).remove();
                    }
                    else if ($(pensionerID)[0]) {
                        $(pensionerID).next("input").remove();
                        $(pensionerID).remove();
                    }
                    else if ($(childID)[0]) {
                        $(childID).next("input").remove();
                        $(childID).remove();
                    }
                }
            }
        )
    }
})