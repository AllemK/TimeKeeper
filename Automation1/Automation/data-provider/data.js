var helper       = require('../helpers/e2e-helper.js');
exports.randomNo = Math.floor(1000000 * Math.random() + 1).toString();

exports.baseUrl                           = 'http://localhost:63342/TimeKeeper.Web';
exports.randomEmail                       = exports.randomNo + '@mailsac.com'
exports.verificationCode                  = '';
exports.thankYouMsg                       = 'Your Email address is confirmed. Now you can start using Seomator';
exports.planAndPricingHeader              = 'PLANS & PRICING';
exports.choosePaymentHeader               = 'Choose your payment method';
exports.thankYouMsg_subscriptionCompleted = 'Your current Plan is Pay as you Go. Enjoy our advanced service!'

exports.employeesName = [
    'Jasmin'
];

exports.Year = [
    '2017'
];


exports.customerName = [
    'Ero'
];

exports.teamName = [
    'Alpha'
];

exports.projectStatus = [
    'On Hold'
];

exports.projectPricing = [
    'Hourly Rate'
];

/*--------------------------------*/
exports.employeeDropdown = [
    'Adis Ramić'
];
exports.monthDropdown = [
    'August'
];



exports.empDropdown = [
    'Adis Ramić'
];
exports.monDropdown = [
    'June'
];



exports.emDropdown = [
    'Adis Ramić'
];
exports.moDropdown = [
    'June'
];

exports.dayDropdown = [
    'Working Day'
];

exports.proNameDropdown = [
    'Method'
];

exports.projectName = [
    '365 Design'
];