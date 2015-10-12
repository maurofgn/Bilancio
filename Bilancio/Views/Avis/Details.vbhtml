@ModelType Bilancio.Models.Avis

@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<fieldset>
    <legend>Avis</legend>

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
        @Html.DisplayNameFor(Function(model) model.dateCreated)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.dateCreated)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Address)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Address)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.City)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.City)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.PostalCode)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.PostalCode)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Region)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Region)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Email)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Email)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.Phone)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.Phone)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.ContactName)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.ContactName)
    </div>
</fieldset>
<p>

    @Html.ActionLink("Edit", "Edit", New With {.id = Model.ID}) |
    @Html.ActionLink("Back to List", "Index")
</p>
