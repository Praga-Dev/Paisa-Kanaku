$(function () {
    $('#formCreateBrand').validate({
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
            disableBtnById('btnSaveBrandSubmit');

            let id = $('#formCreateBrand').data('id');
            let name = $('#name').val();
            let brandInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateBrand').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./brand/update` : `./brand/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: brandInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createBrandModal').modal('hide');
                        showSuccessMsg('Brand saved successfully');
                        getBrandList();
                    } else {
                        showErrorMsg('Brand save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveBrandSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveBrandSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateBrand #name').on('focus', function () {
    $('#formCreateBrand  #brandHelp').show();
});

$('#formCreateBrand #name').on('blur', function () {
    $('#formCreateBrand  #brandHelp').hide();
});

function saveBrand() {
    $('#btnBrandSaveSubmit').click();
}