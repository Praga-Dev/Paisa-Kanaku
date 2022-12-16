function getRepairTypeList() {
    loadSpinner();
    $.ajax({
        url: `./repair-type/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#repairTypeListContainer').html(response);
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


function onCreateRepairType() {
    loadSpinner();
    $('#formCreateRepairType').trigger("reset");
    $('#formCreateRepairType').data('id', '');
    $('#formCreateRepairType').data('isupdate', 'False');
    $('#formCreateRepairType').find(':input,select').val('');
    $('#formCreateRepairType').find('span.error').hide();
    $('#createRepairTypeTitle').text('Create RepairType');
    $('#createRepairTypeModal').modal('show');
    hideSpinner();
}


function editRepairType(repairTypeInfoId) {
    if (repairTypeInfoId) {
        loadSpinner();
        disableBtnById('btnEditBusiness');
        $.ajax({
            url: `./repair-type/${repairTypeInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createRepairTypeFormContainer').empty().html(response);
                    $('#createRepairTypeTitle').text('Update RepairType');
                    $('#createRepairTypeModal').modal('show');
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
                enableBtnById('btnEditBusiness');
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}