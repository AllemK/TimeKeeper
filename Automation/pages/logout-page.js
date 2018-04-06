'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');


var logoutButton = $('[href="#!/Login"]');

var LogoutPage = function () {

    this.click_logout_button = function () {
        helper.waitVisibility(logoutButton);
        return this;
    };
};
module.exports = new LogoutPage();