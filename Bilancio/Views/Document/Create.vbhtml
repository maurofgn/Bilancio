@ModelType Bilancio.Models.Document

@Code
    ViewData("Title") = "Create"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Create</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Document</legend>

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
            @Html.LabelFor(Function(model) model.docNr)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.docNr)
            @Html.ValidationMessageFor(Function(model) model.docNr)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.note)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.note)
            @Html.ValidationMessageFor(Function(model) model.note)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.amount)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.amount)
            @Html.ValidationMessageFor(Function(model) model.amount)
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
