Public Class Form1
    Inherits Form

    Private btnViewSubmissions As Button
    Private btnCreateSubmission As Button

    Public Sub New()
        InitializeComponent1()
    End Sub

    Private Sub InitializeComponent1()
        Me.Text = "Somoprovo Bhattacharjee, Slidely Task 2"
        Me.Size = New System.Drawing.Size(350 * 1.5, 150 * 1.5)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        btnViewSubmissions = New Button()
        btnViewSubmissions.Text = "VIEW SUBMISSIONS (CTRL + V)"
        btnViewSubmissions.Location = New Point(50 * 1.5, 50)
        btnViewSubmissions.Size = New Size(230 * 1.5, 30)
        btnViewSubmissions.BackColor = Color.LightYellow ' Set background color
        AddHandler btnViewSubmissions.Click, AddressOf btnViewSubmissions_Click
        Me.Controls.Add(btnViewSubmissions)

        btnCreateSubmission = New Button()
        btnCreateSubmission.Text = "CREATE NEW SUBMISSION (CTRL + N)"
        btnCreateSubmission.Location = New Point(50 * 1.5, 100)
        btnCreateSubmission.Size = New Size(230 * 1.5, 30)
        btnCreateSubmission.BackColor = Color.LightBlue
        ' Set background color
        AddHandler btnCreateSubmission.Click, AddressOf btnCreateSubmission_Click
        Me.Controls.Add(btnCreateSubmission)
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs)
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.V) Then
            btnViewSubmissions.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.N) Then
            btnCreateSubmission.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
