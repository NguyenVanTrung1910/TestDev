(function ($) {
    var languageDatatable = {
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
    $.fn.dataTable = function (options) {
        var $this = this;
        var table = $this.DataTable({
            "columns": options.columns,
            //"columnDefs": [
            //    { "orderable": false, "targets": [] }
            //],
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
            "language": options.language ? options.language : languageDatatable,
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
            checkPermissions();
        });

        return table;
    };
})(jQuery);