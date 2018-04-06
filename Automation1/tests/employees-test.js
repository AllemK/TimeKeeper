'use strict';


var page = require('../pages/employees-page.js');
var D    = require('../data-provider/data.js');
var loginPage = require('../pages/login-page.js');
var menu = require('../pages/menu.js');



describe('Employees operations', function () {


        beforeAll(function() {

            loginPage.log_in();
            menu.click_Employees_tab();
            browser.sleep(9000);

        });

    xit('1.Verify employees Tab is present', function () {
        page.verify_employee_tab_is_present()

    });


    it('2.Verify employees Table is present', function () {
        page.verify_employees_table_is_present()

    });

    xit('3. Verify result is correct after searching with specific word', function () {
         page.enter_word_on_search()
             .verify_searched_result_is_displayed(D.employeesName[0])
     });

    it('4. Verify that after deleting, record is no more present', function () {
        page.click_Delete_button_on_first_row()
            .click_Yes_sure()


    });

});

