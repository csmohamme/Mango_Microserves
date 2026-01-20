// Copy coupon code to clipboard
function copyCouponCode(code) {
    navigator.clipboard.writeText(code).then(() => {
        showToast('Success', 'Coupon code copied to clipboard!', 'success');
    }).catch(err => {
        showToast('Error', 'Failed to copy coupon code', 'error');
    });
}

// Delete coupon confirmation
function deleteCoupon(code) {
    if (confirm(`Are you sure you want to delete coupon "${code}"?`)) {
        // AJAX call to delete coupon
        fetch(`/Coupon/Delete/${code}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            }
        })
            .then(response => {
                if (response.ok) {
                    showToast('Success', 'Coupon deleted successfully!', 'success');
                    setTimeout(() => location.reload(), 1500);
                } else {
                    showToast('Error', 'Failed to delete coupon', 'error');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                showToast('Error', 'An error occurred', 'error');
            });
    }
}

// Change page size
function changePageSize(size) {
    const url = new URL(window.location.href);
    url.searchParams.set('pageSize', size);
    url.searchParams.set('page', '1'); // Reset to first page
    window.location.href = url.toString();
}

// Search functionality
document.getElementById('searchInput').addEventListener('input', function (e) {
    const searchTerm = e.target.value.toLowerCase();
    const rows = document.querySelectorAll('#couponsTable tbody tr');

    rows.forEach(row => {
        const text = row.textContent.toLowerCase();
        row.style.display = text.includes(searchTerm) ? '' : 'none';
    });
});

// Export functionality
document.getElementById('exportBtn').addEventListener('click', function () {
    // Implement export logic here
    showToast('Info', 'Export feature coming soon!', 'info');
});

// Toast notification function
function showToast(title, message, type) {
    // You can implement a toast notification system here
    // For now, using browser alert
    alert(`${title}: ${message}`);
}

// Add animation to table rows
document.addEventListener('DOMContentLoaded', function () {
    const rows = document.querySelectorAll('#couponsTable tbody tr');
    rows.forEach((row, index) => {
        row.style.animationDelay = `${index * 0.05}s`;
    });
});