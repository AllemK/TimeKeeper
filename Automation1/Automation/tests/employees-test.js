'use strict';


var page = require('../pages/employees-page.js');
var D    = require('../data-provider/data.js');
var loginPage = require('../pages/login-page.js');



describe('Employees operations', function () {


        beforeAll(function() {

            loginPage.log_in();
            browser.sleep(1000);

        });

    it('2. Verify that employee is deleted', function () {
        page.click_Employees_tab()
            .verify_employees_table_is_present()
            .click_delete_icon()
            .click_Yes_delete_it()

    });
});


