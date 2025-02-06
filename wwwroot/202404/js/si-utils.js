
function toggleReadOnlyInput(selector) {

    var input = $(selector);
    if (input.prop('readonly')) {
        input.prop('readonly', false);
    } else {
        input.prop('readonly', true);
    }

}


/**
 * Trả về class của badge dựa trên trạng thái duyệt
 * 
 * @param {number} moderationStatus - Trạng thái duyệt (0 - Duyệt, 1 - Chờ duyệt, 2 - Từ chối, 3 - Chờ xem xét, 4 - Chờ phê duyệt, 5 - Nháp, 6 - Hủy hoặc Xóa)
 * @returns {string} - Class của badge tương ứng
 */
function getModerationStatusClass(moderationStatus) {
    switch (moderationStatus) {
        case 0:
            return "badge-success"; // Duyệt
        case 1:
            return "badge-warning"; // Chờ duyệt
        case 2:
            return "badge-danger"; // Từ chối
        case 3:
            return "badge-info"; // Chờ xem xét
        case 4:
            return "badge-primary"; // Chờ phê duyệt
        case 5:
            return "badge-secondary"; // Nháp
        case 6:
            return "badge-dark"; // Hủy hoặc Xóa
        default:
            return "badge-light"; // Mặc định
    }
}
/**
 * Việt hóat trạng thái công việc
 * 
 * @param {number} 
 * @returns {string} - Class của badge tương ứng
 */
function getStatusJiraText(status) {
    switch (status) {
        case "To Do":
            return 'Chờ làm';
        case "In Progress":
            return 'Đang làm';
        case "Done":
            return 'Làm xong chờ test';
        case "Reopened":
            return 'Test lỗi chờ làm lại';
        case "Closed":
            return 'Đã đóng';

        default:
            return '';
    }
}
function getStatusJiraBadgeClass(status) {
    switch (status) {

        case "To Do":
            return "badge-info"; // Đã giao chờ làm
        case "In Progress":
            return "badge-primary"; // Đang làm
        case "Done":
            return "badge-success"; // Làm xong chờ test
        case "Reopened":
            return "badge-danger"; // Test lỗi chờ làm lại
        case "Closed":
            return "badge-secondary"; // Đã đóng

        default:
            return "badge-light"; // Không xác định
    }
}
/**
 * Trạng thái công việc
 * 
 * @param {number} - Trạng thái duyệt (1 - Chờ giao, 2 - Đã giao chờ làm, 3 - Đang làm, 4 - Làm xong chờ test, 5 - Test lỗi chờ làm lại, 6 - Đã đóng, 7 - Dừng tạm thời)
 * @returns {string} - Class của badge tương ứng
 */
function getStatusSprintText(status) {
    switch (status) {
        case 1:
            return 'Chưa bắt đầu';
        case 2:
            return 'Bắt đầu';
        case 3:
            return 'Done';
        default:
            return '';
    }
}
/**
 * Trạng thái công việc
 * 
 * @param {number} - Trạng thái duyệt (1 - Chờ giao, 2 - Đã giao chờ làm, 3 - Đang làm, 4 - Làm xong chờ test, 5 - Test lỗi chờ làm lại, 6 - Đã đóng, 7 - Dừng tạm thời)
 * @returns {string} - Class của badge tương ứng
 */
function getStatusText(status) {
    switch (status) {
        case 1:
            return 'Chờ giao';
        case 2:
            return 'Đã giao chờ làm';
        case 3:
            return 'Đang làm';
        case 4:
            return 'Làm xong chờ test';
        case 5:
            return 'Test lỗi chờ làm lại';
        case 6:
            return 'Đã đóng';
        case 7:
            return 'Dừng tạm thời';
        default:
            return '';
    }
}
/**
 * Lấy class badge cho trạng thái công việc
 * 
 * @param {number}  - Trạng thái công việc (1 - Chờ giao, 2 - Đã giao chờ làm, 3 - Đang làm, 4 - Làm xong chờ test, 5 - Test lỗi chờ làm lại, 6 - Đã đóng, 7 - Dừng tạm thời)
 * @returns {string} - Class của badge tương ứng
 */

function getStatusBadgeClass(status) {
    switch (status) {
        case 1:
            return "badge-warning"; // Chờ giao
        case 2:
            return "badge-info"; // Đã giao chờ làm
        case 3:
            return "badge-primary"; // Đang làm
        case 4:
            return "badge-success"; // Làm xong chờ test
        case 5:
            return "badge-danger"; // Test lỗi chờ làm lại
        case 6:
            return "badge-secondary"; // Đã đóng
        case 7:
            return "badge-dark"; // Dừng tạm thời
        default:
            return "badge-light"; // Không xác định
    }
}
/**
 * Mức độ ưu tiên
 * 
 * @param {number}  - Trạng thái duyệt (1 - Ưu tiên thấp, 2 - Bình thường, 3 - Cần làm gấp)
 * @returns {string} - Class của badge tương ứng
 */
