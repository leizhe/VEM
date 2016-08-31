
var Index = function () {
    return {
        initCharts: function () {
            if (!jQuery.plot) {
                return;
            }

            var data = [];

            function showTooltip(title, x, y, contents) {
                $('<div id="tooltip" class="chart-tooltip"><div class="date">' + title + '<\/div><div class="label label-success">' + contents + '<\/div><\/div>').css({
                    position: 'absolute',
                    display: 'none',
                    top: y - 100,
                    width: 75,
                    left: x - 40,
                    border: '0px solid #ccc',
                    padding: '2px 6px',
                    'background-color': '#fff',
                }).appendTo("body").fadeIn(200);
            }

            function GetSaleViews(typeid) {
                var r = [];
                AjaxRequest.submit(
                "/Home/GetSalesHistoryChartData",
                { typeId: typeid },
               function (result) {
                   $.each(result, function (n,value) {
                       r.push([value.Day, value.SaleCount])
                   });
               },
               function (XMLHttpRequest) {}
               );
               return r;
            }

            var LastMonthsaleViews = GetSaleViews(1);

            var ThisMonthsaleViews = GetSaleViews(2);

            if ($('#site_statistics').size() != 0) {
                $('#site_statistics_loading').hide();
                $('#site_statistics_content').show();
                var plot_statistics = $.plot($("#site_statistics"), [{
                    data: ThisMonthsaleViews,
                    label: "本月销量"
                }, {
                    data: LastMonthsaleViews,
                    label: "上月销量"
                }
                ], {
                    series: {
                        lines: {
                            show: true,
                            lineWidth: 2,
                            fill: true,
                            fillColor: {
                                colors: [{
                                    opacity: 0.05
                                }, {
                                    opacity: 0.01
                                }
                                ]
                            }
                        },
                        points: {
                            show: true
                        },
                        shadowSize: 2
                    },
                    grid: {
                        hoverable: true,
                        clickable: true,
                        tickColor: "#eee",
                        borderWidth: 0
                    },
                    colors: ["#d12610", "#37b7f3", "#52e136"],
                    xaxis: {
                        ticks: 11,
                        tickDecimals: 0
                    },
                    yaxis: {
                        ticks: 11,
                        tickDecimals: 0
                    }
                });

                var previousPoint = null;
                $("#site_statistics").bind("plothover", function (event, pos, item) {
                    $("#x").text(pos.x.toFixed(2));
                    $("#y").text(pos.y.toFixed(2));
                    if (item) {
                        if (previousPoint != item.dataIndex) {
                            previousPoint = item.dataIndex;

                            $("#tooltip").remove();
                            var x = item.datapoint[0].toFixed(2),
                                y = item.datapoint[1].toFixed(2);

                            showTooltip(item.series.label, item.pageX, item.pageY, parseInt(y) + "件商品");
                        }
                    } else {
                        $("#tooltip").remove();
                        previousPoint = null;
                    }
                });
            }

            if ($('#load_statistics').size() != 0) {

                $('#load_statistics_loading').hide();
                $('#load_statistics_content').show();

                $('#load_statistics').bind("mouseleave", function () {
                    $("#tooltip").remove();
                });
            }

         
        },
    };

}();

jQuery(document).ready(function () {
    Index.initCharts();
});
