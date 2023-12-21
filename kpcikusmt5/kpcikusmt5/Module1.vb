Imports System.Data.Odbc
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Module Module1
    Dim Conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim Da As OdbcDataAdapter
    Dim rd As OdbcDataReader
    Dim Ds As DataSet
    Dim MyDB As String

    Sub conecc()
        MyDB = "Driver={Mysql ODBC 3.51 Driver};Database=cikukp;server=localhost;uid=root"
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
    End Sub
End Module
