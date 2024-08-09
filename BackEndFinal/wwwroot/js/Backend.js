
function isValidUrl(string) {
    try {
        new URL(string);
        return true;
    } catch (_) {
        return false;
    }
}

document.querySelectorAll(".AddBasket").forEach(btn => {
    btn.addEventListener("click", function () {
        const CourseId = $(this).attr("course-Id");
        $.ajax({
            url: `/Basket/Add?CourseId=${CourseId}`,
            method: "post",
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    Swal.fire({
                        icon: "success",
                        title: response.message,
                        timer: 1500
                    });
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Error",
                        text: response.message
                    });
                }
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: "An unexpected error occurred."
                });
            }
        });
});
});

document.querySelectorAll(".basket-item-remove").forEach(btn => {
    btn.addEventListener("click", function () {
        const CourseIdThis = $(this).attr("data-id");
        const basketItem = $(this).closest(".basket-item");
        const itemPrice = parseFloat(basketItem.attr("data-price"));
        const itemTotalPrice = itemPrice * parseFloat(basketItem.find(".basket-item-quantity").text().replace('Qty: ', ''));
        Swal.fire({
            title: 'Are you sure?',
            text: 'This will remove the item from your basket.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, keep it',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Basket/Delete?id=${CourseIdThis}`,
                    method: "get",
                    dataType: "json",
                    success: function (response) {
                        if (response.success) {
                            basketItem.remove();
                            const totalPriceElement = $(".basket-summary-total");
                            const currentTotalPrice = parseFloat(totalPriceElement.text().replace('Total: $', ''));
                            const newTotalPrice = currentTotalPrice - itemTotalPrice;
                            totalPriceElement.text(`Total: $${newTotalPrice.toFixed(2)}`);
                            Swal.fire({
                                icon: "success",
                                title: response.message,
                                timer: 1500
                            });
                        } else {
                            Swal.fire({
                                icon: "error",
                                title: "Error",
                                text: response.message
                            });
                        }
                    },
                    error: function () {
                        Swal.fire({
                            icon: "error",
                            title: "Error",
                            text: "An unexpected error occurred."
                        });
                    }
                        });
    } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire({
            icon: "info",
            title: "Cancelled",
            text: "Your item is safe."
        });
    }
});
            });
        });