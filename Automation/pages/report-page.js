'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');

var reportButton      = element(by.buttonText('Report'));



var ReportPage = function () {

    this.click_report_button = function () {
        helper.waitAndClick(reportButton);
        helper.waitVisibility(reportButton);
        browser.sleep(5000);
        return this;
    };
};
module.exports = new ReportPage();