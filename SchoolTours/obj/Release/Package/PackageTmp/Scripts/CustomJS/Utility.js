var host = window.location.host;
//$(document).ready(function () {

//});

function redirectToAppSetting(page) {
    if (host == '43.229.227.26:81') {
        window.location.href = "/schoolTours/ApplicationsSettings/" + page + "";
    }
    else {
        window.location.href = "/ApplicationsSettings/" + page + "";
    }
}
function redirectTodashboard() {
    if (host == '43.229.227.26:81') {
        window.location.href = "/schoolTours/dashboard";
    }
    else {
        window.location.href = "../dashboard";
    }
}
function MenuRedirect(Menu, Page) {
    var Redirect = "";
    if (host == '43.229.227.26:81') {
        Redirect = "/schoolTours";
    }
    else {
        Redirect = "";
    }
    window.location.href = Redirect + "/" + Menu + "/" + Page;
}
function MenuRedirectpage(Page) {
    var Redirect = "";
    if (host == '43.229.227.26:81') {
        Redirect = "/schoolTours";
    }
    else {
        Redirect = "";
    }
    window.location.href = Redirect + "/" + Page;
}

