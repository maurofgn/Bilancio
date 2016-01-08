@ModelType IEnumerable(Of Bilancio.Models.Report)

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
            @Html.DisplayNameFor(Function(model) model.ModelName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.OutFileName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ActionName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ControllerName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.dateCreated)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.lastUpdate)
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
            @Html.DisplayFor(Function(modelItem) currentItem.ModelName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.OutFileName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.ActionName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.ControllerName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.dateCreated)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) currentItem.lastUpdate)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = currentItem.ID}) |
            @Html.ActionLink("Details", "Details", New With {.id = currentItem.ID}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = currentItem.ID}) |
            @Html.ActionLink("Esegui", currentItem.ActionName, currentItem.ControllerName)
        </td>
    </tr>
Next

</table>
