@ModelType Bilancio.Models.Document

@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<fieldset>
    <legend>Document</legend>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Code)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Code)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Name)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Name)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Active)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Active)
    </div>

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
        @Html.DisplayNameFor(Function(model) model.DocNr)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.DocNr)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Note)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Note)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Amount)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Amount)
    </div>
</fieldset>
<p>

    @Html.ActionLink("Edit", "Edit", New With {.id = Model.ID}) |
    @Html.ActionLink("Back to List", "Index")
</p>
