Public Class Form1
    Dim id = "0"
    Dim aksi = False

    Sub awal()
        DataGridView1.DataSource = getData("select * from transaksi where pembeli like '%" & TextBox1.Text & "%'")
        clearForm(GroupBox2)
        DataGridView1.Columns(1).HeaderText = "Penjual"
        DataGridView1.Columns(2).HeaderText = "Barang"
        DataGridView1.Columns(3).HeaderText = "Harga"
        DataGridView1.Columns(4).HeaderText = "Jumlah"
        DataGridView1.Columns(5).HeaderText = "Total"
        GroupBox1.Enabled = True
        GroupBox2.Enabled = False
        GroupBox3.Enabled = True
        id = "0"
    End Sub

    Sub buka()
        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        GroupBox3.Enabled = False
    End Sub

    Sub TGL()
        Dim tglawal = 32
        Dim tglakhir = 0
        Dim bln = Date.Now.Month
        Dim thn = Date.Now.Year
        Dim awal = thn & "-0" & (bln + 1) & "-" & tglawal
        Dim akhir = thn & "-0" & (bln - 1) & "-" & tglakhir
        MsgBox(awal & " - " & akhir)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        awal()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clearForm(GroupBox2)
        buka()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If id = "0" Then
            MsgBox("Pilih data dulu")
        Else
            If dialog("Apakah anda yakin ingin menghapus?") Then
                Dim sql = "delete from transaksi where id =" & id
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox2)
                awal()
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If id = "0" Then
            MsgBox("Pilih data dulu")
        Else
            aksi = True
            buka()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        awal()
        clearForm(GroupBox2)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If TextBox4.Text = "" Then
            TextBox5.Text = "0"
        Else
            TextBox5.Text = (Double.Parse(TextBox4.Text) * NumericUpDown1.Value).ToString
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text = "" Then
            TextBox5.Text = "0"
        Else
            TextBox5.Text = (Double.Parse(TextBox4.Text) * NumericUpDown1.Value).ToString
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        awal()
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        onlyNumber(e)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If adaKosong(GroupBox2) Then
            MsgBox("Ada data kosong")
        Else
            Dim sql
            If Not aksi Then
                sql = "insert into transaksi values('" & TextBox2.Text & "','" & TextBox3.Text & "',
                '" & TextBox4.Text & "','" & NumericUpDown1.Value & "','" & TextBox5.Text & "')"
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox2)
                MsgBox("Data berhasil ditambah")
                awal()
            Else
                sql = "update transaksi set pembeli = '" & TextBox2.Text & "', barang = '" & TextBox3.Text & "',
                harga = '" & TextBox4.Text & "',jumlah = '" & NumericUpDown1.Value & "',
                total = '" & TextBox5.Text & "' where id = " & id
                'MsgBox(sql)
                exc(sql)
                clearForm(GroupBox2)
                MsgBox("Data berhasil diubah")
                awal()

            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            NumericUpDown1.Value = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value

            id = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            MsgBox(id)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        Form2.ShowDialog()
    End Sub
End Class
