@Helper GetTreeView(allAccount As List(Of Bilancio.Models.AccountCee), parentID As Integer) 
For Each i As Bilancio.Models.AccountCee In allAccount.Where(Function(a) a.Parent.ID.Equals(parentID))
            @<li>
            @code Dim sonsNr = allAccount.Where(Function(a) a.Parent.ID.Equals(i.ID)).Count()end code
            @If (sonsNr > 0) Then
               @<span class="collapse collapsible">&nbsp;</span>
                Else
               @<span style="width:15px; display:inline-block">&nbsp;</span>
                End If

            <span>
                 <a href="/AccountCee/Edit/@i.ID">@i.Name</a>
            </span>

            @If (sonsNr > 0) Then
                @<ul>
                    @Treeview.GetTreeView(allAccount, i.ID)
                    @* Recursive Call for Populate Sub items here*@
                </ul>
                End If
        </li>
    Next
End Helper


