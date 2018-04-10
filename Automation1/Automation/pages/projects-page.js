'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');


var projectsTab = $('[href="#!/projects"]');
var addNewButton = element(by.buttonText('Add new'));
var projectName = element(by.model('project.name'));
var projectMonogram =element(by.model('project.monogram'));
var customerNameDropdown = element(by.model('project.customer.id'));
var teamDropdown = element(by.model('project.team.id'));
var projectAmount = element(by.model('project.amount'));
var projectDescription = element(by.model('project.description'));
var projectStatusDropdown = element(by.model('project.status'));
var projectPricingDropdown = element(by.model('project.pricing'));
var saveButton = element(by.buttonText('Save'));




var ProjectsPage = function () {

    this.click_projects_tab= function () {
        helper.waitAndClick(projectsTab);
        return this;
    };

    this.click_add_New_project_button = function () {
        helper.waitAndClick(addNewButton);
        return this;
    };
    this.enter_project_name_in_input_field = function () {
        helper.clearAndEnterValue(projectName, 'Test Test');
        return this;
    };

    this.enter_project_monogram_in_input_field = function () {
        helper.clearAndEnterValue(projectMonogram,'TSS');
        return this;
    };

    this.enter_new_customer_name_in_input_field = function (customerName) {
        helper.selectDropdownOption(customerNameDropdown, customerName);
        return this;
    };
    this.enter_team_in_team_input_field = function (teamName) {
        helper.selectDropdownOption(teamDropdown,teamName);
        return this;
    };
    this.enter_amount_in_amount_input_field = function () {
        helper.clearAndEnterValue(projectAmount,'1000');
        return this;
    };
    this.enter_description_in_description_input_field = function () {
        helper.clearAndEnterValue(projectDescription,'Desc');
        return this;
    };
    this.enter_status_in_status_input_field = function (projectStatus) {
        helper.selectDropdownOption(projectStatusDropdown, projectStatus);
        return this;
    };
    this.enter_pricing_in_pricing_input_field = function (projectPricing) {
        helper.selectDropdownOption(projectPricingDropdown, projectPricing);
        return this;
    };
    this.click_Save = function () {
        helper.waitAndClick(saveButton);
        browser.sleep(3000);
        return this;
    };

};
module.exports = new ProjectsPage();