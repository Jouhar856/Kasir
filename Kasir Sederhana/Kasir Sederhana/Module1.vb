Imports System.Data.SqlClient
Imports System.Net.Mail
Module Module1

    Public conn As SqlConnection
    Public da As SqlDataAdapter
    Public dr As SqlDataReader
    Public dt As DataTable

    Public cmd As SqlCommand

    'untuk koneksi database data source = "nama server", catalog = "nama database"
    Sub koneksi()
        conn = New SqlConnection("Data Source=DESKTOP-02K9FKP\SQLEXPRESS;
                    Initial Catalog=kasir;Integrated Security=True")
        If conn.State = conn.State.Closed Then
            conn.Open()
        End If
    End Sub

    'menutup koneksi database
    Sub closeKoneksi()
        conn.Close()
        cmd.Dispose()
    End Sub

    'untuk menampilkan tabel
    Function getData(s As String)
        koneksi()
        Try
            cmd = New SqlCommand(s, conn)
            da = New SqlDataAdapter
            dt = New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        Finally
            closeKoneksi()
        End Try
    End Function

    'untuk menampilkan jumlah data
    Function getCount(s As String)
        koneksi()
        Try
            cmd = New SqlCommand(s, conn)
            da = New SqlDataAdapter
            dt = New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt.Rows.Count
        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0
        Finally
            closeKoneksi()
        End Try
    End Function

    'untuk menampilkan data yang diinginkan
    Function getValue(s As String, col As String)
        koneksi()
        Try
            cmd = New SqlCommand(s, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                If IsDBNull(dr.Item(col)) Then
                    Return ""
                Else
                    Return dr.Item(col)
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        Finally
            closeKoneksi()
        End Try
    End Function

    'eksekusi kode
    Function exc(s As String)
        koneksi()
        Try
            cmd = New SqlCommand(s, conn)
            cmd.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            closeKoneksi()
        End Try
    End Function

    'untuk mengecek apakah format email sudah benar
    Function isEmail(s As String)
        Try
            Dim a = New MailAddress(s)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'untuk groupbox apakah datanya kosong
    Function adaKosong(gb As GroupBox)
        Dim status = False
        For Each ct As Control In gb.Controls
            If TypeOf ct Is TextBox Then
                If CType(ct, TextBox).Text = "" Then
                    status = True
                    Exit For
                End If
            End If
            If TypeOf ct Is PictureBox Then
                If CType(ct, PictureBox).ImageLocation = Nothing Then
                    status = True
                    Exit For
                End If
            End If
        Next
        Return status
    End Function

    'untuk menghapus data digroupbox
    Function clearForm(gb As GroupBox)
        For Each ct As Control In gb.Controls
            If TypeOf ct Is TextBox Then
                CType(ct, TextBox).Text = ""
            End If
            If TypeOf ct Is PictureBox Then
                CType(ct, PictureBox).ImageLocation = Nothing
            End If
            If TypeOf ct Is NumericUpDown Then
                CType(ct, NumericUpDown).Value = 0
            End If
            If TypeOf ct Is ComboBox Then
                CType(ct, ComboBox).SelectedIndex = 0
            End If
        Next
    End Function

    'untuk textbox agar berisikan angka saja
    Function onlyNumber(e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Function

    'pesan konfirmasi
    Function dialog(s As String)
        Dim a As DialogResult = MessageBox.Show(s, "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If a = DialogResult.Yes Then
            Return True
        End If
    End Function

End Module
