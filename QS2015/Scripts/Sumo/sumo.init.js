

jQuery(document).ready(function ($) {
    //window.asd = $('.SlectBox').SumoSelect({ csvDispCount: 3 });
    window.asd = $('.SlectBox').SumoSelect(
        {
            okCancelInMulti: false,
            triggerChangeCombined: false
        });

    window.test = $('.testsel').SumoSelect({ okCancelInMulti: true });
    window.testSelAll = $('.testSelAll').SumoSelect({ okCancelInMulti: true, selectAll: true });
    window.testSelAll2 = $('.testSelAll2').SumoSelect({ selectAll: true });
});