'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');
var D = require('../data-provider/data.js');

var teamsButton      = element(by.buttonText('Teams'));
var searchButton  = $('[placeholder="Search for..."]');


var TeamsPage = function () {

    this.click_teams_button = function () {
        helper.waitAndClick(teamsButton);
        helper.waitVisibility(teamsButton);
        browser.sleep(5000);
        return this;
    };

    this.click_search_button = function () {
        helper.waitAndClick(searchButton);
        helper.waitVisibility(searchButton);
        browser.sleep(5000);
        return this;

    };
};
module.exports = new TeamsPage();