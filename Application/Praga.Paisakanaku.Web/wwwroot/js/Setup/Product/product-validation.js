jQuery.validator.addMethod('isDropDownValueValid', function (value, element) {
    return this.optional(element) || value;
});

jQuery.validator.addMethod('isPriceValid', function (value, element) {
    return this.optional(element) || value;
});

$(function () {
    $('#formCreateProduct').validate({
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
            productCategory: {
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
            productCategory: {
                isProductCategoryIdValid: 'Product Category is required'
            },
            preferredRecurringTimePeriod: {
                isPreferredRecurringTimePeriod: 'Preferred Recurring Time Period is required'
            },
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnSaveProductSubmit');

            let id = $('#formCreateProduct').data('id');
            let name = $('#formCreateProduct #name').val();
            let description = $('#formCreateProduct #description').val();
            let price = $('#formCreateProduct #price').val();
            let brandId = $('#formCreateProduct #selectBrand').val();
            let brandName = $('#formCreateProduct #brandName').val();
            let productCategory = $('#formCreateProduct #selectProductCategory').val();            
            let timePeriod = $('#formCreateProduct #selectTimePeriod').val();

            if (brandId === 'NEW')
                brandId = ''

            let productInfo = {
                'Id': id,
                'Name': name,
                'Price': price,
                'Description': description,
                'BrandInfo': {
                    'Id': brandId,
                    'Name': brandName
                },
                'ProductCategoryInfo': {
                    'ProductCategory': productCategory
                },
                'PreferredTimePeriodInfo': {
                    'TimePeriodType': timePeriod
                }
            }

            let isUpdate = $('#formCreateProduct').data('isupdate') === 'True';

            $.ajax({
                url: isUpdate ? `./product/update` : `./product/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: productInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createProductModal').modal('hide');
                        showSuccessMsg('Product saved successfully');

                        // ExpenseView
                        let isExpenseView = $('#formCreateProduct').data('is-expense-view');
                        if (isExpenseView === 'true') {
                            //todo get product list
                            $('#formCreateExpense #selectProduct').val(response.data)
                            getProductDDList(response.data); // refresh the product dd to get new product on the list
                            fetchProductDetails(response.data);
                        } else {
                            getProductList();
                        }

                    } else {
                        showErrorMsg('Product save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveProductSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveProductSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateProduct #name').on('focus', function () {
    $('#formCreateProduct  #productHelp').show();
});

$('#formCreateProduct #name').on('blur', function () {
    $('#formCreateProduct  #productHelp').hide();
});

$('#formCreateProduct #description').on('focus', function () {
    $('#formCreateProduct  #product-description-help').show();
});

$('#formCreateProduct #description').on('blur', function () {
    $('#formCreateProduct  #product-description-help').hide();
});

$('#formCreateProduct #price').on('focus', function () {
    $('#formCreateProduct  #product-price-help').show();
});

$('#formCreateProduct #price').on('blur', function () {
    $('#formCreateProduct  #product-price-help').hide();
});

$('#formCreateProduct #brandName').on('focus', function () {
    $('#formCreateProduct  #product-brand-help').show();
});

$('#formCreateProduct #brandName').on('blur', function () {
    $('#formCreateProduct  #product-brand-help').hide();
});

$('#formCreateProduct #productCategoryName').on('focus', function () {
    $('#formCreateProduct  #product-product-category-help').show();
});

$('#formCreateProduct #productCategoryName').on('blur', function () {
    $('#formCreateProduct  #product-product-category-help').hide();
});

$(document).on('change', '#selectBrand', function () {
    let id = $('#formCreateProduct #selectBrand').val();
    if (id === 'NEW') {
        $('#formCreateProduct #brandNameContainer').show();
    } else {
        $('#formCreateProduct #brandNameContainer').hide();
    }
});

$(document).on('change', '#selectProductCategory', function () {
    let id = $('#formCreateProduct #selectProductCategory').val();
    if (id === 'NEW') {
        $('#formCreateProduct #productCategoryNameContainer').show();
    } else {
        $('#formCreateProduct #productCategoryNameContainer').hide();
    }
});

function saveProduct() {
    $('#btnProductSaveSubmit').click();
}