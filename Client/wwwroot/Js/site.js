function ShowErrorMessage(message) {
    Swal.fire({
        title: 'خطا',
        text: message,
        icon: 'error',
    });
}

function ShowSuccessfullyMessage_SweetAlert(message) {
    Swal.fire({
        title: 'عملیات با موفقیت انجام شد',
        text: message,
        icon: 'success'
    });
}

function ShowSuccessfullyMessage_Toastr(message) {
    toastr.success('',message, { timeOut: 2000 })
}