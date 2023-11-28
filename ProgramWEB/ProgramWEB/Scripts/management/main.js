var createContent = function () {
  return `
        <div id="toast"></div>
        <div class="content-header">
            <h1 class="text-dark">Danh sách ${namePage}</h1>
        </div>
        <!-- Main content -->
        <div class="functions">
            <ul class="list-function">
                <li class="function add">
                    <i class="fa-solid fa-plus"></i>Thêm
                </li>
                <li class="function search">
                    <i class="fa-solid fa-magnifying-glass"></i>Tìm kiếm
                </li>
            </ul>
        </div>
        <div class="content">
            <div class="container-fluid" style="padding: 0">
                <div class="card card-default"
                     id="content-search"
                     style="display: none">
                    <div class="card-header">
                        <h3 class="card-title">Tìm kiếm thành viên</h3>
                        <div class="card-tools">
                            <button type="button"
                                    class="btn btn-tool btn-slide-form"
                                    data-card-widget="collapse">
                                <i class="fas fa-minus"></i>
                            </button>
                            <button type="button"
                                    class="btn btn-tool btn-close-form"
                                    data-card-widget="remove">
                                <i class="fas fa-times"></i>
                            </button>
                        </div>
                    </div>
                </div>

                <div class="card">
                    <div class="card-header">
                        <b style="padding-top: 20px; font-size: 20px;">Tổng số: <span id="count-element">${countData}</span>${
                        " " + namePage
                        }</b>
                        <div class="header-functions d-flex flex-row align-items-center">
                            <span class="tablet-mr-10 page-size form-outline d-flex flex-nowrap align-items-center" style="border: 1px solid #d5d5d5; border-radius: 40px; padding: 8px; margin-right: 10px">
                                <label for="so-luong-hang" style="font-size: 16px; font-weight: 600">Số lượng hàng &nbsp;</label>
                                <input type="number" name="so-luong-hang" id="so-luong-hang" class="form-control" style="max-width: 50px; padding: 0" min="1" max="${
                                  countData > 100 ? 100 : countData
                                }"
                                    value="${pageSize}"/>
                                &nbsp;
                                <a href="#!" id="btn-accept-size-page" class="btn btn-primary" style="border-radius: 30px">Xác nhận</a>
                            </span>
                            <span class="tablet-mr-10 sort-by d-flex flex-nowrap" style="border: 1px solid #d5d5d5; border-radius: 40px; padding: 8px; margin-right: 10px">
                                <input type="checkbox" name="sap-xep-tang-giam" id="sap-xep-tang-giam" />
                                <label for="sap-xep-tang-giam" style="margin-right: 10px; font-size: 16px; font-weight: 600">&nbsp; Sắp xếp giảm dần</label>
                            </span>
                            <div class="tablet-mr-10 header-sort d-block" style="margin-right: 10px">
                                <span class="text-sort position-relative d-flex flex-row align-items-center"
                                      onclick="event.stopPropagation()">
                                    <i class="fa-solid fa-sort" style="margin-right: 5px">
                                    </i>Sắp xếp
                                </span>
                                <ul class="header-sort-list position-absolute d-flex flex-column d-none z-3">
                                    ${usingIndex.reduce(function (
                                      result,
                                      item
                                    ) {
                                      return (
                                        result +
                                        `
                                            <li>
                                                <label for="${
                                                  nameObj[nameTiengViets[item]]
                                                    .id
                                                }-sort">${
                                          nameTiengViets[item]
                                        }</label>
                                                <input class="sort-item" type="checkbox" name="${
                                                  nameObj[nameTiengViets[item]]
                                                    .id
                                                }-sort" id="${
                                          nameObj[nameTiengViets[item]].id
                                        }-sort"
                                                    value="${
                                                      nameTiengViets[item]
                                                    }" ${
                                          nameObj[nameTiengViets[item]].sort ==
                                          true
                                            ? "checked"
                                            : ""
                                        } />
                                            </li>
                                        `
                                      );
                                    },
                                    "")}
                                    <li class="btn-sort">Sắp xếp</li>
                                </ul>
                            </div>
                            <div class="tablet-mr-10 header-view d-block">
                                <span class="text-view position-relative d-flex flex-row align-items-center"
                                      onclick="event.stopPropagation()">
                                    <i class="fa-solid fa-caret-down" style="margin-right: 5px">
                                    </i>Hiển thị
                                </span>
                                <ul class="header-view-list position-absolute d-flex flex-column d-none z-3">
                                    <li>
                                        <label for="select-all">Chọn tất cả</label>
                                        <input class="view-item" type="checkbox" name="select-all" id="select-all">
                                    </li>
                                    ${usingIndex.reduce(function (
                                      result,
                                      item
                                    ) {
                                      return (
                                        result +
                                        `
                                            <li>
                                                <label for="${
                                                  nameObj[nameTiengViets[item]]
                                                    .id
                                                }-view">${
                                          nameTiengViets[item]
                                        }</label>
                                                <input class="view-item" type="checkbox" name="${
                                                  nameObj[nameTiengViets[item]]
                                                    .id
                                                }-view" id="${
                                          nameObj[nameTiengViets[item]].id
                                        }-view"
                                                    value="${
                                                      nameTiengViets[item]
                                                    }" ${
                                          nameObj[nameTiengViets[item]].using ==
                                          true
                                            ? "checked"
                                            : ""
                                        } />
                                            </li>
                                        `
                                      );
                                    },
                                    "")}
                                    <li id="btn-view">Hiển thị</li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- /.card-header -->
                    <div class="card-body p-0">
                        <form method="POST"
                              action="https://1900.com.vn/admin/jobs/update-career">
                            <div class="table-responsive-xl" style="position: relative;">
                            </div>
                        </form>
                    </div>

                    <!-- /.card-body -->
                    <div class="card-footer clearfix p-0"
                         style="padding-top: 10px !important">
                        <ul class="pagination pagination-sm m-0 float-right">
                       <li class="page-item" onclick="toPage(1)">
                           <a class="page-link" href="#!"><< First</a>
                       </li>
                       ${[""].reduce(function (result) {
                         let arr = [];
                         let str = "";
                         let t = 3;
                         let s = 3;
                         for (let i = s; i >= 1; i--) {
                           if (page - i >= 1) {
                             arr[arr.length] = page - i;
                           }
                         }
                         if (arr.length < 3) s += s - arr.length;
                         arr[arr.length] = page;
                         for (let i = 1; i <= s; i++) {
                           if (page + i <= countData / pageSize) {
                             arr[arr.length] = page + i;
                           } else break;
                         }
                         if (arr.length < t * 2 + 1) {
                           for (let i = 1; arr.length < t * 2 + 1; i++)
                             if (arr[0] - 1 >= 1) {
                               arr = [arr[0] - 1, ...arr];
                             }
                         }
                         if (arr[0] > 1)
                           result += `<li class="page-item">
                                       <a class="page-link" style="color: gray" href="#!">...</a>
                                   </li>`;

                         result += arr.reduce(function (result, item) {
                           return (
                             result +
                             `
                                       <li class="page-item" onclick="toPage(${item})">
                                           <a class="page-link ${
                                             item == page ? "active" : ""
                                           }" href="#!">${item}</a>
                                       </li>`
                           );
                         }, "");

                         if (
                           arr[arr.length - 1] < parseInt(countData / pageSize)
                         )
                           result += `<li class="page-item" >
                                       <a class="page-link" style="color: gray" href="#!">...</a>
                                   </li >`;
                         return result;
                       }, "")}
                       <li class="page-item" onclick="toPage(${parseInt(
                         countData / pageSize
                       )})">
                           <a class="page-link" href="#!">End >></a>
                       </li>
                        </ul>
                        <button type="button"
                                class="btn btn-success btn-xuat-file float-end">
                            <span>Xuất file</span>
                            <img src="/icons/excel.png"
                                 alt="icon-excel"
                                 style="width: 32px" />
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div id="part-add">
                <div class="card card-default" id="content-add">
                    <div>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool btn-close-form" data-card-widget="remove">
                                <i class="fas fa-times" style="font-size: 18px;"></i>
                            </button>
                        </div>
                    </div>
                </div>
        </div>
    `;
};
var createTable = function () {
    return `
     <table class="table table-hover" id="table-job" style="overflow: auto;">
        <thead>
            <tr class="align-center">
            <th width="50">
                <label class="icheck-primary mb-0 d-block">
                    <input type="checkbox" value="all" id="table-select-all">
                </label>
            </th>
            ${nameTiengViets.reduce(function (result, item) {
                return (
                    result +
                    `<th class="${nameObj[item].using ? "" : "d-none"}">${item}</th>`
                );
            }, "")}
            <th>Thao tác</th>
            </tr>
        </thead>
        <tbody>
            ${datas != null
            ? datas.reduce(function (result, item) {
                let id = item[nameItems[0]].trim();
                return (
                    result +
                    `
                <tr id="${id}">
                    <td>
                        <label class="icheck-primary mb-0 d-block">
                            <input id="table-select-${id}" type="checkbox" value="${id}" name="delete[]"
                            class="records table-select">
                        </label>
                    </td>
                    ${nameItems.reduce(function (result1, item1, index) {
                        return result1 + createRowTable(item, item1, index, id);
                    }, "")}
                    ${createActionOnRow(id)}
                </tr>
                `
                );
            }, "")
            : ""
        }
        </tbody>
     </table>
    `;
};
var createForm = function (result, thaotac) {
  let html = "";
  if (result) {
    let id = result[nameObj[nameTiengViets[0]].id];
    if (id) id = id.trim();
    let gioitinh = {};
    html = `
                <div class="card-body form-input" id="${thaotac}">
                    <h3 class="card-title">${
                      thaotac == "edit"
                        ? "Sửa thông tin"
                        : thaotac == "add"
                        ? "Thêm " + namePage
                        : "Tìm kiếm"
                    } </h3>
                    <form method="GET" action="#" id="form-${id}">
                        <div class="row">
                            ${
                              thaotac == "search"
                                ? ""
                                : `<div class="alert alert-danger bg-danger text-light border-0 alert-dismissible fade show errorCL" id="error-${thaotac}-${id}" role="alert">
                                <span><span>
                                <button type="button" class="btn-close"><i class="fa-solid fa-xmark" style="color: #ffffff;"></i></button>
                            </div>
                            <div class="alert alert-success bg-success text-light border-0 alert-dismissible fade show successCL" id="success-${thaotac}-${id}" role="alert">
                                <span><span>
                                <button type="button" class="btn-close"><i class="fa-solid fa-xmark" style="color: #ffffff;"></i></button>
                            </div>`
                            }
                            ${nameTiengViets.reduce(function (
                              result1,
                              item,
                              index
                            ) {
                              return (
                                result1 +
                                `<div class="col-md-6">
                                    <div class="form-group">
                                        <label>${item}</label>
                                        ${createInputFormAddOrUpdate(
                                          thaotac,
                                          result,
                                          item,
                                          index,
                                          id
                                        )}
                                    </div>


                                    ${
                                      thaotac == "search"
                                        ? ""
                                        : `<span id="error-${nameObj[item].id}-${thaotac}-${id}" class="text-danger"></span>`
                                    }
                                </div>`
                              );
                            },
                            "")}
                            <div class="col-md-12">
                                <div class="form-group btns">
                                    <button class="btn btn-primary" onclick="thucHienHanhDongDenServer(this, '${thaotac}')" type="button">
                                        ${
                                          thaotac == "edit"
                                            ? "Xác nhận"
                                            : thaotac == "add"
                                            ? "Thêm"
                                            : "Tìm kiếm"
                                        }
                                    </button>
                                    <button class="btn btn-danger"
                                            type="button" onclick="resert(this)">
                                        Đặt lại
                                    </button>
                                        ${
                                          thaotac == "edit"
                                            ? `<button class="btn btn-secondary cancel"
                                            type="button" onclick="cancel(this)">
                                        Hủy
                                    </button>`
                                            : ""
                                        }
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
        `;
  }
  return html;
};
var ready = function () {
    let danhSachHienThi = $('.header-view-list');
    let danhSachSapXep = $('.header-sort-list');
    let nutXuatFile = $('.btn-xuat-file');
    let nutChonTatCaBang = $('#table-select-all');
    let nutChapNhanThayDoiSoLuongHang = $('#btn-accept-size-page');
    let nutAnHienBoxNhapDuLieu = $('.btn-slide-form');
    let nutDongForm = $('.btn-close-form');
    let formThem = $('#part-add');
    let boxNhapDuLieu = $('.form-input');
    let idFormThem = 'content-add';
    let nutMoForm = $('.function');
    let formTimKiem = $('#content-search');
    let oNhapDuLieuFormAdd = $('.input-add');
    let oRadioButtonFormAdd = $('.input-radio-add');
    let nutDongMoDanhSachHienThi = $('.text-view');
    let nutDongMoDanhSachSapXep = $('.text-sort');
    let nutXacNhanSapXep = $('.btn-sort');
    let nutXacNhanHienThi = $('#btn-view');
    let nutChonTatCaDanhSachHienThi = $('.view-item:first');
    let oNhapSoLuongHang = $('#so-luong-hang');
    function upDown(parent, child) {
        if (child) {
            parent.children(`.${child.attr('class').replace(' ', '.')}`).slideToggle();
        } else
            parent.slideToggle();
    }
    nutAnHienBoxNhapDuLieu.click(function (e) {
        upDown($(e.currentTarget).parent().parent().parent(),);
        $(e.currentTarget).children('i').attr('class',
            $(e.currentTarget).children('i')
                .attr('class') == 'fas fa-minus' ? 'fa-solid fa-plus' : 'fas fa-minus'
        );
    })
    nutDongForm.click(function (e) {
        if ($(e.currentTarget).parent().parent().parent().attr('id') == idFormThem) {
            formThem.hide()
        }
        else
            $(e.currentTarget).parent().parent().parent().slideUp();
    })
    nutMoForm.click(function (e) {
        if ($(e.currentTarget).attr('class').indexOf('search') != -1)
            formTimKiem.slideDown();
        else if ($(e.currentTarget).attr('class').indexOf('add') != -1) {
            formThem.show()
            oNhapDuLieuFormAdd.val('')
            oRadioButtonFormAdd.prop('checked', false)
        }
    })
    nutDongMoDanhSachHienThi.click(function () {
        danhSachHienThi.toggleClass('d-none');
        danhSachSapXep.addClass('d-none');
    })
    nutDongMoDanhSachSapXep.click(function () {
        danhSachSapXep.toggleClass('d-none');
        danhSachHienThi.addClass('d-none');
    })
    nutXacNhanSapXep.click(function () {
        for (let e of $(this).parent().children()) {
            console.log($(e).val() + "\n");
        }
        danhSachHienThi.addClass('d-none');
    })
    nutXacNhanHienThi.click(function () {
        for (let item of $('.view-item')) {
            if (nameObj[$(item).val()]) {
                if ($(item).prop('checked')) {
                    nameObj[$(item).val()].using = true;
                } else {
                    nameObj[$(item).val()].using = false;
                }
            }
        }
        loadTable()
    })
    nutChonTatCaDanhSachHienThi.change(function () {
        if ($(this).prop('checked')) {
            $('.view-item').prop('checked', true);
        } else {
            $('.view-item').prop('checked', false);
        }
    })
    oNhapSoLuongHang.change(function () {
        if ($(this).val() > 100)
            $(this).val(100);
    })
    nutChapNhanThayDoiSoLuongHang.click(function () {
        pageSize = parseInt($('#so-luong-hang').val());
        console.log(pageSize);
        load();
    })
    nutChonTatCaBang.change(function () {
        if ($(this).prop('checked')) {
            $('.table-select').prop('checked', true);
        } else {
            $('.table-select').prop('checked', false);
        }
    })
    nutXuatFile.click(function () {
        var table2excel = new Table2Excel();
        console.log('ok')
        table2excel.export($('table'), 'thống kê ' + namePage);
    })
    $('.errorCL').hide()
    $('.successCL').hide()
}
var openEdit = function (e) {
    let edit = $(e).parent().parent();
    let id = edit.attr('id');
    let date = edit.children(`#${nameObj[nameTiengViets[3]].id}-${id}`).text();
    let result = {}
    for (let item of nameTiengViets) {
        result[nameObj[item].id] = edit.children(`#${nameObj[item].id}-${id}`).text()
    }
    $('td').removeClass('d-none')
    for (let e of edit.children('td')) {
        e = $(e)
        e.attr('class', e.attr('class') ? (e.attr('class') + (e.attr('class').indexOf('d-none') == -1 ? ' d-none' : '')) : 'd-none')
    }
    $('.form-edit').remove();
    edit.html(edit.html() + `<td colspan="${count + 2}" id="form-edit-${id}" class="form-edit">${createForm(result, 'edit')}</td>`);
    ready()
}
var cancel = function (e) {
    let edit = $(e).parent().parent().parent().parent().parent().parent().parent();
    let id = edit.attr('id');
    edit.children('td').removeClass('d-none');
    for (let item of nameTiengViets) {
        if (!nameObj[item].using)
            edit.children(`#${nameObj[item].id}-${id}`).addClass('d-none');
    }
    edit.children(`#form-edit-${id}`).addClass('d-none');
}
var resert = function (e) {
    let editor = $(e).parent().parent().parent().parent().parent();
    if (editor.attr('id') == 'edit') {
        edit(editor);
    } else if (editor.attr('id') == 'add') {
        $('#add').remove();
        $('#content-add').html($('#content-add').html() + createForm({}, 'add'));
    } else if (editor.attr('id') == 'search') {
        $('#search').remove();
        $('#content-search').html($('#content-search').html() + createForm({}, 'search'));
    }
    ready()
}
var toPage = function (index) {
    page = index;
    load();
}
var thucHienHanhDongDenServer = function (element, action) {
    if (element) {
        let can = true;
        let obj = {}
        let id = $(element).parent().parent().parent().parent().attr('id').split('-');
        id = id ? id[id.length - 1] : undefined;
        $('#error-' + action + '-' + id).hide()
        $('#success-' + action + '-' + id).hide()
        for (let i = 0; i < nameTiengViets.length; i++) {
            let idName = nameObj[nameTiengViets[i]].id + '-' + action + '-' + id;
            let message;
            obj[nameItems[i]] = convertDataToServer(idName, i)
            message = checkDataToServer(obj[nameItems[i]], i)
            $('#error-' + idName).html(message ? message : '')
            if (message) {
                can = false;
            }
        }
        if (can) {
            let url = window.location.origin + '/' + xoaKhoangTrang(removeVietnameseTones(namePage)) + '/' + action;
            ajaxToServer(url, obj,
                function (data) {
                    if (data['error']) {
                        $('#error-' + action + '-' + id).text(data['error'])
                        $('#error-' + action + '-' + id).show()
                    }
                    if (data['success']) {
                        loadDataTable()
                        if (action == 'add') {
                            $('#part-add').hide()
                        } else if (action == 'edit') {
                            cancel(element)
                        }
                        toast({ title: 'Thành công', message: data['success'], type: 'success', duration: 2000 })
                    }
                },
                function (data) {
                    alert('error')
                    console.log(data)
                }
            )
        }
    }
}
var yeuCauXoaDenServer = function (ma) {
    let url = `${window.location.origin}/${xoaKhoangTrang(removeVietnameseTones(namePage))}/delete?ma=${ma}`;
    let success = function (data) {
        if (data['success']) {
            countData--;
            loadDataTable()
            toast({ title: 'Thành công', message: data['success'], type: 'success', duration: 2000 })
            $('#count-element').text(parseInt($('#count-element').text()) > 0 ? (parseInt($('#count-element').text()) - 1) : 0);
        }
        else if (data['error']) {
            toast({ title: 'Thất bại', message: data['error'], type: 'error', duration: 2000 })
        }
    }
    let error = function (message) {
        alert('error')
        console.log(message)
    }
    ajaxToServer(url,
        {},
        success,
        error
    )
}
var ajaxToServer = function (url, data, success, error) {
    $.ajax({
        url: url,
        data: data,
        contentType: "application/json;charset=utf-8",
        dataType: 'json',
        success: success,
        error: error
    })
}
var loadData = function (myResolve, myReject) {
    let url = `${window.location.origin}/${xoaKhoangTrang(removeVietnameseTones(namePage))}/getAll?page=${page}&pageSize=${pageSize}`;
    $.ajax({
        url: url,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            datas = result["data"];
            countData = result["countData"]
            if (myResolve)
                myResolve()
        },
        error: function (errormessage) {
            alert('T.T Huhuhu, lỗi mất rùi. \nThử lại sau nhé...')
            if (myReject)
                myReject(errormessage)
        }
    });
}
var loadPage = function (f) {
    root.html(createContent());
    $("#content-add").html($("#content-add").html() + createForm({}, "add"));
    $("#content-search").html(
        $("#content-search").html() + createForm({}, "search")
    );
    $("#content-search").slideUp(0);
    $("#part-add").hide();
    $(".header-sort-list").addClass("d-none");
    $(".header-view-list").addClass("d-none");
    f()
};
var loadTable = function () {
    $(".table-responsive-xl").html(createTable());
};
var loadDataTable = function () {
    new Promise(f =>
    {
        loadData(f)
    }).then(f =>
    {
        loadTable(f)
    }).then(f =>
    {
        $(document).ready(ready);
    })
}
var load = function () {
    new Promise(f => {
        loadPage(f)
    }).then(f => {
        loadDataTable()
    })
}