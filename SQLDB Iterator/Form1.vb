Option Explicit On
Imports System.Data.SqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Async Sub btn_Click(sender As Object, e As EventArgs) Handles btnEx1.Click, btnEx3.Click, btnEx2.Click, btnClear1.Click, btnClear2.Click

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
                Using cmd As New SqlCommand("SELECT [name] FROM sys.databases d WHERE d.database_id > 4", con)  'We want the user database only
                    With cmd
                        .CommandType = CommandType.Text
                        Using sReader As SqlDataReader = Await cmd.ExecuteReaderAsync
                            If sReader.HasRows Then
                                r_DBs = New List(Of String)
                                While Await sReader.ReadAsync
                                    r_DBs.Add(sReader("name"))
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

    Private Async Sub btnIterate_Click(sender As Object, e As EventArgs) Handles btnIterate.Click

        Dim ds As String = tDataSource.Text.Trim
        If String.IsNullOrWhiteSpace(ds) Then
            MsgBox("Data Source is required", vbApplicationModal + vbExclamation, "Oops!")
            tDataSource.Select()
            Exit Sub
        End If

        If MsgBox("This will iterate through all databases and all tables and get the number of rows of each table." & vbNewLine & "Do you want to proceed?",
                  vbApplicationModal + vbQuestion + vbYesNo, "Confirm") = vbNo Then
            Exit Sub
        End If

        'Build the output datatable
        Dim outputDT As New DataTable
        With outputDT.Columns
            .Add("sn", GetType(String))
            .Add("Database", GetType(String))
            .Add("Table", GetType(String))
            .Add("Rows", GetType(Integer))
        End With

        btnIterate.Enabled = False
        Try
            'Step 1: Get all DBs
            Dim DBs = Await Get_All_Database_Async(ds)
            If Not DBs.Item1 Then
                MsgBox(DBs.Item3, vbApplicationModal + vbExclamation, "Error")
            Else
                Dim sn As Integer = 0

                'Step 2: Loop thru all the DBs and get their tables with their row count
                For Each db As String In DBs.Item2

                    Dim DTs_TtlRows = Await Get_All_Tables_and_Total_Rows_Async(ds, db, "sa", "P4ssw0rd")
                    If Not DTs_TtlRows.Item1 Then
                        MsgBox(DTs_TtlRows.Item3, vbApplicationModal + vbExclamation, "Error")
                    Else

                        'Step 3: We can actually use merge here, but i want to import row by row for my SNs
                        For Each dtR As DataRow In DTs_TtlRows.Item2.Rows

                            Dim dt As String = dtR("TableName")
                            Dim ttl As Integer = dtR("TotalRowCount")

                            Debug.Print($"sn: {sn}")
                            Debug.Print($"db: {db}")
                            Debug.Print($"dt: {dt}")
                            Debug.Print($"ttl: {ttl}")
                            Debug.Print("")

                            sn += 1
                            outputDT.Rows.Add({sn, db, dt, ttl})
                        Next

                    End If
                Next

                DataGridView1.DataSource = Nothing
                DataGridView1.DataSource = outputDT

            End If
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try


        btnIterate.Enabled = True

    End Sub

    Private Async Function Get_All_Tables_and_Total_Rows_Async(ByVal dataSrc As String, ByVal dataBase As String, ByVal usr As String, ByVal pwd As String) As Task(Of Tuple(Of Boolean, DataTable, String))

        Dim r_Success As Boolean = False
        Dim r_DT As DataTable = Nothing
        Dim r_Error As String = String.Empty

        Dim qry As String = "SELECT SCHEMA_NAME(schema_id) AS [SchemaName],
                            [Tables].name AS [TableName],
                            SUM([Partitions].[rows]) AS [TotalRowCount]
                            FROM sys.tables AS [Tables]
                            JOIN sys.partitions AS [Partitions]
                            ON [Tables].[object_id] = [Partitions].[object_id]
                            AND [Partitions].index_id IN ( 0, 1 )
                            -- WHERE [Tables].name = N'name of the table'
                            GROUP BY SCHEMA_NAME(schema_id), [Tables].name;"
        Try
            Using con As New SqlConnection($"Data Source={dataSrc};Initial Catalog={dataBase};User ID={usr};Password={pwd}")
                Await con.OpenAsync
                Using cmd As New SqlCommand(qry, con)
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

    Private Sub llClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llClear.LinkClicked
        DataGridView1.DataSource = Nothing
    End Sub

    Private Async Sub btnDump_Click(sender As Object, e As EventArgs) Handles btnDump.Click
        Dim ds As String = tDataSource.Text.Trim
        If String.IsNullOrWhiteSpace(ds) Then
            MsgBox("Data Source is required", vbApplicationModal + vbExclamation, "Oops!")
            tDataSource.Select()
            Exit Sub
        End If

        'Build the output datatable
        Dim outputDT As New DataTable
        With outputDT.Columns
            .Add("sn", GetType(String))
            .Add("Database", GetType(String))
            .Add("Table", GetType(String))
            .Add("Rows", GetType(Integer))
        End With

        btnDump.Enabled = False
        Try
            'Step 1: Get all DBs
            Dim dtALL = Await Get_All_UserDB_All_UserTables_and_Total_Rows_Async(ds)
            If Not dtALL.Item1 Then
                MsgBox(dtALL.Item3, vbApplicationModal + vbExclamation, "Error")
            Else
                Dim sn As Integer = 0

                'Step 3: We can actually use merge here, but i want to import row by row for my SNs
                For Each dtR As DataRow In dtALL.Item2.Rows

                    Dim db As String = dtR("databaseName")
                    Dim dt As String = dtR("TableName")
                    Dim ttl As Integer = dtR("TotalRowCount")

                    sn += 1
                    outputDT.Rows.Add({sn, db, dt, ttl})
                Next

                DataGridView1.DataSource = Nothing
                DataGridView1.DataSource = outputDT

            End If

        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

        btnDump.Enabled = True
    End Sub

    Private Async Function Get_All_UserDB_All_UserTables_and_Total_Rows_Async(ByVal dataSrc As String) As Task(Of Tuple(Of Boolean, DataTable, String))

        Dim r_Success As Boolean = False
        Dim r_DT As DataTable = Nothing
        Dim r_Error As String = String.Empty

        Dim qry As String = "DECLARE @TableRowCounts TABLE (
								[databaseName] Varchar(100),
								[TableName] VARCHAR(128),
								[TotalRowCount] INT
							);
                            INSERT INTO @TableRowCounts ([databaseNAme],[TableName],[TotalRowCount])
                            EXEC sp_msforeachdb 'SELECT ""?"" AS db, [Tables].name AS [TableName], SUM([Partitions].[rows]) AS [TotalRowCount]
					                             FROM [?].sys.tables AS [Tables]
					                             JOIN [?].sys.partitions AS [Partitions]
					                             ON [Tables].[object_id] = [Partitions].[object_id]
					                             AND [Partitions].index_id IN ( 0, 1 )
					                             GROUP BY [Tables].name;';						
                            Select * From @TableRowCounts 
                            WHERE databaseName IN (SELECT [name] FROM sys.databases d WHERE d.database_id > 4)
                            ORDER by databaseNAme ASC, TableName ASC"
        Try
            Using con As New SqlConnection($"Data Source={dataSrc};Integrated Security=True")
                Await con.OpenAsync
                Using cmd As New SqlCommand(qry, con)
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
