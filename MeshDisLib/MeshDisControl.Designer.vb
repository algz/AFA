<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MeshDisControl
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MeshDisControl))
        Me.AxGraph3dCtrl1 = New AxMiniGraphicsLib.AxGraph3dCtrl()
        CType(Me.AxGraph3dCtrl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxGraph3dCtrl1
        '
        Me.AxGraph3dCtrl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxGraph3dCtrl1.Enabled = True
        Me.AxGraph3dCtrl1.Location = New System.Drawing.Point(0, 0)
        Me.AxGraph3dCtrl1.Name = "AxGraph3dCtrl1"
        Me.AxGraph3dCtrl1.OcxState = CType(resources.GetObject("AxGraph3dCtrl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxGraph3dCtrl1.Size = New System.Drawing.Size(573, 454)
        Me.AxGraph3dCtrl1.TabIndex = 0
        '
        'MeshDisControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.AxGraph3dCtrl1)
        Me.Name = "MeshDisControl"
        Me.Size = New System.Drawing.Size(573, 454)
        CType(Me.AxGraph3dCtrl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AxGraph3dCtrl1 As AxMiniGraphicsLib.AxGraph3dCtrl

End Class
