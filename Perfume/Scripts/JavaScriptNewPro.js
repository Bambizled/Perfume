document.addEventListener('DOMContentLoaded', function () {
    const slider = document.querySelector('.slider');
    const slides = document.querySelectorAll('.slide');
    const prevBtn = document.querySelector('.prev');
    const nextBtn = document.querySelector('.next');
    const dots = document.querySelectorAll('.dot');

    let currentIndex = 0;
    const totalSlides = slides.length;

    // Hàm cập nhật vị trí slider
    function updateSlider() {
        slider.style.transform = `translateX(-${currentIndex * 100}%)`;

        // Cập nhật trạng thái active cho dots
        dots.forEach((dot, index) => {
            dot.classList.toggle('active', index === currentIndex);
        });
    }

    // Xử lý sự kiện cho nút Previous
    prevBtn.addEventListener('click', function () {
        currentIndex = (currentIndex - 1 + totalSlides) % totalSlides;
        updateSlider();
    });

    // Xử lý sự kiện cho nút Next
    nextBtn.addEventListener('click', function () {
        currentIndex = (currentIndex + 1) % totalSlides;
        updateSlider();
    });

    // Xử lý sự kiện cho dots
    dots.forEach(dot => {
        dot.addEventListener('click', function () {
            currentIndex = parseInt(this.getAttribute('data-index'));
            updateSlider();
        });
    });

    // Tự động chuyển ảnh sau mỗi 5 giây
    setInterval(function () {
        currentIndex = (currentIndex + 1) % totalSlides;
        updateSlider();
    }, 5000);
});