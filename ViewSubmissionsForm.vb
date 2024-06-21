Imports System.Net.Http
Imports Newtonsoft.Json
Public Class ViewSubmissionsForm
    Inherits Form

    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhone As TextBox
    Private txtGithubLink As TextBox
    Private txtStopwatch As TextBox
    Private btnPrevious As Button
    Private btnNext As Button

    Private currentIndex As Integer = 0
    Private submissions As New List(Of Submission)()

    Public Sub New()
        InitializeComponent1()
        LoadSubmissions()
        DisplaySubmission()
    End Sub

    Private Sub InitializeComponent1()
        Me.Text = "John Doe, Slidely Task 2 - View Submissions"
        Me.Size = New System.Drawing.Size(400 * 1.5, 400)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        Dim lblName As New Label()
        lblName.Text = "Name"
        lblName.Location = New Point(50, 30)
        Me.Controls.Add(lblName)

        txtName = New TextBox()
        txtName.Location = New Point(150 * 1.5, 30)
        txtName.Size = New Size(200, 20)
        txtName.BackColor = Color.LightGray
        txtName.ReadOnly = True
        Me.Controls.Add(txtName)

        Dim lblEmail As New Label()
        lblEmail.Text = "Email"
        lblEmail.Location = New Point(50, 70)
        Me.Controls.Add(lblEmail)

        txtEmail = New TextBox()
        txtEmail.Location = New Point(150 * 1.5, 70)
        txtEmail.Size = New Size(200, 20)
        txtEmail.ReadOnly = True
        txtEmail.BackColor = Color.LightGray
        Me.Controls.Add(txtEmail)

        Dim lblPhone As New Label()
        lblPhone.Text = "Phone Num"
        lblPhone.Location = New Point(50, 110)
        Me.Controls.Add(lblPhone)

        txtPhone = New TextBox()
        txtPhone.Location = New Point(150 * 1.5, 110)
        txtPhone.Size = New Size(200, 20)
        txtPhone.ReadOnly = True
        txtPhone.BackColor = Color.LightGray
        Me.Controls.Add(txtPhone)

        Dim lblGithubLink As New Label()
        lblGithubLink.Text = "Github Link For Task 2"
        lblGithubLink.Location = New Point(50, 150)
        Me.Controls.Add(lblGithubLink)

        txtGithubLink = New TextBox()
        txtGithubLink.Location = New Point(150 * 1.5, 150)
        txtGithubLink.Size = New Size(200, 20)
        txtGithubLink.ReadOnly = True
        txtGithubLink.BackColor = Color.LightGray
        Me.Controls.Add(txtGithubLink)

        Dim lblStopwatch As New Label()
        lblStopwatch.Text = "Stopwatch time"
        lblStopwatch.Location = New Point(50, 190)
        Me.Controls.Add(lblStopwatch)

        txtStopwatch = New TextBox()
        txtStopwatch.Location = New Point(150 * 1.5, 190)
        txtStopwatch.Size = New Size(200, 20)
        txtStopwatch.ReadOnly = True
        txtStopwatch.BackColor = Color.LightGray
        Me.Controls.Add(txtStopwatch)

        btnPrevious = New Button()
        btnPrevious.Text = "PREVIOUS (CTRL + P)"
        btnPrevious.Location = New Point(50 * 1.5, 240)
        btnPrevious.Size = New Size(100 * 2, 30)
        btnPrevious.BackColor = Color.LightYellow  ' Set background color
        AddHandler btnPrevious.Click, AddressOf btnPrevious_Click
        Me.Controls.Add(btnPrevious)

        btnNext = New Button()
        btnNext.Text = "NEXT (CTRL + N)"
        btnNext.Location = New Point(275, 240)
        btnNext.Size = New Size(100 * 2, 30)
        btnNext.BackColor = Color.LightBlue  ' Set background color
        AddHandler btnNext.Click, AddressOf btnNext_Click
        Me.Controls.Add(btnNext)
    End Sub

    Private Sub LoadSubmissions()
        ' API call to load submissions
        Try
            Dim httpClient As New HttpClient()
            Dim response As HttpResponseMessage = httpClient.GetAsync("http://localhost:3000/read").Result

            If response.IsSuccessStatusCode Then
                Dim jsonString As String = response.Content.ReadAsStringAsync().Result
                submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(jsonString)
            Else
                MessageBox.Show("Failed to retrieve submissions. Status code: " & response.StatusCode)
            End If
        Catch ex As HttpRequestException
            MessageBox.Show("Error sending request to the server: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred: " & ex.Message)
        End Try
    End Sub

    Private Sub DisplaySubmission()
        If submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhone.Text = submission.Phone
            txtGithubLink.Text = submission.GithubLink
            txtStopwatch.Text = submission.StopwatchTime
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.N) Then
            btnNext.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.P) Then
            btnPrevious.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
