var helper       = require('../helpers/e2e-helper.js');
exports.randomNo = Math.floor(1000000 * Math.random() + 1).toString();

exports.baseUrl                           = 'http://localhost:63342/TimeKeeper.Web';
exports.randomEmail                       = exports.randomNo + '@mailsac.com'
exports.verificationCode                  = '';
exports.thankYouMsg                       = 'Your Email address is confirmed. Now you can start using Seomator';
exports.planAndPricingHeader              = 'PLANS & PRICING';
exports.choosePaymentHeader               = 'Choose your payment method';
exports.thankYouMsg_subscriptionCompleted = 'Your current Plan is Pay as you Go. Enjoy our advanced service!'

exports.employeeName = [
    'Amel Delić'
];

exports.Year = [
    '2017'
];

exports.Month = [
    'July'
];

