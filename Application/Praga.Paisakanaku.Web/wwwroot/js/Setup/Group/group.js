function getGroupList() {
    loadSpinner();
    $.ajax({
        url: `./group/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#groupListContainer').html(response);
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


function onCreateGroup() {
    loadSpinner();
    $('#formCreateGroup').trigger("reset");
    $('#formCreateGroup').data('id', '');
    $('#formCreateGroup').data('isupdate', 'False');
    $('#formCreateGroup').find(':input,select').val('');
    $('#formCreateGroup').find('span.error').hide();
    $('#createGroupTitle').text('Create Group');
    $('#createGroupModal').modal('show');
    hideSpinner();
}


function editGroup(groupInfoId) {
    if (groupInfoId) {
        loadSpinner();
        disableBtnById(`btnEditGroup_${groupInfoId}`);
        $.ajax({
            url: `./group/${groupInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createGroupFormContainer').empty().html(response);
                    $('#createGroupTitle').text('Update Group');
                    $('#createGroupModal').modal('show');
                }
                else {
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function (err) {
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
                enableBtnById(`btnEditGroup_${groupInfoId}`);
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}