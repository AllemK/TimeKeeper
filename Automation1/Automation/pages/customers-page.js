'use strict';

var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');



var customersTab      = $('[href="#!/customers"]');
var addNewbutton = element(by.buttonText('Add New'));
var customerNameEnter = element(by.model('customer.name'));
var customerContactEnter = element(by.model('customer.contact'));
var customerEmailEnter = element(by.model('customer.email'));
var customerPhoneEnter = element(by.model('customer.phone'));
var customerRoadEnter = element(by.model('customer.address_Road'));
var customerZipcodeEnter = element(by.model('customer.address_ZipCode'));
var customerCityEnter = element(by.model('customer.address_City'));
var customerStatus = $$('[type="radio"]').get(0);
var saveButton =  element(by.buttonText('SAVE'));

var tabfromCustomers = $('[href="#!/customers"]');
var editButton = $$('[ng-click="edit(data)"]').get(3);
var customerZipEnter = element(by.model('customer.address_ZipCode'));
var cityEnter = element(by.model('customer.address_City'));
var buttonSave = element(by.buttonText('SAVE'));

var CustomersPage = function () {


    this.click_customers_tab = function () {
        helper.waitAndClick(customersTab);
        browser.sleep(1000);
        return this;
    };


    this.click_add_New_customer_button = function () {
        helper.waitAndClick(addNewbutton);
        return this;
    };

    this.enter_customer_name_in_input_field = function () {
        helper.clearAndEnterValue(customerNameEnter, 'Testing Company');
        return this;

    };
    this.enter_customer_contact_in_input_field = function () {
        helper.clearAndEnterValue(customerContactEnter, 'Testo Testic');
        return this;

    };
    this.enter_customer_email_in_input_field = function () {
        helper.clearAndEnterValue(customerEmailEnter, 'test@test.com');
        return this;

    };
    this.enter_customer_phone_in_input_field = function () {
        helper.clearAndEnterValue(customerPhoneEnter, '061123456');
        return this;

    };
    this.enter_customer_road_in_input_field = function () {
        helper.clearAndEnterValue(customerRoadEnter, 'Maglajska 1');
        return this;

    };
    this.enter_customer_zipcode_in_input_field = function () {
        helper.clearAndEnterValue(customerZipcodeEnter, '71 000');
        return this;

    };
    this.enter_customer_city_in_input_field = function () {
        helper.clearAndEnterValue(customerCityEnter, 'Sarajevo');
        return this;

    };
    this.enter_customer_status_in_input_field = function () {
        helper.waitAndClick(customerStatus);
        return this;

    };
    this.click_Save = function () {
        helper.waitAndClick(saveButton);
        browser.sleep(3000);
        return this;

    };
    this.click_customers_tab = function () {
        helper.waitAndClick(tabfromCustomers);
        browser.sleep(3000);
        return this;

    };

    this.click_edit_button_for_customer = function () {
        helper.waitAndClick(editButton);
        browser.sleep(3000);
        return this;

    };
    this.enter_customer_zipcode_in_input_field = function () {
        helper.clearAndEnterValue(customerZipEnter, '72 270');
        return this;

    };

    this.enter_customer_city_in_input_field = function () {
        helper.clearAndEnterValue(cityEnter, 'Istanbul');
        return this;

    };
    this.click_Save_button = function () {
        helper.waitAndClick(buttonSave);
        return this;

    };


};
module.exports = new CustomersPage();