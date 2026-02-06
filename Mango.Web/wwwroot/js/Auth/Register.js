// Toggle password visibility for register
document.getElementById('toggleRegisterPassword').addEventListener('click', function () {
    const passwordInput = document.getElementById('registerPassword');
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

// Password strength checker
const passwordInput = document.getElementById('registerPassword');
const confirmPasswordInput = document.getElementById('registerConfirmPassword');

passwordInput.addEventListener('input', function () {
    checkPasswordStrength(this.value);
    checkPasswordMatch();
});

confirmPasswordInput.addEventListener('input', checkPasswordMatch);

function checkPasswordStrength(password) {
    let strength = 0;
    const bar = document.getElementById('passwordStrengthBar');
    const text = document.getElementById('passwordStrengthText');

    // Check password length
    if (password.length >= 8) strength += 25;
    if (password.length >= 12) strength += 15;

    // Check for lowercase letters
    if (/[a-z]/.test(password)) strength += 15;

    // Check for uppercase letters
    if (/[A-Z]/.test(password)) strength += 15;

    // Check for numbers
    if (/[0-9]/.test(password)) strength += 15;

    // Check for special characters
    if (/[^A-Za-z0-9]/.test(password)) strength += 15;

    // Update progress bar
    bar.style.width = strength + '%';

    // Update color and text
    if (strength < 40) {
        bar.className = 'progress-bar bg-danger';
        text.textContent = 'Password strength: Weak';
    } else if (strength < 70) {
        bar.className = 'progress-bar bg-warning';
        text.textContent = 'Password strength: Fair';
    } else if (strength < 90) {
        bar.className = 'progress-bar bg-info';
        text.textContent = 'Password strength: Good';
    } else {
        bar.className = 'progress-bar bg-success';
        text.textContent = 'Password strength: Strong';
    }
}

function checkPasswordMatch() {
    const password = passwordInput.value;
    const confirmPassword = confirmPasswordInput.value;
    const matchElement = document.getElementById('passwordMatch');
    const mismatchElement = document.getElementById('passwordMismatch');

    if (confirmPassword === '') {
        matchElement.classList.add('d-none');
        mismatchElement.classList.add('d-none');
        return;
    }

    if (password === confirmPassword) {
        matchElement.classList.remove('d-none');
        mismatchElement.classList.add('d-none');
    } else {
        matchElement.classList.add('d-none');
        mismatchElement.classList.remove('d-none');
    }
}

// Role selection change handler
const roleSelect = document.getElementById('registerRole');
const roleDescription = document.getElementById('roleDescription');

roleSelect.addEventListener('change', function () {
    updateRoleDescription(this.value);
});

function updateRoleDescription(role) {
    switch (role) {
        case '@ViewBag.RoleAdmin':
            roleDescription.innerHTML = '<strong>Admin:</strong> Full access to all system features';
            break;
        case '@ViewBag.RoleCustomer':
            roleDescription.innerHTML = '<strong>Customer:</strong> Access to shopping and personal account features';
            break;
        default:
            roleDescription.textContent = 'Select your role to determine your access level';
            break;
    }
}

// Initialize role description
if (roleSelect.value) {
    updateRoleDescription(roleSelect.value);
}

// Form validation
//document.getElementById('registerForm').addEventListener('submit', function (e) {
//    const name = document.getElementById('registerName').value.trim();
//    const email = document.getElementById('registerEmail').value.trim();
//    const phone = document.getElementById('registerPhone').value.trim();
//    const role = document.getElementById('registerRole').value;
//    const password = document.getElementById('registerPassword').value;
//    const confirmPassword = document.getElementById('registerConfirmPassword').value;
//    const agreeTerms = document.getElementById('agreeTerms').checked;

//    let errors = [];

//    if (!name) errors.push('Name is required');
//    if (!email) errors.push('Email is required');
//    if (!phone) errors.push('Phone number is required');
//    if (!role) errors.push('Role is required');
//    if (password.length < 6) errors.push('Password must be at least 6 characters');
//    if (password !== confirmPassword) errors.push('Passwords do not match');
//    if (!agreeTerms) errors.push('You must agree to the terms');

//    if (errors.length > 0) {
//        e.preventDefault();
//        showToast('Validation Error', errors.join('<br>'), 'danger');
//    }
//});

// Toast notification function
function showToast(title, message, type) {
    // Create toast container if it doesn't exist
    let toastContainer = document.querySelector('.toast-container');
    if (!toastContainer) {
        toastContainer = document.createElement('div');
        toastContainer.className = 'toast-container position-fixed top-0 end-0 p-3';
        document.body.appendChild(toastContainer);
    }

    // Create toast element
    const toastId = 'toast-' + Date.now();
    const toastHtml = `
                <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="toast-header bg-${type} text-white">
                        <i class="bi bi-${type === 'danger' ? 'exclamation-triangle' : type === 'success' ? 'check-circle' : 'info-circle'} me-2"></i>
                        <strong class="me-auto">${title}</strong>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="toast"></button>
                    </div>
                    <div class="toast-body">
                        ${message}
                    </div>
                </div>
            `;

    toastContainer.insertAdjacentHTML('beforeend', toastHtml);

    // Show the toast
    const toastElement = document.getElementById(toastId);
    const toast = new bootstrap.Toast(toastElement, { delay: 3000 });
    toast.show();

    // Remove toast after it's hidden
    toastElement.addEventListener('hidden.bs.toast', function () {
        this.remove();
    });
}