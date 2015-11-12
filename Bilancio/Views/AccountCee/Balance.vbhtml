@ModelType Bilancio.Models.Report

@Code
    ViewData("Title") = "Balance"
End Code

<h2>Balance</h2>

@*@Using Html.BeginForm()*@

@Using Html.BeginForm("Balance", "AccountCee", FormMethod.Post, New With {.target = "_blank"})

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Balance Cee</legend>


         @*name: Nome del campo del form da restituire.
         selectList: Insieme di oggetti System.Web.Mvc.SelectListItem utilizzati per popolare l'elenco a discesa.

         Html.DropDownList(
         string name,
         IEnumerable<selectlistitem>
             selectList,
             string optionLabel,
             object htmlAttributes)*@


             <div class="editor-label">
                 Tipo Bilancio
             </div>
             <div class="editor-field">
                 @Html.DropDownList("NodeType", CType(ViewBag.NodeType, IEnumerable(Of SelectListItem)), String.Empty)
             </div>

             <div class="editor-label">
                 Anno di Riferimento
             </div>
             <div class="editor-field">
                 @Html.DropDownList("yearDoc", CType(ViewBag.yearDoc, IEnumerable(Of SelectListItem)), String.Empty)
             </div>

             <p>
                 <input type="submit" value="Esegui" />
             </p>
</fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")

End Section
