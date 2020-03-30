window.onload = function () {
    var datepickersOpt1 = {
        dateFormat: 'dd.mm.yy',
        minDate: 0
    }

    $("#departDateTime").datepicker($.extend({
        onSelect: function () {
            var minDate = new Date();
            minDate.setDate(minDate.getDate() + parseInt(1));
            $("#departDateTime").datepicker("option", "minDate", minDate);

            var maxDate = new Date();
            maxDate.setDate(maxDate.getDate() + parseInt(45));
            $("#departDateTime").datepicker("option", "maxDate", maxDate);
        }
    }, datepickersOpt1));

    var datepickersOpt2 = {
        dateFormat: 'dd.mm.yy',
        minDate: 0
    }

    var date = new Date();
    var currentMonth = date.getMonth();
    var currentDate = date.getDate();
    var currentYear = date.getFullYear();
    var birthDates = document.getElementsByName("BirthDate");
    for (var i = 0; i < birthDates.length; i++) {
        var elemId = "#" + birthDates[i].id;
        $(elemId).datepicker({
            dateFormat: "dd.mm.yy",
            maxDate: new Date(currentYear, currentMonth, currentDate - 1),
            changeMonth: true,
            changeYear: true,
            yearRange: '-115:+0',
            monthRange: '-12:+0',
        }, datepickersOpt2)
    };
};

function onClick() {
    var seats = [];
    $.each($("input[name='seats']:checked"), function () {
        seats.push($(this).val());
    });
    if (seats.length > 4 || seats.length == 0) {
        if (!$(".warning")[0]) {
            $("<h3 class='warning' style='color:red'>Count of selected seats should be greater than 0 and less than 5!</h3>").insertBefore("#submit");
        }
        return false;
    }
    return true;
}

function validate() {

    var disables = document.getElementsByName("NumbOfCertificateInvalidity");
    var reD = new RegExp("^(DSBLD)\\d{5}$");
    var check = true;
    for (var i = 0; i < disables.length; i++) {
        if (!reD.test(disables[i].value)) {
            var p = document.createElement("p");
            var text = document.createTextNode("You entered invalid disabled card id");
            p.appendChild(text);
            p.setAttribute('class', 'validateError');
            var prevSibling = disables[i].previousSibling;
            var id = prevSibling.id + 'disableValidError';
            p.setAttribute('id', id);
            if (!$('#' + id)[0]) {
                disables[i].after(p);
            }
            check = false;
        }
    }

    var students = document.getElementsByName('StudentCardID');
    var reS = new RegExp("^(STDNT)\\d{6}$");
    for (var i = 0; i < students.length; i++) {
        if (!reS.test(students[i].value)) {
            var p = document.createElement("p");
            var text = document.createTextNode("You entered invalid student card id");
            p.appendChild(text);
            p.setAttribute('class', 'validateError');
            var prevSibling = students[i].previousSibling;
            var id = prevSibling.id + 'studentValidError';
            p.setAttribute('id', id);
            if (!$('#' + id)[0]) {
                students[i].after(p);
            }
            check = false;
        }
    }

    var pensioners = document.getElementsByName('NumbOFPensionCertificate');
    var reP = new RegExp("^(PNSNRS)\\d{7}$");
    for (var i = 0; i < pensioners.length; i++) {
        if (!reP.test(pensioners[i].value)) {
            var p = document.createElement("p");
            var text = document.createTextNode("You entered invalid pensioner card number id");
            p.appendChild(text);
            p.setAttribute('class', 'validateError');
            var prevSibling = pensioners[i].previousSibling;
            var id = prevSibling.id + 'pensionerValidError';
            p.setAttribute('id', id);
            if (!$('#' + id)[0]) {
                pensioners[i].after(p);
            }
            check = false;
        }
    }

    var children = document.getElementsByName('SchoolTicket');
    var reC = new RegExp("^(CHLDRN)\\d{7}$");
    for (var i = 0; i < children.length; i++) {
        if (!reC.test(children[i].value)) {
            var p = document.createElement("p");
            var text = document.createTextNode("You entered invalid school ticket id");
            p.appendChild(text);
            p.setAttribute('class', 'validateError');
            var prevSibling = children[i].previousSibling;
            var id = prevSibling.id + 'childValidError';
            p.setAttribute('id', id);
            if (!$('#' + id)[0]) {
                children[i].after(p);
            }
            check = false;
        }
    }

    return check;
}