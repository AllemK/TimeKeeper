'use strict';

var menu = require('../pages/menu.js');
var D      = require('../data-provider/data.js');
var page = require('../pages/report-page.js');
var loginPage = require('../pages/login-page.js');

describe('Report Page', function () {


    beforeAll(function () {

        loginPage.log_in();
        browser.sleep(4000);

    });

    it('1.Verify Report button is present', function () {
        page.click_report_button()

    });
});