// Toggle password visibility for login
document.getElementById('toggleLoginPassword').addEventListener('click', function () {
    const passwordInput = document.getElementById('loginPassword');
    const icon = this.querySelector('i');

    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        icon.classList.remove('bi-eye');
        icon.classList.add('bi-eye-slash');
    } else {
        passwordInput.type = 'password';
        icon.classList.remove('bi-eye-slash');
        icon.classList.add('bi-eye');
    }
});

// Form validation
document.getElementById('loginForm').addEventListener('submit', function (e) {
    const username = document.getElementById('loginUsername').value.trim();
    const password = document.getElementById('loginPassword').value.trim();

    if (!username || !password) {
        e.preventDefault();
        showToast('Error', 'Please fill in all required fields', 'danger');
    }
});