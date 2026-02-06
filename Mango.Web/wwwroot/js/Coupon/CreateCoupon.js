// Real-time Preview Update
document.addEventListener('DOMContentLoaded', function () {
    // Get form inputs
    const codeInput = document.getElementById('CouponCode');
    const discountInput = document.getElementById('DiscountAmount');
    const minAmountInput = document.getElementById('MinAmount');
    const expiryInput = document.getElementById('expirationDate');

    // Get preview elements
    const previewCode = document.getElementById('previewCode');
    const previewDiscount = document.getElementById('previewDiscount');
    const previewMinAmount = document.getElementById('previewMinAmount');
    const previewExpiry = document.getElementById('previewExpiry');

    // Update preview on input change
    function updatePreview() {
        // Update code
        if (codeInput.value) {
            previewCode.textContent = codeInput.value.toUpperCase();
        } else {
            previewCode.textContent = 'YOURCODE';
        }

        // Update discount
        if (discountInput.value) {
            previewDiscount.textContent = `$${parseFloat(discountInput.value).toFixed(2)} OFF`;
        } else {
            previewDiscount.textContent = '$0.00 OFF';
        }

        // Update minimum amount
        if (minAmountInput.value) {
            previewMinAmount.textContent = `Min. purchase: $${parseInt(minAmountInput.value).toLocaleString()}`;
        } else {
            previewMinAmount.textContent = 'Min. purchase: $0';
        }

        // Update expiry
        if (expiryInput.value) {
            const expiryDate = new Date(expiryInput.value);
            const options = { year: 'numeric', month: 'short', day: 'numeric' };
            previewExpiry.textContent = `Valid until: ${expiryDate.toLocaleDateString('en-US', options)}`;
        } else {
            previewExpiry.textContent = 'No expiration';
        }
    }

    // Add event listeners
    codeInput.addEventListener('input', updatePreview);
    discountInput.addEventListener('input', updatePreview);
    minAmountInput.addEventListener('input', updatePreview);
    expiryInput.addEventListener('input', updatePreview);

    // Auto-format coupon code to uppercase
    codeInput.addEventListener('blur', function () {
        this.value = this.value.toUpperCase().replace(/\s+/g, '');
    });

    // Auto-generate coupon code suggestion
    document.getElementById('generateCode').addEventListener('click', function () {
        const adjectives = ['SUMMER', 'WINTER', 'HAPPY', 'MEGA', 'SUPER', 'FLASH', 'HOLIDAY'];
        const nouns = ['SALE', 'DEAL', 'OFFER', 'DISCOUNT', 'SAVINGS', 'GIFT'];
        const numbers = Math.floor(Math.random() * 90) + 10;

        const randomAdj = adjectives[Math.floor(Math.random() * adjectives.length)];
        const randomNoun = nouns[Math.floor(Math.random() * nouns.length)];

        const generatedCode = `${randomAdj}${randomNoun}${numbers}`;
        codeInput.value = generatedCode;
        updatePreview();
    });

    // Validate discount amount
    discountInput.addEventListener('blur', function () {
        const value = parseFloat(this.value);
        if (value < 0.01) {
            this.value = '0.01';
        }
        if (value > 10000) {
            this.value = '10000';
            alert('Maximum discount amount is $10,000');
        }
        updatePreview();
    });

    // Initialize preview
    updatePreview();
});

// Reset form function
function resetForm() {
    if (confirm('Are you sure you want to reset the form? All entered data will be lost.')) {
        document.getElementById('couponForm').reset();
        updatePreview();
    }
}

// Form validation
document.getElementById('couponForm').addEventListener('submit', function (e) {
    const codeInput = document.getElementById('CouponCode');
    const discountInput = document.getElementById('DiscountAmount');
    const minAmountInput = document.getElementById('MinAmount');

    let isValid = true;
    let errorMessage = '';

    // Validate coupon code
    if (!codeInput.value.trim()) {
        isValid = false;
        errorMessage += '• Coupon Code is required\n';
    }

    // Validate discount amount
    if (!discountInput.value || parseFloat(discountInput.value) <= 0) {
        isValid = false;
        errorMessage += '• Discount Amount must be greater than 0\n';
    }

    // Validate minimum amount
    if (!minAmountInput.value || parseInt(minAmountInput.value) < 0) {
        isValid = false;
        errorMessage += '• Minimum Amount must be 0 or greater\n';
    }

    if (!isValid) {
        e.preventDefault();
        alert('Please fix the following errors:\n\n' + errorMessage);
    } else {
        // Show loading state
        const submitBtn = this.querySelector('button[type="submit"]');
        submitBtn.innerHTML = '<i class="bi bi-hourglass-split"></i> Creating...';
        submitBtn.disabled = true;
    }
});

// Auto-calculate discount percentage
function calculatePercentage() {
    const discountInput = document.getElementById('DiscountAmount');
    const minAmountInput = document.getElementById('MinAmount');

    if (discountInput.value && minAmountInput.value && minAmountInput.value > 0) {
        const discount = parseFloat(discountInput.value);
        const minAmount = parseFloat(minAmountInput.value);
        const percentage = (discount / minAmount) * 100;

        const percentageBadge = document.getElementById('percentageBadge');
        if (percentageBadge) {
            percentageBadge.textContent = `(${percentage.toFixed(1)}% of min. amount)`;
            percentageBadge.style.display = 'inline';
        }
    }
}

// Add percentage badge to discount input
document.addEventListener('DOMContentLoaded', function () {
    const discountInput = document.getElementById('DiscountAmount');
    const minAmountInput = document.getElementById('MinAmount');

    // Create percentage badge
    const percentageBadge = document.createElement('span');
    percentageBadge.id = 'percentageBadge';
    percentageBadge.className = 'badge bg-info bg-opacity-10 text-info ms-2';
    percentageBadge.style.display = 'none';

    discountInput.parentElement.appendChild(percentageBadge);

    // Add event listeners for percentage calculation
    discountInput.addEventListener('input', calculatePercentage);
    minAmountInput.addEventListener('input', calculatePercentage);
});