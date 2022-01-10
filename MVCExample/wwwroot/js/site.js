$(document).ready(function () {

})

function LoadData(txtPhongban, txtSearch, page) {
$.ajax({
    type: 'GET',
    url: '/Staff/GetPaging',
    data: {txtPhongban: txtPhongban, txtSearch: txtSearch, page: page },
    success: function (res) {
        var tbLoi = res.status;
        if (tbLoi == "ERROR") {
            $.notify("Không có nhân viên nào", { position: "top center", autoHideDelay: 5000 , className: "danger"})
        }
        var SetData = $("#tableViewAll");
        var listPage = res.posts;
        var excutePB = res.excutePB;
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
            var Data = "<tr class='row_" + listPage[i].ma_nhanvien + "'>" +
                "<td>" + listPage[i].ma_nhanvien + "</td>" +
                "<td>" + listPage[i].ho_ten + "</td>" +
                "<td>" + date + "</td>" +
                "<td>" + listPage[i].sdt + "</td>" +
                "<td>" + listPage[i].dia_chi + "</td>" +
                "<td>" + listPage[i].chuc_vu + "</td>" +
                "<td>" + listPage[i].phong_ban.ten_phong_ban + "</td>" +
                "<td class='d-flex' style='justify-content: space-around;'>"
                + '<a onclick=showPopUp("/Staff/Edit/' + listPage[i].ma_nhanvien + '","Edit")  class="btn btn-warning"> <i class="far fa-edit"></i>Sửa</a >'
                + '<a onclick=Delete("'+ listPage[i].ma_nhanvien +'") class="btn btn-danger"><i class="fa fa-trash"></i></a >'
                + "</td>" +
                "</tr>";
            SetData.append(Data);
        }
        //create pagination
        var pagination_string = "";
        var pageCurrent = res.page;
        var numSize = res.countPages;

        if (pageCurrent == (1)) {//page PREVIEWS disabled or enable
            pagination_string += '<li class="page-item disabled"><a class="page-link" data-page='
                + (pageCurrent - 1) + '>Trước</a></li>';
        } else {
            pagination_string += '<li class="page-item"><a class="page-link" data-page='
                + (pageCurrent - 1) + '>Trước</a></li>';
        }
        
        for (i = 1; i <= numSize; i++) {
            if (i == pageCurrent) {
                pagination_string += '<li class="page-item active"><a class="page-link" data-page='
                    + i + '>' + pageCurrent + '</a></li>';
            } else {
                pagination_string += '<li class="page-item"><a  class="page-link" data-page='
                    + i + '>' + i + '</a></li>';
            }
        }
        if (pageCurrent == (numSize)) {//page AFTER disabled or enable
            pagination_string += '<li class="page-item disabled"><a class="page-link" data-page='
                + (pageCurrent + 1) + '>Sau</a></li>';
        } else {
            pagination_string += '<li class="page-item"><a class="page-link" data-page='
                + (pageCurrent + 1) + '>Sau</a></li>';
        }
        $("#pagination").append(pagination_string);
    },
    error: function (err) {
        console.log(err);
    }
})
}

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
function addStaff() {
    var staff = {
        ho_ten: $("#ho_ten").val(),
        ngay_sinh: $("#ngay_sinh").val(),
        sdt: $("#sdt").val(),
        dia_chi: $("#dia_chi").val(),
        chuc_vu: $("#chuc_vu").val(),
        phongban_id: $("#phongban_id").val(),
    }
    var ho_ten = $("#ho_ten").val()
    var ngay_sinh = $("#ngay_sinh").val()
    var sdt = $("#sdt").val()
    var dia_chi = $("#dia_chi").val()
    var chuc_vu = $("#chuc_vu").val()
    var phongban_id = $("#phongban_id").val()
    if (ho_ten == "" || ngay_sinh == "" || sdt == "" || dia_chi == "" || chuc_vu == "" || phongban_id == 0) {
        $.notify("Chưa nhập đầy đủ thông tin", { position: "top right",  autoHideDelay: 2000 })
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Staff/create',
            data: {model: staff},
            success: function (res) {
                var countdata = res.data;
                if (res.status == "OK") {
                    $("#form-modal").modal('hide');
                    $("#tableViewAll").children().remove()
                    LoadData(null, null, countdata.length)
                    $("#pagination").html("")
                    $.notify('Thêm thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
                }
                if (res.status == "Duplicate") {
                    $.notify('Nhân viên này đã tồn tại', { autoHideDelay: 3000, globalPosition: "top center", className: "danger" });
                }
                if (res.status == "ERROR") {
                    $.notify('Thêm nhân viên thất bại', { autoHideDelay: 3000, globalPosition: "top center", className: "danger" });
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    }
}

/*Update NoRefresh*/
function updateStaff() {
    var staff = {
        ma_nhanvien: $("#ma_nhanvien").val(),
        ho_ten: $("#ho_ten").val(),
        ngay_sinh: $("#ngay_sinh").val(),
        sdt: $("#sdt").val(),
        dia_chi: $("#dia_chi").val(),
        chuc_vu: $("#chuc_vu").val(),
        phongban_id: $("#phongban_id").val(),
    }
        var ho_ten = $("#ho_ten").val()
        var ngay_sinh = $("#ngay_sinh").val()
        var sdt = $("#sdt").val()
        var dia_chi = $("#dia_chi").val()
        var chuc_vu = $("#chuc_vu").val()
    var phongban_id = $("#phongban_id").val()
    if (ho_ten == "" || ngay_sinh == "" || sdt == "" || dia_chi == "" || chuc_vu == "" || phongban_id == 0) {
            $.notify("Chưa nhập đầy đủ thông tin", { position: "top right",  autoHideDelay: 2000 })
        }else
        if(sdt.length > 10 || sdt.length < 1){
            $.notify("Số điện thoại lớn hơn 0 nhỏ hơn 10 ký tự!", { position: "top right",  autoHideDelay: 2000 })
        }
        else
        {
            $.ajax({
                type: 'POST',
                url: '/Staff/Update',
                data: {staff: staff},
                success: function (res) {
                    var countdata = res.data;
                        if (res.status == "OK") {
                            $("#form-modal").modal('hide');
                            $("#tableViewAll").children().remove()
                            LoadData(null, null, countdata.length)
                            $("#pagination").html("")
                            $.notify('Sửa thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
                        }
                        if (res.status == "Duplicate") {
                            $.notify('Nhân viên này đã tồn tại', { autoHideDelay: 3000, globalPosition: "top center", className: "danger" });
                        }
                        if (res.status == "ERROR") {
                            $.notify('Sửa nhân viên thất bại', { autoHideDelay: 3000, globalPosition: "top center", className: "danger" });
                        }
                },
                error: function (err) {
                    console.log(err);
                }
            })
        }
    }

//DELETE POPUP
function Delete(id){
    var conf = confirm("Bạn có chắc chắn muốn xóa!")
    if(conf){
        $.ajax({
            type: 'POST',
            url: "/Staff/Delete",
            data: {id:id},
            success: function (res) {
                if (res.status == 'OK') {
                    $.notify('Xóa thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
                    LoadData(null, null, 1)
                    $("#form-modal").modal('hide');
                    $("#tableViewAll").children().remove()
                    $("#pagination").html("")
                }
                if (res.status == "ERROR") {
                    $.notify('Xóa nhân viên thất bại', { autoHideDelay: 3000, globalPosition: "top center", className: "danger" });
                }
            },
            error: function (err) {
                console.log(err);
            }
        })   
    }
}
    
function donwloadExcel() {
    $.ajax({
        type: "GET",
        url: "/Staff/ExportExcel",
        success: function (res) {
            if (res.status == "OK") {
                $.notify('Download file Excel thành công', { autoHideDelay: 3000, globalPosition: "top center", className: "success" });
            }
            if (res.status == "ERROR") {
                $.notify('File đã tồn tại', { autoHideDelay: 3000, globalPosition: "top center", className: "danger" });
            }
        },
        error: function (err) {
            console.log(err)
        }
    })
}

//page
$(document).on("click", ".page-item .page-link", function () {
    var page = $(this).attr('data-page');
    $("#tableViewAll").html("")
    $("#pagination").html("")
    LoadData(null, null, page)
})
//pageMap
$(document).on("click", ".page-item .page-link", function () {
    var page = $(this).attr('data-page');
    $("#DanhSachNV").html("")
    $("#paginationMap").html("")
    danhsachNVMap(null, null, page);
})
//nhan enter de search
$(document).on("change", "#txtSearch", function () {
    $("#tableViewAll").html("")
    $("#pagination").html("")
    var txtPhongban = $("#dropdownValue").val();
    var txtSearch = $("#txtSearch").val();
    if (txtSearch != "" && txtPhongban > 0) {
        LoadData(txtPhongban, txtSearch, 1)

    } else if (txtSearch != "" && txtPhongban == 0) {
        LoadData(null, txtSearch, 1)

    } else if (txtSearch == "" && txtPhongban > 0) {
        LoadData(txtPhongban, null, 1)
    }
    else {
        LoadData(null, null, 1);
    }
})
//nhan button de search
$(document).on("click", "#subSearch", function () {
    $("#tableViewAll").html("")
    $("#pagination").html("")
    var txtPhongban = $("#dropdownValue").val();
    var txtSearch = $("#txtSearch").val();
    if (txtSearch != "" && txtPhongban > 0) {
        LoadData(txtPhongban, txtSearch, 1)

    } else if (txtSearch != "" && txtPhongban == 0) {
        LoadData(null, txtSearch, 1)

    } else if (txtSearch == "" && txtPhongban > 0) {
        LoadData(txtPhongban, null, 1)
    }
    else {
        LoadData(null, null, 1);
    }
});//nhan enter de search MAP
$(document).on("change", "#txtSearch", function () {
    $("#DanhSachNV").html("")
    $("#paginationMap").html("")
    var txtSearch = $("#txtSearch").val();
    if (txtSearch != "") {
        danhsachNVMap(null, txtSearch, 1)

    } else if (txtSearch != "") {
        danhsachNVMap(null, txtSearch, 1)

    }
    else {
        danhsachNVMap(null, null, 1);
    }
})
//nhan button de search MAP
$(document).on("click", "#subSearch", function () {
    $("#DanhSachNV").html("")
    $("#paginationMap").html("")
    var txtSearch = $("#txtSearch").val();
    if (txtSearch != "") {
        danhsachNVMap(null, txtSearch, 1)

    } else if (txtSearch != "") {
        danhsachNVMap(null, txtSearch, 1)
    }
    else {
        danhsachNVMap(null, null, 1);
    }
});
//dropdown phong ban
$(document).on("change", "#dropdownValue", function(){
    var txtPhongban = $("#dropdownValue").val();
    $("#tableViewAll").html("")
    $("#pagination").html("")
    if (txtPhongban > 0) {
        LoadData(txtPhongban, null, 1)
    }
    else {
        LoadData(null, null, 1);
    }
})

//dropdown get
function GetDropdown(){
    var SetData = $("#dropdownValue")
    SetData.html("")
    var Data2 = "<option value='0'>--Tất cả--</option>";
    SetData.append(Data2);
    $.ajax({
        type: 'GET',
        url: "/Staff/GetDropdown",
        success: function (res) {
            var Setpb = $("#phongban_id");
            var data = res.data;
            for (let i = 0; i < data.length; i++) {
                var Data = 
                "<option value='"+(i+1)+"'>" + data[i].ten_phong_ban + "</option>";
                SetData.append(Data);
                Setpb.append(Data);
            }
        },
        error: function (err) {
            console.log(err);
        }
    })   
}

function danhsachNVMap(txtPhongban, txtSearch, page) {
    $.ajax({
        type: 'GET',
        url: '/Staff/GetPaging',
        data: { txtPhongban: txtPhongban, txtSearch: txtSearch, page: page },
        success: function (res) {
            var tbLoi = res.status;
            if (tbLoi == "ERROR") {
                $.notify("Không có nhân viên nào", { position: "top center", autoHideDelay: 5000, className: "danger" })
            }
            var data = res.posts;
            var SetData = $("#DanhSachNV");
            for (var i = 0; i < data.length; i++) {
                var Data = "<tr class='tr_dataNV'>" +
                    "<td>" + data[i].ho_ten + "</td>" +
                    "<td>" + data[i].chuc_vu + "</td>" +
                    "<td id='info_td_click' class='"+ data[i].ma_nhanvien +"'>" + "<button type='button' id='valueshowinfo' value='"+ data[i].ma_nhanvien +"'><i id='icon-share' class='fas fa-share'></button>" + "</td>" +
                    "</tr>";
                SetData.append(Data);
            }
            //create pagination
            var pagination_string = "";
            var pageCurrent = res.page;
            var numSize = res.countPages;

            if (pageCurrent == (1)) {//page PREVIEWS disabled or enable
                pagination_string += '<li class="page-item disabled"><a class="page-link" data-page='
                    + (pageCurrent - 1) + '>Trước</a></li>';
            } else {
                pagination_string += '<li class="page-item"><a class="page-link" data-page='
                    + (pageCurrent - 1) + '>Trước</a></li>';
            }

            for (i = 1; i <= numSize; i++) {
                if (i == pageCurrent) {
                    pagination_string += '<li class="page-item active"><a class="page-link" data-page='
                        + i + '>' + pageCurrent + '</a></li>';
                } else {
                    pagination_string += '<li class="page-item"><a  class="page-link" data-page='
                        + i + '>' + i + '</a></li>';
                }
            }
            if (pageCurrent == (numSize)) {//page AFTER disabled or enable
                pagination_string += '<li class="page-item disabled"><a class="page-link" data-page='
                    + (pageCurrent + 1) + '>Sau</a></li>';
            } else {
                pagination_string += '<li class="page-item"><a class="page-link" data-page='
                    + (pageCurrent + 1) + '>Sau</a></li>';
            }
            $("#paginationMap").append(pagination_string);
        },
        error: function (err) {
            console.log(err);
        }
    })
}

function GetMap() {
    $.ajax({
        type: 'GET',
        url: '/Staff/GetMap',
        success: function (res) {
            var data = res.data;
            var map = L.map('map').setView([21.0281326552094, 105.8361773499927], 10);//set map

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            }).addTo(map);//set map



            // click show position
            var popup = L.popup();
            function onMapClick(e) {
                popup
                    .setLatLng(e.latlng)
                    .setContent("Vị trí trên map: " + e.latlng.toString())
                    .openOn(map);
            }
            map.on('click', onMapClick);

            var markersLayer = L.featureGroup().addTo(map)//feature de up properties
           
            var marker = [];//khai bao marker

            //market all nhan vien
            for (var i = 0; i < data.length; i++) {
                marker[i] = L.marker([data[i].x, data[i].y],{title: 'Thông tin nhân viên'}).addTo(map);//set vi tri marker

                marker[i].feature = {//feature properties
                    type: 'Feature', 
                    properties: { ma_nhanvien: data[i].ma_nhanvien
                                , ho_ten: data[i].ho_ten 
                                , ngay_sinh: data[i].ngay_sinh 
                                , sdt: data[i].sdt 
                                , dia_chi: data[i].dia_chi 
                                , chuc_vu: data[i].chuc_vu 
                                , phong_ban: data[i].phong_ban
                                }, 
                    geometry: undefined 
                }
                var marker_Feature = marker[i].feature.properties;

                var DSNV = []
                DSNV[i] = "MNV: " + marker_Feature.ma_nhanvien + "," + " Họ tên: " + marker_Feature.ho_ten + "," + " SĐT: " + marker_Feature.sdt + "," + " Chức vụ: " + marker_Feature.chuc_vu + ","
                    + " Địa chỉ: " + marker_Feature.dia_chi + "," + " Phòng ban: " + marker_Feature.phong_ban.ten_phong_ban;

                marker[i].addTo(markersLayer)
                marker[i].bindPopup(DSNV[i]); // show thong tin nv

                
            }//end for

            

            markersLayer.on("click", markerOnClick);//click tuong tac 2 chieu

            function markerOnClick(e) {
              var attributes = e.layer.feature.properties;//call layer 
              
              var elems = document.querySelectorAll("#info_td_click");//all id

              for (let i = 0; i < elems.length; i++) {
                  var class_button_nv = elems[i].className;
                  
                  if (attributes.ma_nhanvien == class_button_nv) {
                      $("." + elems[i].className + "").parent().css("background-color", "#a5df5a");
                    
                  }else{
                    $("."+elems[i].className+"").parent().css("background-color", "#95ed71");
                  }
              }
              
            }


            //click
            $(document).on("click", "#valueshowinfo", click_showInfomation);
            function click_showInfomation() {
                var elems = document.querySelectorAll("#info_td_click");//all id
                var val_elems = $(this).val()
                for (let i = 0; i < data.length; i++) {
                    if (val_elems == data[i].ma_nhanvien) {
                        map.setView([data[i].x, data[i].y], 15);
                        marker[i].openPopup();
                        $(".tr_dataNV ." + data[i].ma_nhanvien + "").parent().css("background-color", "#a5df5a");
                    } else {
                        $(".tr_dataNV ." + data[i].ma_nhanvien + "").parent().css("background-color", "#95ed71");
                    }
                }
            }


        },
        error: function (err) {
            console.log(err)
        }
    })
}






