'use strict';

var menu = require('../pages/menu.js');
var D      = require('../data-provider/data.js');
var page = require('../pages/page-template.js');

describe('Home Page', function () {


    beforeEach(function () {

        browser.get(D.baseUrl);
        browser.sleep(3000)

    });
    it('1.Verify that Calendar tab is present', function () {
        page.verify_that_calendar_tab_is_present()
            
    });

    it('2.Verify that Teams tab is present', function () {
        page.verify_that_Teams_tab_is_present()

    })

    it('3.Verify that Employees tab is present', function () {
        page.verify_that_Employees_tab_is_present()

    })
});

