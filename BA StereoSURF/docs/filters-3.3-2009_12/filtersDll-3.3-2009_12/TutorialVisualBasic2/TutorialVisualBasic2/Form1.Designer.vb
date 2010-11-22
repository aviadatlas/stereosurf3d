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
        Me.label2 = New System.Windows.Forms.Label
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.cmdLoad = New System.Windows.Forms.Button
        Me.label1 = New System.Windows.Forms.Label
        Me.txtImageCount = New System.Windows.Forms.TextBox
        Me.txtInfo = New System.Windows.Forms.ListBox
        Me.txtImageSize = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.pictureBox1 = New System.Windows.Forms.PictureBox
        Me.tabControl1 = New System.Windows.Forms.TabControl
        Me.tpSobel = New System.Windows.Forms.TabPage
        Me.cmdSobel = New System.Windows.Forms.Button
        Me.tpRotation = New System.Windows.Forms.TabPage
        Me.txtRotationAngle = New System.Windows.Forms.TextBox
        Me.label3 = New System.Windows.Forms.Label
        Me.cmdRotation = New System.Windows.Forms.Button
        Me.tpStackCreator = New System.Windows.Forms.TabPage
        Me.cmdStack = New System.Windows.Forms.Button
        Me.tpBlobRepositioning2 = New System.Windows.Forms.TabPage
        Me.cmdBlobRepositioning2 = New System.Windows.Forms.Button
        Me.tpBlobExplorer = New System.Windows.Forms.TabPage
        Me.cmdBlobExplorer = New System.Windows.Forms.Button
        Me.tabPage1 = New System.Windows.Forms.TabPage
        Me.bROI = New System.Windows.Forms.Button
        Me.tabPage2 = New System.Windows.Forms.TabPage
        Me.bAnalyseImage = New System.Windows.Forms.Button
        Me.tabPage3 = New System.Windows.Forms.TabPage
        Me.bCreateDrawImage = New System.Windows.Forms.Button
        Me.tabPage4 = New System.Windows.Forms.TabPage
        Me.bConvolutionPersonal = New System.Windows.Forms.Button
        Me.bConvolution = New System.Windows.Forms.Button
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl1.SuspendLayout()
        Me.tpSobel.SuspendLayout()
        Me.tpRotation.SuspendLayout()
        Me.tpStackCreator.SuspendLayout()
        Me.tpBlobRepositioning2.SuspendLayout()
        Me.tpBlobExplorer.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.tabPage2.SuspendLayout()
        Me.tabPage3.SuspendLayout()
        Me.tabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(12, 9)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(495, 15)
        Me.label2.TabIndex = 19
        Me.label2.Text = "Project Filters at http://filters.sourceforge.net ,  contact: filters@edurand.com" & _
            ""
        '
        'txtFileName
        '
        Me.txtFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFileName.Location = New System.Drawing.Point(59, 40)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(713, 20)
        Me.txtFileName.TabIndex = 21
        Me.txtFileName.Text = "C:\DEV\FiltersTutorial\Bin\lenna_color.bmp"
        '
        'cmdLoad
        '
        Me.cmdLoad.Location = New System.Drawing.Point(14, 38)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(38, 23)
        Me.cmdLoad.TabIndex = 20
        Me.cmdLoad.Text = "load"
        Me.cmdLoad.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 73)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(65, 13)
        Me.label1.TabIndex = 23
        Me.label1.Text = "image count"
        '
        'txtImageCount
        '
        Me.txtImageCount.Enabled = False
        Me.txtImageCount.Location = New System.Drawing.Point(83, 70)
        Me.txtImageCount.Name = "txtImageCount"
        Me.txtImageCount.Size = New System.Drawing.Size(53, 20)
        Me.txtImageCount.TabIndex = 22
        '
        'txtInfo
        '
        Me.txtInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInfo.FormattingEnabled = True
        Me.txtInfo.Location = New System.Drawing.Point(459, 198)
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.Size = New System.Drawing.Size(298, 368)
        Me.txtInfo.TabIndex = 27
        '
        'txtImageSize
        '
        Me.txtImageSize.Enabled = False
        Me.txtImageSize.Location = New System.Drawing.Point(76, 172)
        Me.txtImageSize.Name = "txtImageSize"
        Me.txtImageSize.Size = New System.Drawing.Size(192, 20)
        Me.txtImageSize.TabIndex = 26
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(13, 172)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(56, 13)
        Me.label4.TabIndex = 25
        Me.label4.Text = "image size"
        '
        'pictureBox1
        '
        Me.pictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pictureBox1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.pictureBox1.Location = New System.Drawing.Point(12, 198)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(428, 378)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox1.TabIndex = 24
        Me.pictureBox1.TabStop = False
        '
        'tabControl1
        '
        Me.tabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControl1.Controls.Add(Me.tpSobel)
        Me.tabControl1.Controls.Add(Me.tpRotation)
        Me.tabControl1.Controls.Add(Me.tpStackCreator)
        Me.tabControl1.Controls.Add(Me.tpBlobRepositioning2)
        Me.tabControl1.Controls.Add(Me.tpBlobExplorer)
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Controls.Add(Me.tabPage3)
        Me.tabControl1.Controls.Add(Me.tabPage4)
        Me.tabControl1.Location = New System.Drawing.Point(12, 96)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(749, 62)
        Me.tabControl1.TabIndex = 28
        '
        'tpSobel
        '
        Me.tpSobel.Controls.Add(Me.cmdSobel)
        Me.tpSobel.Location = New System.Drawing.Point(4, 22)
        Me.tpSobel.Name = "tpSobel"
        Me.tpSobel.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSobel.Size = New System.Drawing.Size(741, 36)
        Me.tpSobel.TabIndex = 0
        Me.tpSobel.Text = "Sobel"
        Me.tpSobel.UseVisualStyleBackColor = True
        '
        'cmdSobel
        '
        Me.cmdSobel.Location = New System.Drawing.Point(6, 6)
        Me.cmdSobel.Name = "cmdSobel"
        Me.cmdSobel.Size = New System.Drawing.Size(55, 23)
        Me.cmdSobel.TabIndex = 8
        Me.cmdSobel.Text = "Sobel"
        Me.cmdSobel.UseVisualStyleBackColor = True
        '
        'tpRotation
        '
        Me.tpRotation.Controls.Add(Me.txtRotationAngle)
        Me.tpRotation.Controls.Add(Me.label3)
        Me.tpRotation.Controls.Add(Me.cmdRotation)
        Me.tpRotation.Location = New System.Drawing.Point(4, 22)
        Me.tpRotation.Name = "tpRotation"
        Me.tpRotation.Padding = New System.Windows.Forms.Padding(3)
        Me.tpRotation.Size = New System.Drawing.Size(741, 41)
        Me.tpRotation.TabIndex = 1
        Me.tpRotation.Text = "Rotation"
        Me.tpRotation.UseVisualStyleBackColor = True
        '
        'txtRotationAngle
        '
        Me.txtRotationAngle.Location = New System.Drawing.Point(133, 7)
        Me.txtRotationAngle.Name = "txtRotationAngle"
        Me.txtRotationAngle.Size = New System.Drawing.Size(45, 20)
        Me.txtRotationAngle.TabIndex = 11
        Me.txtRotationAngle.Text = "66"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(93, 7)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(33, 13)
        Me.label3.TabIndex = 10
        Me.label3.Text = "angle"
        '
        'cmdRotation
        '
        Me.cmdRotation.Location = New System.Drawing.Point(6, 6)
        Me.cmdRotation.Name = "cmdRotation"
        Me.cmdRotation.Size = New System.Drawing.Size(55, 23)
        Me.cmdRotation.TabIndex = 9
        Me.cmdRotation.Text = "Rotation"
        Me.cmdRotation.UseVisualStyleBackColor = True
        '
        'tpStackCreator
        '
        Me.tpStackCreator.Controls.Add(Me.cmdStack)
        Me.tpStackCreator.Location = New System.Drawing.Point(4, 22)
        Me.tpStackCreator.Name = "tpStackCreator"
        Me.tpStackCreator.Padding = New System.Windows.Forms.Padding(3)
        Me.tpStackCreator.Size = New System.Drawing.Size(741, 41)
        Me.tpStackCreator.TabIndex = 2
        Me.tpStackCreator.Text = "StackCreator"
        Me.tpStackCreator.UseVisualStyleBackColor = True
        '
        'cmdStack
        '
        Me.cmdStack.Location = New System.Drawing.Point(6, 6)
        Me.cmdStack.Name = "cmdStack"
        Me.cmdStack.Size = New System.Drawing.Size(55, 23)
        Me.cmdStack.TabIndex = 10
        Me.cmdStack.Text = "Stack"
        Me.cmdStack.UseVisualStyleBackColor = True
        '
        'tpBlobRepositioning2
        '
        Me.tpBlobRepositioning2.Controls.Add(Me.cmdBlobRepositioning2)
        Me.tpBlobRepositioning2.Location = New System.Drawing.Point(4, 22)
        Me.tpBlobRepositioning2.Name = "tpBlobRepositioning2"
        Me.tpBlobRepositioning2.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBlobRepositioning2.Size = New System.Drawing.Size(741, 41)
        Me.tpBlobRepositioning2.TabIndex = 3
        Me.tpBlobRepositioning2.Text = "BlobRepositioning2"
        Me.tpBlobRepositioning2.UseVisualStyleBackColor = True
        '
        'cmdBlobRepositioning2
        '
        Me.cmdBlobRepositioning2.Location = New System.Drawing.Point(6, 7)
        Me.cmdBlobRepositioning2.Name = "cmdBlobRepositioning2"
        Me.cmdBlobRepositioning2.Size = New System.Drawing.Size(115, 23)
        Me.cmdBlobRepositioning2.TabIndex = 11
        Me.cmdBlobRepositioning2.Text = "BlobRepositioning2"
        Me.cmdBlobRepositioning2.UseVisualStyleBackColor = True
        '
        'tpBlobExplorer
        '
        Me.tpBlobExplorer.Controls.Add(Me.cmdBlobExplorer)
        Me.tpBlobExplorer.Location = New System.Drawing.Point(4, 22)
        Me.tpBlobExplorer.Name = "tpBlobExplorer"
        Me.tpBlobExplorer.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBlobExplorer.Size = New System.Drawing.Size(741, 36)
        Me.tpBlobExplorer.TabIndex = 4
        Me.tpBlobExplorer.Text = "BlobExplorer"
        Me.tpBlobExplorer.UseVisualStyleBackColor = True
        '
        'cmdBlobExplorer
        '
        Me.cmdBlobExplorer.Location = New System.Drawing.Point(6, 6)
        Me.cmdBlobExplorer.Name = "cmdBlobExplorer"
        Me.cmdBlobExplorer.Size = New System.Drawing.Size(55, 23)
        Me.cmdBlobExplorer.TabIndex = 9
        Me.cmdBlobExplorer.Text = "Blob"
        Me.cmdBlobExplorer.UseVisualStyleBackColor = True
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.bROI)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage1.Size = New System.Drawing.Size(741, 41)
        Me.tabPage1.TabIndex = 5
        Me.tabPage1.Text = "RegionOfInterest "
        Me.tabPage1.UseVisualStyleBackColor = True
        '
        'bROI
        '
        Me.bROI.Location = New System.Drawing.Point(6, 6)
        Me.bROI.Name = "bROI"
        Me.bROI.Size = New System.Drawing.Size(185, 23)
        Me.bROI.TabIndex = 9
        Me.bROI.Text = "test ROI on filter [filterEnvelope]"
        Me.bROI.UseVisualStyleBackColor = True
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.bAnalyseImage)
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage2.Size = New System.Drawing.Size(741, 41)
        Me.tabPage2.TabIndex = 6
        Me.tabPage2.Text = "analyse image"
        Me.tabPage2.UseVisualStyleBackColor = True
        '
        'bAnalyseImage
        '
        Me.bAnalyseImage.Location = New System.Drawing.Point(6, 7)
        Me.bAnalyseImage.Name = "bAnalyseImage"
        Me.bAnalyseImage.Size = New System.Drawing.Size(246, 23)
        Me.bAnalyseImage.TabIndex = 10
        Me.bAnalyseImage.Text = "search the intensity min and max of the image"
        Me.bAnalyseImage.UseVisualStyleBackColor = True
        '
        'tabPage3
        '
        Me.tabPage3.Controls.Add(Me.bCreateDrawImage)
        Me.tabPage3.Location = New System.Drawing.Point(4, 22)
        Me.tabPage3.Name = "tabPage3"
        Me.tabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage3.Size = New System.Drawing.Size(741, 41)
        Me.tabPage3.TabIndex = 7
        Me.tabPage3.Text = "create image"
        Me.tabPage3.UseVisualStyleBackColor = True
        '
        'bCreateDrawImage
        '
        Me.bCreateDrawImage.Location = New System.Drawing.Point(6, 6)
        Me.bCreateDrawImage.Name = "bCreateDrawImage"
        Me.bCreateDrawImage.Size = New System.Drawing.Size(142, 23)
        Me.bCreateDrawImage.TabIndex = 9
        Me.bCreateDrawImage.Text = "create/draw an image"
        Me.bCreateDrawImage.UseVisualStyleBackColor = True
        '
        'tabPage4
        '
        Me.tabPage4.Controls.Add(Me.bConvolutionPersonal)
        Me.tabPage4.Controls.Add(Me.bConvolution)
        Me.tabPage4.Location = New System.Drawing.Point(4, 22)
        Me.tabPage4.Name = "tabPage4"
        Me.tabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage4.Size = New System.Drawing.Size(741, 41)
        Me.tabPage4.TabIndex = 8
        Me.tabPage4.Text = "Convolution"
        Me.tabPage4.UseVisualStyleBackColor = True
        '
        'bConvolutionPersonal
        '
        Me.bConvolutionPersonal.Location = New System.Drawing.Point(86, 6)
        Me.bConvolutionPersonal.Name = "bConvolutionPersonal"
        Me.bConvolutionPersonal.Size = New System.Drawing.Size(74, 23)
        Me.bConvolutionPersonal.TabIndex = 11
        Me.bConvolutionPersonal.Text = "personal"
        Me.bConvolutionPersonal.UseVisualStyleBackColor = True
        '
        'bConvolution
        '
        Me.bConvolution.Location = New System.Drawing.Point(6, 6)
        Me.bConvolution.Name = "bConvolution"
        Me.bConvolution.Size = New System.Drawing.Size(74, 23)
        Me.bConvolution.TabIndex = 10
        Me.bConvolution.Text = "Laplace"
        Me.bConvolution.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 588)
        Me.Controls.Add(Me.tabControl1)
        Me.Controls.Add(Me.txtInfo)
        Me.Controls.Add(Me.txtImageSize)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txtImageCount)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.cmdLoad)
        Me.Controls.Add(Me.label2)
        Me.Name = "Form1"
        Me.Text = "Filters - Tutorial VisualBasic 2"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl1.ResumeLayout(False)
        Me.tpSobel.ResumeLayout(False)
        Me.tpRotation.ResumeLayout(False)
        Me.tpRotation.PerformLayout()
        Me.tpStackCreator.ResumeLayout(False)
        Me.tpBlobRepositioning2.ResumeLayout(False)
        Me.tpBlobExplorer.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        Me.tabPage2.ResumeLayout(False)
        Me.tabPage3.ResumeLayout(False)
        Me.tabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents txtFileName As System.Windows.Forms.TextBox
    Private WithEvents cmdLoad As System.Windows.Forms.Button
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents txtImageCount As System.Windows.Forms.TextBox
    Private WithEvents txtInfo As System.Windows.Forms.ListBox
    Private WithEvents txtImageSize As System.Windows.Forms.TextBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents tabControl1 As System.Windows.Forms.TabControl
    Private WithEvents tpSobel As System.Windows.Forms.TabPage
    Private WithEvents cmdSobel As System.Windows.Forms.Button
    Private WithEvents tpRotation As System.Windows.Forms.TabPage
    Private WithEvents txtRotationAngle As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents cmdRotation As System.Windows.Forms.Button
    Private WithEvents tpStackCreator As System.Windows.Forms.TabPage
    Private WithEvents cmdStack As System.Windows.Forms.Button
    Private WithEvents tpBlobRepositioning2 As System.Windows.Forms.TabPage
    Private WithEvents cmdBlobRepositioning2 As System.Windows.Forms.Button
    Private WithEvents tpBlobExplorer As System.Windows.Forms.TabPage
    Private WithEvents cmdBlobExplorer As System.Windows.Forms.Button
    Private WithEvents tabPage1 As System.Windows.Forms.TabPage
    Private WithEvents bROI As System.Windows.Forms.Button
    Private WithEvents tabPage2 As System.Windows.Forms.TabPage
    Private WithEvents bAnalyseImage As System.Windows.Forms.Button
    Private WithEvents tabPage3 As System.Windows.Forms.TabPage
    Private WithEvents bCreateDrawImage As System.Windows.Forms.Button
    Private WithEvents tabPage4 As System.Windows.Forms.TabPage
    Private WithEvents bConvolutionPersonal As System.Windows.Forms.Button
    Private WithEvents bConvolution As System.Windows.Forms.Button

End Class
