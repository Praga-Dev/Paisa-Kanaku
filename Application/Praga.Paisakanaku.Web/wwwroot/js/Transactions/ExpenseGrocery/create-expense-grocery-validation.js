jQuery.validator.addMethod('isExpenseDateValid', function (value, element) {
    return this.optional(element) || isNotFutureDate(value);
});

jQuery.validator.addMethod('isSelectValid', function (value, element) {
    return this.optional(element) || value;
});

jQuery.validator.addMethod('isExpenseAmountValid', function (value) {
    return value && value > 0;
});

$(document).ready(function () {
    $('#formCreateExpenseGrocery').validate({
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
            grocery: {
                required: true,
                isSelectValid: true
            },
            selectMeasureType: {
                required: true,
                isSelectValid: true
            },
            expenseAmount: {
                required: true,
                isExpenseAmountValid: true
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
            grocery: {
                required: 'Grocery is required',
                isSelectValid: 'Grocery is not valid'
            },
            selectMeasureType: {
                required: 'Measure Type is required',
                isSelectValid: 'Measure Type is not valid'
            },
            expenseAmount: {
                required: 'Expense Amount is required',
                isExpenseAmountValid: 'Expense Amount is not valid'
            },
            description: {
                minlength: 'Description is too short'
            }
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnAddExpenseSubmit');

            let id = $('#formCreateExpenseGrocery').data('id');
            let expenseInfoId = $('#formCreateExpenseGrocery').data('expense-id');
            let expenseDate = $('#expenseDate').val();
            let groceryInfoId = $('#selectGrocery').val();
            let expenseByInfoId = $('#selectMember').val();
            let groceryPrice = $('#amount').val();
            let measureType = $('#selectMeasureType').val();
            let quantity = $('#quantity').val();
            let expenseAmount = $('#expenseAmount').val();
            let description = $('#expenseDescription').val();

            let expenseGroceryInfo = {
                'Id': id,
                'ExpenseInfoId': expenseInfoId,
                'ExpenseDate': expenseDate,
                'GroceryInfoId': groceryInfoId,
                'ExpenseByInfoId': expenseByInfoId,
                'GroceryPrice': groceryPrice,
                'MeasureType': measureType,
                'Quantity': quantity,
                'ExpenseAmount': expenseAmount,
                'Description': description
            }

            $.ajax({
                url: `./expense-grocery`,
                method: 'PUT',
                data: expenseGroceryInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        showSuccessMsg('Expense saved successfully');
                        getExpenseGroceryInfoList();
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

function resetExpenseGroceryRelatedInputs() {
    $('#selectGrocery, #selectMeasureType').val('');
    $('#expenseAmount, #quantity, #expenseDescription').prop('disabled', true).val('');
    $('#spanQuantityMeasureTypeInfo').text('');
}

function resetGroceryForm() {
    $('#selectMember, #selectGrocery, #selectMeasureType').val('');
    resetExpenseGroceryRelatedInputs();
}

function getCreateFormView() {
    $('#formCreateExpenseGrocery').data('id', '');
    $('#btnAddExpenseSubmit').text('Create Expense');
    resetExpenseGroceryRelatedInputs();
}