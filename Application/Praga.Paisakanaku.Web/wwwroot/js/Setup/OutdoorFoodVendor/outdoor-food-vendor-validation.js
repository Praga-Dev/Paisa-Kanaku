$(function () {
    $('#formCreateOutdoorFoodVendor').validate({
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
            disableBtnById('btnSaveOutdoorFoodVendorSubmit');

            let id = $('#formCreateOutdoorFoodVendor').data('id');
            let name = $('#name').val();
            let outdoorFoodVendorInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateOutdoorFoodVendor').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./outdoor-food-vendor/update` : `./outdoor-food-vendor/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: outdoorFoodVendorInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createOutdoorFoodVendorModal').modal('hide');
                        showSuccessMsg('Outdoor Food Vendor saved successfully');
                        getOutdoorFoodVendorList();
                    } else {
                        showErrorMsg('Outdoor Food Vendor save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveOutdoorFoodVendorSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveOutdoorFoodVendorSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateOutdoorFoodVendor #name').on('focus', function () {
    $('#formCreateOutdoorFoodVendor  #outdoorFoodVendorHelp').show();
});

$('#formCreateOutdoorFoodVendor #name').on('blur', function () {
    $('#formCreateOutdoorFoodVendor  #outdoorFoodVendorHelp').hide();
});

function saveOutdoorFoodVendor() {
    $('#btnOutdoorFoodVendorSaveSubmit').click();
}