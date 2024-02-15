$(function () {
    $('#formCreateBillType').validate({
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
            disableBtnById('btnSaveBillTypeSubmit');

            let id = $('#formCreateBillType').data('id');
            let name = $('#name').val();
            let billTypeInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateBillType').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./bill-type/update` : `./bill-type/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: billTypeInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createBillTypeModal').modal('hide');
                        showSuccessMsg('BillType saved successfully');
                        getBillTypeList();
                    } else {
                        showErrorMsg('BillType save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveBillTypeSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveBillTypeSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateBillType #name').on('focus', function () {
    $('#formCreateBillType  #billTypeHelp').show();
});

$('#formCreateBillType #name').on('blur', function () {
    $('#formCreateBillType  #billTypeHelp').hide();
});

function saveBillType() {
    $('#btnBillTypeSaveSubmit').click();
}