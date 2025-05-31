Imports System.Data.SqlClient

Public Class FormReaders
    Private Sub FormReaders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Quản Lý Độc Giả"
        LoadReaders()
        ClearFields()
    End Sub

    Private Sub LoadReaders()
        Try
            Dim query As String = "SELECT * FROM Readers"
            Dim dataTable As DataTable = DatabaseConnection.ExecuteQuery(query)
            dgvReaders.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearFields()
        txtReaderID.Text = ""
        txtFullName.Text = ""
        cboGender.SelectedIndex = -1
        dtpDateOfBirth.Value = DateTime.Now
        txtAddress.Text = ""
        txtPhone.Text = ""
        txtEmail.Text = ""
        btnSave.Text = "Thêm"
        txtReaderID.Enabled = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateInput() = False Then
            Return
        End If

        Try
            Dim query As String
            Dim parameters As New List(Of SqlParameter)()

            If String.IsNullOrEmpty(txtReaderID.Text) Then
                ' Insert new reader
                query = "INSERT INTO Readers (FullName, Gender, DateOfBirth, Address, Phone, Email) VALUES (@FullName, @Gender, @DateOfBirth, @Address, @Phone, @Email)"
            Else
                ' Update existing reader
                query = "UPDATE Readers SET FullName = @FullName, Gender = @Gender, DateOfBirth = @DateOfBirth, Address = @Address, Phone = @Phone, Email = @Email WHERE ReaderID = @ReaderID"
                parameters.Add(New SqlParameter("@ReaderID", Convert.ToInt32(txtReaderID.Text)))
            End If

            parameters.Add(New SqlParameter("@FullName", txtFullName.Text))
            parameters.Add(New SqlParameter("@Gender", If(cboGender.SelectedItem IsNot Nothing, cboGender.SelectedItem.ToString(), DBNull.Value)))
            parameters.Add(New SqlParameter("@DateOfBirth", dtpDateOfBirth.Value))
            parameters.Add(New SqlParameter("@Address", txtAddress.Text))
            parameters.Add(New SqlParameter("@Phone", txtPhone.Text))
            parameters.Add(New SqlParameter("@Email", txtEmail.Text))

            Dim result As Integer = DatabaseConnection.ExecuteNonQuery(query, parameters)

            If result > 0 Then
                MessageBox.Show("Lưu thông tin độc giả thành công!")
                LoadReaders()
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Function ValidateInput() As Boolean
        If String.IsNullOrEmpty(txtFullName.Text) Then
            MessageBox.Show("Vui lòng nhập họ tên độc giả!")
            Return False
        End If
        
        If Not String.IsNullOrEmpty(txtEmail.Text) AndAlso Not IsValidEmail(txtEmail.Text) Then
            MessageBox.Show("Địa chỉ email không hợp lệ!")
            Return False
        End If

        Return True
    End Function
    
    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr = New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(txtReaderID.Text) Then
            MessageBox.Show("Vui lòng chọn độc giả cần xóa!")
            Return
        End If

        If MessageBox.Show("Bạn có chắc chắn muốn xóa độc giả này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Try
            ' Check if reader has borrowed books
            Dim checkQuery As String = "SELECT COUNT(*) FROM BorrowRecords WHERE ReaderID = @ReaderID AND Status = N'Đang mượn'"
            Dim checkParams As New List(Of SqlParameter)()
            checkParams.Add(New SqlParameter("@ReaderID", Convert.ToInt32(txtReaderID.Text)))
            
            Dim connection As SqlConnection = DatabaseConnection.GetConnection()
            Dim checkCommand As New SqlCommand(checkQuery, connection)
            checkCommand.Parameters.AddRange(checkParams.ToArray())
            
            connection.Open()
            Dim borrowCount As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())
            connection.Close()
            
            If borrowCount > 0 Then
                MessageBox.Show("Không thể xóa độc giả này vì họ đang mượn sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            
            ' Delete reader
            Dim query As String = "DELETE FROM Readers WHERE ReaderID = @ReaderID"
            Dim parameters As New List(Of SqlParameter)()
            parameters.Add(New SqlParameter("@ReaderID", Convert.ToInt32(txtReaderID.Text)))

            Dim result As Integer = DatabaseConnection.ExecuteNonQuery(query, parameters)

            If result > 0 Then
                MessageBox.Show("Xóa độc giả thành công!")
                LoadReaders()
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub dgvReaders_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReaders.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvReaders.Rows(e.RowIndex)
            
            txtReaderID.Text = row.Cells("ReaderID").Value.ToString()
            txtFullName.Text = row.Cells("FullName").Value.ToString()
            
            If row.Cells("Gender").Value IsNot DBNull.Value Then
                Dim gender As String = row.Cells("Gender").Value.ToString()
                For i As Integer = 0 To cboGender.Items.Count - 1
                    If cboGender.Items(i).ToString() = gender Then
                        cboGender.SelectedIndex = i
                        Exit For
                    End If
                Next
            Else
                cboGender.SelectedIndex = -1
            End If
            
            If row.Cells("DateOfBirth").Value IsNot DBNull.Value Then
                dtpDateOfBirth.Value = Convert.ToDateTime(row.Cells("DateOfBirth").Value)
            Else
                dtpDateOfBirth.Value = DateTime.Now
            End If
            
            txtAddress.Text = If(row.Cells("Address").Value Is DBNull.Value, "", row.Cells("Address").Value.ToString())
            txtPhone.Text = If(row.Cells("Phone").Value Is DBNull.Value, "", row.Cells("Phone").Value.ToString())
            txtEmail.Text = If(row.Cells("Email").Value Is DBNull.Value, "", row.Cells("Email").Value.ToString())
            
            btnSave.Text = "Cập nhật"
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            Dim searchText As String = txtSearch.Text.Trim()
            
            If String.IsNullOrEmpty(searchText) Then
                LoadReaders()
                Return
            End If
            
            Dim query As String = "SELECT * FROM Readers WHERE FullName LIKE @SearchText OR Address LIKE @SearchText OR Phone LIKE @SearchText OR Email LIKE @SearchText"
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
            
            dgvReaders.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub
End Class 