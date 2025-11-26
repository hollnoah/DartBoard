'Noah Holloway
'RCET 3371
'Dart Board Game
'

Imports System.IO
Imports System.Diagnostics
Imports System.Linq

Public Class MainForm

    ' --- Constants / Config ---
    Private Const NumThrowsPerRound As Integer = 3
    Private ReadOnly LogFileName As String = Path.Combine(Application.StartupPath, "DartGame.log")
    Private ReadOnly RandomGen As New Random()
    Private Const DartRadius As Integer = 6

    ' --- State ---
    Private currentRound As Integer = 0
    Private currentThrowCount As Integer = 0
    Private currentRoundDarts As New List(Of Point)()
    Private isPlayMode As Boolean = False
    Private isReviewMode As Boolean = False

    ' ---------------------------
    ' FORM LOAD
    ' ---------------------------
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ensure form receives key presses (spacebar)
        Me.KeyPreview = True

        ModeLabel.Text = "Mode: Idle"
        RoundNumberLabel.Text = "Round: 0"
        ThrowCountLabel.Text = $"Throw: 0 / {NumThrowsPerRound}"

        InitializeDartboardImage()
        LoadRoundsIntoListBox()

        ' Tooltips (optional; create MainToolTip in designer)
        Try
            MainToolTip.SetToolTip(DartboardPictureBox, "This is the dartboard!")
            MainToolTip.SetToolTip(OpenLogButton, "Click to open the log file in Notepad.")
            MainToolTip.SetToolTip(StartRoundButton, "Start a new round.")
            MainToolTip.SetToolTip(ReviewButton, "Enter review mode to inspect saved rounds.")
            MainToolTip.SetToolTip(QuitButton, "Quit the game.")
            MainToolTip.SetToolTip(RoundsListBox, "Select a logged round to display its darts.")
        Catch
            ' ignore if tooltip control not present
        End Try

        EnterIdleMode()
    End Sub

    ' ---------------------------
    ' INITIALIZE / CLEAR DARTBOARD
    ' ---------------------------
    Private Sub InitializeDartboardImage()
        Dim bmp As New Bitmap(Math.Max(1, DartboardPictureBox.Width), Math.Max(1, DartboardPictureBox.Height))
        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.White)
        End Using
        DartboardPictureBox.Image = bmp
    End Sub

    Private Sub ClearDartboard()
        If DartboardPictureBox.Image Is Nothing OrElse
           DartboardPictureBox.Image.Width <> DartboardPictureBox.Width OrElse
           DartboardPictureBox.Image.Height <> DartboardPictureBox.Height Then
            InitializeDartboardImage()
            Return
        End If

        Using g As Graphics = Graphics.FromImage(DartboardPictureBox.Image)
            g.Clear(Color.White)
        End Using
        DartboardPictureBox.Invalidate()
    End Sub

    ' ---------------------------
    ' START / NEW ROUND
    ' ---------------------------
    Private Sub StartRoundButton_Click(sender As Object, e As EventArgs) Handles StartRoundButton.Click
        If isReviewMode Then ExitReviewMode()
        StartNewRound()
    End Sub

    Private Sub StartNewRound()
        RoundsListBox.Items.Clear()

        isPlayMode = True
        isReviewMode = False
        currentRound += 1
        currentThrowCount = 0
        currentRoundDarts.Clear()

        ModeLabel.Text = "Mode: Play"
        RoundNumberLabel.Text = $"Round: {currentRound}"
        ThrowCountLabel.Text = $"Throw: {currentThrowCount} / {NumThrowsPerRound}"

        ClearDartboard()
    End Sub

    ' ---------------------------
    ' KEYBOARD (SPACE THROWS DART)
    ' ---------------------------
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Space Then
            e.Handled = True
            e.SuppressKeyPress = True
            If isPlayMode Then
                PerformThrow()
            End If
        End If
    End Sub

    ' ---------------------------
    ' THROW DART
    ' ---------------------------
    Private Sub PerformThrow()
        ' Only allow throws in play mode
        If Not isPlayMode Then Return

        ' Generate point and add to current round
        Dim pt As Point = GenerateRandomPointInsideDartboard(DartRadius)
        currentRoundDarts.Add(pt)
        currentThrowCount += 1

        ' Update UI and draw
        ThrowCountLabel.Text = $"Throw: {currentThrowCount} / {NumThrowsPerRound}"
        DrawDart(pt)

        ' If round complete, log and stop play mode
        If currentThrowCount >= NumThrowsPerRound Then
            Try
                LogRound(currentRound, currentRoundDarts)
                MessageBox.Show($"Round {currentRound} saved to log.", "Round Logged", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Error logging round: {ex.Message}", "File I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' reload listbox so review shows latest
            LoadRoundsIntoListBox()

            ' finish round
            isPlayMode = False
            ModeLabel.Text = "Mode: Idle"
        End If
    End Sub

    Private Function GenerateRandomPointInsideDartboard(radius As Integer) As Point
        Dim bmpW = Math.Max(1, DartboardPictureBox.Width)
        Dim bmpH = Math.Max(1, DartboardPictureBox.Height)

        Dim minX As Integer = radius
        Dim maxX As Integer = Math.Max(radius, bmpW - radius - 1)
        Dim minY As Integer = radius
        Dim maxY As Integer = Math.Max(radius, bmpH - radius - 1)

        Dim x = RandomGen.Next(minX, maxX + 1)
        Dim y = RandomGen.Next(minY, maxY + 1)

        Return New Point(x, y)
    End Function

    Private Sub DrawDart(p As Point)
        If DartboardPictureBox.Image Is Nothing Then InitializeDartboardImage()

        Dim bmp As Bitmap = DirectCast(DartboardPictureBox.Image, Bitmap)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.FillEllipse(Brushes.Red, p.X - DartRadius, p.Y - DartRadius, DartRadius * 2, DartRadius * 2)
            g.DrawLine(Pens.Black, p.X - 3, p.Y, p.X + 3, p.Y)
            g.DrawLine(Pens.Black, p.X, p.Y - 3, p.X, p.Y + 3)
        End Using
        DartboardPictureBox.Invalidate()
    End Sub

    ' ---------------------------
    ' FILE I/O: LOG ROUND (with timestamp)
    ' Format: Round|<round>|yyyy-MM-dd HH:mm:ss|x1,y1;x2,y2;x3,y3
    ' ---------------------------
    Private Sub LogRound(roundNumber As Integer, darts As List(Of Point))
        If darts Is Nothing OrElse darts.Count <> NumThrowsPerRound Then
            Throw New InvalidOperationException("Attempt to log incomplete round.")
        End If

        Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim coords As String = String.Join(";", darts.Select(Function(p) $"{p.X},{p.Y}"))
        Dim line As String = $"Round|{roundNumber}|{timestamp}|{coords}"

        Try
            Using sw As New StreamWriter(LogFileName, True)
                sw.WriteLine(line)
            End Using
        Catch ex As Exception
            Throw ' let caller display message
        End Try
    End Sub

    ' ---------------------------
    ' LOAD ROUNDS INTO LISTBOX (clears first)
    ' ---------------------------
    Private Sub LoadRoundsIntoListBox()
        RoundsListBox.Items.Clear()

        If Not File.Exists(LogFileName) Then
            Return
        End If

        Try
            Dim lines = File.ReadAllLines(LogFileName)
            For Each ln As String In lines
                If String.IsNullOrWhiteSpace(ln) Then Continue For
                ' Add the raw stored line (it's readable) or you could format it for display
                RoundsListBox.Items.Add(ln)
            Next
        Catch ex As Exception
            MessageBox.Show($"Error reading log file: {ex.Message}", "File I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ---------------------------
    ' REVIEW MODE
    ' ---------------------------
    Private Sub ReviewButton_Click(sender As Object, e As EventArgs) Handles ReviewButton.Click
        If isPlayMode Then
            ' finish/stop play mode before entering review
            isPlayMode = False
        End If

        EnterReviewMode()
    End Sub

    Private Sub EnterReviewMode()
        isReviewMode = True
        isPlayMode = False
        ModeLabel.Text = "Mode: Review"
        ThrowCountLabel.Text = $"Throw: 0 / {NumThrowsPerRound}"
        ClearDartboard()
        LoadRoundsIntoListBox()
    End Sub

    Private Sub ExitReviewMode()
        isReviewMode = False
        ModeLabel.Text = "Mode: Idle"
        ClearDartboard()
    End Sub

    ' When user selects a saved round in the ListBox, draw it
    Private Sub RoundsListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RoundsListBox.SelectedIndexChanged
        If RoundsListBox.SelectedIndex = -1 Then Return
        If Not isReviewMode Then Return

        Dim line As String = RoundsListBox.SelectedItem.ToString()
        Dim pts As List(Of Point) = ParseRoundLine(line)
        If pts Is Nothing OrElse pts.Count <> NumThrowsPerRound Then
            MessageBox.Show("Selected round is invalid or corrupted.", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ClearDartboard()
        For Each p In pts
            DrawDart(p)
        Next
    End Sub

    Private Function ParseRoundLine(line As String) As List(Of Point)
        ' Expected format: Round|<round>|yyyy-MM-dd HH:mm:ss|x1,y1;x2,y2;x3,y3
        Try
            Dim parts = line.Split("|"c)
            If parts.Length < 4 Then Return Nothing
            Dim coordsPart = parts(3)
            Dim pairs = coordsPart.Split(";"c)
            Dim pts As New List(Of Point)
            For Each pair In pairs
                Dim xy = pair.Split(","c)
                If xy.Length <> 2 Then Return Nothing
                Dim x = Integer.Parse(xy(0).Trim())
                Dim y = Integer.Parse(xy(1).Trim())
                ' Clip to ensure within bounds
                x = Math.Max(DartRadius, Math.Min(DartboardPictureBox.Width - DartRadius - 1, x))
                y = Math.Max(DartRadius, Math.Min(DartboardPictureBox.Height - DartRadius - 1, y))
                pts.Add(New Point(x, y))
            Next
            Return pts
        Catch
            Return Nothing
        End Try
    End Function

    ' ---------------------------
    ' OPEN LOG IN NOTEPAD
    ' ---------------------------
    Private Sub OpenLogButton_Click(sender As Object, e As EventArgs) Handles OpenLogButton.Click
        If Not File.Exists(LogFileName) Then
            MessageBox.Show("No log file yet! Play a round first.", "Log Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Process.Start("notepad.exe", LogFileName)
        Catch ex As Exception
            MessageBox.Show($"Failed to open Notepad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ---------------------------
    ' QUIT GAME
    ' ---------------------------
    Private Sub QuitButton_Click(sender As Object, e As EventArgs) Handles QuitButton.Click
        Me.Close()
    End Sub

    ' ---------------------------
    ' HELPER: IDLE MODE
    ' ---------------------------
    Private Sub EnterIdleMode()
        isPlayMode = False
        isReviewMode = False
        ModeLabel.Text = "Mode: Idle"
        ThrowCountLabel.Text = $"Throw: 0 / {NumThrowsPerRound}"
        currentRoundDarts.Clear()
    End Sub

End Class


