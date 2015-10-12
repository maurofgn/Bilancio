@ModelType Bilancio.Models.Report

@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>Report</legend>

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
        @Html.DisplayNameFor(Function(model) model.ModelName)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.ModelName)
    </div>

    <div class="display-label">
        @Html.DisplayNameFor(Function(model) model.OutFileName)
    </div>
    <div class="display-field">
        @Html.DisplayFor(Function(model) model.OutFileName)
    </div>
</fieldset>
@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @<p>
        <input type="submit" value="Delete" /> |
        @Html.ActionLink("Back to List", "Index")
    </p>
End Using
