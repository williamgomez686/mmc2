$(document).ready(function () {
    //Peticion a API
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //url:"https://localhost:44318/Graficas/DashBoard/DataPastel",
        url: urlBase + '/DataPastel',
        error: function () {
            alert("Ocurrio un error al consultar los datos");
        },
        success: function (data) {
            console.log(data);
            GraficaPastel(data);
        }
    })
});

function GraficaPastel(data) {

    Highcharts.chart('container', {
        colors: ['#01BAF2', '#f6fa4b', '#FAA74B', '#baf201', '#f201ba'],
        chart: {
            type: 'pie'
        },
        title: {
            text: 'Tipos de casas de estudio Biblico'
        },
        tooltip: {
            valueSuffix: '%'
        },
        subtitle: {
            text:
                'Porcentaje que hay distribuido en las casas de estudio bíblico'
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