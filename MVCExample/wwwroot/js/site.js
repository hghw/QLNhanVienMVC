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
            url: '/Staff/create',
            data: new FormData($("#formCreateAction")[0]),
            contentType: false,
            processData: false,
            success: function (res) {
                if(res.status == "OK"){
                    alert("abaca")
                }
                $("#form-modal ").modal('hide');
            },
            error: function (err) {
                console.log(err);
            }
        })
    
})


/*Update NoRefresh*/

function jQueryFormUpdate(form) {
    $(document).on("click", "#submitUpdate", function () {
        var hoTen = $("#hoTen").val()
        var ngaySinh = $("#ngaySinh").val()
        var sdt = $("#sdt").val()
        var diaChi = $("#diaChi").val()
        var chucVu = $("#chucVu").val()
        if (hoTen == "") {
            $("#hoTen").notify("Chưa nhập họ tên", { position: "left", autoHideDelay: 2000 })
            showPopUp(Staff / Create)
        }
        if (sdt == "") {
            $("#sdt").notify("Chưa nhập sdt", { position: "left", autoHideDelay: 2000 })
            showPopUp(Staff / Create)
        } if (diaChi == "") {
            $("#diaChi").notify("Chưa nhập địa chỉ", { position: "right", autoHideDelay: 2000 })
            showPopUp(Staff / Create)
        } if (chucVu == "") {
            $("#chucVu").notify("Chưa nhập chức vụ", { position: "left", autoHideDelay: 2000 })
            showPopUp(Staff / Create)
        }
        else {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function () {
                    $("#table-refresh").load(" #table-refresh")
                    $("#form-modal ").modal('hide');
                },
                error: function (err) {
                    console.log(err);
                }
            })
        }

    })
    return false;
}

//DELETE POPUP
jQueryDelete = form =>
{
    $(document).on("click", "#DeletePopup", function () {
        $("#confirm-delete").modal("show")
        
    })
    $(document).on("click", "#submitDeleteForm", function () {

        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $("#table-refresh").load(" #table-refresh")
                $("#confirm-delete").modal("hide")
            },
            error: function (err) {
                console.log(err);
            }
        })
    })

return false;

     
}
//delete tahnh cong
$(document).on("click", "#submitDeleteForm",function(){
    $.notify('Xóa thành công', {autoHideDelay: 3000,globalPosition: "top center", className: "success"} );
})
//Them moi thanh cong
$(document).on("click", "#submitFormSuc",function(){
    $.notify('Thành công', {autoHideDelay: 3000,globalPosition: "top center", className: "success"} );
})

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
