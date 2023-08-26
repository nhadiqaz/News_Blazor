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
    toastr.success('', message, { timeOut: 2000 })
}

async function ShowDeleteMessage(title, message) {
    var result =await Swal.fire({
        title: title,
        text: message,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#878787',
        confirmButtonText: 'بلی',
        cancelButtonText: 'خیر',
    })

    console.log('Result:', result); // Debugging line

    if (result.isConfirmed) {
        return true;
    } else {
        return false;
    }
}