$(document).ready(function () {
    //Peticion a API
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //url:"https://localhost:44318/Graficas/DashBoard/DataPastel",
        url: urlBase + '/DataPastelTK',
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

    Highcharts.chart('Pastel', {
        colors: ['#fec438', '#4343ec', '#08f154', '#baf201', '#f201ba', '#fa57f4' ],
        chart: {
            type: 'pie'
        },
        title: {
            text: 'Empresas a las que se les ha dado soporte'
        },
        tooltip: {
            valueSuffix: ' Tickets'
        },
        subtitle: {
            text:
                'Tickets de soporte en porcentaje entre las empresas del Ministerio de Motivación Cristiana'
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
                name: 'Cantidad ',
                colorByPoint: true,
                data: data
            }
        ]
    });
}