document.addEventListener('DOMContentLoaded', function () {
    const citySelect = document.getElementById('city-select');
    const allSections = document.querySelectorAll('.city-section');

    // Ẩn tất cả sections
    function hideAllSections() {
        allSections.forEach(section => {
            section.style.display = 'none';
        });
    }

    // Hiển thị sections theo thành phố
    function showCitySections(cityId) {
        hideAllSections();

        if (cityId === 'all') {
            // Hiển thị tất cả
            allSections.forEach(section => {
                section.style.display = 'grid';
            });
        } else {
            // Hiển thị thành phố được chọn
            const citySections = document.querySelectorAll(`#${cityId}`);
            citySections.forEach(section => {
                section.style.display = 'grid';
            });
        }
    }

    // Sự kiện khi chọn thành phố
    citySelect.addEventListener('change', function () {
        showCitySections(this.value);
    });

    // Mặc định hiển thị "Toàn quốc"
    showCitySections('all');
});