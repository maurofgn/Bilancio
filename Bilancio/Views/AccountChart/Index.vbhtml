@ModelType PagedList.IPagedList(Of Bilancio.Models.AccountChart)
@Imports PagedList.Mvc
<link href="~/Content/PagedList.css" rel="stylesheet" type="text/css" />
@Code
    ViewBag.Title = "Conti del Piano dei Conti"
End Code

<h2>Piano dei conti</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>


@Using Html.BeginForm("Index", "AccountChart", FormMethod.Get)
    @<p>
        @Html.TextBox("SearchString", TryCast(ViewBag.CurrentFilter, String))
        <input type="submit" value="Search" />
    </p>
End Using

<table>
    <tr>
        <th>
            @Html.ActionLink("Codice", "Index", New With {.sortOrder = ViewBag.CodeSortParm, .currentFilter = ViewBag.CurrentFilter})
        </th>
        <th>
            @Html.ActionLink("Descrizione", "Index", New With {.sortOrder = ViewBag.NameSortParm, .currentFilter = ViewBag.CurrentFilter})
        </th>
        <th>
            Attivo
            @*@Html.DisplayNameFor(Function(model) model.Active)*@
        </th>
        <th></th>
    </tr>

@For Each item In Model
    Dim currentItem = item
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.Code)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.Active)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = currentItem.ID}) |
            @Html.ActionLink("Details", "Details", New With {.id = currentItem.ID}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = currentItem.ID})
        </td>
    </tr>
Next

</table>
<br />
<div class="pagination" style="display:inline-block; vertical-align:middle;">
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", _
        New With {page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter}))
</div>