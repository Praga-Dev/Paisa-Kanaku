jQuery.validator.addMethod('isExpenseDateValid', function (value, element) {
    return this.optional(element) || isNotFutureDate(value);
});

jQuery.validator.addMethod('isSelectMemberValid', function (value, element) {
    return this.optional(element) || value;
});

jQuery.validator.addMethod('isExpenseAmountValid', function (value) {
    return value && value > 0;
});

$(document).ready(function () {
    $(EXPENSE_FAMILY_FUND_FORM_ID).validate({
        errorElement: 'span',
        rules:
        {
            expenseDate:
            {
                required: true,
                isExpenseDateValid: true
            },
            selectMember:
            {
                required: true,
                isSelectMemberValid: true
            },
            selectRecipient:
            {
                required: true,
                isSelectMemberValid: true
            },
            expenseAmount:
            {
                required: true,
                isExpenseAmountValid: true
            },
            description:
            {
                minlength: 2
            }
        },
        messages:
        {
            expenseDate:
            {
                required: 'Expense Date is required',
                isExpenseDateValid: 'Expense Date should not be future date'
            },
            selectMember:
            {
                required: 'Member is required',
                isSelectMemberValid: 'Member is not valid'
            },
            selectRecipient:
            {
                required: 'Member is required',
                isSelectMemberValid: 'Member is not valid'
            },
            expenseAmount:
            {
                required: 'Expense Amount is required',
                isExpenseAmountValid: 'Expense Amount is not valid'
            },
            description:
            {
                minlength: 'Description is too short'
            }
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnAddExpenseSubmit');

            let id = $(EXPENSE_FAMILY_FUND_FORM_ID).data('id');
            let expenseInfoId = $(EXPENSE_FAMILY_FUND_FORM_ID).data('expense-id');
            let expenseDate = $('#expenseDate').val();
            let expenseByInfoId = $('#selectMember').val();
            let recipientInfoId = $('#selectRecipient').val();
            let expenseAmount = $('#expenseAmount').val();
            let description = $('#expenseDescription').val();

            let expenseFamilyWellbeingSaveRequestDTO = {
                'Id': id,
                'ExpenseInfoId': expenseInfoId,
                'ExpenseDate': expenseDate,
                'ExpenseByInfoId': expenseByInfoId,
                'RecipientInfoId': recipientInfoId,
                'ExpenseAmount': expenseAmount,
                'Description': description
            }

            $.ajax({
                url: `./expense-family-wellbeing`,
                method: 'PUT',
                data: expenseFamilyWellbeingSaveRequestDTO,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        showSuccessMsg('Expense saved successfully');
                        getExpenseFamilyWellbeingInfoList();
                        getCreateFormView();
                    }
                    else {
                        showErrorMsg('Expense save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnAddExpenseSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnAddExpenseSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
});

function resetExpenseFamilyWellbeingRelatedInputs() {
    $('#expenseAmount, #selectRecipient, #expenseDescription').prop('disabled', true).val('');
}

function resetForm() {
    $(EXPENSE_FAMILY_FUND_FORM_ID).trigger("reset");
    $('#selectMember').val('');
    resetExpenseFamilyWellbeingRelatedInputs();
}

function getCreateFormView() {
    $(EXPENSE_FAMILY_FUND_FORM_ID).data('id', '');
    $('#btnAddExpenseSubmit').text('Create Family Wellbeing Expense');
    resetForm();
}