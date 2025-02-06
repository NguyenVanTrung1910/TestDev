
String.prototype.convertToDateLocal = function () {
    const [day, month, year] = this.split('/');
    return `${month}/${day}/${year}`;
};

function openModalWithApi(apiUrl, width) {
    var widthModal = ""
    switch (width) {
        case "full":
            widthModal = "modal-fullscreen";
            break;
        case "sm":
            widthModal = "modal-sm";
            break;
        case "lg":
            widthModal = "modal-lg";
            break;
        case "xl":
            widthModal = "modal-xl";
            break;
    }
    var randomId = 'Modal' + Math.random().toString(36).substring(7);
    var modalHtml = `<div class="modal fade" id="${randomId}" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                      <div class="modal-dialog ${widthModal}">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h5 class="modal-title" id="${randomId}ModalLabel">Nội dung từ API</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                          </div>
                          <div class="modal-body">
                            <div class="text-center loading">
                              <div class="spinner-border text-primary" role="status">
                                <span class="visually-hidden">Loading...</span>
                              </div>
                              <p>Đang tải dữ liệu...</p>
                            </div>
                            <div class="modalContent" style="display: none;"></div>
                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Đóng</button>
                          </div>
                        </div>
                      </div>
                    </div>`;

    // Thêm modal vào body
    $("body").append(modalHtml);


    var myModal = new bootstrap.Modal($(`#${randomId}`));
    myModal.show();
    $.get(apiUrl, function (html) {
        $(`#${randomId} .loading`).hide();
        $(`#${randomId} .modalContent`).html(html).show();
    }).fail(function (error) {
        $(`#${randomId} .loading`).hide();
        $(`#${randomId} .modalContent`).html("Đã có lỗi xảy ra").show();
        console.error('Lỗi:', error);
    });

    $('#' + randomId).on('hidden.bs.modal', function () {
        $(this).remove();
    });
}
/**
 * Chuyển hướng trang
 * 
 * @param {string} redirectType - Loại chuyển hướng ('back' hoặc 'external',hoặc 'reload')
 * @param {string} url - Đường dẫn URL cần chuyển hướng đến
 * @param {string} redirectMode - Chế độ chuyển hướng ('new_tab' hoặc 'same_tab')
 */
function ChuyenHuong(redirectType, url, redirectMode, gridName) {
    if (redirectType === 'back') {
        // Chuyển hướng back về đường dẫn lần trước
        window.history.back();

    }
    else if (redirectType == 'backWithReloadGrid') {
        // Chuyển hướng back về đường dẫn lần trước
        if (!gridName) {
            alert("Điều hướng thất bại! Phương thức này cần tham số bắt buộc là gridName");
            return;
        }
        sessionStorage.setItem('backWithReloadGrid' + gridName, 'active');
        window.history.back();
    }
    else if (redirectType === 'external') {
        // Chuyển hướng về trang khác
        if (redirectMode === 'new_tab') {
            // Mở tab mới
            window.open(url, '_blank');
        } else {
            // Dữ nguyên tab hiện tại
            window.location.href = url;
        }
    } else if (redirectType === 'reload') {
        // Reload trang hiện tại
        window.location.reload();
    }
}


/**
 * Hiển thị thông báo dạng toast.
 * 
 * @param {string} type - Loại thông báo (primary, secondary, success, danger, warning, info, light, dark)
 * @param {string} message - Nội dung thông báo
 */

