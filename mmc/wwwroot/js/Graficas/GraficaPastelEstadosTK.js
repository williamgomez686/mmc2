$(document).ready(function () {
    //Peticion a API
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: urlBase + '/GetDataPieporEstadosTK',
        error: function () {
            alert("Ocurrio un error al consultar los datos");
        },
        success: function (data) {
            console.log(data);
            GraficaPastel2(data);
        }
    })
});

function GraficaPastel2(data) {

    Highcharts.chart('PastelDia', {
        colors: ['#20fb97', '#aa1bd3', '#f7911f', '#1adae6', '#54e362'],
        chart: {
            type: 'pie'
        },
        title: {
            text: 'Estado de los Tickets Actuales'
        },
        tooltip: {
            valueSuffix: ' Tickets'
        },
        subtitle: {
            text:
                'Porcentaje de los Tickets actualmente'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '{point.name}: {point.percentage:.1f}%'
                },
                showInLegend: true
            }
        },
        series: [
            {
                name: 'Cantidad',
                colorByPoint: true,
                data: data
            }
        ]
    });
}