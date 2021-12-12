// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// Write your Javascript code.
showPopUp = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $("#form-modal .modal-body ").html(res);
            $("#form-modal .modal-title ").html(title);
            $("#form-modal ").modal('show');
        }
    })
}

$(function () {
    $("#loaderbody").addClass('d-none');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('d-none');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('d-none');
    });
});
//them

$(document).on("click", "#submitFormSuc", function () {
        $.ajax({
            type: 'POST',
            url: 'Staff/create',
            data: new FormData($("#formCreateAction")[0]),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.status == "OK") {
                    $("#table-refresh").load(" #table-refresh")
                    $.notify('Thêm thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
                }
                if (res.status == "LOI") {
                    $.notify('Nhân viên này đã tồn tại', { autoHideDelay: 3000, globalPosition: "top center", className: "danger" });
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
})


/*Update NoRefresh*/
    $(document).on("click", "#submitUpdate", function () {
            $.ajax({
                type: 'POST',
                url: 'Staff/update',
                data: new FormData($("#formEditAction")[0]),
                contentType: false,
                processData: false,
                    success: function (res) {
                        if (res.status == "OK") {
                            $("#table-refresh").load(" #table-refresh")
                            $.notify('Sửa thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
                        }
                        // if (res.status == "LOI") {
                        //     $.notify('Nhân viên này đã tồn tại', { autoHideDelay: 3000, globalPosition: "top center", className: "danger" });
                        // }
                },
                error: function (err) {
                    console.log(err);
                }
            })

    })


//DELETE POPUP
formDeleteJqueryy = (form) => {
    $(document).on("click", "#submitDeleteForm2", function () {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData($("#formDeleteView")[0]),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.status == 'OK') {
                    $.notify('Xóa thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
                    $("#table-refresh").load(" #table-refresh")
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    })
    return false;
}
    

function JquerySearchForm() {
    $(document).ready(function () {
        $("#inSearch123").on("change", function () {
            var value = $(this).val().toLowerCase()
            $("#tableViewAll tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            })
        })

        $("#btSearchForm").on("click", function () {
            var value = $(this).val().toLowerCase()
            $("#tableViewAll tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            })
        })

    })
}
