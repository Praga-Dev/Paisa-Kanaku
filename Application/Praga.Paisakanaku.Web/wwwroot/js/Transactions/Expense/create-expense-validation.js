jQuery.validator.addMethod('isExpenseDateValid', function (value, element) {
    return this.optional(element) || isNotFutureDate(value);
});

jQuery.validator.addMethod('isSelectMemberValid', function (value, element) {
    return this.optional(element) || value;
});

jQuery.validator.addMethod('isSelectProductValid', function (value, element) {
    return this.optional(element) || value;
});

jQuery.validator.addMethod('isExpenseAmountValid', function (value) {
    return value && value > 0;
});

$(document).ready(function () {
    $('#formCreateExpense').validate({
        errorElement: 'span',
        rules: {
            expenseDate: {
                required: true,
                isExpenseDateValid: true
            },
            member: {
                required: true,
                isSelectMemberValid: true
            },
            product: {
                required: true,
                isSelectProductValid: true
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
                isSelectMemberValid: 'Member is not valid'
            },
            product: {
                required: 'Product is required',
                isSelectProductValid: 'Product is not valid'
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

            let id = $('#formCreateExpense').data('id');
            let expenseById = $('#selectMember').val();
            let expenseDate = $('#expenseDate').val();
            let productId = $('#selectProduct').val();
            let quantity = $('#quantity').val();
            let expenseAmount = $('#expenseAmount').val();
            let description = $('#expenseDescription').val();

            let expenseBy = {
                'Id': expenseById,                
            }

            let tempProductExpenseInfo = {
                'Id': id,
                'ExpenseBy': expenseBy,
                'ExpenseDate': expenseDate,
                'ProductId': productId,
                'Quantity': quantity,
                'ExpenseAmount': expenseAmount,
                'Description' : description
            }

            debugger;

            $.ajax({
                url: `./expense/product/temp`,
                method: 'PUT',
                data: tempProductExpenseInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        showSuccessMsg('Expense saved successfully');
                        getTempExpenseInfoList();
                        resetTempExpenseForm();
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

function resetTempExpenseForm() {
    $('#selectProduct, #expenseDescription').val('');
    $('#amount, #expenseAmount, #quantity, #expenseDescription').prop('disabled', true).val('');
}

function createExpenseInfo() {
    let expenseDate = $('#expenseDate').val();

    if (expenseData && expenseData.length > 0 && expenseDate) {
        loadSpinner();
        disableBtnById('btnCreateExpense');

        let expenseSaveInfo = {
            'ExpenseDate': expenseDate,
            'ExpenseItemBaseInfoList': expenseData
        }

        $.ajax({
            url: `./expense/`,
            method: 'POST',
            data: expenseSaveInfo,
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                    showSuccessMsg('Expense created successfully');
                    // clear table
                } else {
                    showErrorMsg('Expense create failed');
                }
            },
            error: function (error) {
                showErrorMsg('Something went wrong');
            },
            complete: function () {
                hideSpinner();
                enableBtnById('btnCreateExpense');
            }
        });
    }
}