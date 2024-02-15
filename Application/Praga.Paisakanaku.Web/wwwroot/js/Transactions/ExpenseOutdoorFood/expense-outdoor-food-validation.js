jQuery.validator.addMethod('isExpenseDateValid', function (value, element) {
    return this.optional(element) || isNotFutureDate(value);
});

jQuery.validator.addMethod('isSelectValid', function (value, element) {
    return this.optional(element) || value;
});

jQuery.validator.addMethod('isExpenseAmountValid', function (value) {
    return value && value > 0;
});

jQuery.validator.addMethod('isValidURL', function (value) {
    if (value)
        return isValidURL(value);
    return true;
});

$(document).ready(function () {
    $('#formCreateExpenseOutdoorFood').validate({
        errorElement: 'span',
        rules: {
            expenseDate: {
                required: true,
                isExpenseDateValid: true
            },
            member: {
                required: true,
                isSelectValid: true
            },
            outdoorFoodInfoVendor: {
                required: true,
                isSelectValid: true
            },
            expenseAmount: {
                required: true,
                isExpenseAmountValid: true
            },
            expenseBillImageURL: {
                minlength: 2,
                isValidURL: true
            },
            description: {
                minlength: 2
            }
        },
        messages: {
            expenseDate: {
                required: 'Expense Date is required',
                isExpenseDateValid: 'Expense Date should not be future date'
            },
            member: {
                required: 'Member is required',
                isSelectValid: 'Member is not valid'
            },
            outdoorFoodInfoVendor: {
                required: 'Outdoor Food Vendor is required',
                isSelectValid: 'Outdoor Food Vendor is not valid'
            },
            expenseAmount: {
                required: 'Expense Amount is required',
                isExpenseAmountValid: 'Expense Amount is not valid'
            },
            expenseBillImageURL: {
                minlength: 'Bill Image URL is too short',
                isValidURL: 'Bill Image URL is not valid',
            },
            description: {
                minlength: 'Description is too short'
            }
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnAddExpenseSubmit');

            let id = $('#formCreateExpenseOutdoorFood').data('id');
            let expenseInfoId = $('#formCreateExpenseOutdoorFood').data('expense-id');
            let expenseDate = $('#expenseDate').val();
            let expenseByInfoId = $('#selectMember').val();
            let outdoorFoodVendorInfoId = $('#selectOutdoorFoodVendorInfo').val();
            let expenseAmount = $('#expenseAmount').val();
            let expenseBillImageURL = $('#expenseBillImageURL').val();
            let description = $('#expenseDescription').val();

            let expenseOutdoorFoodInfo = {
                'Id': id,
                'ExpenseInfoId': expenseInfoId,
                'ExpenseDate': expenseDate,
                'ExpenseByInfoId': expenseByInfoId,
                'OutdoorFoodVendorInfoId': outdoorFoodVendorInfoId,
                'ExpenseAmount': expenseAmount,
                'BillImageURL': expenseBillImageURL,
                'Description': description
            }

            $.ajax({
                url: `./expense-outdoor-food`,
                method: 'PUT',
                data: expenseOutdoorFoodInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        showSuccessMsg('Expense saved successfully');
                        getExpenseOutdoorFoodInfoList();
                        getCreateFormView();
                    } else {
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

function resetExpenseOutdoorFoodRelatedInputs() {
    $('#selectOutdoorFoodVendorInfo').val('');
    $('#expenseAmount, #expenseBillImageURL, #expenseDescription').prop('disabled', true).val('');
    $('#spanQuantityMeasureTypeInfo').text('');
}

function resetForm() {
    $(EXPENSE_OUTDOOR_FOOD_FORM_ID).trigger("reset");
    $('#selectMember').val('');
    resetExpenseOutdoorFoodRelatedInputs();
}

function getCreateFormView() {
    $('#formCreateExpenseOutdoorFood').data('id', '');
    $('#btnAddExpenseSubmit').text('Create Expense');
    resetExpenseOutdoorFoodRelatedInputs();
}