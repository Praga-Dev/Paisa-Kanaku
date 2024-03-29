﻿jQuery.validator.addMethod('isDropDownValueValid', function (value, element) {
    return this.optional(element) || value;
});

$(function () {
    $('#formCreateMember').validate({
        errorElement: 'span',
        rules: {
            name: {
                required: true,
                minlength: 2,
            },
            relationshipType: {
                isDropDownValueValid: true
            },
        },
        messages: {
            name: {
                required: $('#name').data('err-required'),
                minlength: $('#name').data('err-min-length')
            },
            relationshipType: {
                isDropDownValueValid: 'Relationship is required'
            },
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnSaveMemberSubmit');

            let id = $('#formCreateMember').data('id');
            let name = $('#name').val();
            let relationshipType = $('#selectRelationshipType').val();

            let memberInfo = {
                'Id': id,
                'Name': name,
                'RelationshipTypeInfo': {
                    'RelationshipType': relationshipType
                }
            }

            let isUpdate = $('#formCreateMember').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./member/update` : `./member/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: memberInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createMemberModal').modal('hide');
                        showSuccessMsg('Member saved successfully');
                        getMemberList();
                    } else {
                        showErrorMsg('Member save failed');
                    }
                },
                error: function (error) {
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveMemberSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveMemberSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateMember #name').on('focus', function () {
    $('#formCreateMember  #memberHelp').show();
});

$('#formCreateMember #name').on('blur', function () {
    $('#formCreateMember  #memberHelp').hide();
});

function saveMember() {
    $('#btnMemberSaveSubmit').click();
}