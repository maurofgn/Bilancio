@ModelType Bilancio.Models.Document

@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Document</legend>

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

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.dateReg)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.dateReg)
            @Html.ValidationMessageFor(Function(model) model.dateReg)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.dateDoc)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.dateDoc)
            @Html.ValidationMessageFor(Function(model) model.dateDoc)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.DocNr)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.DocNr)
            @Html.ValidationMessageFor(Function(model) model.DocNr)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Note)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Note)
            @Html.ValidationMessageFor(Function(model) model.Note)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Amount)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Amount)
            @Html.ValidationMessageFor(Function(model) model.Amount)
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
End Section
