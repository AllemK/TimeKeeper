'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');

var usernameInputField    = element(by.model('user.name'));
var passInputField = element(by.model('user.pass'));
var loginButton        = element(by.buttonText('Sign in'));

var LoginPage = function () {

    this.log_in = function () {
        browser.get(D.baseUrl);
        helper.clearAndEnterValue(usernameInputField,'aselimovic@school.edu');
        helper.clearAndEnterValue(passInputField, '$ch00l');
        loginButton.click();
        return this;
    }
};

module.exports = new LoginPage();

