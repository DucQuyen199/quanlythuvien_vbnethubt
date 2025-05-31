Imports System.Data.SqlClient

Public Class FormBooks
    Private Sub FormBooks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Quản Lý Sách"
        LoadBooks()
        ClearFields()
    End Sub

    Private Sub LoadBooks()
        Try
            Dim query As String = "SELECT * FROM Books"
            Dim dataTable As DataTable = DatabaseConnection.ExecuteQuery(query)
            dgvBooks.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearFields()
        txtBookID.Text = ""
        txtTitle.Text = ""
        txtAuthor.Text = ""
        txtPublisher.Text = ""
        txtYearPublished.Text = ""
        txtGenre.Text = ""
        txtQuantity.Text = "0"
        txtAvailable.Text = "0"
        btnSave.Text = "Thêm"
        txtBookID.Enabled = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateInput() = False Then
            Return
        End If

        Try
            Dim query As String
            Dim parameters As New List(Of SqlParameter)()

            If String.IsNullOrEmpty(txtBookID.Text) Then
                ' Insert new book
                query = "INSERT INTO Books (Title, Author, Publisher, YearPublished, Genre, Quantity, Available) VALUES (@Title, @Author, @Publisher, @YearPublished, @Genre, @Quantity, @Available)"
            Else
                ' Update existing book
                query = "UPDATE Books SET Title = @Title, Author = @Author, Publisher = @Publisher, YearPublished = @YearPublished, Genre = @Genre, Quantity = @Quantity, Available = @Available WHERE BookID = @BookID"
                parameters.Add(New SqlParameter("@BookID", Convert.ToInt32(txtBookID.Text)))
            End If

            parameters.Add(New SqlParameter("@Title", txtTitle.Text))
            parameters.Add(New SqlParameter("@Author", txtAuthor.Text))
            parameters.Add(New SqlParameter("@Publisher", txtPublisher.Text))
            
            Dim yearPublished As Integer
            If Integer.TryParse(txtYearPublished.Text, yearPublished) Then
                parameters.Add(New SqlParameter("@YearPublished", yearPublished))
            Else
                parameters.Add(New SqlParameter("@YearPublished", DBNull.Value))
            End If
            
            parameters.Add(New SqlParameter("@Genre", txtGenre.Text))
            
            Dim quantity As Integer = 0
            Integer.TryParse(txtQuantity.Text, quantity)
            parameters.Add(New SqlParameter("@Quantity", quantity))
            
            Dim available As Integer = 0
            Integer.TryParse(txtAvailable.Text, available)
            parameters.Add(New SqlParameter("@Available", available))

            Dim result As Integer = DatabaseConnection.ExecuteNonQuery(query, parameters)

            If result > 0 Then
                MessageBox.Show("Lưu thông tin sách thành công!")
                LoadBooks()
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Function ValidateInput() As Boolean
        If String.IsNullOrEmpty(txtTitle.Text) Then
            MessageBox.Show("Vui lòng nhập tên sách!")
            Return False
        End If

        Dim yearPublished As Integer
        If Not String.IsNullOrEmpty(txtYearPublished.Text) AndAlso Not Integer.TryParse(txtYearPublished.Text, yearPublished) Then
            MessageBox.Show("Năm xuất bản phải là số!")
            Return False
        End If

        Dim quantity As Integer
        If Not Integer.TryParse(txtQuantity.Text, quantity) Then
            MessageBox.Show("Số lượng phải là số!")
            Return False
        End If

        Dim available As Integer
        If Not Integer.TryParse(txtAvailable.Text, available) Then
            MessageBox.Show("Số lượng khả dụng phải là số!")
            Return False
        End If

        If available > quantity Then
            MessageBox.Show("Số lượng khả dụng không thể lớn hơn tổng số lượng!")
            Return False
        End If

        Return True
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(txtBookID.Text) Then
            MessageBox.Show("Vui lòng chọn sách cần xóa!")
            Return
        End If

        If MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Try
            Dim query As String = "DELETE FROM Books WHERE BookID = @BookID"
            Dim parameters As New List(Of SqlParameter)()
            parameters.Add(New SqlParameter("@BookID", Convert.ToInt32(txtBookID.Text)))

            Dim result As Integer = DatabaseConnection.ExecuteNonQuery(query, parameters)

            If result > 0 Then
                MessageBox.Show("Xóa sách thành công!")
                LoadBooks()
                ClearFields()
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub dgvBooks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBooks.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvBooks.Rows(e.RowIndex)
            
            txtBookID.Text = row.Cells("BookID").Value.ToString()
            txtTitle.Text = row.Cells("Title").Value.ToString()
            txtAuthor.Text = If(row.Cells("Author").Value Is DBNull.Value, "", row.Cells("Author").Value.ToString())
            txtPublisher.Text = If(row.Cells("Publisher").Value Is DBNull.Value, "", row.Cells("Publisher").Value.ToString())
            txtYearPublished.Text = If(row.Cells("YearPublished").Value Is DBNull.Value, "", row.Cells("YearPublished").Value.ToString())
            txtGenre.Text = If(row.Cells("Genre").Value Is DBNull.Value, "", row.Cells("Genre").Value.ToString())
            txtQuantity.Text = row.Cells("Quantity").Value.ToString()
            txtAvailable.Text = row.Cells("Available").Value.ToString()
            
            btnSave.Text = "Cập nhật"
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            Dim searchText As String = txtSearch.Text.Trim()
            
            If String.IsNullOrEmpty(searchText) Then
                LoadBooks()
                Return
            End If
            
            Dim query As String = "SELECT * FROM Books WHERE Title LIKE @SearchText OR Author LIKE @SearchText OR Publisher LIKE @SearchText OR Genre LIKE @SearchText"
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
            
            dgvBooks.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message)
        End Try
    End Sub
End Class 