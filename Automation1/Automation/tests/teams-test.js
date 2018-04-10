'use strict';

;
var D      = require('../data-provider/data.js');
var page = require('../pages/teams-page.js');
var loginPage = require('../pages/login-page.js');

describe('Teams Page', function () {


        beforeAll(function () {

            browser.get(D.baseUrl);
            loginPage.log_in();
            browser.sleep(3000);
        });

        it('1. Verify record is present after creating it', function () {
            page.click_teams_tab()
                .click_add_New_teams_button()
                .enter_Team_ID_in_input_field()
                .enter_Team_name_in_input_field()
                .enter_description_in_description_input_field()
                .click_Save()
        });

        it('2. Verify that after deleting, record is no more present', function () {
        page.click_Delete_button_on_last_row()
            .click_Yes_sure()
            .verify_new_record_is_not_displayed()
    });


});

