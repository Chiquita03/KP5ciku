Imports System.ComponentModel
Imports System.Data.Odbc
Imports System.Drawing.Printing
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Xml
Imports System.Xml.Schema
Public Class Form3

    Dim Conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim Da As OdbcDataAdapter
    Dim rd As OdbcDataReader
    Dim Ds As DataSet
    Dim MyDB As String
    Dim tharga As Long
    Dim tqty As Long
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog

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

    Sub hitung()
        Dim sum As Decimal = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            sum += DataGridView1.Rows(i).Cells(3).Value
        Next
        TextBox4.Text = sum
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Form1.Show()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ComboBox2.Items.Add("Small")
        Me.ComboBox2.Items.Add("Medium")
        Me.ComboBox2.Items.Add("Large")
        Me.ComboBox2.Items.Add("Jumbo")
        Call kondisiawal()
        Call tampilgrid2()
        With DataGridView1.ColumnCount = 3
            DataGridView1.Columns(0).Name = "kodemenu"
            DataGridView1.Columns(1).Name = "namamenu"
            DataGridView1.Columns(2).Name = "size"
            DataGridView1.Columns(3).Name = "harga"
        End With
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        DataGridView1.Rows.Add(TextBox1.Text, TextBox2.Text, ComboBox2.Text, TextBox3.Text)

        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        Call kondisiawal()
        Call hitung()
        TextBox1.Focus()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call conecc()
            cmd = New OdbcCommand("Select * from tbmenu where kodemenu ='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                TextBox2.Text = rd.Item("namamenu")
                TextBox3.Text = rd.Item("harga")
                TextBox1.Focus()
            Else
                MsgBox("Data Tidak Ada!")
            End If
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If Me.ComboBox2.Text = "Small" Then
            Me.TextBox3.Text += 0
        End If
        If Me.ComboBox2.Text = "Medium" Then
            Me.TextBox3.Text += 3000
        End If
        If Me.ComboBox2.Text = "Large" Then
            Me.TextBox3.Text += 5000
        End If
        If Me.ComboBox2.Text = "Jumbo" Then
            Me.TextBox3.Text += 6000
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox5.Text < TextBox4.Text Then
            TextBox5.Text = ""
            MsgBox("Maaf, Uang Anda Kurang...!", vbInformation, "Gagal!")
        Else
            TextBox6.Text = Val(TextBox5.Text) - Val(TextBox4.Text)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PPD.Document = PD
        PPD.ShowDialog()
        'PD.Print()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim f10 As New Font("Times New Roman", 10, FontStyle.Regular)
        Dim f10b As New Font("Times New Roman", 10, FontStyle.Bold)
        Dim f14 As New Font("Times New Roman", 10, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        Dim kanan As New StringFormat
        Dim tengah As New StringFormat
        kanan.Alignment = StringAlignment.Far
        tengah.Alignment = StringAlignment.Center

        Dim garis As String
        garis = "----------------------------------------------------------------------------------------------------------------------------------------------------------------"
        Dim logo As Image = My.Resources.ResourceManager.GetObject("WhatsApp_Image_2023-12-21_at_7.48.48_AM-removebg-preview.png")
        e.Graphics.DrawImage(logo, CInt((e.PageBounds.Width - 485) / 2), 8, 80, 60)
        e.Graphics.DrawString("Chicream", f14, Brushes.Black, centermargin, 10, tengah)
        e.Graphics.DrawString("Mangunjaya, Tambun Selatan Jl. Kartini VII, ", f10, Brushes.Black, centermargin, 35, tengah)
        e.Graphics.DrawString("Gandamekar, Kec. Tambun Selatan Bar., Kabupaten Bekasi.", f10, Brushes.Black, centermargin, 50, tengah)
        e.Graphics.DrawString(" No : (021)8980678", f10, Brushes.Black, centermargin, 65, tengah)
        e.Graphics.DrawString(garis, f10, Brushes.Black, centermargin, 95, tengah)
        e.Graphics.DrawString("Nama", f10b, Brushes.Black, 0, 130)
        e.Graphics.DrawString("Size", f10b, Brushes.Black, 225, 130)
        e.Graphics.DrawString("Harga", f10b, Brushes.Black, 300, 130)
        DataGridView1.AllowUserToAddRows = False
        Dim tinggi As Integer
        For baris As Integer = 0 To DataGridView1.RowCount - 1
            tinggi += 20
            e.Graphics.DrawString(DataGridView1.Rows(baris).Cells(1).Value.ToString, f10, Brushes.Black, 0, 130 + tinggi)
            e.Graphics.DrawString(DataGridView1.Rows(baris).Cells(2).Value.ToString, f10, Brushes.Black, 225, 130 + tinggi)
            e.Graphics.DrawString(DataGridView1.Rows(baris).Cells(3).Value.ToString, f10, Brushes.Black, 300, 130 + tinggi)

        Next
        tinggi = 170 + tinggi
        Call hitot()
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString(tqty, f10b, Brushes.Black, rightmargin, 10 + tinggi)
        e.Graphics.DrawString("Total : Rp. " & Format(tharga, "##,###,00"), f10b, Brushes.Black, rightmargin, 10 + tinggi, kanan)
        e.Graphics.DrawString("TERIMA KASIH", f14, Brushes.Black, centermargin, 70 + tinggi, tengah)
    End Sub

    Sub hitot()
        Dim hitung As Long = 0
        For baris As Long = 0 To DataGridView1.RowCount - 1
            hitung = hitung + DataGridView1.Rows(baris).Cells(3).Value
        Next
        tharga = hitung

        Dim hitung2 As Long = 0
        For baris As Long = 0 To DataGridView1.RowCount - 1
            hitung2 = hitung2 + DataGridView1.Rows(baris).Cells(3).Value
        Next
        tqty = hitung2
    End Sub

    Private Sub PrintDocument1_BeginPrint(sender As Object, e As PrintEventArgs) Handles PrintDocument1.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("custom", 500, 500)
        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f10 As New Font("Times New Roman", 10, FontStyle.Regular)
        Dim f10b As New Font("Times New Roman", 10, FontStyle.Bold)
        Dim f14 As New Font("Times New Roman", 10, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        Dim kanan As New StringFormat
        Dim tengah As New StringFormat
        kanan.Alignment = StringAlignment.Far
        tengah.Alignment = StringAlignment.Center

        Dim garis As String
        garis = "----------------------------------------------------------------------------------------------------------------------------------------------------------------"
        Dim logo As Image = My.Resources.ResourceManager.GetObject("logociku")
        e.Graphics.DrawImage(logo, CInt((e.PageBounds.Width - 485) / 2), 8, 80, 60)
        e.Graphics.DrawString("Chicream", f14, Brushes.Black, centermargin, 10, tengah)
        e.Graphics.DrawString("Mangunjaya, Tambun Selatan Jl. Kartini VII, ", f10, Brushes.Black, centermargin, 35, tengah)
        e.Graphics.DrawString("Gandamekar, Kec. Tambun Selatan Bar., Kabupaten Bekasi.", f10, Brushes.Black, centermargin, 50, tengah)
        e.Graphics.DrawString(" No : (021)8980678", f10, Brushes.Black, centermargin, 65, tengah)
        e.Graphics.DrawString(garis, f10, Brushes.Black, centermargin, 95, tengah)
        e.Graphics.DrawString("Nama", f10b, Brushes.Black, 0, 130)
        e.Graphics.DrawString("Size", f10b, Brushes.Black, 225, 130)
        e.Graphics.DrawString("Harga", f10b, Brushes.Black, 450, 130)
        DataGridView1.AllowUserToAddRows = False
        Dim tinggi As Integer
        For baris As Integer = 0 To DataGridView1.RowCount - 1
            tinggi += 20
            e.Graphics.DrawString(DataGridView1.Rows(baris).Cells(1).Value.ToString, f10, Brushes.Black, 0, 130 + tinggi)
            e.Graphics.DrawString(DataGridView1.Rows(baris).Cells(2).Value.ToString, f10, Brushes.Black, 225, 130 + tinggi)
            e.Graphics.DrawString(DataGridView1.Rows(baris).Cells(3).Value.ToString, f10, Brushes.Black, 450, 130 + tinggi)

        Next
        tinggi = 170 + tinggi
        Call hitot()
        e.Graphics.DrawString(garis, f10, Brushes.Black, 0, tinggi)
        e.Graphics.DrawString(tqty, f10b, Brushes.Black, rightmargin, 10 + tinggi)
        e.Graphics.DrawString("Total : Rp. " & Format(tharga, "##,###,00"), f10b, Brushes.Black, rightmargin, 10 + tinggi, kanan)
        e.Graphics.DrawString("TERIMA KASIH", f14, Brushes.Black, centermargin, 70 + tinggi, tengah)
    End Sub

    Private Sub PD_BeginPrint(sender As Object, e As PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("custom", 500, 500)
        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        DataGridView1.Rows.Clear()
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form2.Show()
    End Sub
End Class