Imports System.IO
Imports System.Math
Imports System.Drawing

Public Class MeshDisControl

    Dim gtm As MiniGraphicsLib.Geom3dTriMesh
    Dim CoordSystem As MiniGraphicsLib.GlCoordSystem '定义坐标系
    Dim PolygonCirCle As MiniGraphicsLib.Geom3dPolygon '
    Dim group
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


        'Dim X_min1 As Double = (X_max + X_min) / 2 + (X_max - X_min) / 4
        'Dim Y_min1 As Double
        'Dim Z_min1 As Double
        'Dim X_max1 As Double
        'Dim Y_max1 As Double
        'Dim Z_max1 As Double


        Dim pointjudge(Points.Count - 1) As Boolean
        For i = 1 To Points.Count - 1
            If round(points(i).x, 3) = round(x_min, 3) Or round(points(i).x, 3) = round(x_max, 3) Then
                pointjudge(i) = True
            End If
            If round(points(i).y, 3) = round(y_min, 3) Or round(points(i).y, 3) = round(y_max, 3) Then
                pointjudge(i) = True
            End If
            If round(points(i).z, 3) = round(z_min, 3) Or round(points(i).z, 3) = round(z_max, 3) Then
                pointjudge(i) = True
            End If
        Next


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
            If pointjudge(Grids(i).Num1) Or pointjudge(Grids(i).Num2) Or pointjudge(Grids(i).Num3) Then '如果在物面边界上
                gtm.Faces.Add(Grids(i).Num1, Grids(i).Num2, Grids(i).Num3)
            End If
        Next



        gtm = AxGraph3dCtrl1.Document.AddNew(2, MiniGraphicsLib.Geom3dType.GT_TRIMESH)
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
            If pointjudge(Grids(i).Num1) Or pointjudge(Grids(i).Num2) Or pointjudge(Grids(i).Num3) Then '如果在物面边界上
            Else
                gtm.Faces.Add(Grids(i).Num1, Grids(i).Num2, Grids(i).Num3)
            End If
        Next


        Dim vvv As MiniGraphicsLib.Geom3dPoint = AxGraph3dCtrl1.Document.AddNew(5, MiniGraphicsLib.Geom3dType.GT_POINT)
        vvv.Vertex.x = 0
        vvv.Vertex.y = 0
        vvv.Vertex.z = 0
        vvv.fSize = 5
        Dim mycolor1 As Color = System.Drawing.Color.Red
        vvv.Color = ColorTranslator.ToWin32(mycolor1)


        AxGraph3dCtrl1.CoordSystem.bShow = True '显示坐标轴
        AxGraph3dCtrl1.SetViewFitAll()

        'AxGraph3dCtrl1.SetView(MiniGraphicsLib.ViewType.VT_XP_YP) '图形在中间

        AxGraph3dCtrl1.SetView(MiniGraphicsLib.ViewType.VT_ISO) '从（-1,-1,-1）角度看过去
        'AxGraph3dCtrl1.SetViewCustom(1, 1, 1, 0, 0, 1, False)

    End Sub
    'Public Sub clearRotor(ByVal n As Integer)
    '    For i = 3 To n + 2
    '        AxGraph3dCtrl1.Document.Remove(i, MiniGraphicsLib.Geom3dType.GT_TRIMESH) '这个控件的knots是从0开始的,Faces是从1开始的
    '    Next

    'End Sub
    Public Sub addRotor(ByVal rotors(,) As Double)


        Dim CoordSystem = New MiniGraphicsLib.GlCoordSystem '定义坐标系


        Try
            AxGraph3dCtrl1.Document.Remove(1, MiniGraphicsLib.Geom3dType.GT_GROUP) '这个控件的knots是从0开始的,Faces是从1开始的
            AxGraph3dCtrl1.Document.Remove(2, MiniGraphicsLib.Geom3dType.GT_GROUP) '这个控件的knots是从0开始的,Faces是从1开始的
            AxGraph3dCtrl1.Document.Remove(3, MiniGraphicsLib.Geom3dType.GT_GROUP) '这个控件的knots是从0开始的,Faces是从1开始的
            AxGraph3dCtrl1.Document.Remove(4, MiniGraphicsLib.Geom3dType.GT_GROUP) '这个控件的knots是从0开始的,Faces是从1开始的
        Catch ex As Exception
        End Try

        Dim group = CType(AxGraph3dCtrl1.Document.AddNew(1, MiniGraphicsLib.Geom3dType.GT_GROUP), MiniGraphicsLib.IGeom3dGroup2)
        Dim group1 = CType(AxGraph3dCtrl1.Document.AddNew(2, MiniGraphicsLib.Geom3dType.GT_GROUP), MiniGraphicsLib.IGeom3dGroup2)
        Dim group11 = CType(AxGraph3dCtrl1.Document.AddNew(3, MiniGraphicsLib.Geom3dType.GT_GROUP), MiniGraphicsLib.IGeom3dGroup2)
        Dim group12 = CType(AxGraph3dCtrl1.Document.AddNew(4, MiniGraphicsLib.Geom3dType.GT_GROUP), MiniGraphicsLib.IGeom3dGroup2)

        group1.ParentGroup = group
        group11.ParentGroup = group1
        group12.ParentGroup = group1

        Dim n As Integer = UBound(rotors, 1)


        group.bMultiInstanceValid = True
        group.nInstanceCount = n + 1


        '=====================================画上下两个桨面
        'If PolygonCirCle Is Nothing Then
        PolygonCirCle = CType(AxGraph3dCtrl1.Document.AddNew(0, MiniGraphicsLib.Geom3dType.GT_POLYGON), MiniGraphicsLib.Geom3dPolygon)
        PolygonCirCle.ParentGroup = group11
        PolygonCirCle.eFillMode = MiniGraphicsLib.PolygonFillMode.PGNFM_LINE
        PolygonCirCle.SetRegularPolygon(100, CoordSystem)
        'End If

        group11.bMultiInstanceValid = True
        group11.nInstanceCount = 2
        group11.SubCoordSystem(0).Scale(1, 1, 1)
        group11.SubCoordSystem(1).Scale(1, 1, 1)
        group11.SubCoordSystem(0).Oz = -1 / 2
        group11.SubCoordSystem(1).Oz = 1 / 2
        '=====================================画上下两个桨面


        '=====================================画桨面的四条线
        Dim line = New MiniGraphicsLib.Geom3dLine
        line.ParentGroup = group12


        line.Vertex0.SetData(1, 0, 1 / 2)
        line.Vertex1.SetData(1, 0, -1 / 2)

        group12.bMultiInstanceValid = True
        group12.nInstanceCount = 4

        group12.SubCoordSystem(0).Rotate(0 * Math.PI / 180, 0, 0, 1)
        group12.SubCoordSystem(1).Rotate(90 * Math.PI / 180, 0, 0, 1)
        group12.SubCoordSystem(2).Rotate(180 * Math.PI / 180, 0, 0, 1)
        group12.SubCoordSystem(3).Rotate(270 * Math.PI / 180, 0, 0, 1)
        '=====================================画桨面的四条线

        For i = 0 To n

            Dim x As Double = rotors(i, 0)
            Dim y As Double = rotors(i, 1)
            Dim z As Double = rotors(i, 2)
            Dim r As Double = rotors(i, 3) '旋翼半径
            Dim d As Double = rotors(i, 4) '旋翼厚度
            Dim v As Integer = rotors(i, 5) '中心轴是哪个轴
            Dim a As Double = rotors(i, 6) '绕（-X）轴旋转多少度




            group.SubCoordSystem(i).Scale(r, r, d)




            '=====================================先旋转到中心轴位置上
            Select Case v
                Case -1, 1
                    group.SubCoordSystem(i).Rotate(90 * Math.PI / 180, 0, 1, 0)
                Case -2, 2
                    group.SubCoordSystem(i).Rotate(90 * Math.PI / 180, 1, 0, 0)
                Case -3, 3

            End Select
            '=====================================先旋转到中心轴位置上


            group.SubCoordSystem(i).Rotate(a * Math.PI / 180, -1, 0, 0) '=====================================再绕着-X轴旋转

            group.SubCoordSystem(i).SetOrigin(x, y, z) '=====================================平移到中心点位置上


        Next


        AxGraph3dCtrl1.CoordSystem.bShow = True '显示坐标轴
        'AxGraph3dCtrl1.SetViewFitAll()
        AxGraph3dCtrl1.Document.UpdateAllViews()



    End Sub
    Public Sub showEdge(ByVal flag As Boolean)
        gtm = AxGraph3dCtrl1.Document.GetItem(1, MiniGraphicsLib.Geom3dType.GT_TRIMESH)

        If flag = True Then '显示物面
            gtm.eShowState = True
        Else
            gtm.eShowState = False
        End If

        AxGraph3dCtrl1.CoordSystem.bShow = True '显示坐标轴
        'AxGraph3dCtrl1.SetViewFitAll()
        AxGraph3dCtrl1.Document.UpdateAllViews()


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
