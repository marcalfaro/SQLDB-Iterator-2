Option Explicit On
Imports System.Data.SqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Async Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnEx1.Click, btnEx3.Click, btnEx2.Click, btnIterate.Click, btnClear1.Click, btnClear2.Click

        Dim btn As String = CType(sender, Button).Name
        Select Case btn
            Case btnClear1.Name : cboDB.DataSource = Nothing
            Case btnClear2.Name : cboDT.DataSource = Nothing

            Case Else
                Dim ds As String = tDataSource.Text.Trim
                Dim db As String = cboDB.Text.Trim
                Dim dt As String = cboDT.Text.Trim

                Select Case btn
                    Case btnEx1.Name    'Get all DBs here

#Region " List all Databases "
                        If String.IsNullOrWhiteSpace(ds) Then
                            MsgBox("Data Source is required", vbApplicationModal + vbExclamation, "Oops!")
                            tDataSource.Select()
                            Exit Sub
                        End If

                        btnEx1.Enabled = False
                        Dim rslt = Await Get_All_Database_Async(ds)
                        If Not rslt.Item1 Then
                            MsgBox(rslt.Item3, vbApplicationModal + vbExclamation, "Error")
                        Else
                            cboDB.DataSource = Nothing
                            cboDB.DataSource = rslt.Item2
                        End If
                        btnEx1.Enabled = True
#End Region

                    Case btnEx2.Name    'Get all Tables here

#Region " List all tables in specified database "
                        If String.IsNullOrWhiteSpace(ds) Then
                            MsgBox("Data Source is required", vbApplicationModal + vbExclamation, "Oops!")
                            tDataSource.Select()
                            Exit Sub
                        End If

                        If String.IsNullOrWhiteSpace(db) Then
                            MsgBox("Database is required", vbApplicationModal + vbExclamation, "Oops!")
                            cboDB.Select()
                            Exit Sub
                        End If

                        btnEx2.Enabled = False
                        Dim rslt = Await Get_All_DataTables_Async(ds, db)
                        If Not rslt.Item1 Then
                            MsgBox(rslt.Item3, vbApplicationModal + vbExclamation, "Error")
                        Else
                            cboDT.DataSource = Nothing
                            cboDT.DataSource = rslt.Item2
                        End If
                        btnEx2.Enabled = True
#End Region

                    Case btnEx3.Name

#Region " Get data from table from specified database "
                        If String.IsNullOrWhiteSpace(ds) Then
                            MsgBox("Data Source is required", vbApplicationModal + vbExclamation, "Oops!")
                            tDataSource.Select()
                            Exit Sub
                        End If

                        If String.IsNullOrWhiteSpace(db) Then
                            MsgBox("Database is required", vbApplicationModal + vbExclamation, "Oops!")
                            cboDB.Select()
                            Exit Sub
                        End If

                        If String.IsNullOrWhiteSpace(dt) Then
                            MsgBox("Target Table is required", vbApplicationModal + vbExclamation, "Oops!")
                            cboDT.Select()
                            Exit Sub
                        End If

                        btnEx3.Enabled = False
                        Dim rslt = Await Get_All_Data_Async(ds, db, dt, "sa", "P4ssw0rd")
                        If Not rslt.Item1 Then
                            MsgBox(rslt.Item3, vbApplicationModal + vbExclamation, "Error")
                        Else
                            DataGridView1.DataSource = Nothing
                            DataGridView1.DataSource = rslt.Item2
                        End If
                        btnEx3.Enabled = True
