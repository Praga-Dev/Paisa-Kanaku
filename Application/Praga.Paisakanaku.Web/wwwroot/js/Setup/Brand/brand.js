function getBrandList() {
    loadSpinner();
    $.ajax({
        url: `./brand/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#brandListContainer').html(response);
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


function onCreateBrand() {
    loadSpinner();
    $('#formCreateBrand').trigger("reset");
    $('#formCreateBrand').data('id', '');
    $('#formCreateBrand').data('isupdate', 'False');
    $('#formCreateBrand').find(':input,select').val('');
    $('#formCreateBrand').find('span.error').hide();
    $('#createBrandTitle').text('Create Brand');
    $('#createBrandModal').modal('show');
    hideSpinner();
}


function editBrand(brandInfoId) {
    if (brandInfoId) {
        loadSpinner();
        disableBtnById('btnEditBusiness');
        $.ajax({
            url: `./brand/${brandInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createBrandFormContainer').empty().html(response);
                    $('#createBrandTitle').text('Update Brand');
                    $('#createBrandModal').modal('show');
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