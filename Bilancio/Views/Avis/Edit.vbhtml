@ModelType Bilancio.Models.Avis

@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Avis</legend>

        @Html.HiddenFor(Function(model) model.ID)

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

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.dateCreated)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.dateCreated)
            @Html.ValidationMessageFor(Function(model) model.dateCreated)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Address)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Address)
            @Html.ValidationMessageFor(Function(model) model.Address)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.City)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.City)
            @Html.ValidationMessageFor(Function(model) model.City)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.PostalCode)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.PostalCode)
            @Html.ValidationMessageFor(Function(model) model.PostalCode)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Region)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Region)
            @Html.ValidationMessageFor(Function(model) model.Region)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Email)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Email)
            @Html.ValidationMessageFor(Function(model) model.Email)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Phone)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Phone)
            @Html.ValidationMessageFor(Function(model) model.Phone)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.ContactName)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.ContactName)
            @Html.ValidationMessageFor(Function(model) model.ContactName)
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