function showNotification(type, message) {

    // Tạo toast container nếu chưa tồn tại
    let toastContainer = document.querySelector(".toast-container");
    if (!toastContainer) {
        toastContainer = document.createElement("div");
        toastContainer.classList.add("toast-container", "position-absolute","mt-5", "top-0","end-0", "p-3", "toast-index", "toast-rtl", "custom-toast-z-index");
        document.body.appendChild(toastContainer);
    }
    toastContainer.style.zIndex = "2147483647";
    // Tạo toast element
    const toast = document.createElement("div");
    toast.classList.add("toast", "fade", "hide");
    toast.setAttribute("role", "alert");
    toast.setAttribute("aria-live", "assertive");
    toast.setAttribute("aria-atomic", "true");

    // Tạo nội dung toast
    const toastBody = document.createElement("div");
    toastBody.classList.add("d-flex", "justify-content-between");
    if (type === "primary" ||
        type === "secondary" ||
        type === "success" ||
        type === "danger" ||
        type === "warning" ||
        type === "info" ||
        type === "light" ||
        type === "dark") {
        toastBody.classList.add("bg-" + type, "text-white");
    }
    const bodyContent = document.createElement("div");
    bodyContent.classList.add("toast-body");
    bodyContent.textContent = message;
    const closeBtn = document.createElement("button");
    closeBtn.classList.add("btn-close", "btn-close-white", "me-2", "m-auto");
    closeBtn.setAttribute("type", "button");
    closeBtn.setAttribute("data-bs-dismiss", "toast");
    closeBtn.setAttribute("aria-label", "Close");

    // Gắn nội dung vào toast
    toastBody.appendChild(bodyContent);
    toastBody.appendChild(closeBtn);
    toast.appendChild(toastBody);

    // Thêm toast vào container
    toastContainer.appendChild(toast);

    // Hiển thị toast
    const toastElement = new bootstrap.Toast(toast);
    toastElement.show();
}
/**
 * Tạo thông báo có nút xác nhận
 * @param {string} message - Nội dung thông báo xuất hiện
 * @param {Function} actionYes - chức năng sẽ thực thi khi người dùng ấn đồng ý
 * @param {Function} actionNo - chức năng sẽ thực thi khi người dùng ấn đồng ý
 */
function showNotificationWithConfirm(message, actionYes, actionNo) {

    if (!message)
        message = "Bạn có chắc chắn muốn thực hiện hành động này?";
    function createBackdrop() {
        const backdrop = document.createElement('div');
        backdrop.classList.add('toast-backdrop');
        backdrop.style.position = 'fixed';
        backdrop.style.top = '0';
        backdrop.style.left = '0';
        backdrop.style.width = '100vw';
        backdrop.style.height = '100vh';
        backdrop.style.backgroundColor = 'rgba(0, 0, 0, 0.5)';
        backdrop.style.zIndex = '2147483646'; // Một số nhỏ hơn z-index của toast
        backdrop.style.opacity = '0';
        backdrop.style.transition = 'opacity 0.3s ease-in-out';
        document.body.appendChild(backdrop);

        setTimeout(() => {
            backdrop.style.opacity = '1';
        }, 10);

        return backdrop;
    }

    const backdrop = createBackdrop();


    // Tạo toast container nếu chưa tồn tại
    let toastContainer = document.querySelector(".toast-container-center");
    if (!toastContainer) {
        toastContainer = document.createElement("div");
        toastContainer.classList.add("toast-container-center", "position-fixed", "top-50", "start-50", "translate-middle", "custom-toast-z-index");
        document.body.appendChild(toastContainer);
    }
    toastContainer.style.zIndex = "2147483647";

    // Tạo toast element
    const toast = document.createElement("div");
    toast.classList.add("toast", "fade", "hide");
    toast.setAttribute("role", "alert");
    toast.setAttribute("aria-live", "assertive");
    toast.setAttribute("aria-atomic", "true");

    // Tạo nội dung toast
    const toastHeader = document.createElement("div");
    toastHeader.classList.add("toast-header", "d-flex", "align-items-center");

    const headerLogo = document.createElement("img");
    headerLogo.src = "/202404/images/icon-warning.png";
    headerLogo.classList.add("rounded", "me-2");
    headerLogo.alt = "profile";
    headerLogo.style.width = "20px";
    headerLogo.style.height = "20px";

    const headerIcon = document.createElement("i");
    headerIcon.classList.add("bi", "bi-exclamation-circle", "me-2");
    headerIcon.style.fontSize = "1.2rem";
    headerIcon.style.color = "#007bff";

    const headerText = document.createElement("strong");
    headerText.classList.add("me-auto");
    headerText.textContent = "Xác nhận";

    const closeBtn = document.createElement("button");
    closeBtn.classList.add("btn-close");
    closeBtn.setAttribute("type", "button");
    closeBtn.setAttribute("data-bs-dismiss", "toast");
    closeBtn.setAttribute("aria-label", "Close");

    toastHeader.appendChild(headerLogo);
    toastHeader.appendChild(headerIcon);
    toastHeader.appendChild(headerText);
    //toastHeader.appendChild(closeBtn);

    const toastBody = document.createElement("div");
    toastBody.classList.add("toast-body");
    toastBody.textContent = message;

    const buttonContainer = document.createElement("div");
    buttonContainer.classList.add("my-2", "pt-2", "border-top", "d-flex", "justify-content-center");

    const confirmBtn = document.createElement("button");
    confirmBtn.classList.add("btn", "btn-primary", "btn-sm", "me-2");
    confirmBtn.textContent = "Đồng ý";
    confirmBtn.addEventListener("click", () => {
        actionYes();
        bootstrap.Toast.getInstance(toast).hide();
    });

    const cancelBtn = document.createElement("button");
    cancelBtn.classList.add("btn", "btn-secondary", "btn-sm");
    cancelBtn.textContent = "Hủy";
    cancelBtn.setAttribute("data-bs-dismiss", "toast");
    cancelBtn.addEventListener("click", () => {
        actionNo();
    });

    buttonContainer.appendChild(confirmBtn);
    buttonContainer.appendChild(cancelBtn);

    // Gắn nội dung vào toast
    toast.appendChild(toastHeader);
    toast.appendChild(toastBody);
    toast.appendChild(buttonContainer);

    // Thêm toast vào container
    toastContainer.appendChild(toast);

    // Thêm sự kiện để xóa backdrop khi toast bị đóng
    toast.addEventListener('hidden.bs.toast', () => {
        backdrop.style.opacity = '0';
        setTimeout(() => {
            document.body.removeChild(backdrop);
            toastContainer.remove();
        }, 300);

    });

    // Hiển thị toast
    const toastElement = new bootstrap.Toast(toast, {
        autohide: false
    });
    toastElement.show();
}

