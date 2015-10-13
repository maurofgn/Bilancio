﻿@ModelType Bilancio.Models.AccountCee

@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>AccountCee</legend>

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
            @Html.LabelFor(Function(model) model.SeqNo)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.SeqNo)
            @Html.ValidationMessageFor(Function(model) model.SeqNo)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Summary)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Summary)
            @Html.ValidationMessageFor(Function(model) model.Summary)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Total)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Total)
            @Html.ValidationMessageFor(Function(model) model.Total)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.Debit)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.Debit)
            @Html.ValidationMessageFor(Function(model) model.Debit)
        </div>

         <div class="editor-label">
            @Html.LabelFor(Function(model) model.Parent)
         </div>
         <div class="editor-field">
            @Html.DropDownList("ParentID", String.Empty)

            @*@Html.DropDownListFor(Function(model) model.ParentID, CType(ViewBag.ParentID, List(Of SelectListItem)), String.Empty)*@

            @Html.ValidationMessageFor(Function(model) model.ParentID)
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
