@Model int

@* le classi input-prepend e input-append posizioneranno gli add-on in maniera contigua alla casella *@
<div class="input-prepend input-append">
    <span class="add-on">€</span>
    @Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue, New With {.class = "text-right"})})
    <span class="add-on">,00</span>
</div>
