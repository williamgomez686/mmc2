var datatable; //nombre de mi variable

$(document).ready(function () { // de esta manera accedemos al arvhibo index html y llamavos a una funcion
    loadDataTable();
});

function loadDataTable() {// esta funcion la declaramos mas abajo
    //usamos nuesta variable declarada en un inicio y con el numeral hacemos referencia al id del index.html
    datatable = $('#tblDatos').DataTable({//lo que es el .datatable hace referencia a un css de terceros que agregamos en el layout
        "ajax": {// hacemos uso de ajax de javascript
            "url": "/Admin/Usuario/ObtenerTodos" //URL de nuestro metodo que esta en el controlador
        },
        "columns": [
            { "data": "UserName", "width": "20%" },//data tiene la informacion del metodo de accion ene l controlador
            { "data": "nombre", "width": "20%" },
            { "data": "apellido", "width": "20%" },
            { "data": "Email", "width": "20%" },
            { "data": "extencion", "width": "20%" },
            { "data": "role", "width": "20%" },
            //{ // en esta columna se renderezan los botones de Editar y Eliminar
            //    "data": "estadoId", //data trae la informacion y id es para obtner el parametro id
            //    "render": function (data) { //render es para renderizar html y en data ya
            //        return `
            //            <div class="text-center">
            //                <a href="/Admin/Estados/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
            //                    <i class="fas fa-edit"></i>
            //                </a>
            //                <a onclick=Delete("/Admin/Estados/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
            //                    <i class="fas fa-trash"></i>
            //                </a>
            //            </div>
            //            `;
            //    }, "width": "10%"
            // }
        ]
    });
}

//    function Delete(url) {

//        swal({
//            title: "Esta Seguro que quiere Eliminar?",
//            text: "Este Registro no se podra recuperar",
//            icon: "warning",
//            buttons: true,
//            dangerMode: true
//        }).then((borrar) => {
//            if (borrar) {
//                $.ajax({
//                    type: "DELETE",
//                    url: url,
//                    success: function (data) {
//                        if (data.success) {
//                            toastr.success(data.message);
//                            datatable.ajax.reload();
//                        }
//                        else {
//                            toastr.error(data.message);
//                        }
//                    }
//                });
//            }
//        });
//    }
//}


