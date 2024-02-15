function getOutdoorFoodVendorList() {
    loadSpinner();
    $.ajax({
        url: `./outdoor-food-vendor/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#outdoorFoodVendorListContainer').html(response);
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


function onCreateOutdoorFoodVendor() {
    loadSpinner();
    $('#formCreateOutdoorFoodVendor').trigger("reset");
    $('#formCreateOutdoorFoodVendor').data('id', '');
    $('#formCreateOutdoorFoodVendor').data('isupdate', 'False');
    $('#formCreateOutdoorFoodVendor').find(':input,select').val('');
    $('#formCreateOutdoorFoodVendor').find('span.error').hide();
    $('#createOutdoorFoodVendorTitle').text('Create Outdoor Food Vendor');
    $('#createOutdoorFoodVendorModal').modal('show');
    hideSpinner();
}


function editOutdoorFoodVendor(outdoorFoodVendorInfoId) {
    if (outdoorFoodVendorInfoId) {
        loadSpinner();
        disableBtnById(`btnEditOutdoorFoodVendor_${outdoorFoodVendorInfoId}`);
        $.ajax({
            url: `./outdoor-food-vendor/${outdoorFoodVendorInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createOutdoorFoodVendorFormContainer').empty().html(response);
                    $('#createOutdoorFoodVendorTitle').text('Update Outdoor Food Vendor');
                    $('#createOutdoorFoodVendorModal').modal('show');
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
                enableBtnById(`btnEditOutdoorFoodVendor_${outdoorFoodVendorInfoId}`);
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}