'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');

var teamsTab      = $('[href="#!/teams"]');
var addNewButton  = element(by.buttonText('Add new'));
var teamID =    element(by.model('team.id'));
var teamName = element(by.model('team.name'));
var teamDescription = element(by.model('team.description'));
var saveButton = element(by.buttonText('Save'));


var TeamsPage = function () {

    this.click_teams_tab = function () {
        helper.waitAndClick(teamsTab);
        return this;

    };

    this.click_add_New_teams_button = function () {
        helper.waitAndClick(addNewButton);
        return this;

    };
    this.enter_Team_ID_in_input_field = function () {
        helper.clearAndEnterValue(teamID,'123');
        return this;

    };
    this.enter_Team_name_in_input_field = function () {
        helper.clearAndEnterValue(teamName,'DREAM TEAM');
        return this;

    };
    this.enter_description_in_description_input_field = function () {
        helper.clearAndEnterValue(teamDescription,'We are dream team');
        return this;

    };

    this.click_Save = function () {
        helper.waitAndClick(saveButton);
        browser.sleep(3000);
        return this;

    };


};
module.exports = new TeamsPage();