function getMucdoText(status) {
    switch (status) {
        case 1:
            return 'Ưu tiên thấp';
        case 2:
            return 'Bình thường';
        case 3:
            return 'Cần làm gấp';

        default:
            return '';
    }
}
/**
 * Lấy class badge cho mức độ ưu tiên
 * 
 * @param {number}  - Mức độ ưu tiên (1 - Ưu tiên thấp, 2 - Bình thường, 3 - Cần làm gấp)
 * @returns {string} - Class của badge tương ứng
 */
function getMucdoBadgeClass(status) {
    switch (status) {
        case 1:
            return "badge-success"; // Ưu tiên thấp
        case 2:
            return "badge-primary"; // Bình thường
        case 3:
            return "badge-danger"; // Cần làm gấp
        default:
            return "badge-secondary"; // Không xác định
    }
}

/**
 * Loại việc
 * 
 * @param {number}  - Trạng thái duyệt (1 - Việc mới, 2 - Việc sửa bug, 3 - Bug kéo về)
 * @returns {string} - Class của badge tương ứng
 */
function getLoaiText(status) {
    switch (status) {
        case 1:
            return 'Việc mới';
        case 2:
            return 'Việc sửa bug';
        case 3:
            return 'Bug kéo về';

        default:
            return '';
    }
}
/**
 * Lấy class badge cho loại việc
 * 
 * @param {number}  - Loại việc (1 - Việc mới, 2 - Việc sửa bug, 3 - Bug kéo về)
 * @returns {string} - Class của badge tương ứng
 */
function getLoaiBadgeClass(status) {
    switch (status) {
        case 1:
            return "badge-info"; // Việc mới
        case 2:
            return "badge-warning"; // Việc sửa bug
        case 3:
            return "badge-danger"; // Bug kéo về
        default:
            return "badge-light"; // ""
    }
}
/**
 * Trả về class của badge dựa trên trạng enable
 * 
 * @param {number}  - Trạng thái duyệt (1 - Duyệt, 0 - Chờ duyệt, )
 * @returns {string} - Class của badge tương ứng
 */
function getEnableStatusClass(moderationStatus) {
    switch (moderationStatus) {
        case true:
            return "badge-success"; // Duyệt
        case false:
        default:
            return "badge-warning"; // Mặc định
    }
}

// chuyển đổi ngày giờ 
function chuyenDoiMinutesToHoursOrDays(phut) {
    let totalMinutes = phut;
    let days = Math.floor(totalMinutes / (60 * 8));
    let remainingHours = Math.floor(totalMinutes / 60) % 8;
    let minutes = totalMinutes % 60;
    if (phut == null || phut === '') {
        return `0 phút`;
    }
    if (days > 0) {
        if (remainingHours > 0 && minutes > 0) {
            return `${days} ngày ${remainingHours} giờ ${minutes} phút`;
        } else if (remainingHours > 0) {
            return `${days} ngày ${remainingHours} giờ`;
        } else if (minutes > 0) {
            return `${days} ngày ${minutes} phút`;
        } else {
            return `${days} ngày`;
        }
    } else if (remainingHours > 0) {
        if (minutes > 0) {
            return `${remainingHours} giờ ${minutes} phút`;
        } else {
            return `${remainingHours} giờ`;
        }
    } else {
        return `${minutes} phút`;
    }
}

/**
 * Trạng thái fix code
 */
function getTrangThaiFixCodeText(TrangThai) {
    switch (TrangThai) {
        case 1:
            return 'Chưa fix';
        case 2:
            return 'Đã fix';
        default:
            return '';
    }
}
/**
 * Lấy class badge cho Trạng thái fix code
 * 
 * @param {number}  - Trạng thái fix code (1 - Chưa fix, 2 - Đã fix)
 * @returns {string} - Class của badge tương ứng
 */
function getTrangThaiFixCodeBadgeClass(TrangThai) {
    switch (TrangThai) {
        case 1:
            return "badge-danger"; // Chưa fix
        case 2:
            return "badge-success"; // Đã fix
        default:
            return "badge-light"; // ""
    }
}

///chỗ này bắt đầu của JM
/**
 * chuyển time stamp sang thời gian dễ nhìn hơn.
 * @param {any} timestamp
 * @returns
 */
