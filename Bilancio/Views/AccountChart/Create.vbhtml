@ModelType Bilancio.Models.AccountChart

@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>AccountChart</legend>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Code)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Code)
            @Html.ValidationMessageFor(Function(model) model.Code)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Name)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Name)
            @Html.ValidationMessageFor(Function(model) model.Name)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Active)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Active)
            @Html.ValidationMessageFor(Function(model) model.Active)
        </div>

         <div class="editor-field">
             @Html.DropDownList("AccountCeeID", String.Empty)
             @Html.ValidationMessageFor(Function(model) model.AccountCeeID)
         </div>

        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")


<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js"></script>
<script type="text/javascript">
  $(function() {
      $("#Code").change(checkUniqueCode);
  });

  function checkUniqueCode() {
    $.ajax({
      type: "POST",
      url: "WebServiceCode.asmx/ExistCode",
      data: { tableName: "AccountChart", code: $('#Code').val() },
      success: function(response) {
        $("#duplicate").empty();
        if (response.d != "0") {
          $("#duplicate").html(' That user name has already been taken');
        }
      }
    });
  }

</script> 

End Section
