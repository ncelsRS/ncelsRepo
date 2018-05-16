function showDocComment(control) {
    var thisControl = $(control).find("i");
    var documentId = $("#modelId").val();
    debugger;
    var controlEdit = $(control).closest("tr");
    var categoryId = $(control).attr("name");
    var url = "/Upload/ShowDocComment?documentId=" + documentId + "&categoryId=" + categoryId;
    $("<div style=" + '"' + "text-align: center;" + '"' + "><img src=" + '"' + "../../content/img/spinner.gif" + '"' + " style=" + '"' + "display: block; margin: 0 auto;" + '"' + " /></br>" + "....</div>")
          .addClass("dialog")
          .attr("id", $(this)
              .attr("data-dialog-id"))
          .appendTo("body")
          .dialog({ 
              title: "Описание",
              closeText: "x",
              open: function (event, ui) {
                  $(event.target).parent().css('position', 'fixed');
                  $(event.target).parent().css('top', '150px');
                  $(event.target).parent().css('center');
                  $(this).closest(".ui-dialog")
    .find(".ui-dialog-titlebar-close")
//      .removeClass("ui-dialog-titlebar-close")
    .html("<span class='ui-button-icon-primary ui-icon ui-icon-closethick'></span>");


              },
              close: function () { $(this).remove(); },
              width: 800,
              height: 400,
              modal: true,
              //   open: function(event, ui) { $(".ui-dialog-titlebar-close").text(''); },
              buttons: {
                  Сохранить: function () {
                      var comment = $("#NoteComment").val();
//                      var userId = $("#commentUserId").val();
                      var isError;
                      if ($("#IsError").is(":checked")) {
                          isError = true;
                      } else {
                          isError = false;
                      }
                      var params = JSON.stringify({ 'documentId': documentId, 'categoryId': categoryId, 'isError': isError, 'comment': comment });
                      $.ajax({
                          type: "POST",
                          url: '/Upload/SaveComment',
                          data: params,
                          dataType: 'json',
                          cache: false,
                          contentType: "application/json; charset=utf-8",
                          success: function (data) {
                              if (!thisControl.hasClass('mark-icon')) {
                                  thisControl.addClass('mark-icon');

                              }
                              if (isError) {
                                  if (!controlEdit.hasClass('control-error')) {
                                      controlEdit.addClass('control-error');
                                  }
                                  if (controlEdit.hasClass('control-good')) {
                                      controlEdit.removeClass('control-good');
                                  }
                              } else {
                                  if (!controlEdit.hasClass('control-good')) {
                                      controlEdit.addClass('control-good');
                                  }
                                  if (controlEdit.hasClass('control-error')) {
                                      controlEdit.removeClass('control-error');
                                  }
                              }

                          },
                          error: function () {
                              alert("Connection Failed. Please Try Again");
                          }
                      });
                      $(this).dialog("close");
                  }
              }
          })
          .load(url);
}