function formatTimeTimeStamp(timestamp) {
    // Ensure timestamp is a number
    timestamp = Number(timestamp);

    const now = Date.now();
    const timeDifference = now - timestamp;

    if (timeDifference < 0) {
        const futureDifference = Math.abs(timeDifference);

        const msInSecond = 1000;
        const msInMinute = 60 * msInSecond;
        const msInHour = 60 * msInMinute;
        const msInDay = 24 * msInHour;

        if (futureDifference < msInMinute) {
            return `${Math.floor(futureDifference / msInSecond)} giây`;
        } else if (futureDifference < msInHour) {
            return `${Math.floor(futureDifference / msInMinute)} phút`;
        } else if (futureDifference < msInDay) {
            return `${Math.floor(futureDifference / msInHour)} giờ`;
        } else {
            const date = new Date(timestamp);
            const hours = date.getHours().toString().padStart(2, '0');
            const minutes = date.getMinutes().toString().padStart(2, '0');
            const day = date.getDate().toString().padStart(2, '0');
            const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Months are 0-based
            const year = date.getFullYear();
            return `${hours}:${minutes} ${day}/${month}/${year}`;
        }
    }

    const msInSecond = 1000;
    const msInMinute = 60 * msInSecond;
    const msInHour = 60 * msInMinute;
    const msInDay = 24 * msInHour;

    if (timeDifference < msInMinute) {
        return `${Math.floor(timeDifference / msInSecond)} giây trước`;
    } else if (timeDifference < msInHour) {
        return `${Math.floor(timeDifference / msInMinute)} phút trước`;
    } else if (timeDifference < msInDay) {
        return `${Math.floor(timeDifference / msInHour)} giờ trước`;
    } else {
        const date = new Date(timestamp);
        const hours = date.getHours().toString().padStart(2, '0');
        const minutes = date.getMinutes().toString().padStart(2, '0');
        const day = date.getDate().toString().padStart(2, '0');
        const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Months are 0-based
        const year = date.getFullYear();
        return `${hours}:${minutes} ${day}/${month}/${year}`;
    }
}

function formatTimeDifference(timestamp) {
    const now = Date.now();
    const timeDifference = now - timestamp;
    if (timeDifference < 0) {
        return "";
    }

    const msInSecond = 1000;
    const msInMinute = 60 * msInSecond;
    const msInHour = 60 * msInMinute;
    const msInDay = 24 * msInHour;

    if (timeDifference < msInSecond) {
        return `${Math.floor(timeDifference / msInSecond)} giây trước`;
    } else if (timeDifference < msInMinute) {
        return `${Math.floor(timeDifference / msInSecond)} giây trước`;
    } else if (timeDifference < msInHour) {
        return `${Math.floor(timeDifference / msInMinute)} phút trước`;
    } else if (timeDifference < msInDay) {
        return `${Math.floor(timeDifference / msInHour)} giờ trước`;
    } else {
        const date = new Date(timestamp);
        const formattedDate = `${date.getHours()}:${date.getMinutes()} ${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
        return formattedDate;
    }
}
/**
 * Chuyển ms sang đơn vị dễ nhìn hơn
 * @param {any} latencyMs
 * @returns
 */
function formatLatency(latencyMs) {
    const msInSecond = 1000;
    const msInMinute = 60 * msInSecond;
    const msInHour = 60 * msInMinute;
    const msInDay = 24 * msInHour;
    if (latencyMs < msInSecond) {
        return `${latencyMs} ms`;
    } else if (latencyMs < msInMinute) {
        const seconds = (latencyMs / msInSecond).toFixed(3);
        return `${seconds} giây`;
    } else if (latencyMs < msInHour) {
        const minutes = Math.floor(latencyMs / msInMinute);
        const remainingMs = latencyMs % msInMinute;
        const seconds = (remainingMs / msInSecond).toFixed(3);
        return `${minutes} phút ${seconds} giây`;
    } else if (latencyMs < msInDay) {
        const hours = Math.floor(latencyMs / msInHour);
        const remainingMsAfterHours = latencyMs % msInHour;
        const minutes = Math.floor(remainingMsAfterHours / msInMinute);
        const remainingMs = remainingMsAfterHours % msInMinute;
        const seconds = (remainingMs / msInSecond).toFixed(3);
        return `${hours} giờ ${minutes} phút ${seconds} giây`;
    } else {
        const days = Math.floor(latencyMs / msInDay);
        const remainingMsAfterDays = latencyMs % msInDay;
        const hours = Math.floor(remainingMsAfterDays / msInHour);
        const remainingMsAfterHours = remainingMsAfterDays % msInHour;
        const minutes = Math.floor(remainingMsAfterHours / msInMinute);
        const remainingMs = remainingMsAfterHours % msInMinute;
        const seconds = (remainingMs / msInSecond).toFixed(3);
        return `${days} ngày ${hours} giờ ${minutes} phút ${seconds} giây`;
    }
}

//kết thúc JM
