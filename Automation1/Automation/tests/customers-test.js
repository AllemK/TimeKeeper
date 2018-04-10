'use strict';

var menu = require('../pages/menu-page.js');
var D      = require('../data-provider/data.js');
var page = require('../pages/customers-page.js');
var loginPage = require('../pages/login-page.js');


describe('Customers Page', function () {


    beforeAll(function () {

        browser.get(D.baseUrl);
        loginPage.log_in();
        browser.sleep(6000);

    });

    it('2. Verify customer is present after creating it', function () {
        page.click_customers_tab()
            .click_add_New_customer_button()
            .enter_customer_name_in_input_field()
            .enter_customer_contact_in_input_field()
            .enter_customer_email_in_input_field()
            .enter_customer_phone_in_input_field()
            .enter_customer_road_in_input_field()
            .enter_customer_zipcode_in_input_field()
            .enter_customer_city_in_input_field()
            .enter_customer_status_in_input_field()
            .click_Save()
    });

    it('2. Verify customer is updated', function () {
        page.click_customers_tab()
            .click_edit_button_for_customer()
            .enter_customer_zipcode_in_input_field()
            .enter_customer_city_in_input_field()
            .click_Save()
    });

    fit('3. Verify customer is deleted', function () {
        page.click_customers_tab()
            .click_delete_button_for_customer()
            .enter_customer_zipcode_in_input_field()
            .enter_customer_city_in_input_field()
            .click_Save()
    });
});
