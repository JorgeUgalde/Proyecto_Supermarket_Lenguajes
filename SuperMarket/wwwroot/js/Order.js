var dataTable;

$(document).ready(function () {
    loadDataTable();
    console.log("sirve")
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "https://localhost:7050/operator/listOrder/GetAll"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "status", "width": "15%" },
            {
                "data": "productOrders",
                "render": function (data) {
                    return `
                    <a href="/Operator/ListOrder/Details/${data}" 
                    class="btn btn-primary mx-2">
                        <i class="bi bi-pencil-square"></i> Details
                    </a>

                    `
                },
                "width": "30%"
            }

        ]
    });
}
