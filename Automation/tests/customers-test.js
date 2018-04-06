'use strict';

var menu = require('../pages/menu');
var D      = require('../data-provider/data.js');
var page = require('../pages/customers-page.js');
var loginPage = require('../pages/login-page.js')


describe('Customers Page', function () {


    beforeAll(function () {

        loginPage.log_in();
        browser.sleep(6000);

    });

    it('1.Verify customers Tab is present', function () {
        page.verify_customers_tab_is_present()

    });



    it('2. Verify that customer is deleted', function () {
        page.click_customers_tab()
            .click_Delete_button_on_first_row()
            .click_Yes_delete_it()



    });


});
