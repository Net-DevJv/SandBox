﻿document.addEventListener('DOMContentLoaded', function () {
    window.addEventListener('resize', function () {
        const dropdownToggles = document.querySelectorAll('.dropdown-toggle[aria-expanded="true"]');
        dropdownToggles.forEach(toggle => {
            const instance = bootstrap.Dropdown.getInstance(toggle);
            if (instance) {
                instance.hide();
            }
        });

        const searchInput = document.querySelector('.form-control');
        if (searchInput && document.activeElement === searchInput) {
            searchInput.blur();
        }
    });
});