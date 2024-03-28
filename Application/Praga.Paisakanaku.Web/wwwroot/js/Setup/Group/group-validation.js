$(function () {
    $('#formCreateGroup').validate({
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
            disableBtnById('btnSaveGroupSubmit');

            let id = $('#formCreateGroup').data('id');
            let name = $('#name').val();
            let groupInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateGroup').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./group/update` : `./group/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: groupInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createGroupModal').modal('hide');
                        showSuccessMsg('Group saved successfully');
                        getGroupList();
                    } else {
                        showErrorMsg('Group save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveGroupSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveGroupSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateGroup #name').on('focus', function () {
    $('#formCreateGroup  #groupHelp').show();
});

$('#formCreateGroup #name').on('blur', function () {
    $('#formCreateGroup  #groupHelp').hide();
});

function saveGroup() {
    $('#btnGroupSaveSubmit').click();
}