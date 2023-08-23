function ShowErrorMessage(message) {
    Swal.fire({
        title: 'خطا',
        text: message,
        icon: 'error',
    });
}

function ShowSuccessfullyMessage(message) {
    Swal.fire({
        title: 'عملیات با موفقیت انجام شد',
        text: message,
        icon: 'success'
    });
}