'use strict';

var menu = require('../pages/menu-page.js');
var D      = require('../data-provider/data.js');
var page = require('../pages/calendar-page.js');
var loginPage = require('../pages/login-page.js');


describe('Calendar Page', function () {


    beforeEach(function () {

        browser.get(D.baseUrl);
        loginPage.log_in();
        browser.sleep(1000);
        menu.click_Calendar_tab();


    });
    xit('1. Verify employee, year and month are listed and new task added', function () {
        page.choose_an_employee_from_the_Dropdown(D.employeeDropdown[0])
            .enter_year()
            .enter_month_from_the_dropdown(D.monthDropdown[0])
            .click_list_Button()
            .choose_a_day_from_the_calendar()
            .choose_a_project_from_the_dropdown(D.projectName[0])
            .enter_description()
            .enter_time()
            .click_Save_button()
            .click_Save_tab()
    });
    fit('2. Verify employee, year and month are listed and task deleted', function () {
        page.choose_an_employee_from_Dropdown(D.empDropdown[0])
            .enter_year_of_work()
            .enter_month_from_dropdown(D.monDropdown[0])
            .click_li_Button()
            .choose_a_day_in_calendar()
            .click_delete_button()
            .click_button_save()
    });

    xit('3. Verify employee, year and month are listed, day type changed, project, description and time added', function () {
        page.choose_an_employee_from_the_Dropdown(D.emDropdown[0])
            .enter_year_of_work()
            .enter_month_from_dropdown(D.moDropdown[0])
            .click_li_Button()
            .choose_a_day_in_calendar()
            .enter_day_from_dropdown(D.dayDropdown[0])
            .choose_a_pro_from_the_dropdown(D.proNameDropdown[0])
            .enter_descr()
            .enter_time_for_project()
            .click_Sa_button()
            .click_Sa_tab()
    });
});

