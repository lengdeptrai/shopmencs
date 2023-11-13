// SLIDE


const slideImages = document.querySelectorAll(".slider img");
const radioButtons = document.querySelectorAll(".radio_img span");
let currentIndex = -1;
let isDelayed = false;

function showImage(index) {
    slideImages.forEach((img, i) => {
        if (i === index) {
            img.style.display = "block";
        } else {
            img.style.display = "none";
        }
    });

    radioButtons.forEach((radio, i) => {
        if (i === index) {
            radio.classList.add("activated");
        } else {
            radio.classList.remove("activated");
        }
    });
}

function nextSlide() {
    currentIndex = (currentIndex + 1) % slideImages.length;
    showImage(currentIndex);
}

function prevSlide() {
    currentIndex = (currentIndex - 1 + slideImages.length) % slideImages.length;
    showImage(currentIndex);
}

radioButtons.forEach((radio, i) => {
    radio.addEventListener("click", () => {
        if (!isDelayed) {
            currentIndex = i;
            showImage(currentIndex);
            isDelayed = true;
            setTimeout(() => {
                isDelayed = false;
            }, 2500); // 3 seconds delay
        }
    });
});

// Auto slide change
setInterval(() => {
    if (!isDelayed) {
        nextSlide();
    }
}, 2500);

const inputSeach = document.getElementById("seach");
const btnSeach = document.getElementById("btnSeach");

inputSeach.addEventListener('keypress', (e) => {
    if (e.key === "Enter") {
        e.preventDefault();
        btnSeach.click(); // Xử lý nút btnLogin
    }
})
function handleKeyPress(e) {
    if (e.keyCode === 13) {
        // Người dùng đã nhấn phím Enter
        e.preventDefault(); // Ngừng hành động mặc định của phím Enter (gửi form)
        document.getElementById('<%= btnSeach.ClientID %>').click(); // Gọi sự kiện click của LinkButton
    }
}

// CUONT DOWN

function countdown() {
    const hourElement = document.querySelector('.hour_sale');
    const minuteElement = document.querySelector('.minute_sale');
    const secondElement = document.querySelector('.second_sale');

    let remainingTime = 60 * 60; // 60 minutes in seconds

    function updateTimer() {
        const hours = Math.floor(remainingTime / 3600);
        const minutes = Math.floor((remainingTime % 3600) / 60);
        const seconds = remainingTime % 60;

        hourElement.textContent = hours.toString().padStart(2, '0');
        minuteElement.textContent = minutes.toString().padStart(2, '0');
        secondElement.textContent = seconds.toString().padStart(2, '0');

        if (remainingTime <= 0) {
            clearInterval(timerInterval);
        } else {
            remainingTime--;
        }
    }

    updateTimer(); // Call it once to set initial values
    const timerInterval = setInterval(updateTimer, 1000); // Update every second
}

// Start the countdown when the page loads
window.onload = countdown;



// phân trang

// Define the current page variable
// Define the current page variable
let currentPage = 0;
const itemsPerPage = 12; // Number of items per page
const totalItems = document.querySelectorAll('.list_sale_off li');
const listPage = document.querySelectorAll('.listPage li');
// Calculate the total number of pages
const maxPage = Math.ceil(totalItems.length / itemsPerPage);

// Function to show items for the current page
function showItems(page) {
    const listItems = document.querySelectorAll('.item_top_selling');
    if (listItems.length > 12 && currentPage == 0) listPage[1].style.display = "flex";

    // Calculate the start and end index of items for the current page
    const startIndex = page * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;

    // Hide all items
    listItems.forEach(item => {
        item.style.display = 'none';
    });

    // Show items for the current page
    for (let i = startIndex; i < endIndex; i++) {
        listItems[i].style.display = 'block';
    }
}

// Function to navigate to the previous page
function prevPage(maxPage) {
    if (currentPage > 0) {
        currentPage--;
        if (currentPage <= 0) {
            listPage[0].style.display = 'none';
            listPage[1].style.display = 'flex';
        }
        showItems(currentPage);
    } 
}

// Function to navigate to the next page
function nextPage(maxPage) {
    if (currentPage < maxPage) {
        currentPage++;
        if (currentPage >= maxPage) {
            listPage[0].style.display = 'flex';
            listPage[1].style.display = 'none';
        }
        showItems(currentPage);
    }
}

// Initial show for the first page
showItems(currentPage);