#End Region

                    Case btnIterate.Name
                        btnIterate.Enabled = False

                        btnIterate.Enabled = True

                End Select
        End Select

    End Sub

    Private Function Get_All_Database(ByVal dataSrc As String) As Tuple(Of Boolean, List(Of String), String)

        Dim r_Success As Boolean = False
        Dim r_DBs As List(Of String) = Nothing
        Dim r_Error As String = String.Empty

        Try
            'There are 3 queries to do this:
            '1. EXEC sp_databases
            '2. SELECT name FROM master.sys.databases
            '3. SELECT * FROM sys.databases d WHERE d.database_id > 4

            Using con As New SqlConnection($"Data Source={dataSrc};Integrated Security=True")
                con.Open()
                Using cmd As New SqlCommand("sp_databases", con)
                    With cmd
                        .CommandType = CommandType.StoredProcedure
                        Using sReader As SqlDataReader = cmd.ExecuteReader
                            If sReader.HasRows Then
                                r_DBs = New List(Of String)
                                While sReader.Read
                                    r_DBs.Add(sReader("DATABASE_NAME"))
                                End While
                            End If
                            r_Success = True
                        End Using
                    End With
                End Using
                con.Close()
            End Using
        Catch ex As Exception
            r_Error = ex.Message
        End Try

        Return New Tuple(Of Boolean, List(Of String), String)(r_Success, r_DBs, r_Error)
    End Function

    Private Async Function Get_All_Database_Async(ByVal dataSrc As String) As Task(Of Tuple(Of Boolean, List(Of String), String))

        Dim r_Success As Boolean = False
        Dim r_DBs As List(Of String) = Nothing
        Dim r_Error As String = String.Empty

        Try
            'There are 3 queries to do this:
            '1. EXEC sp_databases
            '2. SELECT name FROM master.sys.databases
            '3. SELECT * FROM sys.databases d WHERE d.database_id > 4

            Using con As New SqlConnection($"Data Source={dataSrc};Integrated Security=True")
                Await con.OpenAsync
                Using cmd As New SqlCommand("sp_databases", con)
                    With cmd
                        .CommandType = CommandType.StoredProcedure
                        Using sReader As SqlDataReader = Await cmd.ExecuteReaderAsync
                            If sReader.HasRows Then
                                r_DBs = New List(Of String)
                                While Await sReader.ReadAsync
                                    r_DBs.Add(sReader("DATABASE_NAME"))
                                End While
                            End If
                            r_Success = True
                        End Using
                    End With
                End Using
                con.Close()
            End Using
        Catch ex As Exception
            r_Error = ex.Message
        End Try

        Return New Tuple(Of Boolean, List(Of String), String)(r_Success, r_DBs, r_Error)
    End Function

    Private Async Function Get_All_DataTables_Async(ByVal dataSrc As String, ByVal dataBase As String) As Task(Of Tuple(Of Boolean, List(Of String), String))

        Dim r_Success As Boolean = False
        Dim r_DTs As List(Of String) = Nothing
        Dim r_Error As String = String.Empty

        Try
            Using con As New SqlConnection($"Data Source={dataSrc};Database={dataBase};Integrated Security=True")
                Await con.OpenAsync
                Using cmd As New SqlCommand("SELECT * FROM INFORMATION_SCHEMA.tables", con)
                    With cmd
                        .CommandType = CommandType.Text
                        Using sReader As SqlDataReader = Await cmd.ExecuteReaderAsync
                            If sReader.HasRows Then
                                r_DTs = New List(Of String)
                                While Await sReader.ReadAsync
                                    r_DTs.Add(sReader("TABLE_NAME"))
                                End While
                            End If
                            r_Success = True
                        End Using
                    End With
                End Using
                con.Close()
            End Using
        Catch ex As Exception
            r_Error = ex.Message
        End Try

        Return New Tuple(Of Boolean, List(Of String), String)(r_Success, r_DTs, r_Error)
    End Function

    Private Async Function Get_All_Data_Async(ByVal dataSrc As String, ByVal dataBase As String, ByVal dataTable As String, ByVal usr As String, ByVal pwd As String) As Task(Of Tuple(Of Boolean, DataTable, String))

        Dim r_Success As Boolean = False
        Dim r_DT As DataTable = Nothing
        Dim r_Error As String = String.Empty

        Try
            Using con As New SqlConnection($"Data Source={dataSrc};Initial Catalog={dataBase};User ID={usr};Password={pwd}")
                Await con.OpenAsync
                Using cmd As New SqlCommand($"SELECT * FROM {dataTable}", con)
                    With cmd
                        .CommandType = CommandType.Text
                        Using sReader As SqlDataReader = Await cmd.ExecuteReaderAsync
                            If sReader.HasRows Then
                                r_DT = New DataTable
                                r_DT.Load(sReader)
                            End If
                            r_Success = True
                        End Using
                    End With
                End Using
                con.Close()
            End Using
        Catch ex As Exception
            r_Error = ex.Message
        End Try

        Return New Tuple(Of Boolean, DataTable, String)(r_Success, r_DT, r_Error)
    End Function

End Class
