var page = require('../pages/logout-page.js');

describe('Logout functionality', function () {

    beforeAll(function() {
        page.log_in();
        browser.sleep(5000)
    });

    fit('1. Verify user can log out', function () {
        page.click_logout_button();


    });

});