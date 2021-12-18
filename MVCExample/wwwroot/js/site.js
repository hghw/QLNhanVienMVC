﻿
$(document).ready(function () {
    LoadSearachPage(null, 1);
})

function LoadSearachPage(txtSearch, page) {
$.ajax({
    type: 'GET',
    url: 'Staff/GetPaging',
    data: { txtSearch: txtSearch, page: page },
    success: function (res) {
        var SetData = $("#tableViewAll");
        var listPage = res.posts;
        for (var i = 0; i < listPage.length; i++) {
            var Data = "<tr class='row_" + listPage[i].ma_nhanvien + "'>" +
                "<td>" + listPage[i].ma_nhanvien + "</td>" +
                "<td>" + listPage[i].ho_ten + "</td>" +
                "<td>" + listPage[i].ngay_sinh + "</td>" +
                "<td>" + listPage[i].sdt + "</td>" +
                "<td>" + listPage[i].dia_chi + "</td>" +
                "<td>" + listPage[i].chuc_vu + "</td>" +
                "<td class='d-flex' style='justify-content: space-around;'>"
                + '<a onclick=showPopUp("Staff/Edit/' + listPage[i].ma_nhanvien + '","Edit") class="btn btn-warning"> <i class="far fa-edit"></i>Sửa</a >'
                + '<a onclick=showPopUp("Staff/Delete/' + listPage[i].ma_nhanvien + '","Delete") class="btn btn-danger"><i class="fa fa-trash"></i></a >'
                + "</td>" +
                "</tr>";
            SetData.append(Data);
        }
        //create pagination
        var pagination_string = "";
        var pageCurrent = res.page;
        var numSize = res.countPages;

        for (i = 1; i <= numSize; i++) {
            if (i == pageCurrent) {
                pagination_string += '<li class="page-item active"><a class="page-link" data-page='
                    + i + '>' + pageCurrent + '</a></li>';
            } else {
                pagination_string += '<li class="page-item"><a  class="page-link" data-page='
                    + i + '>' + i + '</a></li>';
            }
        }
        $("#pagination").append(pagination_string);
    }
})

}
$(document).on("click", ".page-item .page-link", function () {
var page = $(this).attr('data-page');
$("#tableViewAll").html("")
$("#pagination").html("")
LoadSearachPage(null, page)
})
$(document).on("click", "#subSearch", function () {
    $("#tableViewAll").html("")
    $("#pagination").html("")
var txtSearch = $("#txtSearch").val();
if (txtSearch != "") {
    LoadSearachPage(txtSearch, 1)
}
else {
    LoadSearachPage(null, 1);
}
});



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
                    $("#tableViewAll").children().remove()
                    LoadSearachPage()
                    $("#pagination").html("")

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
                            $("#tableViewAll").children().remove()
                            LoadSearachPage()
$("#pagination").html("")

                            $.notify('Sửa thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
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
                    $("#tableViewAll").children().remove()
                    LoadSearachPage()
$("#pagination").html("")

                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    })
    return false;
}
    
