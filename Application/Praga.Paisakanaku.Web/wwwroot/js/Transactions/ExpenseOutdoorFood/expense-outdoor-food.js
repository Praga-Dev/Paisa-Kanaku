let EXPENSE_OUTDOOR_FOOD_FORM_ID = '#formCreateExpenseOutdoorFood';

$(document).ready(function () {
    getMemberDDList();
    getOutdoorFoodVendorDDList();
    getExpenseOutdoorFoodInfoList();
    resetForm();
});

$(document).on('change', `${EXPENSE_OUTDOOR_FOOD_FORM_ID} #expenseDate`, function () {
    getExpenseOutdoorFoodInfoList();
});

$(document).on('change', `${EXPENSE_OUTDOOR_FOOD_FORM_ID} #selectOutdoorFoodVendorInfo`, function () {
    $('#expenseAmount, #expenseBillImageURL, #expenseDescription').prop('disabled', false).val('');
});

function getExpenseOutdoorFoodInfoList(date) {
    let expenseDate = date ?? $('#expenseDate').val();
    if (expenseDate) {
        loadSpinner();
        $.ajax({
            url: `./expense-outdoor-food/${expenseDate}/cart`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divExpenseOutdoorFoodInfoListContainer').html(response);
                    $(`${EXPENSE_OUTDOOR_FOOD_FORM_ID} input[type="select"]`).val('');
                    $(`${EXPENSE_OUTDOOR_FOOD_FORM_ID} input[type="number"]`).val('');
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
            url: `./expense-outdoor-food/${id}/`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divTempExpenseFormContainer').html(response);
                    let memberId = $('#memberListDDContainer').data('val');
                    getMemberDDList(memberId);

                    let outdoorFoodVendorId = $('#outdoorFoodVendorListDDContainer').data('val');
                    if (outdoorFoodVendorId) {
                        getOutdoorFoodVendorDDList(outdoorFoodVendorId);
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
            url: `./expense-outdoor-food/${tempExpenseInfoId}/`,
            method: 'DELETE',
            success: function (response) {
                if (response && response.data && response.isSuccess) {
                    getExpenseOutdoorFoodInfoList();
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