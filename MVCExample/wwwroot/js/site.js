// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// Write your Javascript code.

function callback() {
    $.ajax({
        type: 'POST',
        url: 'Staff/GetDataList',
        success: function (res) {
            var list= res.posts;
            var SetData = $("#tableViewAll");
            for (var i = 0; i < list.length; i++) {
                var Data = "<tr class='row_" + list[i].ma_nhanvien + "'>" +
                    "<td>" + list[i].ma_nhanvien + "</td>" +
                    "<td>" + list[i].ho_ten + "</td>" +
                    "<td>" + list[i].ngay_sinh + "</td>" +
                    "<td>" + list[i].sdt + "</td>" +
                    "<td>" + list[i].dia_chi + "</td>" +
                    "<td>" + list[i].chuc_vu + "</td>" +
                    "<td class='d-flex' style='justify-content: space-around;'>" 
                    // + '<a onclick=showPopUp("@Url.Action('+"'Edit'"+",'Staff'"+',new{'+'id'+"=" + list[i].ma_nhanvien + '})","Sửa") class="btn btn-warning"> <i class="far fa-edit"></i>Sửa</a >' 
                   
                    + '<a onclick=showPopUp("Staff/Edit/'+ list[i].ma_nhanvien + '","Edit") class="btn btn-warning"> <i class="far fa-edit"></i>Sửa</a >' 
                    + '<a onclick=showPopUp("Staff/Delete/' + list[i].ma_nhanvien + '","Delete") class="btn btn-danger"><i class="fa fa-trash"></i></a >' 
                    + "</td>" +
                    "</tr>";
                SetData.append(Data);
            }
            //page
            var SetDataPage = $("#pagination");
            var rowData = "";
            for (var i = 0; i < res.countPages; i++) {
                rowData = rowData + '<li class="page-item"><a class="page-link" href="#">'+(i+1)+'</a></li>';
            }
            SetDataPage.append(rowData)
        }
    })
    
}

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
                    callback(res.data)
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
                            callback(res.data)
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
                    callback(res.data)
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    })
    return false;
}
    


    $(document).ready(function () {
        $("#keyword").on("change", function () {
            var value = $(this).val().toLowerCase()
            $("#tableViewAll tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            })

        })

        $("#subSearch").on("click", function () {
            var value = $("#keyword").val().toLowerCase()
            $("#tableViewAll tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            })
            // $.ajax({//no
            //     type: 'GET',
            //     url: 'Staff/GetDataList/',
            //     success: function (res) {
            //         var SetData = $("#tableViewAll");
            //         var listSearch2 = res.listSearch;  
            //          SetData.remove()
            //          SetData.append(listSearch2)
            //     }
            // })
            
        })

    })

    