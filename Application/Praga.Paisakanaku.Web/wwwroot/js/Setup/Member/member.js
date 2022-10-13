function getMemberList() {
    loadSpinner();
    $.ajax({
        url: `./member/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#memberListContainer').html(response);
            }
            else {
                // TODO Alert
            }
        },
        error: function () {
            // TODO Alert
        },
        complete: function () {
            hideSpinner();
        }
    })
}


function onCreateMember() {
    loadSpinner();
    $('#formCreateMember').trigger("reset");
    $('#formCreateMember').data('id', '');
    $('#formCreateMember').data('isupdate', 'False');
    $('#formCreateMember').find(':input,select').val('');
    $('#formCreateMember').find('span.error').hide();
    $('#createMemberTitle').text('Create Member');
    $('#createMemberModal').modal('show');
    hideSpinner();
}


function editMember(memberInfoId) {
    if (memberInfoId) {
        loadSpinner();
        disableBtnById('btnEditBusiness');
        $.ajax({
            url: `./member/${memberInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createMemberFormContainer').empty().html(response);
                    $('#createMemberTitle').text('Update Member');
                    $('#createMemberModal').modal('show');
                }
                else {
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function (err) {
                debugger;
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
                enableBtnById('btnEditBusiness');
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}