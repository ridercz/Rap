$(function () {
    if (!Modernizr.inputtypes.date) {
        $.datepicker.regional["cs"] = {
            closeText: "Zavřít",
            prevText: "&#x3c;Dříve",
            nextText: "Později&#x3e;",
            currentText: "Dnes",
            monthNames: ["leden", "únor", "březen", "duben", "květen", "červen", "červenec", "srpen", "září", "říjen", "listopad", "prosinec"],
            monthNamesShort: ["I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII"],
            dayNames: ["neděle", "pondělí", "úterý", "středa", "čtvrtek", "pátek", "sobota"],
            dayNamesShort: ["ne", "po", "út", "st", "čt", "pá", "so"],
            dayNamesMin: ["ne", "po", "út", "st", "čt", "pá", "so"],
            weekHeader: "Týd",
            dateFormat: "yy-mm-dd",
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ""
        };
        $.datepicker.setDefaults($.datepicker.regional["cs"]);
        $("input[type=date]").datepicker();
    }
    if (!Modernizr.inputtypes.time) {
        $("input[type=time]").timepicker({
            showPeriodLabels: false,
            hourText: "Hodina",
            minuteText: "Minuta"
        });
    }
});