$(function () {
    $('#formCreateProductCategory').validate({
        errorElement: 'span',
        rules: {
            name: {
                required: true,
                minlength: 2,
            }
        },
        messages: {
            name: {
                required: $('#name').data('err-required'),
                minlength: $('#name').data('err-min-length')
            }
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnSaveProductCategorySubmit');

            let id = $('#formCreateProductCategory').data('id');
            let name = $('#name').val();
            let productCategoryInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateProductCategory').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./product-category/update` : `./product-category/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: productCategoryInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createProductCategoryModal').modal('hide');
                        showSuccessMsg('Product Category saved successfully');
                        getProductCategoryList();
                    } else {
                        showErrorMsg('Product Category save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveProductCategorySubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveProductCategorySubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateProductCategory #name').on('focus', function () {
    $('#formCreateProductCategory  #productCategoryHelp').show();
});

$('#formCreateProductCategory #name').on('blur', function () {
    $('#formCreateProductCategory  #productCategoryHelp').hide();
});

function saveProductCategory() {
    $('#btnProductCategorySaveSubmit').click();
}