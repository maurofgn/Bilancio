@Model string
<div class="input-prepend">
    @* aggungiamo l'icona scegliendo una fra le tante classi icon-* *@
    <span class="add-on"><i class="icon-envelope"></i></span>
    @Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue)})
</div>