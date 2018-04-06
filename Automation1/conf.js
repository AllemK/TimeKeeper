var HtmlReporter = require('protractor-jasmine2-screenshot-reporter');

exports.config = {


  /*  multiCapabilities: [
       {
            browserName: 'MicrosoftEdge',
            specs: ['./tests/!*',],
            params: 'desktop version'
        },*/
       /* {
            browserName: 'firefox',
            specs: ['./tests/*',],
            params: 'desktop version'
        },
        {
            browserName: 'chrome',
            specs: ['./tests/*',],
            params: 'desktop version',
        },
        {
            browserName: 'chrome',
            specs: ['./tests/*',],
            params: 'tablet version',
        },
        {
            browserName: 'chrome',
            //chromeOptions: {mobileEmulation: {deviceName: 'Google Nexus 5'}},
            specs: ['./tests/*'],
            params: 'mobile version'
        },
    ],*/


    framework: 'jasmine',

    specs:
      //  ['./tests/test-template.js',
      './tests/customers-test.js',
       //'./tests/employees-test.js',
       // './tests/calendar-test.js',
       //  './tests/projects-test.js',
      //  './tests/menu.js',


    onPrepare: function () {

        //protractor-jasmine2-screenshot-reporter
        jasmine.getEnv().addReporter(
            new HtmlReporter({
                ignoreSkippedSpecs: true,
                test: '_REPORT_',
                filename: 'results.html'
            })
        );

        browser.ignoreSynchronization = true;
        var D                      = require('./data-provider/data.js');

        // returning the promise makes protractor wait for the reporter config before executing tests
        return global.browser.getProcessedConfig().then(function (config) {
            var chromeOptions = config.capabilities.chromeOptions;
            var browserName = config.capabilities.browserName;
            var parameter = config.capabilities.params;

     /*       D.selectedBrowser = browserName.toUpperCase();
            if (D.selectedBrowser === 'MICROSOFTEDGE') D.selectedBrowser = 'EDGE';

            function setConfigurationForMobile() {
                if (parameter === 'mobile version') {
                    browser.driver.manage().window().setSize(D.screenResolution.mobileSize.width, D.screenResolution.mobileSize.height);
                    D.selectedVersion = D.versions.mobile;
                }
            }

            function setConfigurationForDesktop() {
                if (parameter === 'desktop version') {
                    browser.driver.manage().window().setSize(D.screenResolution.desktopSize.width, D.screenResolution.desktopSize.height);
                    D.selectedVersion = D.versions.desktop;
                }
            }

            function setConfigurationForDesktop_SmallResolution() {
                if (parameter === 'tablet version') {
                    browser.driver.manage().window().setSize(D.screenResolution.tabletSize.width, D.screenResolution.tabletSize.height);
                    D.selectedVersion = D.versions.tablet;
                }
            }

            setConfigurationForDesktop();
            setConfigurationForMobile();
            setConfigurationForDesktop_SmallResolution()*/
        });

    },


    // Options to be passed to Jasmine.
    jasmineNodeOpts: {
        defaultTimeoutInterval: 30000
    },

   plugins: [{
        package: 'protractor-screenshoter-plugin',
        screenshotPath: './report-with-screenshots',
        screenshotOnExpect: 'failure+success',
        screenshotOnSpec: 'none',
        withLogs: 'false',
        writeReportFreq: 'asap',
        imageToAscii: 'none',
        clearFoldersBeforeTest: true
    }]
};
