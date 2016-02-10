<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCorrelOptions
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
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboEndTrack = New System.Windows.Forms.ComboBox()
        Me.chkSummary = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.radNormEvents = New System.Windows.Forms.RadioButton()
        Me.radInstances = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.radThird = New System.Windows.Forms.RadioButton()
        Me.radSecond = New System.Windows.Forms.RadioButton()
        Me.radFirst = New System.Windows.Forms.RadioButton()
        Me.radFull = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboTracks = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.nudWinEndPost = New System.Windows.Forms.NumericUpDown()
        Me.nudWinStartPost = New System.Windows.Forms.NumericUpDown()
        Me.nudWinEndPre = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nudWinStartPre = New System.Windows.Forms.NumericUpDown()
        Me.chkWindowOnly = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.nudWinEndPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWinStartPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWinEndPre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWinStartPre, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdStart
        '
        Me.cmdStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStart.Location = New System.Drawing.Point(346, 202)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(75, 23)
        Me.cmdStart.TabIndex = 0
        Me.cmdStart.Text = "Go!"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cboEndTrack)
        Me.GroupBox1.Controls.Add(Me.chkSummary)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.radNormEvents)
        Me.GroupBox1.Controls.Add(Me.radInstances)
        Me.GroupBox1.Controls.Add(Me.cmdStart)
        Me.GroupBox1.Location = New System.Drawing.Point(457, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 240)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General Options:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 85)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(322, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Track which always goes to end of coding (to calc per min values):"
        '
        'cboEndTrack
        '
        Me.cboEndTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEndTrack.Enabled = False
        Me.cboEndTrack.FormattingEnabled = True
        Me.cboEndTrack.Location = New System.Drawing.Point(12, 101)
        Me.cboEndTrack.Name = "cboEndTrack"
        Me.cboEndTrack.Size = New System.Drawing.Size(409, 21)
        Me.cboEndTrack.TabIndex = 18
        '
        'chkSummary
        '
        Me.chkSummary.AutoSize = True
        Me.chkSummary.Location = New System.Drawing.Point(13, 153)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(134, 17)
        Me.chkSummary.TabIndex = 17
        Me.chkSummary.Text = "Include summary sheet"
        Me.chkSummary.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(36, 207)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(291, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Results will open in an excel spreadsheet, please be patient!"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(183, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 17)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "(count of the overlaps)"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(183, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(170, 30)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "(overlaps / min)"
        '
        'radNormEvents
        '
        Me.radNormEvents.AutoSize = True
        Me.radNormEvents.Location = New System.Drawing.Point(13, 53)
        Me.radNormEvents.Name = "radNormEvents"
        Me.radNormEvents.Size = New System.Drawing.Size(148, 17)
        Me.radNormEvents.TabIndex = 5
        Me.radNormEvents.Text = "Normalise to no. of events"
        Me.radNormEvents.UseVisualStyleBackColor = True
        '
        'radInstances
        '
        Me.radInstances.AutoSize = True
        Me.radInstances.Checked = True
        Me.radInstances.Location = New System.Drawing.Point(13, 26)
        Me.radInstances.Name = "radInstances"
        Me.radInstances.Size = New System.Drawing.Size(102, 17)
        Me.radInstances.TabIndex = 4
        Me.radInstances.TabStop = True
        Me.radInstances.Text = "Count Instances"
        Me.radInstances.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.radThird)
        Me.GroupBox2.Controls.Add(Me.radSecond)
        Me.GroupBox2.Controls.Add(Me.radFirst)
        Me.GroupBox2.Controls.Add(Me.radFull)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.cboTracks)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(438, 106)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Restrict to Tracks"
        '
        'radThird
        '
        Me.radThird.AutoSize = True
        Me.radThird.Location = New System.Drawing.Point(335, 75)
        Me.radThird.Name = "radThird"
        Me.radThird.Size = New System.Drawing.Size(74, 17)
        Me.radThird.TabIndex = 18
        Me.radThird.Text = "Final Third"
        Me.radThird.UseVisualStyleBackColor = True
        '
        'radSecond
        '
        Me.radSecond.AutoSize = True
        Me.radSecond.Location = New System.Drawing.Point(222, 75)
        Me.radSecond.Name = "radSecond"
        Me.radSecond.Size = New System.Drawing.Size(83, 17)
        Me.radSecond.TabIndex = 17
        Me.radSecond.Text = "Middle Third"
        Me.radSecond.UseVisualStyleBackColor = True
        '
        'radFirst
        '
        Me.radFirst.AutoSize = True
        Me.radFirst.Location = New System.Drawing.Point(115, 75)
        Me.radFirst.Name = "radFirst"
        Me.radFirst.Size = New System.Drawing.Size(71, 17)
        Me.radFirst.TabIndex = 16
        Me.radFirst.Text = "First Third"
        Me.radFirst.UseVisualStyleBackColor = True
        '
        'radFull
        '
        Me.radFull.AutoSize = True
        Me.radFull.Checked = True
        Me.radFull.Location = New System.Drawing.Point(19, 75)
        Me.radFull.Name = "radFull"
        Me.radFull.Size = New System.Drawing.Size(72, 17)
        Me.radFull.TabIndex = 15
        Me.radFull.TabStop = True
        Me.radFull.Text = "Full Track"
        Me.radFull.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(245, 13)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Only include if overlapping with the following track:"
        '
        'cboTracks
        '
        Me.cboTracks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTracks.FormattingEnabled = True
        Me.cboTracks.Location = New System.Drawing.Point(16, 48)
        Me.cboTracks.Name = "cboTracks"
        Me.cboTracks.Size = New System.Drawing.Size(409, 21)
        Me.cboTracks.TabIndex = 11
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.nudWinEndPost)
        Me.GroupBox3.Controls.Add(Me.nudWinStartPost)
        Me.GroupBox3.Controls.Add(Me.nudWinEndPre)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.nudWinStartPre)
        Me.GroupBox3.Controls.Add(Me.chkWindowOnly)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 124)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(438, 128)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Add a Window"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(154, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(244, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "(event must start within the window to be counted)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(252, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "After edge"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(150, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Before edge"
        '
        'nudWinEndPost
        '
        Me.nudWinEndPost.DecimalPlaces = 2
        Me.nudWinEndPost.Location = New System.Drawing.Point(255, 98)
        Me.nudWinEndPost.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudWinEndPost.Name = "nudWinEndPost"
        Me.nudWinEndPost.Size = New System.Drawing.Size(77, 20)
        Me.nudWinEndPost.TabIndex = 20
        '
        'nudWinStartPost
        '
        Me.nudWinStartPost.DecimalPlaces = 2
        Me.nudWinStartPost.Location = New System.Drawing.Point(255, 72)
        Me.nudWinStartPost.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudWinStartPost.Name = "nudWinStartPost"
        Me.nudWinStartPost.Size = New System.Drawing.Size(77, 20)
        Me.nudWinStartPost.TabIndex = 19
        '
        'nudWinEndPre
        '
        Me.nudWinEndPre.DecimalPlaces = 2
        Me.nudWinEndPre.Location = New System.Drawing.Point(153, 98)
        Me.nudWinEndPre.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudWinEndPre.Name = "nudWinEndPre"
        Me.nudWinEndPre.Size = New System.Drawing.Size(77, 20)
        Me.nudWinEndPre.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Window at end (secs):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Window at start (secs):"
        '
        'nudWinStartPre
        '
        Me.nudWinStartPre.DecimalPlaces = 2
        Me.nudWinStartPre.Location = New System.Drawing.Point(153, 72)
        Me.nudWinStartPre.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nudWinStartPre.Name = "nudWinStartPre"
        Me.nudWinStartPre.Size = New System.Drawing.Size(77, 20)
        Me.nudWinStartPre.TabIndex = 15
        '
        'chkWindowOnly
        '
        Me.chkWindowOnly.AutoSize = True
        Me.chkWindowOnly.Location = New System.Drawing.Point(16, 28)
        Me.chkWindowOnly.Name = "chkWindowOnly"
        Me.chkWindowOnly.Size = New System.Drawing.Size(141, 17)
        Me.chkWindowOnly.TabIndex = 14
        Me.chkWindowOnly.Text = "Window segments only?"
        Me.chkWindowOnly.UseVisualStyleBackColor = True
        '
        'frmCorrelOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(903, 263)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCorrelOptions"
        Me.Text = "Correlation Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.nudWinEndPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWinStartPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWinEndPre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWinStartPre, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents radNormEvents As System.Windows.Forms.RadioButton
    Friend WithEvents radInstances As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkSummary As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboEndTrack As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents radThird As System.Windows.Forms.RadioButton
    Friend WithEvents radSecond As System.Windows.Forms.RadioButton
    Friend WithEvents radFirst As System.Windows.Forms.RadioButton
    Friend WithEvents radFull As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboTracks As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nudWinEndPost As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudWinStartPost As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudWinEndPre As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents nudWinStartPre As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkWindowOnly As System.Windows.Forms.CheckBox
End Class
