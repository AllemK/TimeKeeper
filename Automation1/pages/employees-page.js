'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');


var employeesTab      = $('[href="#!/employees"]');
var employeesTable = $('.col-md-12');
var employeeInputField = $$('col-md-12');
var deleteButton = $$('.glyphicon-trash').get(0);
var searchIcon = $('.glyphicon-search');
var yesButton = element(by.buttonText('Yes, sure'));

//var supplierOnList_first = $$('[ng-repeat="supplier in suppliers"]').first();

var EmployeesPage = function () {


    this.click_on_employees_tab = function () {
        helper.waitAndClick(employeesTab);
        helper.waitVisibility()
        return this;
    };


    this.verify_employees_table_is_present= function () {
        helper.waitAndClick(employeesTab);
        browser.sleep(3000);
        helper.waitVisibility(employeesTable);
        return this;
    };


    this.verify_searched_result_is_displayed = function () {
        helper.waitVisibility(employeeInputField, 'Jasmin Lakić');
        return this;
    };

    this.verify_employee_is_present_in_table = function (searchedText) {
        helper.verifyText(table,searchedText);
        return this;
    };


    this.click_Delete_button_on_first_row = function () {
        helper.waitAndClick(employeesTab);
        helper.waitAndClick(deleteButton);
        return this;
    };

    this.click_Yes_sure = function () {
        helper.waitAndClick(yesButton);
        return this;
    };

    this.verify_new_record_is_not_displayed = function () {
        helper.waitAndClick(employeesTable);
        return this;
    };

      this.enter_word_on_search = function () {
          helper.clearAndEnterValue(searchInputField, 'Amel');
          browser.sleep(2000);
          helper.waitAndClick(searchIcon);
          return this;
      };

    this.verify_employee_tab_is_present = function () {
         helper.waitVisibility(employeesTab)
        return this;
    };






};
module.exports = new EmployeesPage();