let EXPENSE_FAMILY_FUND_FORM_ID = '#formCreateExpenseFamilyWellbeing';

$(document).ready(function () {
    getMemberDDList();
    getExpenseFamilyWellbeingInfoList();
    resetForm();
});

function getRecipientDDList(recipientId = '') {
    loadSpinner();
    let excludedMemberInfoId = $('#selectMember').val();
    if (excludedMemberInfoId) {
        $.ajax({
            url: `./member/data-list/${excludedMemberInfoId}`,
            method: 'GET',
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#recipientListDDContainer').html(response);
                    if (!recipientId) {
                        recipientId = $('#recipientListDDContainer').data('val')
                    }

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
    getExpenseFamilyWellbeingInfoList();
});

function getExpenseFamilyWellbeingInfoList(date) {
    let expenseDate = date ?? $('#expenseDate').val();
    if (expenseDate) {
        loadSpinner();
        $.ajax({
            url: `./expense-family-wellbeing/${expenseDate}/cart`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divExpenseFamilyWellbeingInfoListContainer').html(response);
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
        resetExpenseFamilyWellbeingRelatedInputs();
    }
}

function editCartItem(id) {
    if (id) {
        loadSpinner();
        $.ajax({
            url: `./expense-family-wellbeing/${id}/`,
            method: 'GET',
            success: function (response) {
                if (response) {
                    $('#divTempExpenseFormContainer').html(response);
                    getRecipientDDList();
                    let memberId = $('#memberListDDContainer').data('val');
                    getMemberDDList(memberId, getRecipientDDList);

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
            url: `./expense-family-wellbeing/${tempExpenseInfoId}/`,
            method: 'DELETE',
            success: function (response) {
                if (response && response.data && response.isSuccess) {
                    getExpenseFamilyWellbeingInfoList();
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