document.addEventListener('DOMContentLoaded', function () {
    const hamburger = document.getElementById('hamburgerMenuButton');
    const mobileMenu = document.getElementById('mobileMenu');
    const closeMenu = document.getElementById('closeMenu');

    if (hamburger && mobileMenu && closeMenu) {
        hamburger.addEventListener('click', () => {
            mobileMenu.classList.add('open');
        });

        closeMenu.addEventListener('click', () => {
            mobileMenu.classList.remove('open');
        });
    }
});

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.message-item').forEach(item => {
        item.addEventListener('click', function () {
            const id = this.dataset.id;

            fetch(`/Inbox/GetMessageDetail/${id}`)
                .then(response => response.text())
                .then(html => {
                    document.getElementById('messageDetailContainer').innerHTML = html;

                    document.querySelectorAll('.message-item').forEach(el => {
                        el.classList.remove('message-item--active');
                    });

                    this.classList.add('message-item--active');
                });
        });
    });
});
