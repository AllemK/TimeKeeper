'use strict';

var menu = require('../pages/menu.js');
var D    = require('../data-provider/data.js');
var page = require('../pages/projects-page.js');
var loginPage = require('../pages/login-page.js')



describe('Projects Page', function () {

    beforeAll(function() {

        loginPage.log_in();
        menu.click_Employees_tab()
        browser.sleep(3000);
    });

    it('1.Verify projects heading is present', function () {
        page.click_projects_heading()

    });

});