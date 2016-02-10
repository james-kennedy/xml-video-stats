<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblLocInfo = New System.Windows.Forms.Label()
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.txtAnnotationsPath = New System.Windows.Forms.TextBox()
        Me.lblFolder = New System.Windows.Forms.Label()
        Me.cmdBatchCorrels = New System.Windows.Forms.Button()
        Me.cmdBatchStats = New System.Windows.Forms.Button()
        Me.cmdCorrelations = New System.Windows.Forms.Button()
        Me.nudVideoLength = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.nudCoders = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstvResults = New System.Windows.Forms.ListView()
        Me.colhTrack = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colhAmount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colhCount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colhAverage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colhTimeMin = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colhCountMin = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmdCopyClip = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdCoder = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.nudVideoLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCoders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblLocInfo)
        Me.GroupBox1.Controls.Add(Me.cmdBrowse)
        Me.GroupBox1.Controls.Add(Me.txtAnnotationsPath)
        Me.GroupBox1.Controls.Add(Me.lblFolder)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(499, 86)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Annotation Location"
        '
        'lblLocInfo
        '
        Me.lblLocInfo.AutoSize = True
        Me.lblLocInfo.Location = New System.Drawing.Point(12, 25)
        Me.lblLocInfo.Name = "lblLocInfo"
        Me.lblLocInfo.Size = New System.Drawing.Size(442, 13)
        Me.lblLocInfo.TabIndex = 3
        Me.lblLocInfo.Text = "Please enter the root folder for the annotations. Place all annotations in the ro" & _
    "ot of this folder."
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(406, 51)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(75, 23)
        Me.cmdBrowse.TabIndex = 1
        Me.cmdBrowse.Text = "Browse"
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'txtAnnotationsPath
        '
        Me.txtAnnotationsPath.Location = New System.Drawing.Point(116, 53)
        Me.txtAnnotationsPath.Name = "txtAnnotationsPath"
        Me.txtAnnotationsPath.Size = New System.Drawing.Size(284, 20)
        Me.txtAnnotationsPath.TabIndex = 0
        '
        'lblFolder
        '
        Me.lblFolder.AutoSize = True
        Me.lblFolder.Location = New System.Drawing.Point(12, 56)
        Me.lblFolder.Name = "lblFolder"
        Me.lblFolder.Size = New System.Drawing.Size(98, 13)
        Me.lblFolder.TabIndex = 0
        Me.lblFolder.Text = "Annotations Folder:"
        '
        'cmdBatchCorrels
        '
        Me.cmdBatchCorrels.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBatchCorrels.Location = New System.Drawing.Point(6, 51)
        Me.cmdBatchCorrels.Name = "cmdBatchCorrels"
        Me.cmdBatchCorrels.Size = New System.Drawing.Size(170, 26)
        Me.cmdBatchCorrels.TabIndex = 10
        Me.cmdBatchCorrels.Text = "Batch Correlations"
        Me.cmdBatchCorrels.UseVisualStyleBackColor = True
        '
        'cmdBatchStats
        '
        Me.cmdBatchStats.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBatchStats.Location = New System.Drawing.Point(6, 18)
        Me.cmdBatchStats.Name = "cmdBatchStats"
        Me.cmdBatchStats.Size = New System.Drawing.Size(170, 27)
        Me.cmdBatchStats.TabIndex = 9
        Me.cmdBatchStats.Text = "Batch Stats"
        Me.cmdBatchStats.UseVisualStyleBackColor = True
        '
        'cmdCorrelations
        '
        Me.cmdCorrelations.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCorrelations.Location = New System.Drawing.Point(671, 21)
        Me.cmdCorrelations.Name = "cmdCorrelations"
        Me.cmdCorrelations.Size = New System.Drawing.Size(169, 26)
        Me.cmdCorrelations.TabIndex = 8
        Me.cmdCorrelations.Text = "Popul. Correlations"
        Me.cmdCorrelations.UseVisualStyleBackColor = True
        '
        'nudVideoLength
        '
        Me.nudVideoLength.Location = New System.Drawing.Point(361, 26)
        Me.nudVideoLength.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.nudVideoLength.Name = "nudVideoLength"
        Me.nudVideoLength.Size = New System.Drawing.Size(82, 20)
        Me.nudVideoLength.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(233, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Total video length (secs):"
        '
        'nudCoders
        '
        Me.nudCoders.Location = New System.Drawing.Point(124, 26)
        Me.nudCoders.Name = "nudCoders"
        Me.nudCoders.Size = New System.Drawing.Size(54, 20)
        Me.nudCoders.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Number of coders:"
        '
        'cmdStart
        '
        Me.cmdStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStart.Location = New System.Drawing.Point(496, 21)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(169, 26)
        Me.cmdStart.TabIndex = 4
        Me.cmdStart.Text = "Population Stats"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(783, 463)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 6
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.nudVideoLength)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.nudCoders)
        Me.GroupBox2.Controls.Add(Me.lstvResults)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cmdCorrelations)
        Me.GroupBox2.Controls.Add(Me.cmdStart)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 104)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(846, 353)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Population Results"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(20, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(806, 35)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'lstvResults
        '
        Me.lstvResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colhTrack, Me.colhAmount, Me.colhCount, Me.colhAverage, Me.colhTimeMin, Me.colhCountMin})
        Me.lstvResults.FullRowSelect = True
        Me.lstvResults.GridLines = True
        Me.lstvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstvResults.Location = New System.Drawing.Point(6, 97)
        Me.lstvResults.Name = "lstvResults"
        Me.lstvResults.Size = New System.Drawing.Size(834, 250)
        Me.lstvResults.TabIndex = 5
        Me.lstvResults.UseCompatibleStateImageBehavior = False
        Me.lstvResults.View = System.Windows.Forms.View.Details
        '
        'colhTrack
        '
        Me.colhTrack.Text = "Track"
        Me.colhTrack.Width = 350
        '
        'colhAmount
        '
        Me.colhAmount.Text = "Time Elapsed (s)"
        Me.colhAmount.Width = 110
        '
        'colhCount
        '
        Me.colhCount.Text = "Count"
        Me.colhCount.Width = 50
        '
        'colhAverage
        '
        Me.colhAverage.Text = "Average Time (s)"
        Me.colhAverage.Width = 116
        '
        'colhTimeMin
        '
        Me.colhTimeMin.Text = "Time /min"
        Me.colhTimeMin.Width = 87
        '
        'colhCountMin
        '
        Me.colhCountMin.Text = "Count /min"
        Me.colhCountMin.Width = 66
        '
        'cmdCopyClip
        '
        Me.cmdCopyClip.Location = New System.Drawing.Point(12, 463)
        Me.cmdCopyClip.Name = "cmdCopyClip"
        Me.cmdCopyClip.Size = New System.Drawing.Size(103, 23)
        Me.cmdCopyClip.TabIndex = 7
        Me.cmdCopyClip.Text = "Copy to Clipboard"
        Me.cmdCopyClip.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmdBatchCorrels)
        Me.GroupBox3.Controls.Add(Me.cmdBatchStats)
        Me.GroupBox3.Location = New System.Drawing.Point(517, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(181, 86)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Export Individuals to Excel"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.cmdCoder)
        Me.GroupBox4.Location = New System.Drawing.Point(704, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(154, 86)
        Me.GroupBox4.TabIndex = 9
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Coder Agreement"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Put pairs in folders"
        '
        'cmdCoder
        '
        Me.cmdCoder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCoder.Location = New System.Drawing.Point(6, 18)
        Me.cmdCoder.Name = "cmdCoder"
        Me.cmdCoder.Size = New System.Drawing.Size(142, 27)
        Me.cmdCoder.TabIndex = 0
        Me.cmdCoder.Text = "Coder Agreement"
        Me.cmdCoder.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(870, 496)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cmdCopyClip)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "XML Annotation Reader"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.nudVideoLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCoders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblLocInfo As System.Windows.Forms.Label
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents txtAnnotationsPath As System.Windows.Forms.TextBox
    Friend WithEvents lblFolder As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstvResults As System.Windows.Forms.ListView
    Friend WithEvents colhTrack As System.Windows.Forms.ColumnHeader
    Friend WithEvents colhAmount As System.Windows.Forms.ColumnHeader
    Friend WithEvents colhCount As System.Windows.Forms.ColumnHeader
    Friend WithEvents colhAverage As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents nudVideoLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nudCoders As System.Windows.Forms.NumericUpDown
    Friend WithEvents colhTimeMin As System.Windows.Forms.ColumnHeader
    Friend WithEvents colhCountMin As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdCorrelations As System.Windows.Forms.Button
    Friend WithEvents cmdCopyClip As System.Windows.Forms.Button
    Friend WithEvents cmdBatchCorrels As System.Windows.Forms.Button
    Friend WithEvents cmdBatchStats As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdCoder As System.Windows.Forms.Button

End Class
