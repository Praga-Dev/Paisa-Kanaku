$(function () {
    $('#formCreateRepairType').validate({
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
            disableBtnById('btnSaveRepairTypeSubmit');

            let id = $('#formCreateRepairType').data('id');
            let name = $('#name').val();
            let repairTypeInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateRepairType').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./repair-type/update` : `./repair-type/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: repairTypeInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createRepairTypeModal').modal('hide');
                        showSuccessMsg('RepairType saved successfully');
                        getRepairTypeList();
                    } else {
                        showErrorMsg('RepairType save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveRepairTypeSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveRepairTypeSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateRepairType #name').on('focus', function () {
    $('#formCreateRepairType  #repairTypeHelp').show();
});

$('#formCreateRepairType #name').on('blur', function () {
    $('#formCreateRepairType  #repairTypeHelp').hide();
});

function saveRepairType() {
    $('#btnRepairTypeSaveSubmit').click();
}