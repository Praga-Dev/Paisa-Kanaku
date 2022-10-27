function getProductList() {
    loadSpinner();
    $.ajax({
        url: `./product/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#productListContainer').html(response);
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


function onCreateProduct() {
    loadSpinner();
    $('#formCreateProduct').trigger("reset");
    $('#formCreateProduct').data('id', '');
    $('#formCreateProduct').data('isupdate', 'False');
    $('#formCreateProduct').find(':input,select').val('');
    $('#formCreateProduct').find('span.error').hide();
    $('#createProductTitle').text('Create Product');
    $('#brandNameContainer').hide();
    $('#productCategoryNameContainer').hide();
    $('#createProductModal').modal('show');
    getBrandDDList();
    getProductCategoryDDList();
    getTimePeriodDDList();
    getExpenseTypeDDList();
    hideSpinner();
}


function editProduct(productInfoId) {
    if (productInfoId) {
        loadSpinner();
        disableBtnById('btnEditBusiness');
        $.ajax({
            url: `./product/${productInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createProductFormContainer').empty().html(response);
                    getBrandDDList();
                    getProductCategoryDDList();
                    getExpenseTypeDDList();
                    getTimePeriodDDList();
                    $('#createProductTitle').text('Update Product');
                    $('#createProductModal').modal('show');
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

function downloadProduct() {
    loadSpinner();
    disableBtnById('btnDownloadProduct');
    $.ajax({
        url: `./product/${productInfoId}`,
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#createProductFormContainer').empty().html(response);
                getBrandDDList();
                getProductCategoryDDList();
                getExpenseTypeDDList();
                getTimePeriodDDList();
                $('#createProductTitle').text('Update Product');
                $('#createProductModal').modal('show');
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
            enableBtnById('btnDownloadProduct');
        }
    })
}