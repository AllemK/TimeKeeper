'use strict';

var EC     = protractor.ExpectedConditions;
var helper = require('../helpers/e2e-helper.js');


var projectsHeading = $('[href="#!/projects"]');

var ProjectsPage = function () {

    this.click_projects_heading = function () {
        helper.waitVisibility(projectsHeading);
        return this;
    };
};
module.exports = new ProjectsPage();