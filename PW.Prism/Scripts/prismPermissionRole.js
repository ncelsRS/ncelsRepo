var permissionRolesFindButton;
function InitFilterPermissionRoleAllGrid(name) {
    $("#reload" + name).click(function (e) {
        var grid = $("#gridPermissionRoleList" + name).data("kendoGrid");
        grid.dataSource.read();
    });
    permissionRolesFindButton = "#find" + name;
    $(permissionRolesFindButton).click(function (e) {
        var text = $("#findText" + name).val();
        //var findType = $("#findType" + name).val();
        var grid = $("#gridPermissionRoleList" + name).data("kendoGrid");
        if (text != '') {
            $filter = new Array();
           // if (findType == 0) {
                $filter.push({ field: "Name", operator: "contains", value: text });
          //  }
            grid.dataSource.filter({
                logic: "or",
                filters: $filter
            });
        } else {

            grid.dataSource.filter([]);
        }
    });
    $("#findText" + name).keypress(function (d) {
        if (d.keyCode == 13) {
            $("#find" + name).click();
        }
    });
    $("#findText" + name).change(function (e) {
        if ($("#findText" + name).val() == '') {
            var grid = $("#gridPermissionRoleList" + name).data("kendoGrid");
            grid.dataSource.read();
        }
    });
}

function openPermissionRole(roleId, roleName) {
    var parameters = "permissionRole" + roleId;
    if (docArray.indexOf(parameters.toLowerCase()) !== -1)
        docArray.splice(docArray.indexOf(parameters.toLowerCase()), 1);
    var element = document.getElementById(parameters);
    if (element === null) {
        var tabStrip = $("#tabstrip").data("kendoTabStrip");
        var content = '<div id="' + parameters + '"> </div>';
        var idContent = '#' + parameters;
        tabStrip.append({
            text: 'Роль доступа: ' + roleName,
            content: content

        });

        tabStrip.select(tabStrip.items().length - 1);

        var gridElement = $(idContent);

        gridElement.height($(window).height() - 100);

        $.ajax({
            url: "/Employe/PermissionRoleKeysList?roleId=" + roleId,
            success: function (result) {
                $(idContent).html(result);
            }
        });
    } else {

        var itesm = $('#' + parameters)[0].parentElement.getAttribute('id').split('-')[1];
        if (itesm) {
            $("#tabstrip").data("kendoTabStrip").select(itesm - 1);
        }
    }
}