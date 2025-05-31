Imports System.Data.SqlClient
Imports System.Security.Cryptography

Public Class FormLibrarians
    Private Sub FormLibrarians_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Quản Lý Nhân Viên Thư Viện"
        LoadLibrarians()
        ClearFields()
    End Sub

    Private Sub LoadLibrarians()
        Try
            Dim query As String = "SELECT LibrarianID, FullName, Username, Role, Email, Phone FROM Librarians"
            Dim dataTable As DataTable = DatabaseConnection.ExecuteQuery(query)
            dgvLibrarians.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearFields()
        txtLibrarianID.Text = ""
        txtFullName.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        cboRole.SelectedIndex = -1
        txtEmail.Text = ""
        txtPhone.Text = ""
        btnSave.Text = "Thêm"
        
        txtLibrarianID.Enabled = False
        txtUsername.Enabled = True
        lblPassword.Visible = True
        txtPassword.Visible = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateInput() = False Then
            Return
        End If

        Try
            Dim query As String
            Dim parameters As New List(Of SqlParameter)()

            If String.IsNullOrEmpty(txtLibrarianID.Text) Then
                ' Insert new librarian
                query = "INSERT INTO Librarians (FullName, Username, PasswordHash, Role, Email, Phone) VALUES (@FullName, @Username, @PasswordHash, @Role, @Email, @Phone)"
                
                ' Check if username already exists
                Dim checkQuery As String = "SELECT COUNT(*) FROM Librarians WHERE Username = @Username"
                Dim checkParams As New List(Of SqlParameter)()
                checkParams.Add(New SqlParameter("@Username", txtUsername.Text))
                
                Dim connection As SqlConnection = DatabaseConnection.GetConnection()
                Dim checkCommand As New SqlCommand(checkQuery, connection)
                checkCommand.Parameters.AddRange(checkParams.ToArray())
                
                connection.Open()
                Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())
                connection.Close()
                
                If count > 0 Then
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                
                parameters.Add(New SqlParameter("@PasswordHash", HashPassword(txtPassword.Text)))
            Else
                ' Update existing librarian
                If String.IsNullOrEmpty(txtPassword.Text) Then
                    query = "UPDATE Librarians SET FullName = @FullName, Role = @Role, Email = @Email, Phone = @Phone WHERE LibrarianID = @LibrarianID"
                Else
                    query = "UPDATE Librarians SET FullName = @FullName, PasswordHash = @PasswordHash, Role = @Role, Email = @Email, Phone = @Phone WHERE LibrarianID = @LibrarianID"
                    parameters.Add(New SqlParameter("@PasswordHash", HashPassword(txtPassword.Text)))
                End If
                parameters.Add(New SqlParameter("@LibrarianID", Convert.ToInt32(txtLibrarianID.Text)))
            End If

            parameters.Add(New SqlParameter("@FullName", txtFullName.Text))
            parameters.Add(New SqlParameter("@Username", txtUsername.Text))
            parameters.Add(New SqlParameter("@Role", If(cboRole.SelectedItem IsNot Nothing, cboRole.SelectedItem.ToString(), DBNull.Value)))
            parameters.Add(New SqlParameter("@Email", txtEmail.Text))
            parameters.Add(New SqlParameter("@Phone", txtPhone.Text))

            Dim result As Integer = DatabaseConnection.ExecuteNonQuery(query, parameters)

            If result > 0 Then
                MessageBox.Show("Lưu thông tin nhân viên thành công!")
                LoadLibrarians()
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Function ValidateInput() As Boolean
        If String.IsNullOrEmpty(txtFullName.Text) Then
            MessageBox.Show("Vui lòng nhập họ tên nhân viên!")
            Return False
        End If

        If String.IsNullOrEmpty(txtLibrarianID.Text) Then ' New librarian
            If String.IsNullOrEmpty(txtUsername.Text) Then
                MessageBox.Show("Vui lòng nhập tên đăng nhập!")
                Return False
            End If
            
            If String.IsNullOrEmpty(txtPassword.Text) Then
                MessageBox.Show("Vui lòng nhập mật khẩu!")
                Return False
            End If
        End If

        If cboRole.SelectedIndex = -1 Then
            MessageBox.Show("Vui lòng chọn vai trò!")
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

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
            Dim builder As New System.Text.StringBuilder()
            
            For i As Integer = 0 To hashedBytes.Length - 1
                builder.Append(hashedBytes(i).ToString("x2"))
            Next
            
            Return builder.ToString()
        End Using
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(txtLibrarianID.Text) Then
            MessageBox.Show("Vui lòng chọn nhân viên cần xóa!")
            Return
        End If

        ' Check if we're trying to delete the currently logged-in account
        ' This would typically be implemented with a proper authentication system
        ' For now, we'll just prevent deletion of 'Admin' accounts
        If cboRole.Text = "Admin" Then
            MessageBox.Show("Không thể xóa tài khoản Admin!")
            Return
        End If

        If MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Try
            Dim query As String = "DELETE FROM Librarians WHERE LibrarianID = @LibrarianID"
            Dim parameters As New List(Of SqlParameter)()
            parameters.Add(New SqlParameter("@LibrarianID", Convert.ToInt32(txtLibrarianID.Text)))

            Dim result As Integer = DatabaseConnection.ExecuteNonQuery(query, parameters)

            If result > 0 Then
                MessageBox.Show("Xóa nhân viên thành công!")
                LoadLibrarians()
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub dgvLibrarians_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLibrarians.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvLibrarians.Rows(e.RowIndex)
            
            txtLibrarianID.Text = row.Cells("LibrarianID").Value.ToString()
            txtFullName.Text = row.Cells("FullName").Value.ToString()
            txtUsername.Text = row.Cells("Username").Value.ToString()
            txtUsername.Enabled = False ' Can't change username once created
            
            If row.Cells("Role").Value IsNot DBNull.Value Then
                Dim role As String = row.Cells("Role").Value.ToString()
                For i As Integer = 0 To cboRole.Items.Count - 1
                    If cboRole.Items(i).ToString() = role Then
                        cboRole.SelectedIndex = i
                        Exit For
                    End If
                Next
            Else
                cboRole.SelectedIndex = -1
            End If
            
            txtEmail.Text = If(row.Cells("Email").Value Is DBNull.Value, "", row.Cells("Email").Value.ToString())
            txtPhone.Text = If(row.Cells("Phone").Value Is DBNull.Value, "", row.Cells("Phone").Value.ToString())
            
            txtPassword.Text = ""
            lblPassword.Text = "Mật khẩu mới (để trống nếu không thay đổi):"
            
            btnSave.Text = "Cập nhật"
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            Dim searchText As String = txtSearch.Text.Trim()
            
            If String.IsNullOrEmpty(searchText) Then
                LoadLibrarians()
                Return
            End If
            
            Dim query As String = "SELECT LibrarianID, FullName, Username, Role, Email, Phone FROM Librarians " &
                                 "WHERE FullName LIKE @SearchText OR Username LIKE @SearchText OR Role LIKE @SearchText OR Email LIKE @SearchText"
            
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
            
            dgvLibrarians.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub
End Class 