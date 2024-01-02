// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// #region Setup methods for list data

// #endregion

let SPINNER_CURRENT_LOAD = 0;

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})

function getBrandDDList() {
    loadSpinner();
    $.ajax({
        url: `./brand/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#brandListDDContainer').html(response);

                if ($('#selectBrand option').length == 1) {
                    $('#brandNameContainer').show();
                }

                let val = $('#brandListDDContainer').data('val');
                if (val) {
                    $('#selectBrand').val(val);
                }
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

function getMemberDDList(memberId = '', callbackFn = '') {
    loadSpinner();
    $.ajax({
        url: `./member/manages-expense/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#memberListDDContainer').html(response);

                if (memberId) {
                    $('#selectMember').val(memberId);
                }

                if (callbackFn) {
                    callbackFn();
                }
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

function getProductCategoryDDList() {
    loadSpinner();
    $.ajax({
        url: `./lookup/product-category`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#productCategoryListDDContainer').html(response);

                if ($('#selectProductCategory option').length == 1) {
                    $('#productCategoryNameContainer').show();
                }

                let val = $('#productCategoryListDDContainer').data('val');
                if (val) {
                    $('#selectProductCategory').val(val);
                }
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

function getProductDDList(productId = '') {
    loadSpinner();
    $.ajax({
        url: `./product/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#productListDDContainer').html(response);

                let val = $('#productListDDContainer').data('val')
                if (val) {
                    $('#selectProduct').val(val);
                }
                if (productId) {
                    $('#selectProduct').val(productId);
                }

                if (!(productId || val)) {
                    $('#selectProduct').val('');   
                }
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

function getGroceryDDList(groceryId = '') {
    loadSpinner();
    $.ajax({
        url: `./grocery/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#groceryListDDContainer').html(response);
                let val = $('#groceryListDDContainer').data('val')
                if (val) {
                    $('#selectGrocery').val(val);
                }
                if (groceryId) {
                    $('#selectGrocery').val(groceryId);
                }

                if (!(groceryId || val)) {
                    $('#selectGrocery').val('');
                }
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

function getTimePeriodDDList() {
    loadSpinner();
    $.ajax({
        url: `./lookup/time-period`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#timePeriodListDDContainer').html(response);
                let val = $('#timePeriodListDDContainer').data('val');
                if (val) {
                    $('#selectTimePeriod').val(val);
                }
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

function getExpenseTypeDDList() {
    loadSpinner();
    $.ajax({
        url: `./lookup/expense-type`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#expenseTypeListDDContainer').html(response);
                let val = $('#expenseTypeListDDContainer').data('val')
                if (val) {
                    $('#selectExpenseType').val(val);
                }
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

function getRelationshipTypeDDList(relationshipType = '') {
    loadSpinner();
    $.ajax({
        url: `./lookup/relationship-type`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#RelationshipTypeListDDContainer').html(response);

                if (relationshipType === '') {
                    relationshipType = $('#RelationshipTypeListDDContainer').data('val')
                }

                if (relationshipType) {
                    $('#selectRelationshipType').val(relationshipType);
                }
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

function getMeasureTypeDDList(measureType = '') {
    loadSpinner();
    $.ajax({
        url: `./lookup/measure-type`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#measureTypeListDDContainer').html(response);

                if (measureType === '') {
                    measureType = $('#measureTypeListDDContainer').data('val')
                }

                if (measureType) {
                    $('#selectMeasureType').val(measureType);
                }
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

function getMeasureTypeDDListByGroceryId(groceryInfoId, measureType = '') {
    if (groceryInfoId) {
        loadSpinner();
        $.ajax({
            url: `./lookup/measure-type/grocery/${groceryInfoId}`,
            method: 'GET',
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#measureTypeListDDContainer').html(response);

                    if (measureType === '') {
                        measureType = $('#measureTypeListDDContainer').data('val')
                    }

                    if (measureType) {
                        $('#selectMeasureType').val(measureType);
                        updateMeasureTypeTextOnQuantity();
                    }


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
    } else {
        // TODO Alert
    }
}


// #region Common

function showToast() {
    $('#toast').toast({ delay: 5000 });
    $('#toast').toast('show');
}

function showSuccessMsg(message) {
    if (message) {
        $('#toast:first').attr('class', 'toast primary align-items-center text-white border-0')
        $('#toast #toast-message').text(message);
        showToast();
    }
}

function showErrorMsg(message) {
    if (message) {
        $('#toast:first').attr('class', 'toast bg-danger align-items-center text-white border-0')
        $('#toast #toast-message').text(message);
        showToast();
    }
}

function loadSpinner() {
    SPINNER_CURRENT_LOAD += 1
    checkSpinnerStatus();
}

function checkSpinnerStatus() {
    if (SPINNER_CURRENT_LOAD > 0) {
        $('#loader').show();
        $('main').addClass('pk-backdrop');
    } else {
        $('#loader').hide();
        $('main').removeClass('pk-backdrop');
    }
}

function hideSpinner() {
    SPINNER_CURRENT_LOAD -= 1;
    checkSpinnerStatus();
}

function disableBtnById(btnId) {
    if (btnId) {
        $('#' + btnId).prop('disabled', true);
    }
}

function enableBtnById(btnId) {
    if (btnId) {
        $('#' + btnId).prop('disabled', false);
    }
}

// #endregion


// #region helpers

function isNotFutureDate(date) {
    if (date) {
        date = new Date(date);
        date.setHours(0, 0, 0, 0);
        return new Date(date) <= new Date();
    }
}

// #endregion