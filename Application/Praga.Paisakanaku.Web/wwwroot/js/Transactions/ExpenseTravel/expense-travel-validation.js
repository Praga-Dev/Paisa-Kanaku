jQuery.validator.addMethod('isExpenseDateValid', function (value, element) {
    return this.optional(element) || isNotFutureDate(value);
});

jQuery.validator.addMethod('isSelectValid', function (value, element) {
    return value && value.trim() !== '';
});

jQuery.validator.addMethod('isExpenseAmountValid', function (value) {
    return value && value > 0;
});

$(document).ready(function () {
    $(EXPENSE_TRAVEL_FORM_ID).validate({
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
            source: {
                required: true,
            },
            destination: {
                required: true,
            },
            expenseAmount: {
                required: true,
                isExpenseAmountValid: true
            },
            travelService: {
                isSelectValid: true
            },
            transportMode: {
                isSelectValid: true
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
            source: {
                required: 'From is required',
            },
            destination: {
                required: 'To is required',
            },
            expenseAmount: {
                required: 'Expense Amount is required',
                isExpenseAmountValid: 'Expense Amount is not valid'
            },
            travelService: {
                isSelectValid: 'Travel Service is not valid'
            },
            transportMode: {
                isSelectValid: 'Transport Mode is not valid'
            },
            description: {
                minlength: 'Description is too short'
            }
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnAddExpenseSubmit');

            let id = $(EXPENSE_TRAVEL_FORM_ID).data('id');
            let expenseInfoId = $(EXPENSE_TRAVEL_FORM_ID).data('expense-id');
            let expenseDate = $('#expenseDate').val();
            let expenseByInfoId = $('#selectMember').val();
            let source = $('#source').val();
            let destination = $('#destination').val();
            let travelService = $('#selectTravelService').val();
            let transportMode = $('#selectTransportMode').val();
            let expenseAmount = $('#expenseAmount').val();
            let description = $('#expenseDescription').val();

            let expenseTravelInfo = {
                'Id': id,
                'ExpenseInfoId': expenseInfoId,
                'ExpenseDate': expenseDate,
                'ExpenseByInfoId': expenseByInfoId,
                'Source': source,
                'Destination': destination,
                'TransportMode': transportMode,
                'TravelService': travelService,
                'ExpenseAmount': expenseAmount,
                'Description': description
            }

            $.ajax({
                url: `./expense-travel`,
                method: 'PUT',
                data: expenseTravelInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        showSuccessMsg('Expense saved successfully');
                        getExpenseTravelInfoList();
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

function resetExpenseTravelRelatedInputs() {
    //$('#expenseAmount, #expenseDescription').prop('disabled', true).val('');
    $('#source, #destination, #expenseAmount, #expenseDescription').val('');
    $('#selectTravelService', '#selectTransportMode').val('');
}

function resetForm() {
    $(EXPENSE_TRAVEL_FORM_ID).trigger("reset");
    $('#selectMember').val('');
    resetExpenseTravelRelatedInputs();
}

function getCreateFormView() {
    $(EXPENSE_TRAVEL_FORM_ID).data('id', '');
    $('#btnAddExpenseSubmit').text('Create Expense');
    resetExpenseTravelRelatedInputs();
}