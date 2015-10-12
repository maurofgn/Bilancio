@ModelType Bilancio.Models.Avis

@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @<p>
        <input type="submit" value="Delete" /> |
        @Html.ActionLink("Back to List", "Index")
    </p>
End Using
