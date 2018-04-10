'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');

var logoutButton = element(by.buttonText('Logout'));


var LogoutPage = function () {

    this.click_logout_button = function () {
        helper.waitVisibility(logoutButton);
        browser.sleep(1000);
        return this;
    };
};
module.exports = new LogoutPage();