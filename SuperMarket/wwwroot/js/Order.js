var dataTable;

$(document).ready(function () {
    loadDataTable();
    setupStatusFilter();
   
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Operator/listOrder/GetAll"
        },
        "dom": 'lrtip',
        "columns": [
            { "data": "id", "width": "33.3%", "searchable":false },
            {
                "data": "status",
                "width": "33.3%",
                "render": function (data) {
                    if (data === 1) {
                        return "New";
                    } else if (data === 2) {
                        return "In Progress";
                    } else if (data === 3) {
                        return "Finished";
                    } else {
                        return "Unknown";
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    if (data !== 3) {
                        return `
                            <a href="/Operator/ListOrder/Details/${data}" 
                                class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Details
                            </a>
                        `;
                    } else {
                        return "";
                    }
                },
                "width": "33.3%",
            }
        ]
    });
}


function setupStatusFilter() {
    $('#statusFilter').on('change', function () {

        var data = $(this).val();
        var status = "";

        if (data == 1) {
            status = "New";
        } else if (data == 2) {
            status = "In Progress";
        } else if (data == 3) {
            status = "Finished";
        }
       dataTable.column(1).search(status).draw();
    });
}