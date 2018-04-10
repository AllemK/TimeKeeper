'use strict';


var D    = require('../data-provider/data.js');
var page = require('../pages/projects-page.js');
var loginPage = require('../pages/login-page.js');




describe('Projects Page', function () {

    beforeAll(function () {

        browser.get(D.baseUrl);
        loginPage.log_in();
        browser.sleep(9000);
    });

    it('1. Verify record is present after creating it', function () {
        page.click_projects_tab()
            .click_add_New_project_button()
            .enter_project_name_in_input_field()
            .enter_project_monogram_in_input_field()
            .enter_new_customer_name_in_input_field(D.customerName[0])
            .enter_team_in_team_input_field(D.teamName[0])
            .enter_amount_in_amount_input_field()
            .enter_description_in_description_input_field()
            .enter_status_in_status_input_field(D.projectStatus[0])
            .enter_pricing_in_pricing_input_field(D.projectPricing[0])
            .click_Save()

    });
});