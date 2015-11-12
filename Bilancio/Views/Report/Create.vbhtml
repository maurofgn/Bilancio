@ModelType Bilancio.Models.Report

@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Report</legend>

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
            @Html.LabelFor(Function(model) model.ModelName)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.ModelName)
            @Html.ValidationMessageFor(Function(model) model.ModelName)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.OutFileName)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.OutFileName)
            @Html.ValidationMessageFor(Function(model) model.OutFileName)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.ActioneName)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.ActioneName)
            @Html.ValidationMessageFor(Function(model) model.ActioneName)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.ControllerName)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.ControllerName)
            @Html.ValidationMessageFor(Function(model) model.ControllerName)
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
