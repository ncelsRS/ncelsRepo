function Showbusy() {
    if (event.ctrlKey || event.shiftKey || event.metaKey || (event.button && event.button === 1)) {
        return;
    }
    event.preventDefault();
    ShowSpinner();
}

function ShowSpinner() {
    $("#loading").fadeIn();
    var opts = {
        lines: 15, // The number of lines to draw
        length: 34, // The length of each line
        width: 14, // The line thickness
        radius: 41, // The radius of the inner circle
        corners: 1, // Corner roundness (0..1)
        rotate: 64, // The rotation offset
        direction: 1, // 1: clockwise, -1: counterclockwise
        color: '#000', // #rgb or #rrggbb
        speed: 1, // Rounds per second
        trail: 60, // Afterglow percentage
        shadow: false, // Whether to render a shadow
        hwaccel: false, // Whether to use hardware acceleration
        className: 'spinner', // The CSS class to assign to the spinner
        zIndex: 2e9, // The z-index (defaults to 2000000000)
        top: 'auto', // Top position relative to parent in px
        left: 'auto' // Left position relative to parent in px
    };
    var target = document.getElementById('loading');
    var spinner = new Spinner(opts).spin(target);
}
function replaceAll(find, replace, str) {
    while (str.indexOf(find) > -1) {
        str = str.replace(find, replace);
    }
    return str;
}