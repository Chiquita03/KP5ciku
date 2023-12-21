Imports System.Data.Odbc
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Public Class Form2
    Dim Conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim Da As OdbcDataAdapter
    Dim rd As OdbcDataReader
    Dim Ds As DataSet
    Dim MyDB As String

    Sub ketemu()
        On Error Resume Next
        TextBox4.Text = rd(0) '=kodemenu
        TextBox1.Text = rd(1) '=namamenu
        TextBox3.Text = rd(2) '=harga

    End Sub
    Sub carikode()
        cmd = New OdbcCommand("select * from tbmenu where kodemenu='" & TextBox4.Text & "'", Conn)
        rd = cmd.ExecuteReader
        rd.Read()

    End Sub

    Sub tampilgrid2()

        DataGridView1.ReadOnly = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill

    End Sub

    Sub conecc()
        MyDB = "Driver={Mysql ODBC 3.51 Driver};Database=cikukp;server=localhost;uid=root"
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
    End Sub
    Sub TAMPILDIGRID()
        Call conecc()
        Da = New OdbcDataAdapter("select * from tbmenu", Conn)
        Ds = New DataSet
        Da.Fill(Ds)
        DataGridView1.DataSource = Ds.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        Form3.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'KpcikuDataSet.menu' table. You can move, or remove it, as needed.

        Call TAMPILDIGRID()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Call conecc()
        Da = New OdbcDataAdapter("select * from tbmenu Order  by tbmenu.kodemenu DESC", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "tbmenu")
        DataGridView1.DataSource = (Ds.Tables("tbmenu"))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox4.Text = "" Or TextBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data Belum Lengkap..!", vbInformation, "GAGAL!")
        Else
            Call conecc()
            Dim ADD As String = "Insert into tbmenu values('" & TextBox4.Text & "','" & TextBox1.Text & "','" & TextBox3.Text & "')"
            cmd = New OdbcCommand(ADD, Conn)
            cmd.ExecuteNonQuery()
            MsgBox("Input Data Berhasil!..", vbInformation, "SUKSES!")
            Call kondisiawal()
            Call TAMPILDIGRID()
            Call tampilgrid2()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox4.Text = "" Or TextBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data Belum Lengkap..!", vbInformation, "GAGAL!")
        Else

            Call conecc()
            Dim EDIT As String = "Update tbmenu set namamenu='" & TextBox1.Text & "',harga='" & TextBox3.Text & "' where kodemenu= '" & TextBox4.Text & "'"
            cmd = New OdbcCommand(EDIT, Conn)
            cmd = New OdbcCommand(EDIT, Conn)
            cmd.ExecuteNonQuery()
            MsgBox("Edit Data Berhasil!..", vbInformation, "SUKSES!")
            Call kondisiawal()
            Call tampilgrid2()
            Call TAMPILDIGRID()
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Call carikode()
        If rd.HasRows Then
            Call ketemu()

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox4.Text = "" Or TextBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data Yang Akan Dihapus Belum Lengkap..!", vbInformation, "GAGAL!")
        Else
            If MessageBox.Show("Yakin Ingin Menghapus Data?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call conecc()
                Dim HAPUS As String = "delete from tbmenu where kodemenu= '" & TextBox4.Text & "'"
                cmd = New OdbcCommand(HAPUS, Conn)
                cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil!..", vbInformation, "SUKSES!")
                Call kondisiawal()
                Call tampilgrid2()
                Call TAMPILDIGRID()
            End If
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Da = New OdbcDataAdapter("Select * from tbmenu where namamenu like '%" & TextBox2.Text & "%' or kodemenu Like '%" & TextBox2.Text & "%'", Conn)
        Ds = New DataSet
        Da.Fill(Ds)
        DataGridView1.DataSource = Ds.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub
End Class