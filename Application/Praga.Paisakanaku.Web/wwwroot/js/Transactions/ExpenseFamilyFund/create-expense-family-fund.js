let EXPENSE_FAMILY_FUND_FORM_ID = '#formCreateExpenseFamilyFund';

$(document).ready(function () {
    getMemberDDList();
    getExpenseFamilyFundInfoList();
    resetForm();
});

function getRecipientDDList(recipientId = '') {
    debugger;
    loadSpinner();
    let excludedMemberInfoId = $('#selectMember').val();
    if (excludedMemberInfoId) {
        $.ajax({
            url: `./member/data-list/${excludedMemberInfoId}`,
            method: 'GET',
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#recipientListDDContainer').html(response);

                    if (recipientId) {
                        $('#selectRecipient').val(recipientId);
                    }
                    $('#selectRecipient').prop('disabled', false);
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

$(document).on('change', `${EXPENSE_FAMILY_FUND_FORM_ID} #selectMember`, function () {
    getRecipientDDList();
});

$(document).on('change', `${EXPENSE_FAMILY_FUND_FORM_ID} #selectRecipient`, function () {
    $('#expenseAmount, #expenseDescription').prop('disabled', false);
});

$(document).on('change', `${EXPENSE_FAMILY_FUND_FORM_ID} #expenseDate`, function () {
    getExpenseFamilyFundInfoList();
});

function getExpenseFamilyFundInfoList(date) {
    let expenseDate = date ?? $('#expenseDate').val();
    if (expenseDate) {
        loadSpinner();
        $.ajax({
            url: `./expense-family-fund/${expenseDate}/cart`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divExpenseFamilyFundInfoListContainer').html(response);
                    $(`${EXPENSE_FAMILY_FUND_FORM_ID} input[type="select"]`).val('');
                    $(`${EXPENSE_FAMILY_FUND_FORM_ID} input[type="number"]`).val('');
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

function fetchRecipientDetails(familyMemberId) {
    if (familyMemberId) {
        getRecipientDDList(familyMemberId);
        $('#expenseAmount, #selectRecipient, #expenseDescription').prop('disabled', false);
    } else {
        resetExpenseFamilyFundRelatedInputs();
    }
}

function editCartItem(id) {
    if (id) {
        loadSpinner();
        $.ajax({
            url: `./expense-family-fund/${id}/`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divTempExpenseFormContainer').html(response);
                    getRecipientDDList();

                    let memberId = $('#memberListDDContainer').data('val');
                    getMemberDDList(memberId);

                    let familyMemberId = $('#recipientListDDContainer').data('val');
                    if (familyMemberId) {
                        fetchRecipientDetails(familyMemberId)
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
            url: `./expense-family-fund/${tempExpenseInfoId}/`,
            method: 'DELETE',
            success: function (response) {
                if (response && response.data && response.isSuccess) {
                    getExpenseFamilyFundInfoList();
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