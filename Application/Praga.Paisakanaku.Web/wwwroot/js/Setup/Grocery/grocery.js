function getGroceryList() {
    loadSpinner();
    $.ajax({
        url: `./grocery/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#groceryListContainer').html(response);
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


function onCreateGrocery() {
    loadSpinner();
    $('#formCreateGrocery').trigger("reset");
    $('#formCreateGrocery').data('id', '');
    $('#formCreateGrocery').data('isupdate', 'False');
    $('#formCreateGrocery').find(':input,select').val('');
    $('#formCreateGrocery').find('span.error').hide();
    $('#createGroceryTitle').text('Create Grocery');
    $('#brandNameContainer').hide();
    $('#groceryCategoryNameContainer').hide();
    $('#createGroceryModal').modal('show');
    getBrandDDList();
    getGroceryCategoryDDList();
    getTimePeriodDDList();
    hideSpinner();
}


function editGrocery(groceryInfoId) {
    if (groceryInfoId) {
        loadSpinner();
        disableBtnById(`btnEditGrocery_${groceryInfoId}`);
        $.ajax({
            url: `./grocery/${groceryInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createGroceryFormContainer').empty().html(response);
                    getBrandDDList();
                    getGroceryCategoryDDList();
                    getTimePeriodDDList();
                    $('#createGroceryTitle').text('Update Grocery');
                    $('#createGroceryModal').modal('show');
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
                enableBtnById(`btnEditGrocery_${groceryInfoId}`);
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}
