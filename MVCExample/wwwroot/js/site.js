// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
showPopup = (url, title) => {
    $.ajax({
        type: 'POST',
        url: url,
        success: function (res) {
            $("#form-modal .modal-body ").html(res);
            $("#form-modal .modal-title ").html(title);
            $("#form-modal ").modal('show');

        }
    })
}

jQueryDelete = form => {
    var conf = confirm("Bạn có chắc chắn muốn xóa?");
    if (conf == true) {
        $(document).on("click", "#submitDeleteForm", function(){
            let submit = $(this)
            // an huy van xoa tren html
            submit.parent().parent().parent().remove()
        })
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

        }
    return false;
}



JquerySearch = form => {
    $.ajax({
        type: 'POST',
        url: form.action,
        data: new FormData(form),
                contentType: false,
                processData: false,
                success: function(){

                },
                error: function(){
                    console.log(err);
                } 
    })
}

$('AlertBox').remove('hide'); //xoa class hide de hien thi thong bao
$('AlertBox').delay(1000); // animation cua thong bao
$('AlertBox').slideUp(500); // animation cua thong bao
