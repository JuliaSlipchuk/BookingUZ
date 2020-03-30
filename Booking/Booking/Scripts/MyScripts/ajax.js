$(document).ready(function () {
    var list = [];
    $.each($("[id$=Station]"), function () {
        list.push($(this).attr('id'));
    });

    for (var i = 0; i < list.length; i++) {
        var id = '#' + list[i];
        $(id).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Train/GetStations",
                    type: "POST",
                    dataType: "json",
                    data: { prefix: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item, value: item }
                        }))
                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        });
    }
})