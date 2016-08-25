(function ($) {
    var defaults = {
        tabAttr: "tab",
        tabChosenClassName: "tab-sl",
        switchAttr: "tab-s",
        switchChosenClassName: "tabsw-sl",
        animateTime: 120
    };
    var settings;
    $.fn.tabs = function (options) {
        if (options == undefined || typeof (options) === "object") {
            settings = $.extend({}, defaults, options);
            settings.tabChosenClass = "." + settings.tabChosenClassName;
            settings.switchChosenClass = "." + settings.switchChosenClassName;
            settings.tabAttrStr = "[" + settings.tabAttr + "]";
            settings.switchAttrStr = "[" + settings.switchAttr + "]";
            var tabsc = $("[" + settings.switchAttr + "]");
            $(tabsc).on("click", function () {
                tabChange(this);
            });
            tabChange($(tabsc).first());
            return this;
        }
        return null;
    };

    var tabChange = function (selectSw) {
        selectSw = $(selectSw);
        var chosenSw = $(settings.switchAttrStr).filter(settings.switchChosenClass);
        var chosenTab = $(settings.tabAttrStr).filter(settings.tabChosenClass);
        var num = selectSw.attr(settings.switchAttr);
        var selectTab = $("[" + settings.tabAttr + "=" + num + "]");

        if (num && selectTab != undefined && num !== chosenTab.attr(settings.tabAttr)) {
            if (chosenTab[0]) {
                chosenTab.animate({ "opacity": 0 }, settings.animateTime, function () {
                    chosenSw.removeClass(settings.switchChosenClassName);
                    selectSw.addClass(settings.switchChosenClassName);
                    chosenTab.removeClass(settings.tabChosenClassName).removeAttr("style");
                    selectTab.css("opacity", 0).addClass(settings.tabChosenClassName).animate({ "opacity": 1 }, settings.animateTime);
                    if (selectTab[0].onChoseEvent) {
                        selectTab[0].onChoseEvent(selectTab[0]);
                    }
                });
            } else {
                $(selectSw).addClass(settings.switchChosenClass);
                $(selectTab).css("opacity", 0).addClass(settings.tabChosenClassName).animate({ "opacity": 1 }, settings.animateTime);
            }
        }
    }
    $.fn.onChoseTab = function (func) {
        if (func && typeof (func) === "function") {
            for (var i = 0; i < this.length;i+=1)
            this[i].onChoseEvent = func;
        }
    };
}(jQuery));