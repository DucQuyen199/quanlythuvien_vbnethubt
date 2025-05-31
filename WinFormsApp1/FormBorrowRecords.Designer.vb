<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormBorrowRecords
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
        Me.dgvBorrowRecords = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpActualReturnDate = New System.Windows.Forms.DateTimePicker()
        Me.chkReturned = New System.Windows.Forms.CheckBox()
        Me.dtpReturnDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpBorrowDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboBook = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboReader = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBorrowID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.dgvBorrowRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(391, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(291, 41)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "QUẢN LÝ MƯỢN TRẢ"
        '
        'dgvBorrowRecords
        '
        Me.dgvBorrowRecords.AllowUserToAddRows = False
        Me.dgvBorrowRecords.AllowUserToDeleteRows = False
        Me.dgvBorrowRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBorrowRecords.Location = New System.Drawing.Point(12, 327)
        Me.dgvBorrowRecords.Name = "dgvBorrowRecords"
        Me.dgvBorrowRecords.ReadOnly = True
        Me.dgvBorrowRecords.RowHeadersWidth = 51
        Me.dgvBorrowRecords.RowTemplate.Height = 29
        Me.dgvBorrowRecords.Size = New System.Drawing.Size(1053, 323)
        Me.dgvBorrowRecords.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboStatus)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.dtpActualReturnDate)
        Me.GroupBox1.Controls.Add(Me.chkReturned)
        Me.GroupBox1.Controls.Add(Me.dtpReturnDate)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.dtpBorrowDate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cboBook)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboReader)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtBorrowID)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1053, 195)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin mượn trả"
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"Đang mượn", "Đã trả", "Quá hạn"})
        Me.cboStatus.Location = New System.Drawing.Point(797, 134)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(154, 28)
        Me.cboStatus.TabIndex = 16
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(722, 137)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 20)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Trạng thái:"
        '
        'dtpActualReturnDate
        '
        Me.dtpActualReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpActualReturnDate.Location = New System.Drawing.Point(544, 134)
        Me.dtpActualReturnDate.Name = "dtpActualReturnDate"
        Me.dtpActualReturnDate.Size = New System.Drawing.Size(162, 27)
        Me.dtpActualReturnDate.TabIndex = 14
        '
        'chkReturned
        '
        Me.chkReturned.AutoSize = True
        Me.chkReturned.Location = New System.Drawing.Point(460, 136)
        Me.chkReturned.Name = "chkReturned"
        Me.chkReturned.Size = New System.Drawing.Size(78, 24)
        Me.chkReturned.TabIndex = 13
        Me.chkReturned.Text = "Đã trả:"
        Me.chkReturned.UseVisualStyleBackColor = True
        '
        'dtpReturnDate
        '
        Me.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpReturnDate.Location = New System.Drawing.Point(544, 88)
        Me.dtpReturnDate.Name = "dtpReturnDate"
        Me.dtpReturnDate.Size = New System.Drawing.Size(162, 27)
        Me.dtpReturnDate.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(390, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(148, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Ngày trả (dự kiến):"
        '
        'dtpBorrowDate
        '
        Me.dtpBorrowDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpBorrowDate.Location = New System.Drawing.Point(141, 88)
        Me.dtpBorrowDate.Name = "dtpBorrowDate"
        Me.dtpBorrowDate.Size = New System.Drawing.Size(162, 27)
        Me.dtpBorrowDate.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(53, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 20)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Ngày mượn:"
        '
        'cboBook
        '
        Me.cboBook.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBook.FormattingEnabled = True
        Me.cboBook.Location = New System.Drawing.Point(544, 41)
        Me.cboBook.Name = "cboBook"
        Me.cboBook.Size = New System.Drawing.Size(407, 28)
        Me.cboBook.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(420, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 20)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Sách đang mượn:"
        '
        'cboReader
        '
        Me.cboReader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReader.FormattingEnabled = True
        Me.cboReader.Location = New System.Drawing.Point(141, 41)
        Me.cboReader.Name = "cboReader"
        Me.cboReader.Size = New System.Drawing.Size(273, 28)
        Me.cboReader.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(74, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Độc giả:"
        '
        'txtBorrowID
        '
        Me.txtBorrowID.Enabled = False
        Me.txtBorrowID.Location = New System.Drawing.Point(141, 134)
        Me.txtBorrowID.Name = "txtBorrowID"
        Me.txtBorrowID.Size = New System.Drawing.Size(162, 27)
        Me.txtBorrowID.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(51, 137)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Mã mượn:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(716, 265)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(111, 40)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Thêm"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(833, 265)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(111, 40)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "Xóa"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(950, 265)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(111, 40)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Làm mới"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(153, 272)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(542, 27)
        Me.txtSearch.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(70, 275)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 20)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Tìm kiếm:"
        '
        'FormBorrowRecords
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1077, 662)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvBorrowRecords)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormBorrowRecords"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quản Lý Mượn Trả Sách"
        CType(Me.dgvBorrowRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents dgvBorrowRecords As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dtpReturnDate As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents dtpBorrowDate As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents cboBook As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cboReader As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtBorrowID As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents dtpActualReturnDate As DateTimePicker
    Friend WithEvents chkReturned As CheckBox
End Class 