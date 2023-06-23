var dataTable;

$(document).ready(function () {
    loadDataTable();
   
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "https://localhost:7050/operator/listOrder/GetAll"
        },
        "columns": [
            { "data": "id", "width": "15%", "searchable":false },
            {
                "data": "status",
                "width": "15%",
                "render": function (data) {
                    if (data === 1) {
                        return "New";
                    } else if (data === 2) {
                        return "In Progress";
                    } else if (data === 3) {
                        return "Finish";
                    } else {
                        return "Unknown";
                    }
                }
            },
            {
                "data": "productOrders",
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
                "width": "30%"
            }
        ],
        "language": {
            "search": "STATUS:"
        }
    });
}
