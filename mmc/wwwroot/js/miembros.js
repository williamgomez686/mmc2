var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Iglesia/MiembrosCEB/ObtenerTodos"
        },
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "lastName", "width": "10%" },
            { "data": "addres", "width": "15%" },
            { "data": "phone", "width": "10%" },
            { "data": "phone2", "width": "10%" },
            { "data": "dpi", "width": "10%" },
            { "data": "cargos", "width": "15%" },
            { "data": "regionName", "width": "15%" },
            //{ "data": "marca.nombre", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Iglesia/MiembrosCEB/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Iglesia/MiembrosCEB/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>
                        `;
                }, "width": "25%"
            }
        ]
    });
}


function Delete(url) {

    swal({
        title: "Esta Seguro que quiere Eliminar la Categoria?",
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