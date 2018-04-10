var page = require('../pages/logout-page.js');
var D    = require('../data-provider/data.js');
var loginPage = require('../pages/login-page.js');

describe('Logout functionality', function () {

    beforeAll(function() {

        loginPage.log_in();
        browser.sleep(9000);

    });

    fit('1. Verify user can log out', function () {
        page.click_logout_button();


    });

});