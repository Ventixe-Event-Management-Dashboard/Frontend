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

//tagit hjälp av chatgpt då mitt första Javascript som jag skrev hade problem med cors.

document.getElementById("openComposeBtn").addEventListener("click", function () {
    fetch("/Inbox/GetComposeForm")
        .then(res => res.text())
        .then(html => {
            document.getElementById("composeContent").innerHTML = html;
            document.getElementById("composeModal").style.display = "block";

            const form = document.getElementById("composeForm");
            form.addEventListener("submit", function (e) {
                e.preventDefault();

                const formData = new FormData(form);

                const payload = {
                    receiverId: formData.get("receiverId"),
                    subject: formData.get("subject"),
                    content: formData.get("content")
                };

                console.log("Skickar payload till backend:", payload);

                fetch("/Inbox/Compose", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(payload)
                })
                    .then(res => {
                        console.log("Svarskod:", res.status);
                        return res.text().then(text => {
                            console.log("Svarstext:", text);
                            if (res.ok) {
                                alert("Meddelande skickat!");
                                document.getElementById("composeModal").style.display = "none";
                            } else {
                                alert("Fel: " + res.status);
                            }
                        });
                    });
            });
        });
});
