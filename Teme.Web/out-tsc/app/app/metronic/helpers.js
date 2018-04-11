import * as $ from "jquery";
var Helpers = (function () {
    function Helpers() {
    }
    Helpers.loadStyles = function (tag, src) {
        if (Array.isArray(src)) {
            $.each(src, function (k, s) {
                $(tag).append($('<link/>').attr('href', s).attr('rel', 'stylesheet').attr('type', 'text/css'));
            });
        }
        else {
            $(tag).append($('<link/>').attr('href', src).attr('rel', 'stylesheet').attr('type', 'text/css'));
        }
    };
    Helpers.unwrapTag = function (element) {
        $(element).removeAttr('appunwraptag').unwrap();
    };
    /**
     * Set title markup
     * @param title
     */
    Helpers.setTitle = function (title) {
        $('.m-subheader__title').text(title);
    };
    /**
     * Breadcrumbs markup
     * @param breadcrumbs
     */
    Helpers.setBreadcrumbs = function (breadcrumbs) {
        if (breadcrumbs)
            $('.m-subheader__title').addClass('m-subheader__title--separator');
        var ul = $('.m-subheader__breadcrumbs');
        if ($(ul).length === 0) {
            ul = $('<ul/>').addClass('m-subheader__breadcrumbs m-nav m-nav--inline')
                .append($('<li/>').addClass('m-nav__item')
                .append($('<a/>').addClass('m-nav__link m-nav__link--icon')
                .append($('<i/>').addClass('m-nav__link-icon la la-home'))));
        }
        $(ul).find('li:not(:first-child)').remove();
        $.each(breadcrumbs, function (k, v) {
            var li = $('<li/>').addClass('m-nav__item')
                .append($('<a/>').addClass('m-nav__link m-nav__link--icon').attr('routerLink', v.href).attr('title', v.title)
                .append($('<span/>').addClass('m-nav__link-text').text(v.text)));
            $(ul).append($('<li/>').addClass('m-nav__separator').text('-')).append(li);
        });
        $('.m-subheader .m-stack__item:first-child').append(ul);
    };
    Helpers.setLoading = function (enable) {
        var body = $('body');
        if (enable) {
            $(body).addClass('m-page--loading-non-block');
        }
        else {
            $(body).removeClass('m-page--loading-non-block');
        }
    };
    Helpers.bodyClass = function (strClass) {
        $('body').attr('class', strClass);
    };
    return Helpers;
}());
export { Helpers };
//# sourceMappingURL=helpers.js.map