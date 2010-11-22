<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.label3 = New System.Windows.Forms.Label
        Me.lstOutputs = New System.Windows.Forms.ListBox
        Me.label2 = New System.Windows.Forms.Label
        Me.lstParameters = New System.Windows.Forms.ListBox
        Me.cbFiltersName = New System.Windows.Forms.ComboBox
        Me.txtVersion = New System.Windows.Forms.TextBox
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(12, 202)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(48, 13)
        Me.label3.TabIndex = 13
        Me.label3.Text = "outputs :"
        '
        'lstOutputs
        '
        Me.lstOutputs.FormattingEnabled = True
        Me.lstOutputs.Location = New System.Drawing.Point(12, 215)
        Me.lstOutputs.Name = "lstOutputs"
        Me.lstOutputs.Size = New System.Drawing.Size(465, 95)
        Me.lstOutputs.TabIndex = 12
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(12, 81)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(65, 13)
        Me.label2.TabIndex = 11
        Me.label2.Text = "parameters :"
        '
        'lstParameters
        '
        Me.lstParameters.FormattingEnabled = True
        Me.lstParameters.Location = New System.Drawing.Point(12, 94)
        Me.lstParameters.Name = "lstParameters"
        Me.lstParameters.Size = New System.Drawing.Size(465, 95)
        Me.lstParameters.TabIndex = 10
        '
        'cbFiltersName
        '
        Me.cbFiltersName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFiltersName.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbFiltersName.FormattingEnabled = True
        Me.cbFiltersName.Location = New System.Drawing.Point(15, 53)
        Me.cbFiltersName.Name = "cbFiltersName"
        Me.cbFiltersName.Size = New System.Drawing.Size(465, 21)
        Me.cbFiltersName.TabIndex = 9
        '
        'txtVersion
        '
        Me.txtVersion.Enabled = False
        Me.txtVersion.Location = New System.Drawing.Point(122, 15)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(134, 20)
        Me.txtVersion.TabIndex = 8
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 18)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(104, 13)
        Me.label1.TabIndex = 7
        Me.label1.Text = "Filters DLL Version ="
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 324)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.lstOutputs)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.lstParameters)
        Me.Controls.Add(Me.cbFiltersName)
        Me.Controls.Add(Me.txtVersion)
        Me.Controls.Add(Me.label1)
        Me.Name = "Form1"
        Me.Text = "Filters - Tutorial Visual Basic 1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents lstOutputs As System.Windows.Forms.ListBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents lstParameters As System.Windows.Forms.ListBox
    Private WithEvents cbFiltersName As System.Windows.Forms.ComboBox
    Private WithEvents txtVersion As System.Windows.Forms.TextBox
    Private WithEvents label1 As System.Windows.Forms.Label

End Class
