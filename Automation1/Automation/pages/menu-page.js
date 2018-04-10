'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');


var calendarTab      = $('[href="#!/calendar"]');
var teamsTab        = $('[href="#!/teams"]');
var projectsTab      = $('[href="#!/projects"]');
var employeesTab     = $('[href="#!/employees"]');
var customersTab    = $('[href="#!/customers"]');
var reportsTab     = $('[href="#!/reports"]');
var invoicesTab    = $('[href="#!/invoices"]');


var MenuPage = function () {


this.click_Calendar_tab = function () {
    helper.waitAndClick(calendarTab);
    return this;
};

this.click_Employees_tab = function () {
    helper.waitAndClick(employeesTab);
    return this;
};

this.click_Teams_tab= function () {
    helper.verifyPresenceOfElement(teamsTab);
    return this;
};

this.click_Projects_tab= function () {
    helper.verifyPresenceOfElement(projectsTab);
    return this;
};

this.click_Customers_tab = function () {
    browser.sleep(3000);
    helper.verifyPresenceOfElement(customersTab);
    return this;
};
this.click_Reports_tab = function () {
    helper.scrollTo(reportsTab);
    helper.verifyPresenceOfElement(reportsTab);
    return this;
};
this.click_Invoices_tab = function () {
    helper.scrollTo(invoicesTab);
    helper.verifyPresenceOfElement(invoicesTab);
    browser.sleep(3000);
    return this;
};

};
module.exports = new MenuPage();