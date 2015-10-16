@ModelType PagedList.IPagedList(Of Bilancio.Models.Document)
@Imports PagedList.Mvc
<link href="~/Content/PagedList.css" rel="stylesheet" type="text/css" />
@Code
    ViewBag.Title = "Documenti"
End Code

<h2>Documenti</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table>
    <tr>
        <th>
            @Html.ActionLink("Data Reg", "Index", New With {.sortOrder = ViewBag.DateRegSortParm, .currentFilter = ViewBag.CurrentFilter})
        </th>
        <th>
            @Html.ActionLink("Data Doc", "Index", New With {.sortOrder = ViewBag.DateDocSortParm, .currentFilter = ViewBag.CurrentFilter})
        </th>
        <th>
            @Html.ActionLink("Nr Doc", "Index", New With {.sortOrder = ViewBag.NrDocSortParm, .currentFilter = ViewBag.CurrentFilter})
        </th>
        <th>
            @Html.ActionLink("Tipo Doc", "Index", New With {.sortOrder = ViewBag.DocTypeSortParm, .currentFilter = ViewBag.CurrentFilter})
        </th>
        <th>
            Importo
        </th>
        <th>
            Nota
        </th>
        <th></th>
    </tr>

@For Each item In Model
    Dim currentItem = item
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.dateReg)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.dateDoc)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.docNr)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.documentType.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.amount)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.note)
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
