﻿@ModelType Bilancio.Models.AccountChart

@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>AccountChart</legend>

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
