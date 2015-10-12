@ModelType Bilancio.Models.AccountCee

@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<fieldset>
    <legend>AccountCee</legend>

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
        @Html.DisplayNameFor(Function(model) model.SeqNo)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.SeqNo)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Summary)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Summary)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Total)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Total)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Debit)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Debit)
    </div>
</fieldset>
<p>

    @Html.ActionLink("Edit", "Edit", New With {.id = Model.ID}) |
    @Html.ActionLink("Back to List", "Index")
</p>
