Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text
Public Class CreateSubmissionForm
    Inherits Form

    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhone As TextBox
    Private txtGithubLink As TextBox
    Private txtStopwatch As TextBox
    Private btnToggleStopwatch As Button
    Private btnSubmit As Button
    Private stopwatch As New Stopwatch()

    Public Sub New()
        InitializeComponent2()
    End Sub

    Private Sub InitializeComponent2()
        Me.Text = "John Doe, Slidely Task 2 - Create Submission"
        Me.Size = New System.Drawing.Size(400 * 1.5, 400)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        Dim lblName As New Label()
        lblName.Text = "Name"
        lblName.Location = New Point(50, 30)
        Me.Controls.Add(lblName)

        txtName = New TextBox()
        txtName.Location = New Point(150 * 1.5, 30)
        txtName.Size = New Size(200 * 1.5, 20)
        Me.Controls.Add(txtName)

        Dim lblEmail As New Label()
        lblEmail.Text = "Email"
        lblEmail.Location = New Point(50, 70)
        Me.Controls.Add(lblEmail)

        txtEmail = New TextBox()
        txtEmail.Location = New Point(150 * 1.5, 70)
        txtEmail.Size = New Size(200 * 1.5, 20)
        Me.Controls.Add(txtEmail)

        Dim lblPhone As New Label()
        lblPhone.Text = "Phone Num"
        lblPhone.Location = New Point(50, 110)
        Me.Controls.Add(lblPhone)

        txtPhone = New TextBox()
        txtPhone.Location = New Point(150 * 1.5, 110)
        txtPhone.Size = New Size(200 * 1.5, 20)
        Me.Controls.Add(txtPhone)

        Dim lblGithubLink As New Label()
        lblGithubLink.Text = "Github Link For Task 2"
        lblGithubLink.Location = New Point(50, 150)
        Me.Controls.Add(lblGithubLink)

        txtGithubLink = New TextBox()
        txtGithubLink.Location = New Point(150 * 1.5, 150)
        txtGithubLink.Size = New Size(200 * 1.5, 20)
        Me.Controls.Add(txtGithubLink)

        Dim lblStopwatch As New Label()
        lblStopwatch.Text = "Stopwatch time"
        lblStopwatch.Location = New Point(50, 190)
        Me.Controls.Add(lblStopwatch)

        txtStopwatch = New TextBox()
        txtStopwatch.Location = New Point(150 * 1.5, 190)
        txtStopwatch.Size = New Size(200 * 1.5, 20)
        txtStopwatch.ReadOnly = True
        Me.Controls.Add(txtStopwatch)

        btnToggleStopwatch = New Button()
        btnToggleStopwatch.Text = "TOGGLE (CTRL + T)"
        btnToggleStopwatch.Location = New Point(50 * 1.5, 240)
        btnToggleStopwatch.Size = New Size(150 * 1.5, 30)
        btnToggleStopwatch.BackColor = Color.LightYellow
        AddHandler btnToggleStopwatch.Click, AddressOf btnToggleStopwatch_Click
        Me.Controls.Add(btnToggleStopwatch)

        btnSubmit = New Button()
        btnSubmit.Text = "SUBMIT (CTRL + S)"
        btnSubmit.Location = New Point(230 * 1.5, 240)
        btnSubmit.Size = New Size(120 * 1.5, 30)
        btnSubmit.BackColor = Color.LightBlue
        AddHandler btnSubmit.Click, AddressOf btnSubmit_Click
        Me.Controls.Add(btnSubmit)
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
        Else
            stopwatch.Start()
        End If
        UpdateStopwatchDisplay()
    End Sub

    Private Sub UpdateStopwatchDisplay()
        txtStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            Dim httpClient As New HttpClient()
            Dim newSubmission As New Dictionary(Of String, String) From {
            {"name", txtName.Text},
            {"email", txtEmail.Text},
            {"phone", txtPhone.Text},
            {"github_link", txtGithubLink.Text},
            {"stopwatch_time", txtStopwatch.Text}
        }
            Dim content As New StringContent(JsonConvert.SerializeObject(newSubmission), Encoding.UTF8, "application/json")
            Dim response = httpClient.PostAsync("http://localhost:3000/submit", content).Result

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission successful!")
            Else
                MessageBox.Show("Submission failed. Status code: " & response.StatusCode)
            End If
        Catch ex As Exception
            MessageBox.Show("Error submitting: " & ex.Message)
        End Try
    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.S) Then
            btnSubmit.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.T) Then
            btnToggleStopwatch.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
