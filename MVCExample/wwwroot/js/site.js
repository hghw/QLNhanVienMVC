$(document).ready(function () {
    LoadData(null, 1);
})

function LoadData(txtSearch, page) {
$.ajax({
    type: 'GET',
    url: 'Staff/GetPaging',
    data: { txtSearch: txtSearch, page: page },
    success: function (res) {
        var SetData = $("#tableViewAll");
        var listPage = res.posts;
        for (var i = 0; i < listPage.length; i++) {
            // format ngay thang nam
            var dateformat = new Date(listPage[i].ngay_sinh);
            var d = dateformat.getDate();
            var m = dateformat.getMonth() + 1;
            var y = dateformat.getFullYear();
            if(d < 10){
                d = "0" + d;
            }
            if(m < 10){
                m = "0" + m;
            }
            var date = (d + '/' + m + '/' + y);
            //
            var Data = "<tr class='row_" + listPage[i].ma_nhanvien + "'>" +
                "<td>" + listPage[i].ma_nhanvien + "</td>" +
                "<td>" + listPage[i].ho_ten + "</td>" +
                "<td>" + date + "</td>" +
                "<td>" + listPage[i].sdt + "</td>" +
                "<td>" + listPage[i].dia_chi + "</td>" +
                "<td>" + listPage[i].chuc_vu + "</td>" +
                "<td class='d-flex' style='justify-content: space-around;'>"
                + '<a onclick=showPopUp("Staff/Edit/' + listPage[i].ma_nhanvien + '","Edit")  class="btn btn-warning"> <i class="far fa-edit"></i>Sửa</a >'
                + '<a onclick=Delete("'+ listPage[i].ma_nhanvien +'") class="btn btn-danger"><i class="fa fa-trash"></i></a >'
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
    },
    error: function (err) {
        console.log(err);
    }
})

}
$(document).on("click", ".page-item .page-link", function () {
    var page = $(this).attr('data-page');
    $("#tableViewAll").html("")
    $("#pagination").html("")
    LoadData(null, page)
})
$(document).on("click", "#subSearch", function () {
    $("#tableViewAll").html("")
    $("#pagination").html("")
    var txtSearch = $("#txtSearch").val();
    if (txtSearch != "") {
        LoadData(txtSearch, 1)
    }
    else {
        LoadData(null, 1);
    }
});


// popup
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
//load body quay tron :v
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
    var ho_ten = $("#ho_ten").val()
    var ngay_sinh = $("#ngay_sinh").val()
    var sdt = $("#sdt").val()
    var dia_chi = $("#dia_chi").val()
    var chuc_vu = $("#chuc_vu").val()
    if (ho_ten == "" || ngay_sinh == "" || sdt == "" || dia_chi == "" || chuc_vu == "") {
        $.notify("Chưa nhập đầy đủ thông tin", { position: "top right",  autoHideDelay: 2000 })
        showPopUp("Staff/Create","Thêm mới")
    }
    else {
        $.ajax({
            type: 'POST',
            url: 'Staff/create',
            data: new FormData($("#formCreateAction")[0]),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.status == "OK") {
                    // $("#form-modal ").modal('hide');
                    $("#tableViewAll").children().remove()
                    LoadData()
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
    }
})


/*Update NoRefresh*/
    $(document).on("click", "#submitUpdate", function () {
        var ho_ten = $("#ho_ten").val()
        var ngay_sinh = $("#ngay_sinh").val()
        var sdt = $("#sdt").val()
        var dia_chi = $("#dia_chi").val()
        var chuc_vu = $("#chuc_vu").val()
        if (ho_ten == "" || ngay_sinh == "" || sdt == "" || dia_chi == "" || chuc_vu == "") {
            $.notify("Chưa nhập đầy đủ thông tin", { position: "top right",  autoHideDelay: 2000 })
            showPopUp("Staff/Create","Thêm mới")
        }
        else
        {
            $.ajax({
                type: 'POST',
                url: 'Staff/Update',
                data: new FormData($("#formEditAction")[0]),
                contentType: false,
                processData: false,
                    success: function (res) {
                        if (res.status == "OK") {
                            $("#tableViewAll").children().remove()
                            LoadData()
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
        }
    })

//DELETE POPUP
function Delete(id){
    var conf = confirm("Bạn có chắc chắn muốn xóa!")
    if(conf){
        $.ajax({
            type: 'POST',
            url: "Staff/Delete",
            data: {id:id},
            success: function (res) {
                if (res.status == 'OK') {
                    $.notify('Xóa thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
                    $("#tableViewAll").children().remove()
                    LoadData()
                    $("#pagination").html("")
                }
            },
            error: function (err) {
                console.log(err);
            }
        })   
    }
}
    
    