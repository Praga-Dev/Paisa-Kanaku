function getBillTypeList() {
    loadSpinner();
    $.ajax({
        url: `./bill-type/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#billTypeListContainer').html(response);
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


function onCreateBillType() {
    loadSpinner();
    $('#formCreateBillType').trigger("reset");
    $('#formCreateBillType').data('id', '');
    $('#formCreateBillType').data('isupdate', 'False');
    $('#formCreateBillType').find(':input,select').val('');
    $('#formCreateBillType').find('span.error').hide();
    $('#createBillTypeTitle').text('Create BillType');
    $('#createBillTypeModal').modal('show');
    hideSpinner();
}


function editBillType(billTypeInfoId) {
    if (billTypeInfoId) {
        loadSpinner();
        disableBtnById('btnEditBusiness');
        $.ajax({
            url: `./bill-type/${billTypeInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createBillTypeFormContainer').empty().html(response);
                    $('#createBillTypeTitle').text('Update BillType');
                    $('#createBillTypeModal').modal('show');
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