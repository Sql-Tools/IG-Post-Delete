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
        Me.UserBox = New System.Windows.Forms.TextBox()
        Me.PassBox = New System.Windows.Forms.TextBox()
        Me.LoginB = New System.Windows.Forms.Button()
        Me.Log = New System.Windows.Forms.TextBox()
        Me.GrabbedC = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'UserBox
        '
        Me.UserBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UserBox.ForeColor = System.Drawing.Color.Gray
        Me.UserBox.Location = New System.Drawing.Point(12, 12)
        Me.UserBox.Name = "UserBox"
        Me.UserBox.Size = New System.Drawing.Size(100, 20)
        Me.UserBox.TabIndex = 0
        Me.UserBox.Text = "User"
        Me.UserBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PassBox
        '
        Me.PassBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PassBox.ForeColor = System.Drawing.Color.Gray
        Me.PassBox.Location = New System.Drawing.Point(12, 38)
        Me.PassBox.Name = "PassBox"
        Me.PassBox.Size = New System.Drawing.Size(100, 20)
        Me.PassBox.TabIndex = 1
        Me.PassBox.Text = "Pass"
        Me.PassBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.PassBox.UseSystemPasswordChar = True
        '
        'LoginB
        '
        Me.LoginB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LoginB.ForeColor = System.Drawing.Color.Gray
        Me.LoginB.Location = New System.Drawing.Point(12, 64)
        Me.LoginB.Name = "LoginB"
        Me.LoginB.Size = New System.Drawing.Size(100, 23)
        Me.LoginB.TabIndex = 2
        Me.LoginB.Text = "Login"
        Me.LoginB.UseVisualStyleBackColor = True
        '
        'Log
        '
        Me.Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Log.ForeColor = System.Drawing.Color.DarkGray
        Me.Log.Location = New System.Drawing.Point(118, 12)
        Me.Log.Multiline = True
        Me.Log.Name = "Log"
        Me.Log.Size = New System.Drawing.Size(184, 56)
        Me.Log.TabIndex = 3
        '
        'GrabbedC
        '
        Me.GrabbedC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GrabbedC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrabbedC.ForeColor = System.Drawing.Color.Indigo
        Me.GrabbedC.Location = New System.Drawing.Point(118, 67)
        Me.GrabbedC.Name = "GrabbedC"
        Me.GrabbedC.ReadOnly = True
        Me.GrabbedC.Size = New System.Drawing.Size(184, 21)
        Me.GrabbedC.TabIndex = 5
        Me.GrabbedC.Text = "0"
        Me.GrabbedC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 99)
        Me.Controls.Add(Me.GrabbedC)
        Me.Controls.Add(Me.Log)
        Me.Controls.Add(Me.LoginB)
        Me.Controls.Add(Me.PassBox)
        Me.Controls.Add(Me.UserBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sql-Post-Delete"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UserBox As TextBox
    Friend WithEvents PassBox As TextBox
    Friend WithEvents LoginB As Button
    Friend WithEvents Log As TextBox
    Friend WithEvents GrabbedC As TextBox
End Class
