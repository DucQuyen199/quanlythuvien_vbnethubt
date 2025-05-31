Imports System.Data.SqlClient

Public Class FormBorrowRecords
    Private Sub FormBorrowRecords_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Quản Lý Mượn Trả Sách"
        LoadBorrowRecords()
        LoadReaders()
        LoadBooks()
        ClearFields()
    End Sub

    Private Sub LoadBorrowRecords()
        Try
            Dim query As String = "SELECT b.BorrowID, r.FullName AS ReaderName, bo.Title AS BookTitle, b.BorrowDate, b.ReturnDate, b.ActualReturnDate, b.Status " &
                                 "FROM BorrowRecords b " &
                                 "JOIN Readers r ON b.ReaderID = r.ReaderID " &
                                 "JOIN Books bo ON b.BookID = bo.BookID " &
                                 "ORDER BY b.BorrowDate DESC"
            Dim dataTable As DataTable = DatabaseConnection.ExecuteQuery(query)
            dgvBorrowRecords.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadReaders()
        Try
            Dim query As String = "SELECT ReaderID, FullName FROM Readers ORDER BY FullName"
            Dim dataTable As DataTable = DatabaseConnection.ExecuteQuery(query)
            
            cboReader.DataSource = dataTable
            cboReader.DisplayMember = "FullName"
            cboReader.ValueMember = "ReaderID"
            cboReader.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadBooks()
        Try
            Dim query As String = "SELECT BookID, Title FROM Books WHERE Available > 0 ORDER BY Title"
            Dim dataTable As DataTable = DatabaseConnection.ExecuteQuery(query)
            
            cboBook.DataSource = dataTable
            cboBook.DisplayMember = "Title"
            cboBook.ValueMember = "BookID"
            cboBook.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearFields()
        txtBorrowID.Text = ""
        cboReader.SelectedIndex = -1
        cboBook.SelectedIndex = -1
        dtpBorrowDate.Value = DateTime.Today
        dtpReturnDate.Value = DateTime.Today.AddDays(14)
        dtpActualReturnDate.Value = DateTime.Today
        cboStatus.SelectedIndex = -1
        chkReturned.Checked = False
        
        btnSave.Text = "Thêm"
        txtBorrowID.Enabled = False
        dtpActualReturnDate.Enabled = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateInput() = False Then
            Return
        End If

        Try
            Dim query As String
            Dim parameters As New List(Of SqlParameter)()

            If String.IsNullOrEmpty(txtBorrowID.Text) Then
                ' Insert new borrow record
                query = "INSERT INTO BorrowRecords (ReaderID, BookID, BorrowDate, ReturnDate, Status) VALUES (@ReaderID, @BookID, @BorrowDate, @ReturnDate, @Status); " &
                        "UPDATE Books SET Available = Available - 1 WHERE BookID = @BookID"
            Else
                ' Update existing borrow record
                query = "UPDATE BorrowRecords SET ReaderID = @ReaderID, BookID = @BookID, BorrowDate = @BorrowDate, ReturnDate = @ReturnDate, "
                
                If chkReturned.Checked Then
                    query += "ActualReturnDate = @ActualReturnDate, "
                End If
                
                query += "Status = @Status WHERE BorrowID = @BorrowID"
                parameters.Add(New SqlParameter("@BorrowID", Convert.ToInt32(txtBorrowID.Text)))
                
                ' If status changes to "Đã trả" and it wasn't before
                If cboStatus.Text = "Đã trả" And (String.IsNullOrEmpty(_previousStatus) Or _previousStatus <> "Đã trả") Then
                    query += "; UPDATE Books SET Available = Available + 1 WHERE BookID = @BookID"
                End If
            End If

            parameters.Add(New SqlParameter("@ReaderID", cboReader.SelectedValue))
            parameters.Add(New SqlParameter("@BookID", cboBook.SelectedValue))
            parameters.Add(New SqlParameter("@BorrowDate", dtpBorrowDate.Value.Date))
            parameters.Add(New SqlParameter("@ReturnDate", dtpReturnDate.Value.Date))
            If chkReturned.Checked Then
                parameters.Add(New SqlParameter("@ActualReturnDate", dtpActualReturnDate.Value.Date))
            End If
            parameters.Add(New SqlParameter("@Status", cboStatus.Text))

            Dim result As Integer = DatabaseConnection.ExecuteNonQuery(query, parameters)

            If result > 0 Then
                MessageBox.Show("Lưu thông tin mượn/trả sách thành công!")
                LoadBorrowRecords()
                LoadBooks() ' Reload books to update available counts
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Function ValidateInput() As Boolean
        If cboReader.SelectedIndex = -1 Then
            MessageBox.Show("Vui lòng chọn độc giả!")
            Return False
        End If

        If cboBook.SelectedIndex = -1 Then
            MessageBox.Show("Vui lòng chọn sách!")
            Return False
        End If

        If dtpBorrowDate.Value > dtpReturnDate.Value Then
            MessageBox.Show("Ngày mượn không thể sau ngày trả dự kiến!")
            Return False
        End If

        If chkReturned.Checked And dtpActualReturnDate.Value < dtpBorrowDate.Value Then
            MessageBox.Show("Ngày trả thực tế không thể trước ngày mượn!")
            Return False
        End If

        If cboStatus.SelectedIndex = -1 Then
            MessageBox.Show("Vui lòng chọn trạng thái!")
            Return False
        End If

        Return True
    End Function

    Private _previousStatus As String = ""

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(txtBorrowID.Text) Then
            MessageBox.Show("Vui lòng chọn bản ghi cần xóa!")
            Return
        End If

        If MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Try
            ' If book is still out, update available count
            Dim checkQuery As String = "SELECT Status, BookID FROM BorrowRecords WHERE BorrowID = @BorrowID"
            Dim checkParams As New List(Of SqlParameter)()
            checkParams.Add(New SqlParameter("@BorrowID", Convert.ToInt32(txtBorrowID.Text)))
            
            Dim connection As SqlConnection = DatabaseConnection.GetConnection()
            Dim checkCommand As New SqlCommand(checkQuery, connection)
            checkCommand.Parameters.AddRange(checkParams.ToArray())
            
            connection.Open()
            Dim reader As SqlDataReader = checkCommand.ExecuteReader()
            
            Dim status As String = ""
            Dim bookID As Integer = 0
            
            If reader.Read() Then
                status = If(reader("Status") IsNot DBNull.Value, reader("Status").ToString(), "")
                bookID = Convert.ToInt32(reader("BookID"))
            End If
            
            reader.Close()
            connection.Close()
            
            Dim query As String = "DELETE FROM BorrowRecords WHERE BorrowID = @BorrowID"
            
            ' If book wasn't returned, increment available count
            If status = "Đang mượn" Or status = "Quá hạn" Then
                query += "; UPDATE Books SET Available = Available + 1 WHERE BookID = @BookID"
            End If
            
            Dim parameters As New List(Of SqlParameter)()
            parameters.Add(New SqlParameter("@BorrowID", Convert.ToInt32(txtBorrowID.Text)))
            parameters.Add(New SqlParameter("@BookID", bookID))

            Dim result As Integer = DatabaseConnection.ExecuteNonQuery(query, parameters)

            If result > 0 Then
                MessageBox.Show("Xóa bản ghi thành công!")
                LoadBorrowRecords()
                LoadBooks() ' Reload books to update available counts
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub dgvBorrowRecords_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBorrowRecords.CellClick
        If e.RowIndex >= 0 Then
            Try
                Dim borrowID As Integer = Convert.ToInt32(dgvBorrowRecords.Rows(e.RowIndex).Cells("BorrowID").Value)
                
                Dim query As String = "SELECT b.*, r.FullName, bk.Title FROM BorrowRecords b " &
                                      "JOIN Readers r ON b.ReaderID = r.ReaderID " &
                                      "JOIN Books bk ON b.BookID = bk.BookID " &
                                      "WHERE b.BorrowID = @BorrowID"
                
                Dim parameters As New List(Of SqlParameter)()
                parameters.Add(New SqlParameter("@BorrowID", borrowID))
                
                Dim connection As SqlConnection = DatabaseConnection.GetConnection()
                Dim command As New SqlCommand(query, connection)
                command.Parameters.AddRange(parameters.ToArray())
                
                connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()
                
                If reader.Read() Then
                    txtBorrowID.Text = reader("BorrowID").ToString()
                    
                    ' Find and select reader in combo box
                    Dim readerID As Integer = Convert.ToInt32(reader("ReaderID"))
                    For i As Integer = 0 To cboReader.Items.Count - 1
                        Dim item As DataRowView = DirectCast(cboReader.Items(i), DataRowView)
                        If Convert.ToInt32(item("ReaderID")) = readerID Then
                            cboReader.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    
                    ' Find and select book in combo box
                    Dim bookID As Integer = Convert.ToInt32(reader("BookID"))
                    For i As Integer = 0 To cboBook.Items.Count - 1
                        Dim item As DataRowView = DirectCast(cboBook.Items(i), DataRowView)
                        If Convert.ToInt32(item("BookID")) = bookID Then
                            cboBook.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    
                    dtpBorrowDate.Value = Convert.ToDateTime(reader("BorrowDate"))
                    dtpReturnDate.Value = Convert.ToDateTime(reader("ReturnDate"))
                    
                    If reader("ActualReturnDate") IsNot DBNull.Value Then
                        dtpActualReturnDate.Value = Convert.ToDateTime(reader("ActualReturnDate"))
                        chkReturned.Checked = True
                        dtpActualReturnDate.Enabled = True
                    Else
                        chkReturned.Checked = False
                        dtpActualReturnDate.Enabled = False
                    End If
                    
                    _previousStatus = reader("Status").ToString()
                    cboStatus.Text = _previousStatus
                    
                    btnSave.Text = "Cập nhật"
                End If
                
                reader.Close()
                connection.Close()
                
            Catch ex As Exception
                MessageBox.Show("Lỗi: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub chkReturned_CheckedChanged(sender As Object, e As EventArgs) Handles chkReturned.CheckedChanged
        dtpActualReturnDate.Enabled = chkReturned.Checked
        If chkReturned.Checked Then
            cboStatus.Text = "Đã trả"
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            Dim searchText As String = txtSearch.Text.Trim()
            
            If String.IsNullOrEmpty(searchText) Then
                LoadBorrowRecords()
                Return
            End If
            
            Dim query As String = "SELECT b.BorrowID, r.FullName AS ReaderName, bo.Title AS BookTitle, b.BorrowDate, b.ReturnDate, b.ActualReturnDate, b.Status " &
                                 "FROM BorrowRecords b " &
                                 "JOIN Readers r ON b.ReaderID = r.ReaderID " &
                                 "JOIN Books bo ON b.BookID = bo.BookID " &
                                 "WHERE r.FullName LIKE @SearchText OR bo.Title LIKE @SearchText OR b.Status LIKE @SearchText " &
                                 "ORDER BY b.BorrowDate DESC"
            
            Dim parameters As New List(Of SqlParameter)()
            parameters.Add(New SqlParameter("@SearchText", "%" & searchText & "%"))
            
            Dim connection As SqlConnection = DatabaseConnection.GetConnection()
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddRange(parameters.ToArray())
            
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()
            
            connection.Open()
            adapter.Fill(dataTable)
            connection.Close()
            
            dgvBorrowRecords.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub
End Class 