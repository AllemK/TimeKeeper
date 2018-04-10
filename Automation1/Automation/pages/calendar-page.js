'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');


var employeeDropdown =  element(by.model('employeeId'));
var yearEnter = element(by.model('year'));
var monthDropdown = element(by.model('month'));
var listButton = element(by.partialButtonText('LIST!'));
var calendarDay = $$('[ng-click="edit(day)"]').get(2);
var projectNameinput = element(by.model('newTask.project.id'));
var descriptionInput = element(by.model('newTask.description'));
var timeInput = element(by.model('newTask.hours'));
var saveButton = $('[ng-click="add(newTask)"]');
var saveTab = $('.tk-button-green');

var empDropdown = element(by.model('employeeId'));
var enterYear = element(by.model('year'));
var monDropdown = element(by.model('month'));
var liButton = element(by.partialButtonText('LIST!'));
var dayInCalendar = $$('[ng-click="edit(day)"]').get(14);
var deleteButton = $('[ng-click="del($index)"]');
var buttonSave = $('.tk-button-green');


var emDropdown = element(by.model('employeeId'));
var entYear = element(by.model('year'));
var moDropdown = element(by.model('month'));
var lButton = element(by.partialButtonText('LIST!'));
var dayCalendar = $$('[ng-click="edit(day)"]').get(14);
var dayDropdown = element(by.model('day.type'));
var proNameDropdown = element(by.model('newTask.project.id'));
var descrInput = element(by.model('newTask.description'));
var timeProjectInput = element(by.model('newTask.hours'));
var Sabutton = $('[ng-click="add(newTask)"]');
var Satab = $('.tk-button-green');



var CalendarPage = function () {

    this.choose_an_employee_from_the_Dropdown = function (employeeName) {
        helper.selectDropdownOption(employeeDropdown, employeeName);
        return this;
    };

    this.enter_year = function () {
        helper.clearAndEnterValue(yearEnter, '2017');
        return this;
    };

    this.enter_month_from_the_dropdown = function (employeeMonth) {
        helper.selectDropdownOption(monthDropdown, employeeMonth);
        return this;
    };

    this.click_list_Button = function () {
        helper.waitAndClick(listButton);
        browser.sleep(1000);
        return this;
    };
    this.choose_a_day_from_the_calendar = function () {
        helper.waitAndClick(calendarDay);
        return this;
    };

    this.choose_a_project_from_the_dropdown = function (projectName) {
        helper.selectDropdownOption(projectNameinput, projectName);
        browser.sleep(1000);
        return this;
    };
    this.enter_description = function () {
        helper.clearAndEnterValue(descriptionInput, 'New project added by administrator');
        return this;
    };
    this.enter_time = function () {
        helper.clearAndEnterValue(timeInput, '6');
        return this;
    };
    this.click_Save_button = function () {
        helper.waitAndClick(saveButton);
        return this;
    };

    this.click_Save_tab = function () {
        helper.waitAndClick(saveTab);
        browser.sleep(1000);
        return this;
    };

    this.choose_an_employee_from_Dropdown = function (nameDropdown) {
        helper.selectDropdownOption(empDropdown,nameDropdown);
        return this;
    };
    this.enter_year_of_work = function () {
        helper.clearAndEnterValue(enterYear, '2017');
        return this;
    };
    this.enter_month_from_dropdown = function (monthEmployee) {
        helper.selectDropdownOption(monDropdown,monthEmployee);
        return this;
    };
    this.click_li_Button = function () {
        helper.waitAndClick(lButton);
        return this;
    };
    this.choose_a_day_in_calendar = function () {
        helper.waitAndClick(dayInCalendar);
        return this;
    };
    this.click_delete_button = function () {
        helper.waitAndClick(deleteButton);
        browser.sleep(1000);
        return this;
    };
    this.click_button_save = function () {
        helper.waitAndClick(buttonSave);
        browser.sleep(1000);
        return this;
    };

    this.choose_an_employee_from_Dropdown = function (nameDropdown) {
        helper.selectDropdownOption(emDropdown,nameDropdown);
        return this;
    };
    this.enter_year_of_work = function () {
        helper.clearAndEnterValue(entYear, '2017');
        return this;
    };
    this.enter_month_from_dropdown = function (monthEmployee) {
        helper.selectDropdownOption(moDropdown,monthEmployee);
        return this;
    };
    this.click_li_Button = function () {
        helper.waitAndClick(liButton);
        return this;
    };
    this.choose_a_day_in_calendar = function () {
        helper.waitAndClick(dayCalendar);
        return this;

    };
    this.enter_day_from_dropdown = function (dayType) {
        helper.selectDropdownOption(dayDropdown,dayType);
        return this;
    };

    this.choose_a_pro_from_the_dropdown = function (projectName) {
        helper.selectDropdownOption(proNameDropdown, projectName);
        browser.sleep(1000);
        return this;
    };
    this.enter_descr = function () {
        helper.clearAndEnterValue(descrInput, 'New project added successfully');
        return this;
    };
    this.enter_time_for_project = function () {
        helper.clearAndEnterValue(timeProjectInput, '8');
        return this;
    };
    this.click_Sa_button = function () {
        helper.waitAndClick(Sabutton);
        return this;
    };


    this.click_Sa_tab = function () {
        helper.waitAndClick(Satab);
        browser.sleep(3000);
        return this;
    };
};
module.exports = new CalendarPage();