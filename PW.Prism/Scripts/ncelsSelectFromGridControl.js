var SelectFromGridControl = {
    showGridDialog: function(url, containerName, titleText, textHolder) {
        var window = $("#" + containerName);

        window.kendoWindow({
            width: "550px",
            height: "auto",
            modal: true,
            title: titleText,
            actions: ["Pin", "Refresh", "Maximize", "Close"]
        });
        window.data("kendoWindow").controlId = textHolder;
        // window.containerId = containerName;
        window.data("kendoWindow").title(titleText);
        window.data("kendoWindow").setOptions({
            width: 550,
            height: 'auto'
        });

        window.data("kendoWindow").refresh(url);// '?containerId=' + containerName

        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
        return false;
    },
    serializeObject: function (form) {
        var o = {};
        var a = form.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        form.find(".multiselect-control").each(function () {
            var name = $(this).attr("name");
            o[name] = [];

            var ids = $(this).attr("data-ids");
            if (ids) {
                var valueArray = ids.split(';');

                for (var i = 0; i < valueArray.length; i++) {
                    if (valueArray[i] !== "")
                        o[name].push({ Id: valueArray[i] });
                }
            }
        });

        return o;
    }
};