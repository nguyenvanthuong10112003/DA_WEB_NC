﻿var namePage;
var setting = (namePage) => this.namePage = namePage
var alertDelete = () => "Việc xóa " + namePage + " sẽ xóa tất cả các thông tin liên quan, bạn có chắc chắn muốn xóa không?"
var alertDeletes = (item) => "Việc xóa " + namePage + " sẽ xóa tất cả các thông tin liên quan, bạn có chắc chắn muốn xóa " + item + " bản ghi đã chọn không?"
var noiDungCanhBao = $('Dialog_content__fC8ze');
var createContent = function () {
  return `
        <div id="toast"></div>
        <div class="content-header">
            <h1 class="text-dark">Danh sách ${namePage}</h1>
        </div>
        <!-- Main content -->
        <div class="functions">
            <ul class="list-function">
                ${hanhDong && hanhDong.add ?
                `
                <li class="function add">
                    <i class="fa-solid fa-plus"></i>Thêm
                </li>
                `
                : ''}
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
                        <h3 class="card-title">Tìm kiếm ${namePage}</h3>
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

                <div class="card" id="content-table">
                    
                </div>
            </div>
        </div>
        ${hanhDong && hanhDong.add ? 
        `<div id="part-add">
                <div class="card card-default" id="content-add">
                    <div>
                        <div class="card-tools">
                            <button type="button" class="btn btn-tool btn-close-form" data-card-widget="remove">
                                <i class="fas fa-times" style="font-size: 18px;"></i>
                            </button>
                        </div>
                    </div>
                </div>
        </div>` : ''
        }
        <div id="alert-canh-bao">
            <div class="Dialog_wrapper__5aD4q">
                <div class="Dialog_overlay__27wcK">
                </div>
            <div class="Dialog_main__PPXtm">
                <div class="Dialog_header__0dUcJ">
                    <h3 class="Dialog_title__+aMqC">
                        Cảnh báo
                    </h3>
                    <button class="Dialog_close-button__3tLfG">
                        <svg aria-hidden="true" focusable="false" data-prefix="fas" data-icon="circle-xmark" class="svg-inline--fa fa-circle-xmark " role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                            <path fill="currentColor" d="M0 256C0 114.6 114.6 0 256 0C397.4 0 512 114.6 512 256C512 397.4 397.4 512 256 512C114.6 512 0 397.4 0 256zM175 208.1L222.1 255.1L175 303C165.7 312.4 165.7 327.6 175 336.1C184.4 346.3 199.6 346.3 208.1 336.1L255.1 289.9L303 336.1C312.4 346.3 327.6 346.3 336.1 336.1C346.3 327.6 346.3 312.4 336.1 303L289.9 255.1L336.1 208.1C346.3 199.6 346.3 184.4 336.1 175C327.6 165.7 312.4 165.7 303 175L255.1 222.1L208.1 175C199.6 165.7 184.4 165.7 175 175C165.7 184.4 165.7 199.6 175 208.1V208.1z">
                            </path>
                        </svg>
                    </button>
                </div>
                <div class="Dialog_content-wrapper__oTjv7">
                    <div class="Dialog_content__fC8ze">
                        
                    </div>
                    <div class="Dialog_action__kFm-L">
                        <button class="btn btn-secondary Button_btn__RW1e2 Button_primary__86yfm Dialog_confirm-button__j4ByN" id="canh-bao-cancel">
                            Hủy bỏ
                        </button>
                        <button class="btn btn-primary Button_btn__RW1e2 Button_primary__86yfm Dialog_confirm-button__j4ByN" id="canh-bao-accept">
                            Đồng ý
                        </button>
                    </div>
                </div>
            </div>
        </div>
    `;
};
var createTable = function () {
                return `
                    <div class="card-header">
                        <b style="padding-top: 20px; font-size: 20px;">Tổng số: <span id="count-element">${countData}</span>${
                        " " + namePage
                        }</b>
                        <div class="header-functions d-flex flex-row align-items-center">
                            <span class="tablet-mr-10 page-size form-outline d-flex flex-nowrap align-items-center" style="margin-right: 10px">
                                <button type="button" disabled id="btn-delete-rows" class="btn btn-danger" style="border-radius: 30px; padding-left: 16px; padding-right: 16px; font-weight: 600">Xóa</button>
                            </span>
                            <span class="tablet-mr-10 page-size form-outline d-flex flex-nowrap align-items-center" style="border: 1px solid #d5d5d5; border-radius: 40px; padding: 8px; margin-right: 10px">
                                <label for="so-luong-hang" style="font-size: 16px; font-weight: 600">Số lượng hàng &nbsp;</label>
                                <input type="number" name="so-luong-hang" id="so-luong-hang" class="form-control" style="max-width: 50px; padding: 0" min="1" 
                                max="${countData > 100 ? 100 : countData}"
                                    value="${pageSize > countData ? countData : pageSize}"/>
                                &nbsp;
                                <a href="#!" id="btn-accept-size-page" class="btn btn-primary" style="border-radius: 30px">Xác nhận</a>
                            </span>
                            <span class="tablet-mr-10 sort-by d-flex flex-nowrap" style="border: 1px solid #d5d5d5; border-radius: 40px; padding: 8px; margin-right: 10px">
                                <input type="checkbox" name="sap-xep-tang-giam" id="sap-xep-tang-giam" ${sortTangDan ? '' : 'checked'}/>
                                <label for="sap-xep-tang-giam" style="margin-right: 10px; font-size: 16px; font-weight: 600">&nbsp; Sắp xếp giảm dần</label>
                            </span>
                            <div class="tablet-mr-10 header-sort d-block" style="margin-right: 10px">
                                <span class="text-sort position-relative d-flex flex-row align-items-center"
                                      onclick="event.stopPropagation()">
                                    <i class="fa-solid fa-sort" style="margin-right: 5px">
                                    </i>Sắp xếp
                                </span>
                                <ul class="header-sort-list position-absolute d-flex flex-column d-none z-3">
                                    ${usingIndex.reduce(
                                        function (result, item, index) {
                                            if (!item)
                                                return result;
                                            let id = nameObj[nameTiengViets[index]].id
                                            let name = nameTiengViets[index]
                                            let nameTT = nameItems[index]
                                            return (
                                                result +
                                            `
                                            <li>
                                                <label for="${id}-sort">${name}</label>
                                                <input class="sort-item" type="radio" name="sortBy" 
                                                id="${id}-sort"
                                                value="${nameTT}"
                                                ${sortBy == nameTT ? "checked" : ""}/>
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
                                    ${usingIndex.reduce(
                                        function (result, item, index) {
                                            if (!item)
                                                return result;
                                            let name = nameTiengViets[index];
                                            let id = nameObj[name].id 
                                            return (
                                            result +
                                            `
                                            <li>
                                                <label for="${id}-view">
                                                    ${name}
                                                </label>
                                                <input class="view-item" type="checkbox"
                                                    name="view" 
                                                    id="${id}-view"
                                                    value="${name}" 
                                                    ${nameObj[name].using ? "checked" : ""}/>
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
                                 <table class="table table-hover" id="table-job" style="overflow: auto;">
                                    <thead>
                                        <tr class="align-center">
                                        <th width="50">
                                            <label class="icheck-primary mb-0 d-block">
                                                <input type="checkbox" value="all" id="table-select-all">
                                            </label>
                                        </th>
                                        ${usingIndex.reduce(function (result, item, index) {
                                            if (!item)
                                                return result;
                                            return (
                                                result +
                                                `<th class="${nameObj[nameTiengViets[index]].using ? "" : "d-none"}">${nameTiengViets[index]}</th>`
                                            );
                                        }, "")}
                                        ${hanhDong ? `<th>Thao tác</th>` : ''}
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
                                                    ${usingIndex.reduce(function (result1, item1, index) {
                                                        if (!item1)
                                                            return result1
                                                        return result1 + createRowTable(item, nameItems[index], index, id);
                                                    }, "")}
                                                    ${hanhDong ? createActionOnRow(id) : ''}
                                                </tr>
                                                `
                                            );
                                        }, "")
                                        : ""
                                    }
                                    </tbody>
                                 </table>
                            </div>
                        </form>
                    </div>

                    <!-- /.card-body -->
                    <div class="card-footer clearfix p-0"
                         style="padding-top: 10px !important">
                        <ul class="pagination pagination-sm m-0 float-right">
                      ${countData == 0 ? '' : `
                       <li class="page-item" onclick="toPage(1)">
                           <a class="page-link" href="#!"><< First</a>
                       </li>
                       ${countData > pageSize ? `
                       ${
                        [""].reduce(function (result) {
                         let arr = [];
                         let str = "";
                         let t = 3;
                         let s = 3;
                         let n = (countData % pageSize == 0 ? countData / pageSize : countData / pageSize + 1)
                         for (let i = s; i >= 1; i--) {
                             if (page - i >= 1) {
                                 arr = [...arr, page - i]
                           }
                         }
                         if (n > s * 2 && arr.length < 3)
                            s += s - arr.length;
                         arr = [...arr, page];
                         for (let i = 1; i <= s; i++) {
                            if (page + i <= n) {
                                arr = [...arr, page + i];
                            } else break;
                         }
                         if (arr.length < t * 2 + 1) {
                            for (let i = 1; arr.length < t * 2 + 1; i++)
                                if (arr[0] - 1 >= 1) 
                                    arr = [arr[0] - 1, ...arr]
                                else break;

                         }
                         if (arr[0] > 1) {
                            result += `<li class="page-item">
                                <a class="page-link" style="color: gray" href="#!">...</a>
                            </li>`;
                         }
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

                         if (arr[arr.length - 1] < n - 1)
                           result += `<li class="page-item" >
                                       <a class="page-link" style="color: gray" href="#!">...</a>
                                   </li >`;
                         return result;
                        }, "")
                        }` :
                        `
                        <li class="page-item" onclick="toPage(1)">
                            <a class="page-link active" href="#!">1</a>
                        </li>`
                       }
                       <li class="page-item"
                           onclick="toPage(${parseInt(countData % pageSize == 0
                               ? countData / pageSize : countData / pageSize + 1)})">
                           <a class="page-link" href="#!">End >></a>
                       </li>`
                      }
                        </ul>
                        <button type="button"
                                class="btn btn-success btn-xuat-file float-end">
                            <span>Xuất file</span>
                            <img src="/icons/excel.png"
                                 alt="icon-excel"
                                 style="width: 32px" />
                        </button>
                    </div>
    `
};
var createForm = function (result, thaotac) {
  let html = "";
  if (result) {
    let id = result[nameObj[nameTiengViets[0]].id];
    if (id) id = id.trim();
    let gioitinh = {};
    return hanhDong[thaotac] ? `
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
                            ${usingIndex.reduce(
                                function (result1, item, index) {
                                    if (!item)
                                        return result1
                                    return (
                                        result1 +
                                        `<div class="col-md-6">
                                            <div class="form-group">
                                                <label>${nameTiengViets[index]}</label>
                                                ${createInputFormAddOrUpdate(
                                                  thaotac,
                                                    result,
                                                    nameTiengViets[index],
                                                  index, 
                                                  id
                                                )}
                                            </div>
                                            ${
                                              thaotac == "search"
                                            ? ""
                                            : `<span id="error-${nameObj[nameTiengViets[index]].id}-${thaotac}-${id}" class="text-danger"></span>`
                                            }
                                        </div>`
                                    );
                                },
                            "")}
                            <div class="col-md-12">
                                <div class="form-group btns">
                                    <button class="btn btn-primary" id="${thaotac}-submit" onclick="thucHienHanhDongDenServer(this, '${thaotac}')" type="button">
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
        ` : '';
  }
};
var ready = function () {
    let danhSachHienThi = $('.header-view-list');
    let danhSachSapXep = $('.header-sort-list');
    let nutXuatFile = $('.btn-xuat-file');
    let nutChonTatCaBang = $('#table-select-all');
    let nutChapNhanThayDoiSoLuongHang = $('#btn-accept-size-page');
    let nutDongMoDanhSachHienThi = $('.text-view');
    let nutDongMoDanhSachSapXep = $('.text-sort');
    let nutXacNhanSapXep = $('.btn-sort');
    let nutXacNhanHienThi = $('#btn-view');
    let nutChonTatCaDanhSachHienThi = $('.view-item:first');
    let oNhapSoLuongHang = $('#so-luong-hang');
    let nutThayDoiCheDoSapXep = $('#sap-xep-tang-giam');
    let nutChonHang = $('.table-select');
    let nutXoaCacHangDaChon = $('#btn-delete-rows');
    let nutXoaHang = $('.nut-xoa-hang')
    nutChonHang.change(function () {
        if (nutChonHang.toArray().some(item => $(item).prop('checked')))
            nutXoaCacHangDaChon.removeAttr('disabled');      
        else
            nutXoaCacHangDaChon.attr('disabled', true)
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
        danhSachHienThi.addClass('d-none')
        danhSachSapXep.addClass('d-none')
        $('.sort-item').toArray().forEach(function (element) {
            if ($(element).prop('checked')) {
                sortBy = $(element).val();
            }
        })
        loadDataTable()
    })
    nutXacNhanHienThi.click(function () {
        danhSachHienThi.addClass('d-none')
        danhSachSapXep.addClass('d-none')
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
        if ($(this).val() > parseInt($(this).attr('max')))
            $(this).val(parseInt($(this).attr('max')));
    })
    nutChapNhanThayDoiSoLuongHang.click(function () {
        pageSize = parseInt($('#so-luong-hang').val());
        loadDataTable();
    })
    nutChonTatCaBang.change(function () {
        if ($(this).prop('checked')) {
            $('.table-select').prop('checked', true);
            nutXoaCacHangDaChon.removeAttr('disabled');
        } else {
            $('.table-select').prop('checked', false);
            nutXoaCacHangDaChon.attr('disabled', true)
        }
    })
    nutXuatFile.click(function () {
        var table2excel = new Table2Excel();
        table2excel.export($('table'), 'thống kê ' + namePage);
    })
    nutThayDoiCheDoSapXep.change(function () {
        sortTangDan = !nutThayDoiCheDoSapXep.prop('checked');
        loadDataTable()
    })
    nutXoaHang.click(function (e) {
        let id = $(e.currentTarget).attr('id').split('-')
        onCanhBao(alertDelete(), yeuCauXoaDenServer, id[id.length - 1])
    })
    nutXoaCacHangDaChon.click(function () {
        let arrId = []
        nutChonHang.toArray().forEach(
            item => {
                if ($(item).prop('checked')) {
                    let id = $(item).attr('id').split('-');
                    arrId = [...arrId, id[id.length - 1]]
                }
            }
        )
        onCanhBao(alertDeletes(arrId.length), yeuCauXoaDenServer, arrId)
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
        openEdit(editor);
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
    loadDataTable();
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
                        new Promise(f => loadDataTable(f))
                            .then(f => {
                                if (action == 'add') {
                                    $('#part-add').hide()
                                } else if (action == 'edit') {
                                    cancel(element)
                                }
                                toast({ title: 'Thành công', message: data['success'], type: 'success', duration: 2000 })
                            })
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
    let url = `${window.location.origin}/${xoaKhoangTrang(removeVietnameseTones(namePage))}/delete`;
    let success = function (data) {
        if (data['success']) {
            new Promise(f => loadDataTable(f))
                .then(f => {
                    toast({ title: 'Thành công', message: data['success'], type: 'success', duration: 2000 })
                    $('#count-element').text(parseInt($('#count-element').text()) > 0 ? (parseInt($('#count-element').text()) - 1) : 0);
                })
        }
        if (data['error'] && data['error'].length > 0) {
            toast({ title: 'Thất bại', message: data['error'], type: 'error', duration: 2000 })
        }
    }
    let error = function (message) {
        alert('error')
        console.log(message)
    }
    ajaxToServer(url,
        ma,
        success,
        error
    )
}
var ajaxToServer = function (url, data, success, error) {
    $.ajax({
        url: url,
        data: { mas: data },
        contentType: "application/json;charset=utf-8",
        dataType: 'json',
        traditional: true,
        success: success,
        error: error
    })
}
var loadData = function (myResolve, myReject) {
    let url = `${window.location.origin}/${xoaKhoangTrang(removeVietnameseTones(namePage))}/getAll`;
    let data = {
        page: page,
        pageSize: pageSize,
        sortBy: sortBy,
        sortTangDan: sortTangDan
    }
    for (let i = 0; i < count; i++)
        data[nameItems[i]] = findBy[nameItems[i]]
    $.ajax({
        url: url,
        data: data,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            datas = result["data"];
            countData = result["countData"]
            let n = countData % pageSize == 0 ? countData / pageSize : countData / pageSize + 1;
            n = parseInt(n)
            if (page > n)
                page = n;
            if (myResolve && typeof myResolve == 'function')
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
    let objs = {}
    for (let i = 0; i < nameTiengViets.length; i++) {
        objs[nameObj[nameTiengViets[i]].id] = findBy[nameItems[i]]
    }
    root.html(createContent());
    $("#content-add").html($("#content-add").html() + createForm({}, "add"));
    $('#content-search').html(
        $('#content-search').html() + createForm(objs, "search")
    );
    let nutDongYTimKiem = $('#search-submit');
    let nutAnHienBoxNhapDuLieu = $('.btn-slide-form');
    let nutDongForm = $('.btn-close-form');
    let formThem = $('#part-add');
    let idFormThem = 'content-add';
    let nutMoForm = $('.function');
    let formTimKiem = $('#content-search');
    let oNhapDuLieuFormAdd = $('.input-add');
    let oRadioButtonFormAdd = $('.input-radio-add');
    let thongBaoNen = $('#alert-canh-bao');
    formTimKiem.slideUp(0);
    formThem.hide();
    thongBaoNen.hide();
    nutAnHienBoxNhapDuLieu.click(function (e) {
        $($(e.currentTarget).parent().parent().parent().children()[1]).slideToggle();
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
            let current = new Date()
            $('.dateNowOutput').val(
                toDateInput(current.getDate() + '-' + (current.getMonth() + 1) + '-' + current.getFullYear())
            )
        }
    })
    nutDongYTimKiem.click(function () {
        for (let i = 0; i < count; i++) {
            let idName = nameObj[nameTiengViets[i]].id + '-' + 'search'
            findBy[nameItems[i]] = convertDataToServer(idName, i)
            findBy[nameItems[i]] = findBy[nameItems[i]] ? findBy[nameItems[i]] : ''
        }
        loadDataTable()
    })
};
var loadTable = function (f) {
    $("#content-table").html(createTable());
    if (f && typeof f == 'function') 
        f()
};
var loadDataTable = function (f) {
    new Promise(f => loadData(f))
        .then(f => loadTable(f))
        .then(f => $(document).ready(ready))
    if (f && typeof f == 'function')
        f()
}
var load = function (f) {
    new Promise(f => {
        loadData(f)
    }).then(f => {
        loadPage(f)
    }).then(f => {
        loadTable()
    }).then(f => {
        $(document).ready(ready)
    })
    if (f && typeof f == 'function')
        f()
}
var onCanhBao = function (content, fun, data) {
    $('.Dialog_content__fC8ze').text(content);
    $('#alert-canh-bao').show();
    $('#canh-bao-cancel').click(function () {
        $('#alert-canh-bao').hide();
    });
    $('.svg-inline--fa.fa-circle-xmark').click(function () {
        $('#alert-canh-bao').hide();
    })
    $('#canh-bao-accept').click(function () {
        $('#alert-canh-bao').hide();
        fun(data);
        fun = function () { }
    })
}