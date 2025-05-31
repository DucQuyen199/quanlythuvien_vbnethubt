<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBooks = New System.Windows.Forms.Button()
        Me.btnReaders = New System.Windows.Forms.Button()
        Me.btnBorrow = New System.Windows.Forms.Button()
        Me.btnLibrarians = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(178, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(345, 41)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "QUẢN LÝ THƯ VIỆN"
        '
        'btnBooks
        '
        Me.btnBooks.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnBooks.Location = New System.Drawing.Point(233, 107)
        Me.btnBooks.Name = "btnBooks"
        Me.btnBooks.Size = New System.Drawing.Size(235, 60)
        Me.btnBooks.TabIndex = 1
        Me.btnBooks.Text = "Quản Lý Sách"
        Me.btnBooks.UseVisualStyleBackColor = True
        '
        'btnReaders
        '
        Me.btnReaders.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnReaders.Location = New System.Drawing.Point(233, 173)
        Me.btnReaders.Name = "btnReaders"
        Me.btnReaders.Size = New System.Drawing.Size(235, 60)
        Me.btnReaders.TabIndex = 2
        Me.btnReaders.Text = "Quản Lý Độc Giả"
        Me.btnReaders.UseVisualStyleBackColor = True
        '
        'btnBorrow
        '
        Me.btnBorrow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnBorrow.Location = New System.Drawing.Point(233, 239)
        Me.btnBorrow.Name = "btnBorrow"
        Me.btnBorrow.Size = New System.Drawing.Size(235, 60)
        Me.btnBorrow.TabIndex = 3
        Me.btnBorrow.Text = "Quản Lý Mượn/Trả"
        Me.btnBorrow.UseVisualStyleBackColor = True
        '
        'btnLibrarians
        '
        Me.btnLibrarians.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnLibrarians.Location = New System.Drawing.Point(233, 305)
        Me.btnLibrarians.Name = "btnLibrarians"
        Me.btnLibrarians.Size = New System.Drawing.Size(235, 60)
        Me.btnLibrarians.TabIndex = 4
        Me.btnLibrarians.Text = "Quản Lý Nhân Viên"
        Me.btnLibrarians.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnExit.Location = New System.Drawing.Point(233, 371)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(235, 60)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "Thoát"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 463)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnLibrarians)
        Me.Controls.Add(Me.btnBorrow)
        Me.Controls.Add(Me.btnReaders)
        Me.Controls.Add(Me.btnBooks)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quản Lý Thư Viện"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btnBooks As Button
    Friend WithEvents btnReaders As Button
    Friend WithEvents btnBorrow As Button
    Friend WithEvents btnLibrarians As Button
    Friend WithEvents btnExit As Button
End Class
