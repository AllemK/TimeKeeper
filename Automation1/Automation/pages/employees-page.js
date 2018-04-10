'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');



var employeesTab      = $('[href="#!/employees"]');
var employeesTable  = $$('col-md-12');
var deleteIcon_first =  Delete
var yesButton = element(by.buttonText('Yes, sure'));



var EmployeesPage = function () {



    this.click_Employees_tab = function () {
        helper.waitAndClick(employeesTab);
        return this;
    };

      /*this.enter_word_on_search = function () {
          helper.clearAndEnterValue(searchInputField, 'Jasmin');
          browser.sleep(2000);
          helper.waitAndClick(searchIcon);
          return this;
      };
*/
    this.verify_employees_table_is_present = function () {
        helper.waitAndClick(employeesTable);
        return this;
    }
    this.click_delete_icon = function () {
        helper.waitAndClick(deleteIcon_first);
        return this;
    };


    this.click_Yes_delete_it = function () {
        helper.waitAndClick(yesButton);
        return this;
    };


};
module.exports = new EmployeesPage();