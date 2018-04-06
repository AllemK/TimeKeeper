'use strict';

var menu = require('../pages/menu.js');
var D      = require('../data-provider/data.js');
var page = require('../pages/teams-page.js');
var loginPage = require('../pages/login-page.js');

describe('Teams Page', function () {


        beforeAll(function() {

            loginPage.log_in();
            menu.click_Teams_tab()
            browser.sleep(3000);

    });

    it('1.Verify teams button is present', function () {
        page.click_teams_button()

    });

    it('1.Verify search button is present', function () {
        page.click_search_button()

    });



});

