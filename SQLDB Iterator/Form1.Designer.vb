<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnEx1 = New System.Windows.Forms.Button()
        Me.cboDB = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboDT = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tDataSource = New System.Windows.Forms.TextBox()
        Me.btnEx2 = New System.Windows.Forms.Button()
        Me.btnEx3 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnIterate = New System.Windows.Forms.Button()
        Me.btnClear1 = New System.Windows.Forms.Button()
        Me.btnClear2 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnEx1
        '
        Me.btnEx1.Location = New System.Drawing.Point(15, 60)
        Me.btnEx1.Name = "btnEx1"
        Me.btnEx1.Size = New System.Drawing.Size(75, 23)
        Me.btnEx1.TabIndex = 0
        Me.btnEx1.Text = "Execute"
        Me.btnEx1.UseVisualStyleBackColor = True
        '
        'cboDB
        '
        Me.cboDB.FormattingEnabled = True
        Me.cboDB.Location = New System.Drawing.Point(234, 34)
        Me.cboDB.Name = "cboDB"
        Me.cboDB.Size = New System.Drawing.Size(181, 21)
        Me.cboDB.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(234, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Databases:"
        '
        'cboDT
        '
        Me.cboDT.FormattingEnabled = True
        Me.cboDT.Location = New System.Drawing.Point(431, 34)
        Me.cboDT.Name = "cboDT"
        Me.cboDT.Size = New System.Drawing.Size(205, 21)
        Me.cboDT.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(431, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Datatables:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Data Source:"
        '
        'tDataSource
        '
        Me.tDataSource.Location = New System.Drawing.Point(15, 34)
        Me.tDataSource.Name = "tDataSource"
        Me.tDataSource.Size = New System.Drawing.Size(200, 20)
        Me.tDataSource.TabIndex = 4
        Me.tDataSource.Text = "MARCVX"
        '
        'btnEx2
        '
        Me.btnEx2.Location = New System.Drawing.Point(234, 60)
        Me.btnEx2.Name = "btnEx2"
        Me.btnEx2.Size = New System.Drawing.Size(75, 23)
        Me.btnEx2.TabIndex = 0
        Me.btnEx2.Text = "Execute"
        Me.btnEx2.UseVisualStyleBackColor = True
        '
        'btnEx3
        '
        Me.btnEx3.Location = New System.Drawing.Point(431, 60)
        Me.btnEx3.Name = "btnEx3"
        Me.btnEx3.Size = New System.Drawing.Size(75, 23)
        Me.btnEx3.TabIndex = 0
        Me.btnEx3.Text = "Execute"
        Me.btnEx3.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(15, 133)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(621, 423)
        Me.DataGridView1.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Data:"
        '
        'btnIterate
        '
        Me.btnIterate.Location = New System.Drawing.Point(96, 60)
        Me.btnIterate.Name = "btnIterate"
        Me.btnIterate.Size = New System.Drawing.Size(119, 23)
        Me.btnIterate.TabIndex = 0
        Me.btnIterate.Text = "Iterate to console"
        Me.btnIterate.UseVisualStyleBackColor = True
        '
        'btnClear1
        '
        Me.btnClear1.Location = New System.Drawing.Point(340, 60)
        Me.btnClear1.Name = "btnClear1"
        Me.btnClear1.Size = New System.Drawing.Size(75, 23)
        Me.btnClear1.TabIndex = 6
        Me.btnClear1.Text = "Clear"
        Me.btnClear1.UseVisualStyleBackColor = True
        '
        'btnClear2
        '
        Me.btnClear2.Location = New System.Drawing.Point(561, 60)
        Me.btnClear2.Name = "btnClear2"
        Me.btnClear2.Size = New System.Drawing.Size(75, 23)
        Me.btnClear2.TabIndex = 6
        Me.btnClear2.Text = "Clear"
        Me.btnClear2.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(656, 572)
        Me.Controls.Add(Me.btnClear2)
        Me.Controls.Add(Me.btnClear1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.tDataSource)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboDT)
        Me.Controls.Add(Me.cboDB)
        Me.Controls.Add(Me.btnEx3)
        Me.Controls.Add(Me.btnEx2)
        Me.Controls.Add(Me.btnIterate)
        Me.Controls.Add(Me.btnEx1)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SQL Server Iterator"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnEx1 As Button
    Friend WithEvents cboDB As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cboDT As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents tDataSource As TextBox
    Friend WithEvents btnEx2 As Button
    Friend WithEvents btnEx3 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents btnIterate As Button
    Friend WithEvents btnClear1 As Button
    Friend WithEvents btnClear2 As Button
End Class
