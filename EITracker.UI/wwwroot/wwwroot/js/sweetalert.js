function Confirmdailog(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes"
        }).then((result) => {
            resolve(result.isConfirmed)
        });
    });
   
}

function successalert(title) {
    Swal.fire({
        position: "top-end",
        icon: "success",
        title,
        showConfirmButton: false,
        timer: 2000
    });
}

function messegealert(title, text, icon) {
    Swal.fire({
        title,
        text,
        icon
    });
}