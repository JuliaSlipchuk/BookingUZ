<script src="~/Scripts/jquery-3.4.1.js"></script>
<script src="~/Scripts/jquery-ui.js"></script>
<script>
    $(document).ready(function () {
        $("BegStat").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Train/GetStations",
                    type: "POST",
                    dataType: "json",
                    data: { prefix: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.StationName, value: item.StationName }
                        }))
                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        });
})
</script>