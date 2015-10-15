Imports System.IO
Imports System.Math
Imports System.Drawing

Public Class MeshDisControl

    Dim gtm As MiniGraphicsLib.Geom3dTriMesh
    Public Sub MeshDis(ByVal strMeshPath As String)

        Dim str22() As String '定义字符串类型的变量和一维数组

        Dim flag_first As Boolean = True

        Dim reader As StreamReader = New StreamReader(strMeshPath)

        Dim sss As String


        'Dim ee As Integer = CInt("&H" & "8000")

        X_min = 9999999999999999
        Y_min = 9999999999999999
        Z_min = 9999999999999999
        X_max = -9999999999999999
        Y_max = -9999999999999999
        Z_max = -9999999999999999


        Dim x, y, z As Double

        Dim ctria_1, ctria_2, ctria_3 As Integer


        X_min = 9999999999999999
        Y_min = 9999999999999999
        Z_min = 9999999999999999
        X_max = -9999999999999999
        Y_max = -9999999999999999
        Z_max = -9999999999999999


        System.GC.Collect()

        Points.Clear()
        Grids.Clear()

        Points.Add(New VectorClass(0, 0, 0)) '点坐标的数组是从1开始的,不是从0开始的,方便
        Grids.Add(New GridClass(1, 2, 3)) '网格数组也是从1开始的

        Dim Flag_PointIndexMin As Boolean = True
        Dim n = 0



        Do

            sss = reader.ReadLine()

            If reader.EndOfStream Then
                Exit Do
            End If



            str22 = sss.Split(" ")
            str22 = 消去空格(str22)

            If str22.Count = 0 Then
                Continue Do
            End If

            If sss.Split("(").Count = 1 And str22.Count = 3 Then
                x = str22(0)
                y = str22(1)
                z = str22(2)
                Points.Add(New VectorClass(x / Unit, y / Unit, z / Unit))


                X_min = Min(X_min, x / Unit)
                X_max = Max(X_max, x / Unit)
                Y_min = Min(Y_min, y / Unit)
                Y_max = Max(Y_max, y / Unit)
                Z_min = Min(Z_min, z / Unit)
                Z_max = Max(Z_max, z / Unit)

            ElseIf str22.Count = 6 And str22(0) = "3" Then
                If (str22(4) = "0" Or str22(5) = "0") Then
                    ctria_1 = CInt("&H" & str22(1))  '第一个点
                    ctria_2 = CInt("&H" & str22(2)) '第二个点
                    ctria_3 = CInt("&H" & str22(3)) '第三个点


                    Grids.Add(New GridClass(ctria_1, ctria_2, ctria_3))
                End If
                'If Not (str22(4) = "0" Or str22(5) = "0") Then
                '    Exit Do
                'End If



            End If

        Loop





        reader.Dispose()
        reader.Close()
        Dim mycolor As Color = System.Drawing.Color.FromArgb(255, 153, 18)

        If Not gtm Is Nothing Then
            AxGraph3dCtrl1.Document.Remove2(gtm)
        End If

        ' Dim gtm As MiniGraphicsLib.Geom3dTriMesh = AxGraph3dCtrl1.Document.AddNew(1, MiniGraphicsLib.Geom3dType.GT_TRIMESH)
        gtm = AxGraph3dCtrl1.Document.AddNew(1, MiniGraphicsLib.Geom3dType.GT_TRIMESH)
        gtm.ColorBorder = ColorTranslator.ToWin32(Color.Black)
        gtm.eFillMode = MiniGraphicsLib.PolygonFillMode.PGNFM_LINE
        gtm.fWidth = 0.2
        gtm.ColorFillItems(0) = ColorTranslator.ToWin32(mycolor) '本身就有一个颜色



        gtm.Faces.RemoveAll()
        gtm.Knots.RemoveAll()



        For i = 0 To Points.Count - 1
            gtm.Knots.Add(Points(i).x, Points(i).y, Points(i).z)
        Next



        For i = 1 To Grids.Count - 1
            gtm.Faces.Add(Grids(i).Num1, Grids(i).Num2, Grids(i).Num3)
        Next




        AxGraph3dCtrl1.CoordSystem.bShow = True '显示坐标轴
        AxGraph3dCtrl1.SetViewFitAll()

        'AxGraph3dCtrl1.SetView(MiniGraphicsLib.ViewType.VT_XP_YP) '图形在中间

        AxGraph3dCtrl1.SetView(MiniGraphicsLib.ViewType.VT_ISO) '从（-1,-1,-1）角度看过去
        'AxGraph3dCtrl1.SetViewCustom(1, 1, 1, 0, 0, 1, False)

    End Sub


    Public Sub clear()
        If Not gtm Is Nothing Then
            AxGraph3dCtrl1.Document.Remove2(gtm)
        End If
        gtm.Faces.RemoveAll()
        gtm.Knots.RemoveAll()

        AxGraph3dCtrl1.CoordSystem.bShow = True
        AxGraph3dCtrl1.SetViewFitAll()

        AxGraph3dCtrl1.SetView(MiniGraphicsLib.ViewType.VT_ISO)
    End Sub
End Class
