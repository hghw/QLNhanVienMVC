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
        }
    })
}
//ADD POPUP
jQueryAdd = form =>
{
    $(document).on("click", "#AddPopup", function () {
        $("#confirm-add").modal("show")
        let submitAdd = $(this)
        $(document).on("click","submitAddForm",function(){
            $("#maNhanVien").val()
            $("#hoTen").val()
            $("#ngaySinh").val()
            $("#sdt").val()
            $("#diaChi").val()
            $("#chucVu").val()
            submitAdd.parent().parent().parent().remove()
        })
        
    })

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

$('AlertBox').remove('hide'); //xoa class hide de hien thi thong bao
$('AlertBox').delay(1000); // animation cua thong bao
$('AlertBox').slideUp(500); // animation cua thong bao


