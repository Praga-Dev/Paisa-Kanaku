﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// #region Setup methods for list data

// #endregion

function getBrandDDList() {
    loadSpinner();
    $.ajax({
        url: `./brand/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#brandListDDContainer').html(response);
                $('#selectBrand').val($('#brandListDDContainer').data('val'));
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

function getMemberDDList() {
    loadSpinner();
    $.ajax({
        url: `./member/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#memberListDDContainer').html(response);
                $('#selectMember').val('')
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
        url: `./product-category/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#productCategoryListDDContainer').html(response);
                $('#selectProductCategory').val($('#productCategoryListDDContainer').data('val'));
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
                if (productId) {
                    $('#selectProduct').val(productId);
                } else {
                    $('#selectProduct').val($('#productListDDContainer').data('val'));
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
                $('#selectTimePeriod').val($('#timePeriodListDDContainer').data('val'));
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
                $('#selectExpenseType').val($('#expenseTypeListDDContainer').data('val'));
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
    $('#loader').show();
    $('main').addClass('pk-backdrop');
}

function hideSpinner() {
    $('#loader').hide();
    $('main').removeClass('pk-backdrop');
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
    debugger;
    if (date) {
        date = new Date(date);
        date.setHours(0, 0, 0, 0);
        return new Date(date) <= new Date();
    }
}

// #endregion