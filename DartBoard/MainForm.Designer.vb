<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Me.DartboardPictureBox = New System.Windows.Forms.PictureBox()
        Me.StartRoundButton = New System.Windows.Forms.Button()
        Me.ReviewButton = New System.Windows.Forms.Button()
        Me.QuitButton = New System.Windows.Forms.Button()
        Me.RoundsListBox = New System.Windows.Forms.ListBox()
        Me.ModeLabel = New System.Windows.Forms.Label()
        Me.RoundNumberLabel = New System.Windows.Forms.Label()
        Me.ThrowCountLabel = New System.Windows.Forms.Label()
        Me.MainToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenLogButton = New System.Windows.Forms.Button()
        CType(Me.DartboardPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DartboardPictureBox
        '
        Me.DartboardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DartboardPictureBox.Location = New System.Drawing.Point(12, 12)
        Me.DartboardPictureBox.Name = "DartboardPictureBox"
        Me.DartboardPictureBox.Size = New System.Drawing.Size(400, 400)
        Me.DartboardPictureBox.TabIndex = 0
        Me.DartboardPictureBox.TabStop = False
        '
        'StartRoundButton
        '
        Me.StartRoundButton.Location = New System.Drawing.Point(430, 20)
        Me.StartRoundButton.Name = "StartRoundButton"
        Me.StartRoundButton.Size = New System.Drawing.Size(110, 30)
        Me.StartRoundButton.TabIndex = 1
        Me.StartRoundButton.Text = "&Start Round"
        Me.StartRoundButton.UseVisualStyleBackColor = True
        '
        'ReviewButton
        '
        Me.ReviewButton.Location = New System.Drawing.Point(430, 60)
        Me.ReviewButton.Name = "ReviewButton"
        Me.ReviewButton.Size = New System.Drawing.Size(110, 30)
        Me.ReviewButton.TabIndex = 2
        Me.ReviewButton.Text = "&Review Mode"
        Me.ReviewButton.UseVisualStyleBackColor = True
        '
        'QuitButton
        '
        Me.QuitButton.Location = New System.Drawing.Point(430, 100)
        Me.QuitButton.Name = "QuitButton"
        Me.QuitButton.Size = New System.Drawing.Size(110, 30)
        Me.QuitButton.TabIndex = 3
        Me.QuitButton.Text = "&Quit"
        Me.QuitButton.UseVisualStyleBackColor = True
        '
        'RoundsListBox
        '
        Me.RoundsListBox.FormattingEnabled = True
        Me.RoundsListBox.ItemHeight = 16
        Me.RoundsListBox.Location = New System.Drawing.Point(430, 169)
        Me.RoundsListBox.Name = "RoundsListBox"
        Me.RoundsListBox.Size = New System.Drawing.Size(350, 148)
        Me.RoundsListBox.TabIndex = 4
        '
        'ModeLabel
        '
        Me.ModeLabel.AutoSize = True
        Me.ModeLabel.Location = New System.Drawing.Point(427, 353)
        Me.ModeLabel.Name = "ModeLabel"
        Me.ModeLabel.Size = New System.Drawing.Size(75, 16)
        Me.ModeLabel.TabIndex = 5
        Me.ModeLabel.Text = "Mode: Play"
        '
        'RoundNumberLabel
        '
        Me.RoundNumberLabel.AutoSize = True
        Me.RoundNumberLabel.Location = New System.Drawing.Point(427, 378)
        Me.RoundNumberLabel.Name = "RoundNumberLabel"
        Me.RoundNumberLabel.Size = New System.Drawing.Size(60, 16)
        Me.RoundNumberLabel.TabIndex = 6
        Me.RoundNumberLabel.Text = "Round: 0"
        '
        'ThrowCountLabel
        '
        Me.ThrowCountLabel.AutoSize = True
        Me.ThrowCountLabel.Location = New System.Drawing.Point(427, 403)
        Me.ThrowCountLabel.Name = "ThrowCountLabel"
        Me.ThrowCountLabel.Size = New System.Drawing.Size(68, 16)
        Me.ThrowCountLabel.TabIndex = 7
        Me.ThrowCountLabel.Text = "Throw: 0/3"
        '
        'OpenLogButton
        '
        Me.OpenLogButton.Location = New System.Drawing.Point(587, 38)
        Me.OpenLogButton.Name = "OpenLogButton"
        Me.OpenLogButton.Size = New System.Drawing.Size(131, 59)
        Me.OpenLogButton.TabIndex = 8
        Me.OpenLogButton.Text = "Open Log (Notepad)"
        Me.OpenLogButton.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.OpenLogButton)
        Me.Controls.Add(Me.ThrowCountLabel)
        Me.Controls.Add(Me.RoundNumberLabel)
        Me.Controls.Add(Me.ModeLabel)
        Me.Controls.Add(Me.RoundsListBox)
        Me.Controls.Add(Me.QuitButton)
        Me.Controls.Add(Me.ReviewButton)
        Me.Controls.Add(Me.StartRoundButton)
        Me.Controls.Add(Me.DartboardPictureBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dart Game Simulator"
        CType(Me.DartboardPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DartboardPictureBox As PictureBox
    Friend WithEvents StartRoundButton As Button
    Friend WithEvents ReviewButton As Button
    Friend WithEvents QuitButton As Button
    Friend WithEvents RoundsListBox As ListBox
    Friend WithEvents ModeLabel As Label
    Friend WithEvents RoundNumberLabel As Label
    Friend WithEvents ThrowCountLabel As Label
    Friend WithEvents MainToolTip As ToolTip
    Friend WithEvents OpenLogButton As Button
End Class
