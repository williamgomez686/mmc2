
//         FUNCION QUE CARGA LA INFORMACION DE LA TABLA A LOS TEXBOX
        function cargarFila(fila) {
            var celdas = fila.getElementsByTagName("td");

            document.getElementById("Codigovar").value = celdas[0].innerHTML;
            document.getElementById("Nombrevar").value = celdas[1].innerHTML;
        }
//      FUNCION QUE AGREGA EFECTO DE COLORES A LA TABLA
        function cambiar_color_over(celda) {
            celda.style.backgroundColor = "#7ffeff"
        }
        function cambiar_color_click(celda) {
            celda.style.backgroundColor = "#b18704"
        }
        function cambiar_color_out(celda) {
            celda.style.backgroundColor = "#ffffff"
        }

        $(document).ready(function () {
            $('#').DataTable();
        });

//      FUNCION ENCARGADA DE HACER LA CONSULTA AL SERVIDOR Y LA DEVUELVE RENDERISANDOLA EN PANTALLA 
        $(function() {
            //alert("prueba")
            $("#btnbuscar").click(function () {
            
                var Result = {};
                Result.name = "";
                $.ajax({
                    type: "POST",
                    url: "/Iglesia/Cl_Peticiones/ConsultarClientes",
                    data: { "nombre": $("#Nombrevar").val() },
                    success: function (peticiones) {
                        //console.log(peticiones)
                        //console.log(peticiones.length)
                        $("#Codigovar").val(peticiones.codigo)
                        let body = ""
                        for (var i = 0; i < peticiones.length; i++) {
                            body += `<tr onclick="cargarFila(this)" onmouseover="cambiar_color_over(this)" onmouseleave="cambiar_color_out(this)">
                                                <td>${peticiones[i].cliCod}</td>
                                                <td>${peticiones[i].cliRazSoc}</td>
                                                <td>${peticiones[i].cliTel1}</td>
                                                <td>${peticiones[i].cliTel2}</td>
                                                <td>${peticiones[i].cliShDirAct}</td>
                                            </tr>
                                            `
                        }
                        document.getElementById("tabla").innerHTML = body;
                    },
                    failure: function (peticiones) {
                        alert(peticiones.responseText);
                    },
                    error: function (peticiones) {
                        alert(peticiones.responseText);
                    }
                });
            });
        });

/// la hace focus al boton

        document.getElementById('Nombrevar').addEventListener('keydown', inputCharacters);

        function inputCharacters(event) {

            if (event.keyCode == 13) {
                document.getElementById('btnbuscar').focus();
            }

        }

