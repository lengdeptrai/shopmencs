// Lấy các phần tử cần xử lý
const loginDiv = document.getElementById("login");
const registerDiv = document.getElementById("register");
const isLoginButton = document.getElementById("isLogin");
const isRegisterButton = document.getElementById("isRegister");
const isActive = document.getElementsByClassName("active");


    // Ẩn div đăng ký ban đầu
    registerDiv.style.display = "none";

    // Xử lý sự kiện khi nhấn nút "Đã có tài khoản"
    isLoginButton.addEventListener("click", function () {
        loginDiv.style.display = "block";
        registerDiv.style.display = "none";
        isLoginButton.classList.add("active");
        isRegisterButton.classList.remove("active")
    });

    // Xử lý sự kiện khi nhấn nút "Chưa có tài khoản"
    isRegisterButton.addEventListener("click", function () {
        loginDiv.style.display = "none";
        registerDiv.style.display = "block";
        isRegisterButton.classList.add("active");
        isLoginButton.classList.remove("active");
    });


function Handle() {
    var txtPassword = document.getElementById("txtPassword");
    var txtEmail = document.getElementById("txtEmail");
    var btnLogin = document.getElementById("btnLogin");
    var btnRegister = document.getElementById("btnRegister");
    var allTextBoxes = document.querySelectorAll(".inputLogin");

    // Gán sự kiện cho txtPassword để xử lý sự kiện nhấn Enter
    txtPassword.addEventListener("keyup", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            btnLogin.click(); // Xử lý nút btnLogin
        }
    });

    // Gán sự kiện cho txtEmail để xử lý sự kiện nhấn Enter
    txtEmail.addEventListener("keyup", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            btnRegister.click(); // Xử lý nút btnRegister
        }
    });

    // Gán sự kiện cho các TextBox khác để xử lý sự kiện nhấn Enter
    allTextBoxes.forEach(function (textBox, index) {
        textBox.addEventListener("keyup", function (event) {
            if (event.key === "Enter") {
                event.preventDefault();
                var nextIndex = index + 1;
                if (nextIndex < allTextBoxes.length) {
                    // Focus vào TextBox kế tiếp nếu có
                    allTextBoxes[nextIndex].focus();
                }
            }
        });
    });
}

Handle();


document.addEventListener("DOMContentLoaded", function () {
    // Chờ tải hoàn toàn nội dung của DOM
    const btnQR = document.getElementById("btnQR");

    // Thêm lắng nghe sự kiện click cho nút btnQR
    btnQR.addEventListener("click", function () {
        startQRScanner(); // Gọi hàm khi nút được nhấn
    });

    // Phần còn lại của mã JavaScript của bạn...

    // ... (bao gồm hàm Handle() và mã khác)

});

const videoElement = document.getElementById('scanner-video');
const scannerContainer = document.getElementById('scanner-container');
const stopButton = document.getElementById('stop-button');
function startQRScanner() {
    // Kiểm tra xem trình duyệt hỗ trợ navigator.mediaDevices và getUserMedia hay không
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        // Yêu cầu quyền truy cập camera từ người dùng
        navigator.mediaDevices.getUserMedia({ video: true })
            .then(function (stream) {
                // Nếu quyền truy cập được cấp, hiển thị video từ camera trong thẻ video
                scannerContainer.style.display = 'block';
                videoElement.srcObject = stream;
                // Sử dụng Instascan để tạo máy quét QR
                let scanner = new Instascan.Scanner({ video: videoElement });
                // Xử lý sự kiện khi quét thành công
                scanner.addListener('scan', function (content) {
                    // Sử dụng AJAX để gửi dữ liệu về server
                    var xhr = new XMLHttpRequest();
                    xhr.open('POST', 'Login.aspx/ProcessQRCode', true);
                    xhr.setRequestHeader('Content-Type', 'application/json');
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState === 4 && xhr.status === 200) {
                            var json = JSON.parse(xhr.responseText);
                            if (json.d === "Success")
                                window.location.href = "Default.aspx";
                            else alert("Đăng nhập thất bại");
                        }
                    };

                    // Gửi dữ liệu mã QR đến máy chủ
                    var data = JSON.stringify({ qrCode: content });
                    xhr.send(data);
                });

                // Lấy danh sách camera và bắt đầu quét với camera đầu tiên
                Instascan.Camera.getCameras()
                    .then(function (cameras) {
                        if (cameras.length > 0) {
                            scanner.start(cameras[0]);
                        } else {
                            alert('Không tìm thấy camera.');
                        }
                    })
                    .catch(function (error) {
                        alert('Lỗi khi truy cập camera: ' + error);
                    });
            })
            .catch(function (error) {
                // Nếu quyền truy cập bị từ chối hoặc có lỗi, hiển thị thông báo cho người dùng
                alert('Ứng dụng cần quyền truy cập camera để hoạt động.');
            });
    } else {
        // Nếu trình duyệt không hỗ trợ navigator.mediaDevices hoặc getUserMedia, hiển thị thông báo
        alert('Trình duyệt của bạn không hỗ trợ chức năng này.');
    }
}

stopButton.addEventListener('click', function () {
    // Lấy stream từ video
    const stream = video.srcObject;
    // Kiểm tra xem stream có tồn tại hay không
    if (stream) {
        // Lấy tất cả các tracks từ stream
        const tracks = stream.getTracks();
        // Lặp qua từng track và dừng chúng
        tracks.forEach(function (track) {
            track.stop();
        });
        // Gán null vào srcObject để tắt video
        video.srcObject = null;
    }

    // Ẩn div khi nút được nhấn
    scannerContainer.style.display = 'none';
});