var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        responsive: {
            breakpoints: [
                { name: 'bigdesktop', width: Infinity },
                { name: 'meddesktop', width: 1480 },
                { name: 'smalldesktop', width: 1280 },
                { name: 'medium', width: 1188 },
                { name: 'tabletl', width: 1024 },
                { name: 'btwtabllandp', width: 848 },
                { name: 'tabletp', width: 768 },
                { name: 'mobilel', width: 412 },
                { name: 'mobilep', width: 320 }
            ]
        },

        "ajax": {
            "url": "/Iglesia/MiembrosCEB/ObtenerTodos"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "lastName", "width": "15%" },
            { "data": "addres", "width": "25%" },
            { "data": "phone", "width": "10%" },
            //{ "data": "phone2", "width": "10%" },
            //{ "data": "dpi", "width": "10%" },
            { "data": "cargos", "width": "8%" },
            { "data": "regionName", "width": "5%" },
            //{ "data": "marca.nombre", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Iglesia/MiembrosCEB/Upsert/${data}" class="text-success" style="cursor:pointer">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Iglesia/MiembrosCEB/Delete/${data}") class="text-danger" style="cursor:pointer">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>
                        `;
                }, "width": "10%"
            }
        ]
        
    });
}


function Delete(url) {

    swal({
        title: "Esta Seguro que quiere Eliminar a este Lider?",
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