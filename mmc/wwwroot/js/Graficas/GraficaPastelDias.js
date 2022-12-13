$(document).ready(function () {
    //Peticion a API
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: urlBase + '/DataPastelporDia',
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
        colors: ['#3498DB', '#fdfd96', '#FAA74B', '#baf201', '#a3ffac'],
        chart: {
            type: 'pie'
        },
        title: {
            text: 'Días en los que se realizan las casas de estudio'
        },
        tooltip: {
            valueSuffix: '%'
        },
        subtitle: {
            text:
                'Porcentaje de los días en que se realizan las casas de estudio'
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
                name: 'Percentage',
                colorByPoint: true,
                data: data
            }
        ]
    });
}