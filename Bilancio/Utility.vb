Imports Bilancio.Models
Imports Bilancio.DAL


Public Module Utility

    Public Const LINES_PER_PAGE = 10

    Public Enum DareAvere As Integer
        Dare = 1
        Avere = -1
    End Enum

    Public Function negate(da As DareAvere) As DareAvere

        If (DareAvere.Dare.Equals(da)) Then
            Return DareAvere.Avere
        ElseIf (DareAvere.Avere.Equals(da)) Then
            Return DareAvere.Dare
        End If

        Return DareAvere.Avere  ''il default è di norma il Dare, per cui essendo negato torna Avere

    End Function

    Public Function DebitString(debit As DareAvere) As String
        Return [Enum].GetName(GetType(DareAvere), debit)
    End Function

End Module


