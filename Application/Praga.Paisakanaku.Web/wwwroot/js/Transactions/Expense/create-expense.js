$(document).ready(function () {
    getMemberDDList();
    getProductDDList();
    $('#amount, #expenseAmount, #quantity').prop('disabled', true);
});

function onCreateProduct() {
    loadSpinner();
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
        calcExpenseAmount();
        $('#amount, #expenseAmount, #quantity').prop('disabled', false);
    } else {
        $('#amount, #expenseAmount, #quantity').prop('disabled', true);
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

$(document).on('change', '#formCreateExpense #amount', function () {
    calcExpenseAmount();
});

$(document).on('change', '#formCreateExpense #expenseDate', function () {
    getTempExpenseInfoList();
});

function getTempExpenseInfoList() {
    let expenseDate = $('#expenseDate').val();
    if (expenseDate) {
        loadSpinner();
        $.ajax({
            url: `./expense/temp/date/${expenseDate}/`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divTempProductExpenseInfoListContainer').html(response);
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