var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Iglesia/MiembrosCEB/CEBporLiderjson2"
        },
        "columns": [
            { "data": "fecha", "width": "10%" },
            { "data": "totalCristianos", "width": "10%" },
            { "data": "noCristianos", "width": "10%" },
            { "data": "ninos", "width": "10%" },
            { "data": "total", "width": "10%" },
            { "data": "convertidos", "width": "10%" },
            { "data": "reconciliados", "width": "8%" },
            { "data": "ofrenda", "width": "15%" },
            { "data": "tipo", "width": "20%" },
            { "data": "name", "width": "15%" },
            //{ "data": "marca.nombre", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Iglesia/CasaEstudioBiblico/Upsert/${data}" class="text-success" style="cursor:pointer">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Iglesia/CasaEstudioBiblico/Delete/${data}") class="text-danger" style="cursor:pointer">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>
                        `;
                }, "width": "15%"
            }
        ]
    });
}

function GerbyId(url) {

    swal({
        title: "Esta Seguro que quiere Eliminar el registro?",
        text: "Este Registro no se podra recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "GET",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}


function Delete(url) {

    swal({
        title: "Esta Seguro que quiere Eliminar el registro?",
        text: "Este Registro no se podra recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}