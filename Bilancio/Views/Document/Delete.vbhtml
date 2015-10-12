@ModelType Bilancio.Models.Document

@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @<p>
        <input type="submit" value="Delete" /> |
        @Html.ActionLink("Back to List", "Index")
    </p>
End Using
