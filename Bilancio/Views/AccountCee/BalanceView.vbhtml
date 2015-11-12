@ModelType IEnumerable(Of Bilancio.ViewModels.CreditDebitAccount)

@*@Code
    ViewData("Title") = "Bilancio " & ViewBag.type
End Code*@

<table>
    <tr>
        <th colspan="5" align="center">
           Bilancio @ViewBag.type
        </th>
    </tr>

    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Code)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th align="center">
            @ViewBag.year
@*            @Html.DisplayNameFor(Function(model) model.creditDebit.balanceYear)*@
        </th>
        <th align="center">
            @ViewBag.yearPrev
            @*            @Html.DisplayNameFor(Function(model) model.creditDebit.balanceYearPrev)*@
        </th>
        <th align="right">            
            @Html.DisplayNameFor(Function(model) model.creditDebit.delta)
        </th>
    </tr>

@code
    Dim style As String
    Dim totale As String = "Totale"
End Code

@For Each item In Model
    Dim currentItem = item

    @If currentItem.headFoot = Bilancio.ViewModels.HeadFood.BODY Then
            style = "font-weight:normal"
        ElseIf currentItem.headFoot = Bilancio.ViewModels.HeadFood.HEAD_WIDTH_FOOD Then
            style = "font-weight:bold"
        Else
            style = "font-weight:bold"
        End If
    
    @<tr>


    @If (currentItem.headFoot = Bilancio.ViewModels.HeadFood.HEAD_WIDTH_FOOD) Then
        @<td colspan="5" align="center" style="@style">
            @Html.DisplayFor(Function(modelItem) currentItem.Name)
        </td>
    Else
        
        @<td style="@style">
        @If (currentItem.headFoot = Bilancio.ViewModels.HeadFood.FOOD) Then
            @totale
        Else
            @Html.DisplayFor(Function(modelItem) currentItem.Code)
        End If
        </td>
        
        @<td style="@style">
            @Html.DisplayFor(Function(modelItem) currentItem.Name)
        </td>
        
        @<td align="right" style="@style">
            @Html.DisplayFor(Function(modelItem) currentItem.creditDebit.balanceYear)
        </td>
        @<td align="right" style="@style">
            @Html.DisplayFor(Function(modelItem) currentItem.creditDebit.balanceYearPrev)
        </td>
        @<td align="right" style="@style">
            @Html.DisplayFor(Function(modelItem) currentItem.creditDebit.delta)
        </td>
    End If

    </tr>
Next

</table>
