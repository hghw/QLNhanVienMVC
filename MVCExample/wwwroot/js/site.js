// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
showPopUp = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $("#form-modal .modal-body ").html(res);
            $("#form-modal .modal-title ").html(title);
            $("#form-modal ").modal('show');
            $(document).on("click", "#submitDeleteForm", function () {
                ("#loaderbody").removeClass('d-none');
            })
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

function jQueryCreateForm(form) 
{
    $(document).on("click", "#submitFormSuc", function () {
 
        
    $.ajax({
        type: 'POST',
        url: form.action,
        data: new FormData(form),
        contentType: false,
        processData: false,
        success: function (res) {
            if (res) {
                $("#view-all").html(res.html)
                $("#form-modal .modal-body ").html('');
                $("#form-modal .modal-title ").html('');
                $("#form-modal ").modal('hide');
            } else {
                $("#form-modal .modal-body ").html(res.html);
            }
        },
        error: function (err) {
            console.log(err);
        }
    })
})
// return false;
}

//DELETE POPUP
jQueryDelete = form =>
{
    $(document).on("click", "#DeletePopup", function () {
        $("#confirm-delete").modal("show")
        let submitDelete = $(this)
        $(document).on("click", "#submitDeleteForm",function(){
            submitDelete.parent().parent().parent().remove()
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function () {
                },
                error: function (err) {
                    console.log(err);
                }
            })
        })
    })

        
     
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

JquerySearch = form => {
    
    /*$(document).on("click", "#susbmitSearchBT", function () {
        let tableRM = $("#tableViewAll")
        tableRM.load("Staff/Search.cshtml", function () {
            alert("Load was performed.")
        });
    })*/
    $.ajax({
        type: 'POST',
        url: form.action,
        data: new FormData(form),
                contentType: false,
                processData: false,
        success: function () {
            


                },
                error: function(){
                    console.log(err);
                } 
    })
}



//Validate Create
// $(document).on("submit", "#formCreate", function(){
//     var hoTen = $("#hoTen").val()
//     var ngaySinh = $("#ngaySinh").val()
//     var sdt = $("#sdt").val()
//     var diaChi = $("#diaChi").val()
//     var chucVu = $("#chucVu").val()
//     if(hoTen == "" || ngaySinh == "" || sdt == "" || diaChi == "" || chucVu == "" ){
//         $.notify('Không được để trống', { globalPosition: "top center", className: "warning" });
//     }
// })