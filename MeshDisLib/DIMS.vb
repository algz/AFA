
Module DIMS


    Public Points As List(Of VectorClass) = New List(Of VectorClass)
   Public Grids As List(Of GridClass) = New List(Of GridClass)


    Public Unit As Integer = 1 '单位


    Public X_max, X_min As Double
    Public Y_max, Y_min As Double
    Public Z_max, Z_min As Double

    Public Function 消去空格(ByVal ss As String()) As String()
        Dim ss1() As String
        Dim k As Integer = 0
        Dim bbb As Integer
        bbb = UBound(ss)
        ReDim ss1(bbb)
        For i = 0 To bbb
            If ss(i) <> "" Then
                ss1(k) = ss(i)
                k += 1
            End If
        Next
        k = k - 1
        ReDim ss(k)
        For i = 0 To k
            ss(i) = ss1(i)
        Next
        Return ss
    End Function
 
End Module
