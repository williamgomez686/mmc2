var prueba = urlBase + '/DataBarras';
$(document).ready(function () {
    //Peticion a API
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //url: "https://localhost:44318/Graficas/DashBoard/DataBarras",
        //url: urlBase + "/DataBarras",
        url: prueba,
        error: function () {
            alert("Ocurrio un error al consultar la grafica de barras");
        },
        success: function (data) {
            console.log(data);
            GraficaBarras(data);
        }
    })
});



function GraficaBarras(data) {
    Highcharts.chart('barras', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Casas de estudio biblico por región'
        },
        subtitle: {
            text: 'Este es el total de casas que hay por región'
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
                color: '#FFFFFF',
                align: 'right',
                format: '{point.y}', // one decimal
                y: 10, // 10 pixels down from the top
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        }]
    });
}