<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCorrelations
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
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.lstvKey = New System.Windows.Forms.ListView()
        Me.dgvCorrels = New System.Windows.Forms.DataGridView()
        Me.cmdCopyKey = New System.Windows.Forms.Button()
        CType(Me.dgvCorrels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(1047, 578)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'lstvKey
        '
        Me.lstvKey.Location = New System.Drawing.Point(804, 12)
        Me.lstvKey.Name = "lstvKey"
        Me.lstvKey.Size = New System.Drawing.Size(318, 559)
        Me.lstvKey.TabIndex = 2
        Me.lstvKey.UseCompatibleStateImageBehavior = False
        Me.lstvKey.View = System.Windows.Forms.View.List
        '
        'dgvCorrels
        '
        Me.dgvCorrels.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.dgvCorrels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCorrels.GridColor = System.Drawing.SystemColors.Control
        Me.dgvCorrels.Location = New System.Drawing.Point(12, 12)
        Me.dgvCorrels.Name = "dgvCorrels"
        Me.dgvCorrels.Size = New System.Drawing.Size(786, 559)
        Me.dgvCorrels.TabIndex = 3
        '
        'cmdCopyKey
        '
        Me.cmdCopyKey.Location = New System.Drawing.Point(804, 578)
        Me.cmdCopyKey.Name = "cmdCopyKey"
        Me.cmdCopyKey.Size = New System.Drawing.Size(75, 23)
        Me.cmdCopyKey.TabIndex = 4
        Me.cmdCopyKey.Text = "Copy Key"
        Me.cmdCopyKey.UseVisualStyleBackColor = True
        '
        'frmCorrelations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1134, 603)
        Me.Controls.Add(Me.cmdCopyKey)
        Me.Controls.Add(Me.dgvCorrels)
        Me.Controls.Add(Me.lstvKey)
        Me.Controls.Add(Me.cmdClose)
        Me.Name = "frmCorrelations"
        Me.Text = "Track Co-Occurences"
        CType(Me.dgvCorrels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents lstvKey As System.Windows.Forms.ListView
    Friend WithEvents dgvCorrels As System.Windows.Forms.DataGridView
    Friend WithEvents cmdCopyKey As System.Windows.Forms.Button
End Class
