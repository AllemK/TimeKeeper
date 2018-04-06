'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');


var CalendarPage = function () {

    this.choose_an_employee_from_the_dropdown = function (employeeName) {
        helper.selectDropdownOption(employeeDropdown, employeeName);
        return this;
    };

    this.enter_year = function () {
        helper.clearAndEnterValue(yearInputField, D.year);
        return this;
    };

    this.choose_month_from_the_dropdown = function () {
        helper.selectDropdownOption(monthInputField,D.month);
        return this;
    };

    this.click_LIST_button = function () {
        helper.waitAndClick(LISTbutton);
        helper.waitVisibility(modalText)
        return this;
    };
    /* this.choose_an_employee_from_the_dropdown = function (employeeName) {
        helper.selectDropdownOption(employeeDropdown, employeeName);
        return this;
    };

    this.enter_year = function () {
        helper.clearAndEnterValue(yearInputField, '2017');
        return this;
    };

    this.choose_month_from_the_dropdown = function (month) {
        helper.selectDropdownOption(monthInputField, month);
        return this;
    };*/

};
module.exports = new CalendarPage();