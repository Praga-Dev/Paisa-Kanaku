$(document).ready(function () {
    // TODO switch this to onCreateExpense method
    getMemberDDList();
    getProductDDList();
    $('#amount, #expenseAmount, #quantity').prop('disabled', true);
    getTempExpenseInfoList();
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
        let quantity = $('#quantity').val();
        if (quantity === '' || isNaN(quantity)) {
            $('#quantity').val(1);
        }
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
    let isCreate = $('#formCreateExpense').data('isupdate') == 'False';
    if (isCreate) {
        let amount = parseFloat($('#amount').val());
        let quantity = parseInt($('#quantity').val());
        let expenseAmount = Math.ceil(amount * quantity);
        $('#expenseAmount').val(expenseAmount);
    }

}

$(document).on('change', '#formCreateExpense #amount', function () {
    calcExpenseAmount();
});

$(document).on('change', '#formCreateExpense #expenseDate', function () {
    getTempExpenseInfoList();
});

function getTempExpenseInfoList(date) {
    let expenseDate = date ?? $('#expenseDate').val();
    if (expenseDate) {
        loadSpinner();
        $.ajax({
            url: `./expense/temp/date/${expenseDate}/`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divTempProductExpenseInfoListContainer').html(response);
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
            url: `./expense/temp/${id}/`,
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