var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Admin/Estados/ObtenerTodos"
        },
        "columns": [
            { "data": "est_descripcion", "width": "20%" },//data tiene la informacion del metodo de accion ene l controlador
            { "data": "est_est", "width": "20%" },
            { "data": "est_fchalt", "width": "20%" },
            { "data": "est_usu_alt", "width": "10%" },//est linea me da error revisar despues
            {// en esta columna se renderezan los botones de Editar y Eliminar
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Bodega/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a onclick=Delete("/Admin/Bodega/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>
                        `;
                }, "width": "10%"
            }
        ]
    });
}


