Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Quản Lý Thư Viện - Menu Chính"
    End Sub
    
    Private Sub btnBooks_Click(sender As Object, e As EventArgs) Handles btnBooks.Click
        Dim formBooks As New FormBooks()
        formBooks.ShowDialog()
    End Sub
    
    Private Sub btnReaders_Click(sender As Object, e As EventArgs) Handles btnReaders.Click
        Dim formReaders As New FormReaders()
        formReaders.ShowDialog()
    End Sub
    
    Private Sub btnBorrow_Click(sender As Object, e As EventArgs) Handles btnBorrow.Click
        Dim formBorrow As New FormBorrowRecords()
        formBorrow.ShowDialog()
    End Sub
    
    Private Sub btnLibrarians_Click(sender As Object, e As EventArgs) Handles btnLibrarians.Click
        Dim formLibrarians As New FormLibrarians()
        formLibrarians.ShowDialog()
    End Sub
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MessageBox.Show("Bạn có muốn thoát chương trình không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
