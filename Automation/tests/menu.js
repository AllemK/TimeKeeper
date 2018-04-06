'use strict';


var D      = require('../data-provider/data.js');
var page = require('../pages/menu.js');

describe('Home Page', function () {


    beforeEach(function () {

        browser.get(D.baseUrl);
        browser.sleep(3000)

    });

    it('1.Verify calendar tab is present', function () {
        page.click_Calendar_tab()

    });

    it('2.Verify employees tab is present', function () {
        page.click_Employees_tab()

    });

    it('3.Verify teams tab is present', function () {
        page.click_Teams_tab()

    });

    it('4.Verify projects tab is present', function () {
        page.click_Projects_tab()

    });

    it('5.Verify customers tab is present', function () {
        page.click_Customers_tab()

    });

    it('6.Verify report tab is present', function () {
        page.click_Report_tab()

    });

    it('7.Verify roles tab is present', function () {
        page.click_Roles_tab()

    });

});