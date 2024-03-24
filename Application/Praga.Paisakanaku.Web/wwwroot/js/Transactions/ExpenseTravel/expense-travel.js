let EXPENSE_TRAVEL_FORM_ID = '#formCreateExpenseTravel';

$(function () {
    getExpenseTravelInfoList();
    getMemberDDList();
    getTransportModeList();
    getTravelServiceList();
    resetForm();
});

$(document).on('change', `${EXPENSE_TRAVEL_FORM_ID} #expenseDate`, function () {
    getExpenseTravelInfoList();
});

function getExpenseTravelInfoList(date) {
    let expenseDate = date ?? $('#expenseDate').val();
    if (expenseDate) {
        loadSpinner();
        $.ajax({
            url: `./expense-travel/${expenseDate}/cart`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divExpenseTravelInfoListContainer').html(response);
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
            url: `./expense-travel/${id}/`,
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
            url: `./expense-travel/${tempExpenseInfoId}/`,
            method: 'DELETE',
            success: function (response) {
                if (response && response.data && response.isSuccess) {
                    getExpenseTravelInfoList();
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