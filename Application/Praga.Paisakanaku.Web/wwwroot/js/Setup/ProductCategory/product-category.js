function getProductCategoryList() {
    loadSpinner();
    $.ajax({
        url: `./product-category/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#productCategoryListContainer').html(response);
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


function onCreateProductCategory() {
    loadSpinner();
    $('#formCreateProductCategory').trigger("reset");
    $('#formCreateProductCategory').data('id', '');
    $('#formCreateProductCategory').data('isupdate', 'False');
    $('#formCreateProductCategory').find(':input,select').val('');
    $('#formCreateProductCategory').find('span.error').hide();
    $('#createProductCategoryTitle').text('Create Product Category');
    $('#createProductCategoryModal').modal('show');
    hideSpinner();
}


function editProductCategory(productCategoryInfoId) {
    if (productCategoryInfoId) {
        loadSpinner();
        disableBtnById('btnEditBusiness');
        $.ajax({
            url: `./product-category/${productCategoryInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createProductCategoryFormContainer').empty().html(response);
                    $('#createProductCategoryTitle').text('Update Product Category');
                    $('#createProductCategoryModal').modal('show');
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