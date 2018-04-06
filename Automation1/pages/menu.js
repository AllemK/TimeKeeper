'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');

var calendarTab      = $('[href="#!/calendar"]');
var teamsTab        = $('[href="#!/teams"]');
var projectsTab      = $('[href="#!/projects"]');
var employeesTab     = $('[href="#!/employees"]');
var customersTab    = $('[href="#!/customers"]');
var reportTab     = $('[href="#!/report"]');
var rolesTab    = $('[href="#!/roles"]');


var HomePage = function () {
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
        helper.verifyPresenceOfElement(customersTab);
        return this;
    };
    this.click_Report_tab = function () {
        helper.verifyPresenceOfElement(reportTab);
        return this;
    };
    this. click_Roles_tab = function () {
        helper.verifyPresenceOfElement(rolesTab);
        return this;
    };




};
module.exports = new HomePage();