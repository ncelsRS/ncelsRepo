function updateLeftPane() {
    $.ajax({
        url: "/OBKExpertCouncil/GetLeftPane",
        success: function (result) {
            $("#expertCouncil_leftPane").html(result);
        }
    })
}