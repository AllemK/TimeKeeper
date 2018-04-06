'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');

var rolesButton      = element(by.buttonText('Roles'));



var RolesPage = function () {

    this.click_roles_button = function () {
        helper.waitAndClick(rolesButton);
        helper.waitVisibility(rolesButton);
        browser.sleep(5000);
        return this;
    };
};
    module.exports = new RolesPage();