@ModelType Bilancio.Models.Document

@Code
    ViewData("Title") = "Delete"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>Document</legend>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.dateReg)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.dateReg)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.dateDoc)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.dateDoc)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.docNr)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.docNr)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.note)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.note)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.amount)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.amount)
    </div>
</fieldset>
@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @<p>
        <input type="submit" value="Delete" /> |
        @Html.ActionLink("Back to List", "Index")
    </p>
End Using
