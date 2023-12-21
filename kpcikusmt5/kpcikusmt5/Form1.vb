Imports System.Data.Odbc

Public Class Form1
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Username atau Password kosong!", vbInformation, "Gagal!")
        ElseIf TextBox1.Text = "Admin" And TextBox2.Text = "12345" Then
            MsgBox("Selamat Login Anda Berhasil!", vbInformation, "Berhasil!")
            Me.Hide()
            Form2.Show()
            TextBox1.Clear()
            TextBox2.Clear()
        Else
            MsgBox("username atau password salah!", vbInformation, "Gagal!")
            TextBox1.Clear()
            TextBox2.Clear()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
