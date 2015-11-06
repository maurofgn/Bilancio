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

<script type="text/javascript">
    $(document).ready(
         function () {
             $("#Code").change(checkUniqueCode);
         }
    )

    function checkUniqueCode() {

      $.ajax({
          url: "/Util/chkCode",
          data: JSON.stringify({ tableName: "AccountChart", code: $('#Code').val(), id: 0 }),
          type: 'POST',
          contentType: 'application/json;',
          dataType: 'json',
          success: function (response) {
              if (response.Success == "1") {
                  //window.location.href = "/Document/index";
                  //alert("codice libero " + response.ex)
              }
              else {
                  alert(response.ex);
                  $('#Code').val("");
                  $('#Code').focus();
              }
          },
          error: function (jqXHR, textStatus, errorThrown) {
                  alert(jqXHR.statusText + " (ajax error) url: " +  jqXHR.url );
              }
        });
  }

</script> 

End Section
