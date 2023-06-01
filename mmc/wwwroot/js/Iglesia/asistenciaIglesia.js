
document.addEventListener("DOMContentLoaded", function () {
    MostrarAsistieron()
}, false)

$(document).ready(function () {
    $('.check-asistencia').on('change', function () {
        var empleadoId = $(this).val();
        var asistencia = $(this).is(':checked');

        var carnet = $('.input-carnet[data-empleado-id="' + empleadoId + '"]').val();

        $.ajax({
            url: '/Iglesia/IglesiaReunionesServidores/ActualizarAsistencia',
            type: 'POST',
            data: { empleadoId: empleadoId, asistencia: asistencia, carnet: carnet },
            success: function () {
                location.reload(); // Recargar la página después de actualizar la asistencia
                MostrarAsistieron();
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    });
});

function MostrarAsistieron() {
    //PETICION FETCH QUE CONSULTA AL SERVIDOR
    var url = "/Iglesia/IglesiaReunionesServidores/ListaAsistieron"
    fetch(url)
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaEmpleados tbody").html("");

                responseJson.forEach((Articulo) => {
                    $("#tablaEmpleados tbody").append(
                        $("<tr>").append(
                            $("<td>").text(Articulo.nombres),
                            $("<td>").text(Articulo.acompañantes),
                            $("<td>").text(Articulo.asiste),
                            $("<td>").text(Articulo.departamento)
                        )
                    )

                })

            }
        })
}