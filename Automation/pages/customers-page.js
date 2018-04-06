'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');


var customersTab      = $('[href="#!/customers"]');
var deleteButton = $$('.glyphicon-trash').get(0);
var yesButton = element(by.buttonText('Yes, sure'));

var CustomersPage = function () {


    this.verify_customers_tab_is_present = function () {
        helper.waitAndClick(customersTab);
        return this;
    };


    this.click_customers_tab = function () {
        helper.waitAndClick(customersTab);
        return this;
    };


    this.click_Delete_button_on_first_row = function () {
        helper.waitAndClick(customersTab);
        helper.waitAndClick(deleteButton);
        return this;
    };

    this.click_Yes_delete_it = function () {
        helper.waitAndClick(yesButton);
        return this;
    };



};
module.exports = new CustomersPage();