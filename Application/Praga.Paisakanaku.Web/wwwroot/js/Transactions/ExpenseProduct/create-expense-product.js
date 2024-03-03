$(document).ready(function () {
    // TODO switch this to onCreateExpense method
    getMemberDDList();
    getProductDDList();
    getExpenseProductInfoList();
    resetForm();
});

$(document).on('click', '#btnProductClose, #btnProductModalClose', function () {
    $('#selectProduct').val('');
})

function resetForm() {
    $('#selectMember').val('');
    resetProductRelatedInfo();
}

function resetProductRelatedInfo() {
    $('#amount, #expenseAmount, #quantity').prop('disabled', true);
}

function onCreateProduct() {
    loadSpinner();
    resetProductRelatedInfo();
    $('#formCreateProduct').trigger("reset");
    $('#formCreateProduct').data('id', '');
    $('#formCreateProduct').data('isupdate', 'False');
    $('#formCreateProduct').find(':input,select').val('');
    $('#formCreateProduct').find('span.error').hide();
    $('#createProductTitle').text('Create Product');
    $('#brandNameContainer').hide();
    $('#productCategoryNameContainer').hide();
    $('#createProductModal').modal('show');
    $('#formCreateProduct').data('is-expense-view', 'true'); // isExpenseView is true
    getBrandDDList();
    getProductCategoryDDList();
    getTimePeriodDDList();
    getExpenseTypeDDList();
    hideSpinner();
}

function getProductDataById(productId) {
    if (productId) {
        loadSpinner();
        $.ajax({
            url: `./product/${productId}/data`,
            method: 'GET',
            success: function (response) {
                if (response && response.data && response.isSuccess) {
                    let productVal = response.data.price;
                    $('#amount').val(productVal);

                    let price = $('#productListDDContainer').data('price')
                    if (price) {
                        $('#amount').val(price)
                    }

                    calcExpenseAmount();
                }
                else {
                    // TODO Alert
                }
            },
            error: function () {
                // TODO Alert
            },
            complete: function () {
                hideSpinner();
            }
        })
    }
}

$(document).on('change', '#formCreateExpense #selectProduct', function () {
    let val = $('#formCreateExpense #selectProduct').val();
    if (val === 'NEW') {
        onCreateProduct();
    } else {
        fetchProductDetails(val);
    }
});

function fetchProductDetails(productId) {
    if (productId) {
        getProductDataById(productId);
        let quantity = $('#quantity').val();
        if (quantity === '' || isNaN(quantity)) {
            $('#quantity').val(1);
        }
        calcExpenseAmount();
        $('#amount, #expenseAmount, #quantity, #expenseDescription').prop('disabled', false);

    } else {
        $('#amount, #expenseAmount, #quantity, #expenseDescription').prop('disabled', true);
    }
}

$(document).on('change', '#formCreateExpense #amount', function () {
    calcExpenseAmount();
});

$(document).on('change', '#formCreateExpense #quantity', function () {
    calcExpenseAmount();
});


function calcExpenseAmount() {
    let amount = parseFloat($('#amount').val());
    let quantity = parseInt($('#quantity').val());
    let expenseAmount = Math.ceil(amount * quantity);
    $('#expenseAmount').val(expenseAmount);
}


$(document).on('change', '#formCreateExpense #expenseDate', function () {
    getExpenseProductInfoList();
});

function getExpenseProductInfoList(date) {
    let expenseDate = date ?? $('#expenseDate').val();
    if (expenseDate) {
        loadSpinner();
        $.ajax({
            url: `./expense-product/${expenseDate}/cart`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divExpenseProductInfoListContainer').html(response);
                    $('#formCreateExpense input[type="select"]').val('');
                    $('#formCreateExpense input[type="number"]').val('');
                }
                else {
                    // TODO Alert
                }
            },
            error: function () {
                // TODO Alert
            },
            complete: function () {
                hideSpinner();
            }
        })
    }

}

function editCartItem(id) {
    if (id) {
        loadSpinner();
        $.ajax({
            url: `./expense-product/${id}/`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divTempExpenseFormContainer').html(response);

                    let memberId = $('#memberListDDContainer').data('val');
                    getMemberDDList(memberId);

                    getProductDDList();
                    let productId = $('#productListDDContainer').data('val');
                    if (productId) {
                        fetchProductDetails(productId)
                    }

                    $('#btnAddExpenseSubmit').text('Update Expense');
                }
                else {
                    // TODO Alert
                }
            },
            error: function () {
                // TODO Alert
            },
            complete: function () {
                hideSpinner();
            }
        })
    }
}

function deleteCartItem(tempExpenseInfoId, ItemName) {
    if (tempExpenseInfoId) {
        loadSpinner();
        $.ajax({
            url: `./expense-product/${tempExpenseInfoId}/`,
            method: 'DELETE',
            success: function (response) {
                if (response && response.data && response.isSuccess) {
                    getExpenseProductInfoList();
                    showSuccessMsg('Expense deleted successfully');
                }
                else {
                    showErrorMsg('Expense delete failed');
                }
            },
            error: function () {
                showErrorMsg('Expense delete failed');
            },
            complete: function () {
                hideSpinner();
            }
        })
    }
}