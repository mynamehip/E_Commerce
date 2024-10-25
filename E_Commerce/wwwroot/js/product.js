var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall'},
        "columns": [
            { data: 'title', "width": "20%" },
            { data: 'isbn', "width": "20%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "10%" },
            { data: 'price', "width": "10%" },
            {
                data: { id: "id", highlight: "highlight" },
                "render": function (data) {
                    return `<div class="form-check d-flex justify-content-center">
                                <input type="checkbox" class="form-check-input bg-dark" ${data.highlight ? "checked" : ""} onChange="ChangeHighlight(${data.id})" />
                            </div>`
                },
                "width": "5%"
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="btn-group" role="group">
                                <a href="/admin/product/upsert?id=${data}" class="btn btn-info mx-1">
                                    <i class="bi bi-pencil-square"></i> Edit
                                </a>
                                <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-1">
                                    <i class="bi bi-x-square"></i> Delete
                                </a>
                            </div>`
                },
                "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

function ChangeHighlight(id) {
    $.ajax({
        url: "/admin/product/ChangeHighlight",
        type: 'POST',
        data: {id: id},
        success: function () {
            dataTable.ajax.reload();
            toastr.success("Highlight success!")
        },
        error: function () {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
                footer: '<a href="#">Why do I have this issue?</a>'
            });
        }
    })
}