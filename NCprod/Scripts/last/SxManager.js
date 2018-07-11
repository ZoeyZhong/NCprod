var _manager;
$(document).ready(function () {
    _manager = new SxManager();
    _manager.Init();
});

var SxManager = function () {
};

SxManager.prototype.GetInstance = function () {
    return this;
};

SxManager.prototype.Init = function () {
    this.AUTO_HIDE_ALERT_TIME = 2000;
};

SxManager.prototype.GetSucessAlertHtml = function (title) {
    return "<div class=\"alert alert-success\"><a data-dismiss=\"alert\" class=\"close\">×</a>" + title + "</div>";
};

SxManager.prototype.GetSimpleSucessAlertHtml = function (title) {
    return "<div class=\"alert alert-success\">" + title + "</div>";
};

SxManager.prototype.GetErrorAlertHtml = function (title) {
    return "<div class=\"alert alert-error\"><a data-dismiss=\"alert\" class=\"close\">×</a>" + title + "</div>";
};

SxManager.prototype.DelyHideAlert = function () {
    window.setTimeout(function () {
        $("#ActionStateInfo div.alert").hide();
    }, this.AUTO_HIDE_ALERT_TIME);
};

SxManager.prototype.HideAlert = function () {
    $("#ActionStateInfo div.alert").hide();
};

