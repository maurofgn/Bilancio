@ModelType Bilancio.Models.Document

@Code
    ViewData("Title") = "Details"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Details</h2>

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
<p>

    @Html.ActionLink("Edit", "Edit", New With {.id = Model.ID}) |
    @Html.ActionLink("Back to List", "Index")
</p>