/**
 * Khóa hoặc mở khóa container chứa nút trong lưới
 * @param {string} Id - Id của container chứa nút
 */
function toggleButtonContainerLoading(Id) {
    var buttonContainer = document.querySelector(`.button-container-${Id}`);

    if (!buttonContainer.classList.contains("loadding")) {
        buttonContainer.classList.add("loadding");
    }
    else {
        buttonContainer.classList.remove("loadding");
    }
}

(function ($) {
    var languageRegisterGrid = {
        "sEmptyTable": "Không có dữ liệu trong bảng",
        "sInfo": "Hiển thị _START_ đến _END_ của _TOTAL_ bản ghi",
        "sInfoEmpty": "Hiển thị 0 đến 0 của 0 bản ghi",
        "sInfoFiltered": "(được lọc từ _MAX_ tổng số bản ghi)",
        "sLengthMenu": "Hiển thị _MENU_ bản ghi",
        "sLoadingRecords": "Đang tải...",
        "sProcessing": "Đang xử lý...",
        "sSearch": "Tìm kiếm:",
        "sZeroRecords": "Không tìm thấy bản ghi nào",
        "oPaginate": {
            "sFirst": "Đầu",
            "sLast": "Cuối",
            "sNext": "Tiếp",
            "sPrevious": "Trước"
        },
        "oAria": {
            "sSortAscending": ": kích hoạt để sắp xếp cột theo thứ tự tăng dần",
            "sSortDescending": ": kích hoạt để sắp xếp cột theo thứ tự giảm dần"
        },
        "xhr": {
            "error": function (xhr, textStatus, error) {
                console.log("DataTables warning: " + xhr.status + " - " + xhr.statusText); // Ghi thông điệp lỗi vào console
            }
        }
    }
    $.fn.registerGrid = function (options) {
        var $this = this;
        var id = $this.attr('id');
        var table = $this.DataTable({
            "columns": options.columns,
            "columnDefs": [
                { "orderable": false, "targets": [0,1] }
            ],
            autoWidth: true,
            "processing": true,
            "serverSide": true,
            "ajax": {
                type: "POST",
                "url": options.endpoint ? options.endpoint : "",
                "data": options.prepareRequest ? options.prepareRequest : function (d) {
                    if (options.prepareDataRequest)
                        $.extend(d, options.prepareDataRequest);
                    return d;
                },
                dataSrc: options.dataSrc ? options.dataSrc : function (json) {
                    return (json.data);
                }
            },
            "language": options.language ? options.language : languageRegisterGrid,
            "dom": options.dom ? options.dom : `<"row"rt><"row"<"col-sm-3"l><"col-sm-6 text-center"i><"col-sm-3"p>>`,
            "buttons": [
                {
                    text: 'Tìm kiếm',
                    className: 'btn btn-primary',
                    action: function (e, dt, node, config) {
                        var searchValue = $('#data-source-4_filter input').val();
                        dt.search(searchValue).draw();
                        $('.dataTables_processing').hide();

                    }
                }
            ],
            "length": options.length ? options.length : 10
        }).data("datagrid");

        table.on('draw.dt', options.dataBound ? options.dataBound : null);
        table.on('draw.dt', function () {

            //kiểm tra xem có phải vừa có 1 thao tác back về grid không
            var ssName = 'backWithReloadGrid' + id;
            var backCallback = sessionStorage.getItem(ssName);
            if (backCallback) {
                sessionStorage.removeItem(ssName);
                setTimeout(() => {
                    console.log("backWithReloadGrid");
                    table.ajax.reload(null, false)
                }, 700);
            }
        });

        return table;
    };




    /**
     * Đăng ký select2 cho các phần tử jQuery.
     * 
     * @param {object} options - Các tùy chọn của select2
     * @param {string} options.url - Đường dẫn URL để tải dữ liệu cho select2
     * @param {string} options.placeholder - Placeholder cho select2
     * @param {object} options.queryData - Dữ liệu truy vấn bổ sung
     * @param {Array} options.initialSelection - Lựa chọn ban đầu của select2
     */
    $.fn.registerSelect = function (options) {
        // Thiết lập các thiết lập mặc định và ghi đè bằng các thiết lập được cung cấp
        var settings = $.extend({
            url: "",
            queryData: {}, // Dữ liệu truy vấn
            initialSelection: null, // Giá trị ban đầu
            dropdownParent: "form.card", // Giá trị điểm neo
            placeholder: "Chọn",
            tags: false,//cho phép thêm bản ghi ngay trên select
            formatResult: function (data) { return data.text; }, // Hàm định dạng kết quả
            formatSelection: function (data) { return data.text; }, // Hàm định dạng giá trị đã chọn
            minimumInputLength: 0,
            multiple: true,
            language: {
                searching: function () { return "Đang tìm kiếm..."; }
            },
            minimumResultsForSearch: Infinity
        }, options);


        var $select = $(this);
        if (settings.url) {
            // Khởi tạo Select2
            $select.select2({
                placeholder: settings.placeholder,
                tags: settings.tags,
                allowClear: true,
                minimumInputLength: settings.minimumInputLength,
                language: settings.language,
                multiple: settings.multiple,
                dropdownParent: $(settings.dropdownParent),
                ajax: {
                    type: "POST",
                    url: settings.url,
                    dataType: 'json',
                    delay: 250, // Độ trễ trước khi truy vấn (ms)
                    data: options.prepareDataRequest ? options.prepareDataRequest : function (params) {
                        var parameter = {
                            Keyword: params.term,
                            SearchIn: ["Title"],
                        }
                        return $.extend({}, parameter, settings.queryData);
                    },
                    processResults: options.dataProcess ? options.dataProcess : function (data) {
                        return {
                            results: data.data.map(function (item) {
                                return {
                                    id: item.first_name,
                                    text: item.last_name
                                };
                            })
                        };
                    },
                    transport: function (params, success, failure) {
                        var $request = $.ajax(params);
                        $request.then(success);
                        $request.fail(failure);
                        return $request;
                    },
                    cache: false // Lưu cache dữ liệu trả về từ endpoint
                },
                templateResult: settings.formatResult, // Hàm định dạng kết quả
                templateSelection: settings.formatSelection // Hàm định dạng giá trị đã chọn
            });

        } else {
            $select.select2({
                placeholder: settings.placeholder,
                tags: settings.tags,
                allowClear: true,
                language: settings.language,
                multiple: settings.multiple,
                dropdownParent: $(settings.dropdownParent),
                templateResult: settings.formatResult, // Hàm định dạng kết quả
                templateSelection: settings.formatSelection // Hàm định dạng giá trị đã chọn
            });
        }

        // Thiết lập giá trị ban đầu nếu có
        if (settings.initialSelection && settings.initialSelection.length > 0) {

            var selectInitialValue = function (item) {
                var initialValue = {
                    id: item.id,
                    text: item.text
                };
                var $option = $('<option selected></option>').val(initialValue.id).text(initialValue.text);
                $select.append($option).trigger('change');
            };

            if (settings.multiple) {
                settings.initialSelection.forEach(selectInitialValue);
            } else {
                selectInitialValue(settings.initialSelection[0]);
            }
        }



        return $select;

    };


    /**
     * Khởi tạo cây menu dạng cây đa cấp có thể chỉnh sửa
     *
     * @param {object} options - Các tùy chọn cho cây menu
     * @param {string} options.selectMode - Chế độ chọn của cây menu ('single' hoặc 'checkbox')
     * @param {array} options.values - Các giá trị được chọn ban đầu
     * @param {array} options.disables - Các node bị vô hiệu hóa
     * @param {function} options.beforeLoad - Hàm được gọi trước khi dữ liệu được tải lên
     * @param {function} options.loaded - Hàm được gọi sau khi dữ liệu được tải lên
     * @param {string} options.url - URL để tải dữ liệu cây menu
     * @param {function} options.editCallback - Callback được gọi khi một node được chỉnh sửa
     * @param {function} options.approvedCallback - Callback được gọi khi một node được phê duyệt
     * @param {function} options.peddingCallback - Callback được gọi khi một node được chờ duyệt
     * @param {function} options.addsubCallback - Callback được gọi khi một node con được thêm vào
     * @param {function} options.deleteCallback - Callback được gọi khi một node được xóa
     * @param {function} options.onChange - Callback được gọi khi có sự thay đổi trong cây menu
     * @param {string} options.method - Phương thức HTTP được sử dụng để tải dữ liệu cây menu
     * @param {number} options.closeDepth - Số cấp độ tối đa mà cây menu sẽ được mở mặc định
     * @returns {object} treeMenu - Đối tượng cây menu đã được khởi tạo
     */
    $.fn.registerTreeView = function (options) {
        return new Tree(`#${$(this)[0].getAttribute('id')}`, options)
    };

    $.fn.registerButton = function (options) {
        var settings = $.extend({
            type: 'default',
            clickHandler: null,
            customClassButton: "p-2",
            icon: ""
        }, options);

        return this.each(function () {
            var $this = $(this);
            $this.addClass('btn btn-' + settings.type + " " + settings.customClassButton);
            $this.prepend($(`<i class="${settings.icon}" aria-hidden="true"></i>`));
            $this.on('click', function () {
                var $button = $(this);
                $button.addClass('loading');
                $button.prepend($(`<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>`));
                function removeLoading() {

                    $button.find("span.spinner-border").remove();
                    $button.prepend($(`<i class="fa fa-check" aria-hidden="true"></i>`));
                    setTimeout(() => {
                        $button.find("i.fa.fa-check").remove();
                        $button.removeClass('loading');
                    }, 500)

                }
                if (typeof settings.clickHandler === 'function') {
                    settings.clickHandler.call(this).then(function () {
                        removeLoading();
                    }).catch(function (error) {
                        console.error('Error:', error);
                        removeLoading();
                    });
                } else {
                    removeLoading();
                    console.error('No click handler provided.');
                }
            });
        });
    };
    /**
      * Đăng ký nút enter cho selector
      * @param {any} callback
      * @returns
      */
    $.fn.enterKey = function (callback) {
        $(this).keypress(function (event) {
            if (event.which === 13) {
                callback.call(this, event);
            }
        });
        return this;
    };

    /**
     * Plugin jQuery để khởi tạo flatpickr với các tùy chọn tùy chỉnh.
     * @param {Object} options - Các tùy chọn cấu hình cho flatpickr.
     * @param {String} options.mode - Chế độ chọn ngày: "single", "multiple", hoặc "range". Mặc định là "single".
     * @param {Boolean} options.enableTime - Bật picker thời gian. Mặc định là false.
     * @param {String} options.dateFormat - Định dạng ngày trong trường nhập liệu. Mặc định là "d/m/Y".
     * @param {String|Date} options.minDate - Ngày tối thiểu có thể chọn. Mặc định là null.
     * @param {String|Date} options.maxDate - Ngày tối đa có thể chọn. Mặc định là null.
     * @param {String|Date} options.defaultDate - Ngày mặc định khi khởi tạo. Mặc định là null.
     * @returns {jQuery} - Trả về đối tượng jQuery để hỗ trợ chuỗi lệnh.
     */
    $.fn.registerDatePicker = function (options) {
        var settings = $.extend({
            mode: "single",
            enableTime: false,
            dateFormat: "d/m/Y",
            defaultHour: "7",
            minDate: null,
            maxDate: null,
            defaultDate: null
        }, options);

        $datepicker = $(this);
        return this.each(function () {

            flatpickr(this, {
                locale: "vn",
                mode: settings.mode,
                defaultHour: settings.defaultHour,
                enableTime: settings.enableTime,
                dateFormat: settings.enableTime ? "H:i d/m/Y" : "d/m/Y",
                minDate: settings.minDate,
                maxDate: settings.maxDate,
                maxDate: settings.defaultDate

            });
        });
    };

    /**
     * đăng ký 1 input thành 1 trình chọn ảnh từ kho 
     * @param {any} mediaUrl - đường dẫn tới trình media có đường dẫn dạng [https://{{domain}}/IndexImageBrowser?token={{token truy cập}}]
     */
    $.fn.registerImagePicker = function (mediaUrl) {

        try {
            var formWidth = 1225,
                formHeight = 500;
            var $this = $(this);
            const parsedUrl = new URL(mediaUrl);
            var template = `<div class="w-100 text-center">
     <img class="img-fluid img-100" src="${parsedUrl.origin}/Image/noimage.png" alt='chọn ảnh từ kho' onclick="window.open(this.src, '_blank')" />
     <div class="d-flex justify-content-center mt-2">
         <a title="Mở kho dữ liệu" class="open-picker btn btn-primary mx-2"><i class="fa fa-picture-o me-1" aria-hidden="true" title="Chọn ảnh từ kho"></i>Chọn ảnh</a>
         <a title="Xóa ảnh đang chọn" class="delete-picker btn btn-warning"><i class="fa fa-trash me-1" aria-hidden="true" title="Xóa ảnh đã chọn"></i>Xóa ảnh</a>
     </div>
 </div>`
            var boxZone = $this.closest("div");
            boxZone.append($(template));

            var btnChonAnh = boxZone.find("a.open-picker");
            var btnXoaAnh = boxZone.find("a.delete-picker ");

            if ($this.val()) {
                boxZone.find("img").attr("src", $this.val());
            }

            btnChonAnh.unbind("click").bind("click", function () {
                const dualScreenLeft = window.screenLeft !== undefined ? window.screenLeft : window.screenX;
                const dualScreenTop = window.screenTop !== undefined ? window.screenTop : window.screenY;

                const width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
                const height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

                const systemZoom = width / window.screen.availWidth;
                const left = (width - formWidth) / 2 / systemZoom + dualScreenLeft
                const top = (height - formHeight) / 2 / systemZoom + dualScreenTop
                const newWindow = window.open(mediaUrl, "Kho dữ liệu chung", `scrollbars=yes,width=${formWidth / systemZoom},height=${formHeight / systemZoom},top=${top},left=${left}`)

                if (window.focus) newWindow.focus();

                window.addEventListener("message", function (event) {

                    var srcPicked = event.data.url.replaceAll(";", "")
                    boxZone.find("img").attr("src", srcPicked);
                    $this.val(srcPicked)
                });


            })
            btnXoaAnh.unbind("click").bind("click", function () {
                boxZone.find("img").attr("src", `${parsedUrl.origin}/Image/noimage.png`);
                $this.val("");
            })

        } catch (e) {
            console.log(e)
            this.alert("Có lỗi xảy ra khi đăng ký trình chọn ảnh" + e)
        }

    }

})(jQuery);