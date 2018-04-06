'use strict';

var menu = require('../pages/menu.js');
var D      = require('../data-provider/data.js');
var page = require('../pages/calendar-page.js');
var loginPage = require('../pages/login-page.js')


describe('Calendar Page', function () {


    beforeEach(function () {

        browser.get(D.baseUrl);
        browser.sleep(3000);
        loginPage.log_in();
        menu.click_Calendar_tab();
            menu.click_employee(D.employee[0]);
            page.select_year(D.year[0]);
            page.select_month(D.month[0]);

    });
    it('2. Verify employees are listed', function () {
        page.choose_an_employee_from_the_dropdown(D.Employees[0])
            .enter_year()
            .choose_month_from_the_dropdown(D.Months[0])
            .click_LIST_button()
    });


});

