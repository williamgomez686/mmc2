//var prueba = urlBase + '/DataBarras_inactivas';
$(document).ready(function () {
    //Peticion a API
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: urlBase + '/DataBarras_activas', 
        error: function () {
            alert("Ocurrio un error al consultar la grafica de barras");
        },
        success: function (data) {
            console.log(data);
            GraficaBarras_activas(data);
        }
    })
});

function GraficaBarras_activas(data) {
    Highcharts.chart('barras_activas', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Casas de estudio biblico por región Activas'
        },
        subtitle: {
            text: 'Total de casas de estudio biblico Activas por region'
        },
        xAxis: {
            type: 'category',
            labels: {
                rotation: -45,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Numero de casas'
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            pointFormat: 'Hay un total de <b>{point.y} Casas de estudio biblico</b>'
        },
        series: [{
            name: 'Population',
            data: data,

            dataLabels: {
                enabled: true,
                rotation: -90,
                color: '#07F327',
                align: 'right',
                format: '{point.y}', // one decimal
                y: 10, // 10 pixels down from the top
                style: {
                    fontSize: '12px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        }]
    });
}