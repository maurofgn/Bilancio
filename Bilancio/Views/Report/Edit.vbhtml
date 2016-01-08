@ModelType Bilancio.Models.Report

@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Report</legend>

        @Html.HiddenFor(Function(model) model.ID)

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
            @Html.LabelFor(Function(model) model.ActionName)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.ActionName)
            @Html.ValidationMessageFor(Function(model) model.ActionName)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.ControllerName)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.ControllerName)
            @Html.ValidationMessageFor(Function(model) model.ControllerName)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.dateCreated)
        </div>
        <div class="editor-field">
            @Html.TextBoxFor(Function(model) model.dateCreated, New With {.readonly = "readonly"})
            @Html.ValidationMessageFor(Function(model) model.dateCreated)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.lastUpdate)
        </div>
        <div class="editor-field">
            @Html.TextBoxFor(Function(model) model.lastUpdate, New With {.readonly = "true"})
            @Html.ValidationMessageFor(Function(model) model.lastUpdate)
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
