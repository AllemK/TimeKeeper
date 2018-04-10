var page = require('../pages/login-page.js');

describe('Login functionality', function () {

    beforeAll(function() {
        browser.sleep(2000)
    });

    fit('1. Verify user can log in with valid credentials', function () {
        page.log_in();
        browser.sleep(5000)

    });

});