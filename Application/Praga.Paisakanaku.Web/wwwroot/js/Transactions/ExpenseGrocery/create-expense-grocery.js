$(document).ready(function () {
    getMemberDDList();
    getGroceryDDList();
    getExpenseGroceryInfoList();
    resetForm();
});

function onCreateGrocery() {
    loadSpinner();
    resetForm();
    $('#formCreateGrocery').trigger("reset");
    $('#formCreateGrocery').data('id', '');
    $('#formCreateGrocery').data('isupdate', 'False');
    $('#formCreateGrocery').find(':input,select').val('');
    $('#formCreateGrocery').find('span.error').hide();
    $('#createGroceryTitle').text('Create Grocery');
    $('#brandNameContainer').hide();
    $('#groceryCategoryNameContainer').hide();
    $('#createGroceryModal').modal('show');
    $('#formCreateGrocery').data('is-expense-view', 'true'); // isExpenseView is true
    getBrandDDList();
    getGroceryCategoryDDList();
    getTimePeriodDDList();
    getExpenseTypeDDList();
    hideSpinner();
}

function getGroceryDataById(groceryId) {
    if (groceryId) {
        loadSpinner();
        disableBtnById(`btnEditExpenseGroceryCartItem_${groceryId}`);
        $.ajax({
            url: `./grocery/${groceryId}/data`,
            method: 'GET',
            success: function (response) {
                if (response && response.data && response.isSuccess) {
                    groceryInfo = response.data;
                    if (groceryInfo.measureTypeInfo) {
                        $('#spanQuantityMeasureTypeInfo').text(groceryInfo.measureTypeInfo.measureTypeValue);
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
                enableBtnById(`btnEditExpenseGroceryCartItem_${groceryId}`);

            }
        })
    }
}

$(document).on('change', '#formCreateExpenseGrocery #selectGrocery', function () {
    let val = $('#formCreateExpenseGrocery #selectGrocery').val();
    if (val === 'NEW') {
        onCreateGrocery();
    } else {
        fetchGroceryDetails(val);
    }
});

function fetchGroceryDetails(groceryId) {
    if (groceryId) {
        getGroceryDataById(groceryId);
        $('#expenseAmount, #quantity, #expenseDescription').prop('disabled', false);
    } else {
        resetExpenseGroceryRelatedInputs()
    }
}

$(document).on('change', '#formCreateExpenseGrocery #expenseDate', function () {
    getExpenseGroceryInfoList();
});

function getExpenseGroceryInfoList(date) {
    let expenseDate = date ?? $('#expenseDate').val();
    if (expenseDate) {
        loadSpinner();
        $.ajax({
            url: `./expense-grocery/${expenseDate}/cart`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divExpenseGroceryInfoListContainer').html(response);
                    $('#formCreateExpenseGrocery input[type="select"]').val('');
                    $('#formCreateExpenseGrocery input[type="number"]').val('');
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
            url: `./expense-grocery/${id}/`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divTempExpenseFormContainer').html(response);
                    getGroceryDDList();

                    let memberId = $('#memberListDDContainer').data('val');
                    getMemberDDList(memberId);

                    let groceryId = $('#groceryListDDContainer').data('val');
                    if (groceryId) {
                        fetchGroceryDetails(groceryId)
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
            url: `./expense-grocery/${tempExpenseInfoId}/`,
            method: 'DELETE',
            success: function (response) {
                if (response && response.data && response.isSuccess) {
                    getExpenseGroceryInfoList();
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