jQuery.validator.addMethod('isDropDownValueValid', function (value, element) {
    return this.optional(element) || value;
});

jQuery.validator.addMethod('isPriceValid', function (value, element) {
    return this.optional(element) || value;
});

$(function () {
    $('#formCreateGrocery').validate({
        errorElement: 'span',
        rules: {
            name: {
                required: true,
                minlength: 2,
            },
            description: {
                minlength: 2
            },
            price: {
                required: true,
                number: true
            },
            brandId: {
                isDropDownValueValid: true
            },
            brandName: {
                required: true,
                minlength: 2,
            },
            groceryCategory: {
                isDropDownValueValid: true
            },
            preferredRecurringTimePeriod: {
                isDropDownValueValid: true
            },
        },
        messages: {
            name: {
                required: 'Name is required',
                minlength: 'Name is too short'
            },
            description: {
                minlength: 'Description is too short'
            },
            brandId: {
                isBrandIdValid: 'Brand is required'
            },
            brandName: {
                required: 'Brand Name is required',
                minlength: 'Brand Name is too short'
            },
            groceryCategory: {
                isGroceryCategoryIdValid: 'Grocery Category is required'
            },
            preferredRecurringTimePeriod: {
                isPreferredRecurringTimePeriod: 'Preferred Recurring Time Period is required'
            },
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnSaveGrocerySubmit');

            let id = $('#formCreateGrocery').data('id');
            let name = $('#formCreateGrocery #name').val();
            let description = $('#formCreateGrocery #description').val();
            let price = $('#formCreateGrocery #price').val();
            let brandId = $('#formCreateGrocery #selectBrand').val();
            let brandName = $('#formCreateGrocery #brandName').val();
            let groceryCategory = $('#formCreateGrocery #selectGroceryCategory').val();
            let timePeriod = $('#formCreateGrocery #selectTimePeriod').val();

            if (brandId === 'NEW')
                brandId = ''

            let groceryInfo = {
                'Id': id,
                'Name': name,
                'Price': price,
                'Description': description,
                'BrandInfo': {
                    'Id': brandId,
                    'Name': brandName
                },
                'GroceryCategoryInfo': {
                    'GroceryCategory': groceryCategory
                },
                'PreferredTimePeriodInfo': {
                    'TimePeriodType': timePeriod
                }
            }

            let isUpdate = $('#formCreateGrocery').data('isupdate') === 'True';

            $.ajax({
                url: isUpdate ? `./grocery/update` : `./grocery/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: groceryInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createGroceryModal').modal('hide');
                        showSuccessMsg('Grocery saved successfully');

                        // ExpenseView
                        let isExpenseView = $('#formCreateGrocery').data('is-expense-view');
                        if (isExpenseView === 'true') {
                            //todo get grocery list
                            $('#formCreateExpenseGrocery #selectGrocery').val(response.data)
                            getGroceryDDList(response.data); // refresh the grocery dd to get new grocery on the list
                            fetchGroceryDetails(response.data);
                        } else {
                            getGroceryList();
                        }

                    } else {
                        showErrorMsg('Grocery save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveGrocerySubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveGrocerySubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateGrocery #name').on('focus', function () {
    $('#formCreateGrocery  #groceryHelp').show();
});

$('#formCreateGrocery #name').on('blur', function () {
    $('#formCreateGrocery  #groceryHelp').hide();
});

$('#formCreateGrocery #description').on('focus', function () {
    $('#formCreateGrocery  #grocery-description-help').show();
});

$('#formCreateGrocery #description').on('blur', function () {
    $('#formCreateGrocery  #grocery-description-help').hide();
});

$('#formCreateGrocery #price').on('focus', function () {
    $('#formCreateGrocery  #grocery-price-help').show();
});

$('#formCreateGrocery #price').on('blur', function () {
    $('#formCreateGrocery  #grocery-price-help').hide();
});

$('#formCreateGrocery #brandName').on('focus', function () {
    $('#formCreateGrocery  #grocery-brand-help').show();
});

$('#formCreateGrocery #brandName').on('blur', function () {
    $('#formCreateGrocery  #grocery-brand-help').hide();
});

$('#formCreateGrocery #groceryCategoryName').on('focus', function () {
    $('#formCreateGrocery  #grocery-grocery-category-help').show();
});

$('#formCreateGrocery #groceryCategoryName').on('blur', function () {
    $('#formCreateGrocery  #grocery-grocery-category-help').hide();
});

$(document).on('change', '#selectBrand', function () {
    let id = $('#formCreateGrocery #selectBrand').val();
    if (id === 'NEW') {
        $('#formCreateGrocery #brandNameContainer').show();
    } else {
        $('#formCreateGrocery #brandNameContainer').hide();
    }
});

$(document).on('change', '#selectGroceryCategory', function () {
    let id = $('#formCreateGrocery #selectGroceryCategory').val();
    if (id === 'NEW') {
        $('#formCreateGrocery #groceryCategoryNameContainer').show();
    } else {
        $('#formCreateGrocery #groceryCategoryNameContainer').hide();
    }
});

function saveGrocery() {
    $('#btnGrocerySaveSubmit').click();
}