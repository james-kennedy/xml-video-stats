<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatchStats
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radThird = New System.Windows.Forms.RadioButton()
        Me.radSecond = New System.Windows.Forms.RadioButton()
        Me.radFirst = New System.Windows.Forms.RadioButton()
        Me.radFull = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboEndTrack = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkSummary = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboTracks = New System.Windows.Forms.ComboBox()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radThird)
        Me.GroupBox1.Controls.Add(Me.radSecond)
        Me.GroupBox1.Controls.Add(Me.radFirst)
        Me.GroupBox1.Controls.Add(Me.radFull)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cboEndTrack)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.chkSummary)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboTracks)
        Me.GroupBox1.Controls.Add(Me.cmdStart)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(444, 232)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select your options"
        '
        'radThird
        '
        Me.radThird.AutoSize = True
        Me.radThird.Location = New System.Drawing.Point(336, 70)
        Me.radThird.Name = "radThird"
        Me.radThird.Size = New System.Drawing.Size(74, 17)
        Me.radThird.TabIndex = 10
        Me.radThird.Text = "Final Third"
        Me.radThird.UseVisualStyleBackColor = True
        '
        'radSecond
        '
        Me.radSecond.AutoSize = True
        Me.radSecond.Location = New System.Drawing.Point(223, 70)
        Me.radSecond.Name = "radSecond"
        Me.radSecond.Size = New System.Drawing.Size(83, 17)
        Me.radSecond.TabIndex = 9
        Me.radSecond.Text = "Middle Third"
        Me.radSecond.UseVisualStyleBackColor = True
        '
        'radFirst
        '
        Me.radFirst.AutoSize = True
        Me.radFirst.Location = New System.Drawing.Point(116, 70)
        Me.radFirst.Name = "radFirst"
        Me.radFirst.Size = New System.Drawing.Size(71, 17)
        Me.radFirst.TabIndex = 8
        Me.radFirst.Text = "First Third"
        Me.radFirst.UseVisualStyleBackColor = True
        '
        'radFull
        '
        Me.radFull.AutoSize = True
        Me.radFull.Checked = True
        Me.radFull.Location = New System.Drawing.Point(20, 70)
        Me.radFull.Name = "radFull"
        Me.radFull.Size = New System.Drawing.Size(72, 17)
        Me.radFull.TabIndex = 7
        Me.radFull.TabStop = True
        Me.radFull.Text = "Full Track"
        Me.radFull.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(322, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Track which always goes to end of coding (to calc per min values):"
        '
        'cboEndTrack
        '
        Me.cboEndTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEndTrack.FormattingEnabled = True
        Me.cboEndTrack.Location = New System.Drawing.Point(17, 134)
        Me.cboEndTrack.Name = "cboEndTrack"
        Me.cboEndTrack.Size = New System.Drawing.Size(409, 21)
        Me.cboEndTrack.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 203)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(304, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "The results will open in a new Excel window, please be patient!"
        '
        'chkSummary
        '
        Me.chkSummary.AutoSize = True
        Me.chkSummary.Location = New System.Drawing.Point(17, 173)
        Me.chkSummary.Name = "chkSummary"
        Me.chkSummary.Size = New System.Drawing.Size(242, 17)
        Me.chkSummary.TabIndex = 3
        Me.chkSummary.Text = "Add sheets for collated and averaged results?"
        Me.chkSummary.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(245, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Only include if overlapping with the following track:"
        '
        'cboTracks
        '
        Me.cboTracks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTracks.FormattingEnabled = True
        Me.cboTracks.Location = New System.Drawing.Point(17, 43)
        Me.cboTracks.Name = "cboTracks"
        Me.cboTracks.Size = New System.Drawing.Size(409, 21)
        Me.cboTracks.TabIndex = 1
        '
        'cmdStart
        '
        Me.cmdStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStart.Location = New System.Drawing.Point(363, 203)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(75, 23)
        Me.cmdStart.TabIndex = 0
        Me.cmdStart.Text = "Go!"
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'frmBatchStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(469, 257)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmBatchStats"
        Me.Text = "Batch Process Stats"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkSummary As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboTracks As System.Windows.Forms.ComboBox
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboEndTrack As System.Windows.Forms.ComboBox
    Friend WithEvents radThird As System.Windows.Forms.RadioButton
    Friend WithEvents radSecond As System.Windows.Forms.RadioButton
    Friend WithEvents radFirst As System.Windows.Forms.RadioButton
    Friend WithEvents radFull As System.Windows.Forms.RadioButton
End Class
