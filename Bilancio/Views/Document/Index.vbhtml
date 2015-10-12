@ModelType IEnumerable(Of Bilancio.Models.Document)

@Code
    ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table>
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Code)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Active)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.dateReg)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.dateDoc)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DocNr)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Note)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Amount)
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
            @Html.DisplayFor(Function(modelItem) currentItem.dateReg)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.dateDoc)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.DocNr)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.Note)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.Amount)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = currentItem.ID}) |
            @Html.ActionLink("Details", "Details", New With {.id = currentItem.ID}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = currentItem.ID})
        </td>
    </tr>
Next

</table>
