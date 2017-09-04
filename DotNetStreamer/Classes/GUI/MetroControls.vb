Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Drawing2D
Imports System.Security.Policy
Imports System.IO
Imports System.Drawing.Text

Public Class MetroButton
    Inherits Control
    Enum MouseState
        None = 0
        Over = 1
        Down = 2
    End Enum
    Private State As MouseState = MouseState.None

#Region "Properties"
    Dim _BGOver As Color '= Color.FromArgb(75, 75, 75)
    Property BackColorOver As Color
        Get
            Return _BGOver
        End Get
        Set(value As Color)
            _BGOver = value
            Invalidate()
        End Set
    End Property

    Dim _BGDown As Color '= Color.FromArgb(55, 55, 55)
    Property BackColorDown As Color
        Get
            Return _BGDown
        End Get
        Set(value As Color)
            _BGDown = value
            Invalidate()
        End Set
    End Property

    Dim _BorderColor As Color
    Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Dim BGC As Color
    Property BackColorNormal As Color
        Get
            Return BGC
        End Get
        Set(value As Color)
            BGC = value
        End Set
    End Property
#End Region

    Sub New()
        ForeColor = Color.White
        Font = New Font("Segoe UI", 9)
        SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True

        Dim R, G, B As Integer
        Dim BG As Color = BackColor

        R = BG.R - 20
        G = BG.G - 20
        B = BG.B - 20

        If R < 0 Then R = 0
        If G < 0 Then G = 0
        If B < 0 Then B = 0

        BGC = Color.FromArgb(R, G, B)

        Size = New Size(105, 27)
    End Sub

    Protected Overrides Sub OnMouseEnter(e As System.EventArgs)
        State = MouseState.Over
        Invalidate()
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As System.EventArgs)
        State = MouseState.None
        Invalidate()
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnMouseDown(e As System.Windows.Forms.MouseEventArgs)
        State = MouseState.Down
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As System.Windows.Forms.MouseEventArgs)
        State = MouseState.Over
        Invalidate()
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        Invalidate()
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        Select Case State
            Case MouseState.None
                G.FillRectangle(New SolidBrush(BGC), New Rectangle(1, 1, Width - 2, Height - 2))
            Case MouseState.Over
                G.FillRectangle(New SolidBrush(BackColorOver), New Rectangle(1, 1, Width - 2, Height - 2))
            Case MouseState.Down
                G.FillRectangle(New SolidBrush(BackColorDown), New Rectangle(1, 1, Width - 2, Height - 2))
        End Select
        G.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, Width - 1, Height - 1))
        Dim SF As New StringFormat : SF.Alignment = StringAlignment.Center : SF.LineAlignment = StringAlignment.Center
        G.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(0, 0, Width - 1, Height - 1), SF)
        MyBase.OnPaint(e)
    End Sub

End Class

Public Class MetroTextBox
    Inherits Control

    Dim _NumericOnly As Boolean = False
    Public Property NumericOnly As Boolean
        Get
            Return _NumericOnly
        End Get
        Set(value As Boolean)
            _NumericOnly = value
        End Set
    End Property

    Dim _BorderColor As Color
    Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Private _TextAlign As HorizontalAlignment = HorizontalAlignment.Left
    Property TextAlign() As HorizontalAlignment
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As HorizontalAlignment)
            _TextAlign = value
            If Base IsNot Nothing Then
                Base.TextAlign = value
            End If
        End Set
    End Property
    Private _MaxLength As Integer = 32767
    Property MaxLength() As Integer
        Get
            Return _MaxLength
        End Get
        Set(ByVal value As Integer)
            _MaxLength = value
            If Base IsNot Nothing Then
                Base.MaxLength = value
            End If
        End Set
    End Property
    Private _ReadOnly As Boolean
    Property [ReadOnly]() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
            If Base IsNot Nothing Then
                Base.ReadOnly = value
            End If
        End Set
    End Property
    Private _UseSystemPasswordChar As Boolean
    Property UseSystemPasswordChar() As Boolean
        Get
            Return _UseSystemPasswordChar
        End Get
        Set(ByVal value As Boolean)
            _UseSystemPasswordChar = value
            If Base IsNot Nothing Then
                Base.UseSystemPasswordChar = value
            End If
        End Set
    End Property
    Private _Multiline As Boolean
    Property Multiline() As Boolean
        Get
            Return _Multiline
        End Get
        Set(ByVal value As Boolean)
            _Multiline = value
            If Base IsNot Nothing Then
                Base.Multiline = value

                If value Then
                    Base.Height = Height - 11
                Else
                End If
            End If
        End Set
    End Property
    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            If Base IsNot Nothing Then
                Base.Text = value
            End If
        End Set
    End Property
    Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            If Base IsNot Nothing Then
                Base.Font = value
                Base.Location = New Point(3, 5)
                Base.Width = Width - 6
            End If
        End Set
    End Property

    Protected Overrides Sub OnParentChanged(e As System.EventArgs)
        If Not Controls.Contains(Base) Then
            Controls.Add(Base)
        End If
    End Sub

    Public Shadows Event KeyDown(ByVal sender As Object, e As KeyEventArgs)
    Private Base As TextBox
    Dim C As Color
    Sub New()
        Font = New Font("Segoe UI", 9)
        Base = New TextBox
        Base.Font = Font
        Base.Text = Text
        Base.MaxLength = _MaxLength
        Base.Multiline = _Multiline
        Base.ReadOnly = _ReadOnly
        Base.UseSystemPasswordChar = _UseSystemPasswordChar
        Base.BorderStyle = BorderStyle.None
        Base.Location = New Point(5, 4)
        Base.Width = Width - 10

        If _Multiline Then
            Base.Height = Height - 11
        End If

        AddHandler Base.TextChanged, AddressOf OnBaseTextChanged
        AddHandler Base.KeyDown, AddressOf OnBaseKeyDown

        Dim R, G, B As Integer

        R = BackColor.R - 15
        G = BackColor.G - 15
        B = BackColor.B - 15

        If R < 0 Then R = 0
        If G < 0 Then G = 0
        If B < 0 Then B = 0

        C = Color.FromArgb(R, G, B)
        Base.BackColor = C
    End Sub

    Protected Overrides Sub OnForeColorChanged(e As EventArgs)
        MyBase.OnForeColorChanged(e)
        Base.ForeColor = ForeColor
    End Sub

    Protected Overrides Sub OnBackColorChanged(e As System.EventArgs)
        MyBase.OnBackColorChanged(e)
        Dim R, G, B As Integer

        R = BackColor.R - 15
        G = BackColor.G - 15
        B = BackColor.B - 15

        If R < 0 Then R = 0
        If G < 0 Then G = 0
        If B < 0 Then B = 0

        C = Color.FromArgb(R, G, B)
        Base.BackColor = C
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim GG As Graphics = e.Graphics
        GG.Clear(C)
        GG.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, Width - 1, Height - 1))
    End Sub

    Public Function FindAllNumbers(ByRef strIn As String) As String
        If String.IsNullOrEmpty(strIn) Then
            Return ""
            Exit Function
        End If
        Dim intY As Short
        Dim intX As Short
        For intY = 1 To CShort(Len(strIn))
            intX = Asc(Mid(strIn, intY))
            If intX >= 48 And intX <= 58 Then
                FindAllNumbers = CStr(CInt(FindAllNumbers & Mid(strIn, intY, 1)))
            End If
        Next intY
    End Function

    Private Sub OnBaseTextChanged(ByVal s As Object, ByVal e As EventArgs)
        If _NumericOnly Then
            Text = FindAllNumbers(Base.Text)
            Base.Text = Text
            Base.Select(Base.Text.Length, 0)
        Else
            Text = Base.Text
        End If
    End Sub

    Private Sub OnBaseKeyDown(ByVal s As Object, ByVal e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.A Then
            Base.SelectAll()
            e.SuppressKeyPress = True
        End If
        RaiseEvent KeyDown(s, e)
    End Sub

    Protected Overrides Sub OnGotFocus(e As EventArgs)
        MyBase.OnGotFocus(e)
        Base.Focus()
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Base.Location = New Point(5, 4)
        Base.Width = Width - 10

        If _Multiline Then
            Base.Height = Height - 11
        End If


        MyBase.OnResize(e)
    End Sub

End Class

Public Class MetroProgressbarHorizontal
    Inherits Control

    Dim _BorderColor As Color = Color.Black
    Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Dim _ProgressColor As Color = Color.FromArgb(10, 150, 40)
    Property ProgressColor As Color
        Get
            Return _ProgressColor
        End Get
        Set(value As Color)
            _ProgressColor = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint, True)
        DoubleBuffered = True
    End Sub

    Dim _Val As Integer = 0
    Property Value As Integer
        Get
            Return _Val
        End Get
        Set(v As Integer)
            If v <= _Max Then _Val = v
            Invalidate()
        End Set
    End Property

    Dim _Max As Integer = 100
    Property Maximum As Integer
        Get
            Return _Max
        End Get
        Set(value As Integer)
            If value >= _Val Then _Max = value
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        Dim Progress As Double = (_Val / _Max) * (Width - 2)

        G.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), New Rectangle(0, 0, Width - 1, Height - 1))
        If Progress > 0 Then G.FillRectangle(New SolidBrush(_ProgressColor), New Rectangle(1, 1, CInt(Progress), Height - 2))

        G.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, Width - 1, Height - 1))
        MyBase.OnPaint(e)
    End Sub
End Class

Public Class MetroProgressbarVertical
    Inherits Control

    Dim _BorderColor As Color = Color.Black
    Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Dim _ProgressColor As Color = Color.FromArgb(10, 150, 40)
    Property ProgressColor As Color
        Get
            Return _ProgressColor
        End Get
        Set(value As Color)
            _ProgressColor = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint, True)
        DoubleBuffered = True
        Size = New Size(7, 50)
    End Sub

    Dim _Val As Integer = 0
    Property Value As Integer
        Get
            Return _Val
        End Get
        Set(v As Integer)
            If v <= _Max Then _Val = v
            Invalidate()
        End Set
    End Property

    Dim _Max As Integer = 100
    Property Maximum As Integer
        Get
            Return _Max
        End Get
        Set(value As Integer)
            If value >= _Val Then _Max = value
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        Dim Progress As Double = (_Val / _Max) * (Height - 2)

        G.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), New Rectangle(0, 0, Width - 1, Height - 1))
        If Progress > 0 Then G.FillRectangle(New SolidBrush(_ProgressColor), New Rectangle(1, Height - 1 - Progress, Width - 2, Progress))

        G.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, Width - 1, Height - 1))
        MyBase.OnPaint(e)
    End Sub
End Class

Public Class MetroCheckbox
    Inherits Control

    Event CheckedChanged()

    Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint, True)
    End Sub

    Dim _CheckColor As Color = Color.FromArgb(54, 74, 74)
    Property CheckColor As Color
        Get
            Return _CheckColor
        End Get
        Set(value As Color)
            _CheckColor = value
            Invalidate()
        End Set
    End Property

    Dim _BorderColor As Color = Color.Black
    Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Dim _FillColor As Color = Color.FromArgb(45, 45, 45)
    Property FillColor As Color
        Get
            Return _FillColor
        End Get
        Set(value As Color)
            _FillColor = value
            Invalidate()
        End Set
    End Property

    Private _Checked As Boolean
    Property Checked As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Using G As Graphics = e.Graphics
            G.FillRectangle(New SolidBrush(_FillColor), New Rectangle(0, 0, 18, 18))
            G.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, 18, 18))
            If _Checked Then G.DrawString("a", New Font("Marlett", 15), New SolidBrush(_CheckColor), New Point(-2, -1))
            G.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(24, 2, Width - 24, Height - 5), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
        End Using
        MyBase.OnPaint(e)
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left And e.X < 21 Then
            _Checked = Not Checked
            RaiseEvent CheckedChanged()
            Invalidate()
        End If
        MyBase.OnMouseClick(e)
    End Sub
End Class

Public Class MetroRadioButton
    Inherits Control

    Event CheckedChanged()

    Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint, True)
    End Sub

    Dim _CheckColor As Color = Color.FromArgb(54, 74, 74)
    Property CheckColor As Color
        Get
            Return _CheckColor
        End Get
        Set(value As Color)
            _CheckColor = value
            Invalidate()
        End Set
    End Property

    Dim _BorderColor As Color = Color.Black
    Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Dim _FillColor As Color = Color.FromArgb(45, 45, 45)
    Property FillColor As Color
        Get
            Return _FillColor
        End Get
        Set(value As Color)
            _FillColor = value
            Invalidate()
        End Set
    End Property

    Private _Checked As Boolean
    Property Checked As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Using G As Graphics = e.Graphics
            G.SmoothingMode = SmoothingMode.HighQuality
            G.FillEllipse(New SolidBrush(Color.Transparent), New Rectangle(0, 0, 14, 14))
            G.DrawEllipse(New Pen(_BorderColor), New Rectangle(0, 0, 14, 14))
            If _Checked Then G.FillEllipse(New SolidBrush(Color.FromArgb(170, 170, 170)), New Rectangle(0, 0, 14, 14))
            'If _Checked Then G.FillEllipse(New SolidBrush(Color.Fuchsia), New Rectangle(0, 0, 14, 14))

            G.DrawString(Text, New Font("Segoe UI Light", 11), New SolidBrush(ForeColor), New Rectangle(18, -2, Width - 24, Height - 5), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
        End Using
        MyBase.OnPaint(e)
    End Sub

    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left And e.X < 21 Then
            _Checked = Not Checked
            RaiseEvent CheckedChanged()
            Invalidate()
        End If
        MyBase.OnMouseClick(e)
    End Sub
End Class

<Designer("System.Windows.Forms.Design.ParentControlDesigner,System.Design", GetType(IDesigner))> _
Public Class SpectrumPanel
    Inherits Control

#Region "Declarations"
    Dim btnPrevious As New Rectangle(8, 9, 27, 27)
    Dim btnPlayPause As New Rectangle(40, 1, 35, 35)
    Dim btnNext As New Rectangle(80, 9, 27, 27)
    Dim btnScreen As New Rectangle(112, 9, 27, 27)

    Dim prgH As New Rectangle(157, 19, 417, 7)
    Dim time As New Point(155, 35)
    Dim timeleft As New Point(595, 35)
    Dim shows As New Point(155, 10)

    'Dim prgH As New Rectangle(125, 19, 462, 7)
    'Dim time As New Point(123, 35)
    'Dim timeleft As New Point(595, 35)
    'Dim shows As New Point(123, 10)
    Dim btnPreviousState, btnPlayPauseState, btnNextState, btnScreenState As Boolean
#End Region
#Region "Structures & custom events"
    Enum PlayPauseButtonStyles
        Play = 0
        Pause = 1
    End Enum

    Public Event Previous_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event PlayPause_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Next_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event FullScreen_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event Progressbar_Clicked(ByVal Sender As Object, ByVal ClickedAtValue As Double)
    Public Event Progressbar_HoverCurrentTime(ByVal sender As Object, ByVal currentTime As TimeSpan)
#End Region
#Region "Properties"

    Dim _PPBS As PlayPauseButtonStyles
    Property PlayPauseButton As PlayPauseButtonStyles
        Get
            Return _PPBS
        End Get
        Set(value As PlayPauseButtonStyles)
            _PPBS = value
            Invalidate()
        End Set
    End Property

    Dim _Seconds As String
    Property Seconds As String
        Get
            Return _Seconds
        End Get
        Set(value As String)
            _Seconds = value
            Invalidate()
        End Set
    End Property

    Dim _SecondsLeft As String
    Property SecondsLeft As String
        Get
            Return _SecondsLeft
        End Get
        Set(value As String)
            _SecondsLeft = value
            Invalidate()
        End Set
    End Property

    Dim _ShowName As String
    Property ShowName As String
        Get
            Return _ShowName
        End Get
        Set(value As String)
            _ShowName = value
            Invalidate()
        End Set
    End Property

    Dim _ProgressValue As Double
    Property ProgressValue As Double
        Get
            Return _ProgressValue
        End Get
        Set(value As Double)
            _ProgressValue = value
            Invalidate()
        End Set
    End Property

    Dim _Max As Double = 100
    Property ProgressMaximum As Double
        Get
            Return _Max
        End Get
        Set(value As Double)
            If value >= _ProgressValue Then _Max = value
        End Set
    End Property

    Dim _MouseOnProgressBar As Boolean
    ReadOnly Property MouseOnProgressBar As Boolean
        Get
            Return _MouseOnProgressBar
        End Get
    End Property

#End Region
#Region "Events"

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        timeleft = New Point(Width - CreateGraphics.MeasureString(_SecondsLeft, New Font("Segoe UI", 7)).Width + 42, 35)
        prgH.Width = Width - 161
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If btnPrevious.Contains(e.Location) Then btnPreviousState = True Else btnPreviousState = False
        If btnPlayPause.Contains(e.Location) Then btnPlayPauseState = True Else btnPlayPauseState = False
        If btnNext.Contains(e.Location) Then btnNextState = True Else btnNextState = False
        If btnScreen.Contains(e.Location) Then btnScreenState = True Else btnScreenState = False

        If prgH.Contains(e.Location) Then
            _MouseOnProgressBar = True
            Dim X As Integer = e.Location.X - prgH.X
            Dim currentMilliseconds As Double = (X / prgH.Width) * _Max
            Dim CurrentMouseTime As TimeSpan = New TimeSpan(0, 0, 0, 0, currentMilliseconds)
            RaiseEvent Progressbar_HoverCurrentTime(Me, CurrentMouseTime)
        Else
            _MouseOnProgressBar = False
        End If

        If e.Button = Windows.Forms.MouseButtons.Left Then
            If prgH.Contains(e.Location) Then
                Dim X As Integer = e.Location.X - prgH.X
                RaiseEvent Progressbar_Clicked(Me, (X / prgH.Width) * _Max)
            End If
        End If

        Invalidate()
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If btnPrevious.Contains(e.Location) Then
            RaiseEvent Previous_Clicked(Me, Nothing)
        End If
        If btnPlayPause.Contains(e.Location) Then
            RaiseEvent PlayPause_Clicked(Me, Nothing)
        End If
        If btnNext.Contains(e.Location) Then
            RaiseEvent Next_Clicked(Me, Nothing)
        End If
        If btnScreen.Contains(e.Location) Then
            RaiseEvent FullScreen_Clicked(Me, Nothing)
        End If
        If prgH.Contains(e.Location) Then
            Dim X As Integer = e.Location.X - prgH.X
            RaiseEvent Progressbar_Clicked(Me, (X / prgH.Width) * _Max)
        End If
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

#End Region

    Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.ContainerControl, True)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Size = New Size(597, 35)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim btnBrush As Brush

        '//////////////////     PREVIOUS BUTTON
        If btnPreviousState = True Then btnBrush = Brushes.White Else btnBrush = Brushes.LightGray
        Dim p1 As New Point(((btnPrevious.Width / 7) * 3) + btnPrevious.X, (btnPrevious.Height / 4) + btnPrevious.Y + 1)
        Dim p2 As New Point(((btnPrevious.Width / 7) * 3) + +btnPrevious.X, (btnPrevious.Height - (btnPrevious.Height / 4) - 1) + +btnPrevious.Y)
        Dim p3 As New Point(((btnPrevious.Width / 9) * 2) + btnPrevious.X, ((btnPrevious.Height - 1) / 2) + btnPrevious.Y)
        Dim p() As Point = {p1, p2, p3}
        e.Graphics.FillPolygon(btnBrush, p)
        p1 = New Point(((btnPrevious.Width / 9) * 6) + btnPrevious.X, (btnPrevious.Height / 4) + btnPrevious.Y + 1)
        p2 = New Point(((btnPrevious.Width / 9) * 6) + btnPrevious.X, (btnPrevious.Height - (btnPrevious.Height / 4) - 1) + btnPrevious.Y)
        p3 = New Point(((btnPrevious.Width / 7) * 3) + btnPrevious.X, ((btnPrevious.Height - 1) / 2) + btnPrevious.Y)
        p = {p1, p2, p3}
        e.Graphics.FillPolygon(btnBrush, p)
        e.Graphics.DrawEllipse(New Pen(btnBrush), btnPrevious)

        '//////////////////     PLAYPAUSE BUTTON
        If btnPlayPauseState = True Then btnBrush = Brushes.White Else btnBrush = Brushes.LightGray

        Select Case _PPBS
            Case PlayPauseButtonStyles.Play
                p1 = New Point(((btnPlayPause.Width / 5) * 2) + btnPlayPause.X, (btnPlayPause.Height / 4) + btnPlayPause.Y)
                p2 = New Point(((btnPlayPause.Width / 5) * 2) + btnPlayPause.X, (btnPlayPause.Height - (btnPlayPause.Height / 4) - 2) + btnPlayPause.Y)
                p3 = New Point(((btnPlayPause.Width / 9) * 6) + btnPlayPause.X, ((btnPlayPause.Height - 1) / 2) + btnPlayPause.Y)
                p = {p1, p2, p3}
                e.Graphics.FillPolygon(btnBrush, p)
            Case PlayPauseButtonStyles.Pause
                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.None
                Dim R1 As New Rectangle(((btnPlayPause.Width / 7) * 2) + btnPlayPause.X, (btnPlayPause.Height / 4) + btnPlayPause.Y, btnPlayPause.Width / 7, (btnPlayPause.Height) - 2 * (btnPlayPause.Height / 4))
                e.Graphics.FillRectangle(btnBrush, R1)
                Dim R2 As New Rectangle((btnPlayPause.Width - ((btnPlayPause.Width / 7) * 3)) + btnPlayPause.X, (btnPlayPause.Height / 4) + btnPlayPause.Y, btnPlayPause.Width / 7, (btnPlayPause.Height) - 2 * (btnPlayPause.Height / 4))
                e.Graphics.FillRectangle(btnBrush, R2)
                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        End Select
        e.Graphics.DrawEllipse(New Pen(btnBrush), btnPlayPause)

        '//////////////////      NEXT BUTTON
        If btnNextState = True Then btnBrush = Brushes.White Else btnBrush = Brushes.LightGray
        p1 = New Point(((btnNext.Width / 7) * 2) + btnNext.X, (btnNext.Height / 4) + btnNext.Y + 1)
        p2 = New Point(((btnNext.Width / 7) * 2) + btnNext.X, (btnNext.Height - (btnNext.Height / 4) - 1) + btnNext.Y)
        p3 = New Point(((btnNext.Width / 11) * 6) + btnNext.X, ((btnNext.Height - 1) / 2) + btnNext.Y)
        p = {p1, p2, p3}
        e.Graphics.FillPolygon(btnBrush, p)
        p1 = New Point(((btnNext.Width / 11) * 6) + btnNext.X, (btnNext.Height / 4) + btnNext.Y + 1)
        p2 = New Point(((btnNext.Width / 11) * 6) + btnNext.X, (btnNext.Height - (btnNext.Height / 4) - 1) + btnNext.Y)
        p3 = New Point(((btnNext.Width / 9) * 7) + btnNext.X, ((btnNext.Height - 1) / 2) + btnNext.Y)
        p = {p1, p2, p3}
        e.Graphics.FillPolygon(btnBrush, p)
        e.Graphics.DrawEllipse(New Pen(btnBrush), btnNext)

        '//////////////////      FULLSCREEN BUTTON
        If btnScreenState = True Then
            e.Graphics.DrawImage(My.Resources.fullscreenwhite, New Rectangle(btnScreen.X + 5.5, btnScreen.Y + 6, btnScreen.Width - 10, btnScreen.Height - 10))
            e.Graphics.DrawEllipse(New Pen(Brushes.White), btnScreen)
        Else
            e.Graphics.DrawImage(My.Resources.fullscreenlightgray, New Rectangle(btnScreen.X + 5.5, btnScreen.Y + 6, btnScreen.Width - 10, btnScreen.Height - 10))
            e.Graphics.DrawEllipse(New Pen(Brushes.LightGray), btnScreen)
        End If



        '//////////////////      HORIZONTAL PROGRESSBAR
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.None
        Dim Progress As Double = (_ProgressValue / _Max) * (prgH.Width - 1)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), New Rectangle(prgH.X, prgH.Y, prgH.Width - 1, prgH.Height - 1))
        If Progress > Width Then Progress = 1
        If Progress > 0 Then e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(150, Color.White)), New Rectangle(prgH.X + 1, prgH.Y + 1, Progress, prgH.Height - 1))
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(30, Color.White)), prgH)

        '//////////////////      TIME & TIME LEFT
        e.Graphics.DrawString(_Seconds, New Font("Segoe UI", 7), Brushes.White, time, New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
        e.Graphics.DrawString(_SecondsLeft, New Font("Segoe UI", 7), Brushes.White, timeleft, New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center})

        '//////////////////      SHOWNAME
        If Not String.IsNullOrEmpty(_ShowName) Then
            e.Graphics.DrawString(Text & _ShowName, New Font("Segoe UI", 9), Brushes.White, shows, New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
        End If

        MyBase.OnPaint(e)
    End Sub

End Class

Public Class MetroListview
    Inherits ListView
    Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
    End Sub
End Class

Public Class MetroCombobox
    Inherits ComboBox
    Private Over As Boolean

#Region "Properties"
    Dim _BGOver As Color '= Color.FromArgb(75, 75, 75)
    Property BackColorOver As Color
        Get
            Return _BGOver
        End Get
        Set(value As Color)
            _BGOver = value
            Invalidate()
        End Set
    End Property

    Dim _BorderColor As Color
    Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Dim BGC As Color
    Property BackColorNormal As Color
        Get
            Return BGC
        End Get
        Set(value As Color)
            BGC = value
        End Set
    End Property
#End Region

    Sub New()
        MyBase.New()
        Font = New Font("Segoe UI", 9)
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        ItemHeight = 16
        DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Protected Overrides Sub OnMouseEnter(e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        Over = True
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        Over = False
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Not DropDownStyle = ComboBoxStyle.DropDownList Then DropDownStyle = ComboBoxStyle.DropDownList
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        G.Clear(Color.FromArgb(50, 50, 50))
        If Not Over Then
            G.FillRectangle(New SolidBrush(BGC), New Rectangle(1, 1, Width - 2, Height - 2))
        Else
            G.FillRectangle(New SolidBrush(_BGOver), New Rectangle(1, 1, Width - 2, Height - 2))
        End If

        G.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, Width - 1, Height - 1))
        Dim SF As New StringFormat : SF.Alignment = StringAlignment.Near : SF.LineAlignment = StringAlignment.Center : SF.Trimming = StringTrimming.EllipsisCharacter
        G.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(4, 2, Width - 5, Height - 5), SF)

        G.FillPolygon(Brushes.Black, Triangle(New Point(Width - 14, Height \ 2), New Size(5, 3)))
        G.FillPolygon(Brushes.White, Triangle(New Point(Width - 15, Height \ 2 - 1), New Size(5, 3)))

        e.Graphics.DrawImage(B.Clone, 0, 0)
        G.Dispose() : B.Dispose()
    End Sub

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 Then Exit Sub
        Dim rect As New Rectangle()
        rect.X = e.Bounds.X
        rect.Y = e.Bounds.Y
        rect.Width = e.Bounds.Width - 1
        rect.Height = e.Bounds.Height - 1

        e.DrawBackground()
        If e.State = 785 Or e.State = 17 Then
            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(50, 50, 50)), e.Bounds)
            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(80, 80, 80)), e.Bounds)
            e.Graphics.DrawString(Me.Items(e.Index).ToString(), e.Font, Brushes.White, e.Bounds.X, e.Bounds.Y + 0)
        Else
            e.Graphics.FillRectangle(New SolidBrush(BackColor), e.Bounds)
            e.Graphics.DrawString(Me.Items(e.Index).ToString(), e.Font, Brushes.White, e.Bounds.X, e.Bounds.Y + 0)
        End If
        MyBase.OnDrawItem(e)
    End Sub

    Public Function Triangle(ByVal Location As Point, ByVal Size As Size) As Point()
        Dim ReturnPoints(0 To 3) As Point
        ReturnPoints(0) = Location
        ReturnPoints(1) = New Point(Location.X + Size.Width, Location.Y)
        ReturnPoints(2) = New Point(Location.X + Size.Width \ 2, Location.Y + Size.Height)
        ReturnPoints(3) = Location

        Return ReturnPoints
    End Function

    Private Sub GhostComboBox_DropDown(sender As Object, e As System.EventArgs) Handles Me.DropDown

    End Sub

    Private Sub GhostComboBox_DropDownClosed(sender As Object, e As System.EventArgs) Handles Me.DropDownClosed
        DropDownStyle = ComboBoxStyle.Simple
        Application.DoEvents()
        DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub GhostCombo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
        Invalidate()
    End Sub

End Class


<DefaultEvent("ItemDoubleClicked")> _
Public Class SeriesView
    Inherits Control
    Sub New()
        VScroll = New LogInVerticalScrollBar
        VScroll.Location = New Point(Width - 17, 0)
        VScroll.Size = New Size(17, Height)
        VScroll.Maximum = 58
        VScroll.Minimum = 0
        VScroll.Value = 0

        AddHandler VScroll.Scroll, AddressOf VScroll_Scroll

        SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)
    End Sub

#Region "Declarations"
    Private ItemSize As New Size(135, 200)
    Public SelectedItem As Integer = -1
    Private ReadOnly ShadowWidth As Integer = 10
    Private ReadOnly VScroll As LogInVerticalScrollBar
    Private MousePos As Point
    Private ExcessHeight As Integer
    Private tmrAnimation As Timer
    Private animationPos As Integer = 0

    Private _UpdateList As New List(Of String())

    Public Event ItemClicked(ByVal Index As Integer, ByVal click As Windows.Forms.MouseButtons)
    Public Event ItemDoubleClicked(ByVal index As Integer)
    Public Event SelectedIndexChanged()
    Public Event LabelClicked(ByVal _SelectedTab As Categories)

    Enum Categories
        Movies = 1
        Series = 2
        Favorites = 3
    End Enum

    Enum States
        Loading
        Loaded
        GotException
        'WatchSeriesOffline
    End Enum

#End Region

#Region "Scrolling"
    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        If Not Controls.Contains(VScroll) Then
            Controls.Add(VScroll)
        End If
        MyBase.OnHandleCreated(e)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        VScroll.Location = New Point(Width - 17, 0)
        VScroll.Size = New Size(17, Height)
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        If e.Delta > 0 Then
            _ScrollValue -= 5
        Else
            _ScrollValue += 5
        End If
        If _ScrollValue > ScrollingMaximum Then _ScrollValue = ScrollingMaximum
        If _ScrollValue < 0 Then _ScrollValue = 0

        VScroll.Value = _ScrollValue
        Invalidate()
        MyBase.OnMouseWheel(e)
    End Sub

    Private Sub VScroll_Scroll(sender As Object)
        _ScrollValue = VScroll.Value
        Invalidate()
    End Sub

#End Region

#Region "Properties "

    Public Property ScrollingMaximum As Integer

    Private _NoFavorite As Boolean
    Public Property NoFavorite As Boolean
        Get
            Return _NoFavorite
        End Get
        Set(value As Boolean)
            _NoFavorite = value
            Invalidate()
        End Set
    End Property


    Private _SelectedTab As Categories = Categories.Series
    Public Property SelectedTab As Categories
        Get
            Return _SelectedTab
        End Get
        Set(value As Categories)
            _SelectedTab = value
            Invalidate()
        End Set
    End Property

    Private _State As States = States.Loading
    Public Property State As States
        Get
            Return _State
        End Get
        Set(value As States)
            _State = value
            Invalidate()
        End Set
    End Property

    Private _Items As New List(Of Item)
    Public ReadOnly Property Items As List(Of Item)
        Get
            Return _Items
        End Get
    End Property

    Dim _ScrollValue As Integer
    Property ScrollValue As Integer
        Get
            Return _ScrollValue
        End Get
        Set(value As Integer)
            _ScrollValue = value
            Invalidate()
        End Set
    End Property
#End Region

#Region "Drawing"

    Private headerFont As New Font("Segoe UI Light", 16)
    Private headerFontSelected As New Font("Segoe UI Light", 16, FontStyle.Underline)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Using B As New Bitmap(Width, Height)
            Using G = Graphics.FromImage(B)

                G.SmoothingMode = SmoothingMode.AntiAlias


                If _SelectedTab = Categories.Series Then
                    e.Graphics.DrawString("New Series", headerFontSelected, Brushes.White, New Point(22, GetItemRectangle(0).Y - 35))

                Else
                    e.Graphics.DrawString("New Series", headerFont, Brushes.White, New Point(22, GetItemRectangle(0).Y - 35))
                End If

                If _SelectedTab = Categories.Movies Then
                    e.Graphics.DrawString("Movies", headerFontSelected, Brushes.White, New Point(134, GetItemRectangle(0).Y - 35))
                Else
                    e.Graphics.DrawString("Movies", headerFont, Brushes.White, New Point(134, GetItemRectangle(0).Y - 35))
                End If

                If _SelectedTab = Categories.Favorites Then
                    e.Graphics.DrawString("Favorites", headerFontSelected, Brushes.White, New Point(211, GetItemRectangle(0).Y - 35))
                Else
                    e.Graphics.DrawString("Favorites", headerFont, Brushes.White, New Point(211, GetItemRectangle(0).Y - 35))
                End If

                If _State = States.Loading AndAlso _SelectedTab <> Categories.Favorites Then
                    e.Graphics.DrawString("Loading ...", New Font("Segoe UI Light", 22), Brushes.White, New Rectangle(10, 10, Width - 20, Height - 20), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                ElseIf _State = States.GotException AndAlso _SelectedTab <> Categories.Favorites Then
                    e.Graphics.DrawString("Unable to load shows ...", New Font("Segoe UI Light", 22), Brushes.White, New Rectangle(10, 10, Width - 20, Height - 20), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                End If

                If NoFavorite = True And _SelectedTab = Categories.Favorites Then
                    e.Graphics.DrawString("No favorite show to load.", New Font("Segoe UI Light", 14), Brushes.White, New Point(23, GetItemRectangle(0).Y + 2))
                End If


                For i = 0 To Items.Count - 1
                    If i = SelectedItem Then Continue For
                    DrawItem(G, Items(i), GetItemRectangle(i))
                Next

                Try

                    If SelectedItem <> -1 Then
                        Dim r = GetItemRectangle(SelectedItem)

                        Using pth As GraphicsPath = CreateRoundRectangle(r, Math.Min(r.Width, r.Height) / 10)
                            pth.Widen(New Pen(Color.Empty, ShadowWidth * 2))

                            Using br As New PathGradientBrush(pth)
                                br.CenterColor = Color.Black
                                br.SurroundColors = {Color.Empty}
                                G.FillPath(br, pth)
                            End Using
                        End Using

                        DrawItem(G, Items(SelectedItem), r)
                    End If
                Catch ex As Exception
                End Try

                e.Graphics.DrawImageUnscaled(B, 0, 0)
            End Using
        End Using
    End Sub

    Private updatedFont As New Font("Segoe UI", 10)
    Private Sub DrawItem(G As Graphics, itm As Item, itmRect As Rectangle)
        With G
            Dim Selected As Boolean = (GetItemRectangle(SelectedItem) = itmRect)
            If Not Selected Then itmRect = New Rectangle(itmRect.X + 7, itmRect.Y + 7, itmRect.Width - 14, itmRect.Height - 14)

            .InterpolationMode = InterpolationMode.HighQualityBicubic
            .DrawImage(itm.Image, New Rectangle(itmRect.X + 1, itmRect.Y + 1, itmRect.Width - 1, itmRect.Height - 1))

            .FillRectangle(New SolidBrush(Color.FromArgb(200, Color.Black)), New Rectangle(itmRect.X, itmRect.Y, itmRect.Width, 30))
            .DrawLine(New Pen(Color.FromArgb(40, Color.White)), itmRect.X + 1, itmRect.Y + 31, itmRect.X + itmRect.Width, itmRect.Y + 31)

            Dim TextSize As SizeF = .MeasureString("Updated Today!", updatedFont)
            Dim FilledBlackRect As New Rectangle(itmRect.X + 8, itmRect.Bottom - 48, TextSize.Width + 2, TextSize.Height + 2)

            If _UpdateList IsNot Nothing Then
                For Each update As String() In _UpdateList
                    If Selected = True And itm.Title = update(0) And update(1).Equals("updated") Then
                        .FillRectangle(New SolidBrush(Color.FromArgb(180, Color.Black)), FilledBlackRect.X + 7, FilledBlackRect.Y, FilledBlackRect.Width, FilledBlackRect.Height)
                        .DrawRectangle(Pens.DimGray, FilledBlackRect.X + 7, FilledBlackRect.Y, FilledBlackRect.Width, FilledBlackRect.Height)
                        .DrawString("Updated Today!", updatedFont, Brushes.White, FilledBlackRect.X + 10, FilledBlackRect.Y + 1)

                    ElseIf itm.Title = update(0) And update(1).Equals("updated") Then
                        .FillRectangle(New SolidBrush(Color.FromArgb(180, Color.Black)), FilledBlackRect)
                        .DrawRectangle(Pens.DimGray, FilledBlackRect)
                        .DrawString("Updated Today!", updatedFont, Brushes.White, itmRect.X + 11, itmRect.Bottom - 47)
                    End If
                Next
            End If

            If Not Selected Then

                .DrawString(itm.Title, New Font("Segoe UI", 13), Brushes.White, New Rectangle(itmRect.X + 7, itmRect.Y + 3, itmRect.Width - 15, 25), New StringFormat With {.Trimming = StringTrimming.EllipsisCharacter})

            Else
                .SetClip(New Rectangle(itmRect.X + 7, itmRect.Y + 3, itmRect.Width - 15, 30))
                .DrawString(itm.Title, New Font("Segoe UI", 13), Brushes.White, New Point(itmRect.X + 7 + animationPos, itmRect.Y + 3))
                .ResetClip()

                .DrawImage(My.Resources.play, New Rectangle(itmRect.X + (itmRect.Width / 2) - 24, itmRect.Y + (itmRect.Height / 2) - 24, 48, 48))

                .FillRectangle(New SolidBrush(Color.FromArgb(200, Color.Black)), New Rectangle(itmRect.X, itmRect.Bottom - 23, itmRect.Width, 23))
                .DrawLine(New Pen(Color.FromArgb(40, Color.White)), itmRect.X + 1, itmRect.Bottom - 24, itmRect.X + itmRect.Width, itmRect.Bottom - 24)
                .DrawString(itm.ImdbRating, updatedFont, Brushes.White, New Point(itmRect.X + 5, itmRect.Bottom - 21))
                .DrawString(itm.EpisodeLength, updatedFont, Brushes.White, New Rectangle(itmRect.X + 7, itmRect.Bottom - 21, itmRect.Width - 14, 15), New StringFormat With {.Alignment = StringAlignment.Far})
            End If

            ' Border
            .DrawRectangle(New Pen(Color.FromArgb(40, Color.White)), itmRect)
        End With
    End Sub

    Private Function TrimString(str$, fnt As Font, g As Graphics, w%) As String
        Dim Current As String = "" : TrimString = ""

        For Each Word As String In Split(str, " ")
            Current &= Word & " "

            If g.MeasureString(Current, fnt).Width < w Then Continue For
            TrimString &= Current.Substring(0, Current.LastIndexOf(" ")).Trim & vbCrLf
            Current = Current.Substring(Current.LastIndexOf(" "))
        Next

        If Current.Trim <> String.Empty Then TrimString &= Current
    End Function

    Private Function CreateRoundRectangle(rectangle As Rectangle, radius!, Optional TopLeft As Boolean = True, Optional TopRigth As Boolean = True, Optional BottomRigth As Boolean = True, Optional BottomLeft As Boolean = True) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim l As Single = rectangle.Left
        Dim t As Single = rectangle.Top
        Dim w As Single = rectangle.Width
        Dim h As Single = rectangle.Height
        Dim d As Single = radius << 1

        If radius <= 0 Then
            path.AddRectangle(rectangle)
            Return path
        End If

        If TopLeft Then
            path.AddArc(l, t, d, d, 180, 90)
            If TopRigth Then path.AddLine(l + radius, t, l + w - radius, t) Else path.AddLine(l + radius, t, l + w, t)
        Else
            If TopRigth Then path.AddLine(l, t, l + w - radius, t) Else path.AddLine(l, t, l + w, t)
        End If

        If TopRigth Then
            path.AddArc(l + w - d, t, d, d, 270, 90)
            If BottomRigth Then path.AddLine(l + w, t + radius, l + w, t + h - radius) Else path.AddLine(l + w, t + radius, l + w, t + h)
        Else
            If BottomRigth Then path.AddLine(l + w, t, l + w, t + h - radius) Else path.AddLine(l + w, t, l + w, t + h)
        End If

        If BottomRigth Then
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90)
            If BottomLeft Then path.AddLine(l + w - radius, t + h, l + radius, t + h) Else path.AddLine(l + w - radius, t + h, l, t + h)
        Else
            If BottomLeft Then path.AddLine(l + w, t + h, l + radius, t + h) Else path.AddLine(l + w, t + h, l, t + h)
        End If

        If BottomLeft Then
            path.AddArc(l, t + h - d, d, d, 90, 90)
            If TopLeft Then path.AddLine(l, t + h - radius, l, t + radius) Else path.AddLine(l, t + h - radius, l, t)
        Else
            If TopLeft Then path.AddLine(l, t + h, l, t + radius) Else path.AddLine(l, t + h, l, t)
        End If

        Return path
    End Function
#End Region

#Region "Items"

#Region "Structures"

    Public Structure Item
        Public Property Title As String
        Public Property ImdbRating As String
        Public Property EpisodeLength As String
        Public Property ThumbnailURL As String
        Public Property Tag As Object
        Public Image As Bitmap

        Sub New(Title As String, ImdbRating As String, EpisodeLength As String, ThumbnailURL As String, Image As Bitmap, Tag As Object)
            Me.Title = Title : Me.ImdbRating = ImdbRating : Me.EpisodeLength = EpisodeLength : Me.Image = Image : Me.Tag = Tag : Me.ThumbnailURL = ThumbnailURL
        End Sub
    End Structure
    <Serializable> _
    Public Structure ItemFavorite
        Public Property Title As String
        Public Property ImdbRating As String
        Public Property EpisodeLength As String
        Public Property Tag As Object
        Public Image As Image

        Sub New(Title As String, ImdbRating As String, EpisodeLength As String, Image As Image, Tag As Object)
            Me.Title = Title : Me.ImdbRating = ImdbRating : Me.EpisodeLength = EpisodeLength : Me.Image = Image : Me.Tag = Tag
        End Sub
    End Structure

    Public Shared Function ImageFromBase64String(ByVal base64 As String) As Image
        Using memStream As New MemoryStream(Convert.FromBase64String(base64))
            Dim result As Image = Image.FromStream(memStream)
            memStream.Close()
            Return result
        End Using
    End Function
#End Region

    Public Sub AddItem(Title As String, ImdbRating As String, EpisodeLength As String, ThumbnailURL As String, Image As Bitmap, Tag As Object, Optional UpdateList As List(Of String()) = Nothing)

        If UpdateList IsNot Nothing Then
            _UpdateList = UpdateList
        End If

        Items.Add(New Item(Title, ImdbRating, EpisodeLength, ThumbnailURL, Image, Tag))
        Invalidate()
    End Sub
    Public Sub AddItems(itemList As List(Of Item), Optional UpdateList As List(Of String()) = Nothing)

        If UpdateList IsNot Nothing Then
            _UpdateList = UpdateList
        End If

        For Each Item As Item In itemList
            Items.Add(New Item(Item.Title, Item.ImdbRating, Item.EpisodeLength, Item.ThumbnailURL, Item.Image, Item.Title))
        Next
        Invalidate()
    End Sub
    Public Sub AddItems(itemList As List(Of ItemFavorite), Optional UpdateList As List(Of String()) = Nothing)

        If UpdateList IsNot Nothing Then
            _UpdateList = UpdateList
        End If

        For Each Item As ItemFavorite In itemList
            Items.Add(New Item(Item.Title, Item.ImdbRating, Item.EpisodeLength, String.Empty, Item.Image, Item.Title))
        Next
        Invalidate()
    End Sub

    Private Function GetItemRectangle(i%) As Rectangle
        Dim ItemsPerRow As Integer = (Width - 17) \ ItemSize.Width
        Dim TotalRows As Integer = (Items.Count \ ItemsPerRow) + 1
        Dim currentRow As Integer = i \ ItemsPerRow
        Dim currentColumn As Integer = i - (currentRow * ItemsPerRow)

        ExcessHeight = (_ScrollValue / 100) * (Height - ((TotalRows + 2) * ItemSize.Height))

        GetItemRectangle = New Rectangle((currentColumn * (ItemSize.Width + 5)) + 20, ((currentRow * (ItemSize.Height + 2)) + ExcessHeight) + 45, ItemSize.Width, ItemSize.Height)

    End Function
#End Region

#Region "Mouse Events"

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Dim G As Graphics = Me.CreateGraphics()
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        Dim SelectedItemRectangle As Rectangle = GetItemRectangle(SelectedItem)

        If SelectedItemRectangle.Contains(e.Location) Then
            RaiseEvent ItemClicked(SelectedItem, e.Button)
        End If

        'Dim topsize As SizeF = g.MeasureString("Top Series", New Font("Segoe UI Light", 16))
        'Dim LabelTopSeries As New Rectangle(22, GetItemRectangle(0).Y - 35, topsize.Width, topsize.Height)

        'If LabelTopSeries.Contains(e.Location) Then
        '    _SelectedTab = Categories.TopSeries
        '    RaiseEvent LabelClicked(_SelectedTab)
        'End If

        Dim controlFont As New Font("Segoe UI Light", 16)

        Dim newSeriesSize As SizeF = G.MeasureString("New Series", controlFont)
        Dim LabelNewSeries As New Rectangle(22, GetItemRectangle(0).Y - 35, newSeriesSize.Width, newSeriesSize.Height)

        Dim moviesSize As SizeF = G.MeasureString("Movies", controlFont)
        Dim labelMovies As New Rectangle(134, GetItemRectangle(0).Y - 35, moviesSize.Width, moviesSize.Height)

        Dim favoriteSize As SizeF = G.MeasureString("Favorites", controlFont)
        Dim LabelFavorites As New Rectangle(211, GetItemRectangle(0).Y - 35, favoriteSize.Width, favoriteSize.Height)

        If LabelNewSeries.Contains(e.Location) Then
            SelectedItem = -1
            _SelectedTab = Categories.Series
            RaiseEvent LabelClicked(_SelectedTab)
        End If

        If labelMovies.Contains(e.Location) Then
            SelectedItem = -1
            _SelectedTab = Categories.Movies
            RaiseEvent LabelClicked(_SelectedTab)
        End If

        If LabelFavorites.Contains(e.Location) Then
            SelectedItem = -1
            _SelectedTab = Categories.Favorites
            RaiseEvent LabelClicked(_SelectedTab)
        End If
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        Focus()
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        SelectedItem = -1
        Invalidate()
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnDoubleClick(e As EventArgs)
        If GetItemRectangle(SelectedItem).Contains(MousePos) Then
            RaiseEvent ItemDoubleClicked(SelectedItem)
        End If
        MyBase.OnDoubleClick(e)
    End Sub

    Private OldIndex As Integer = -1
    Private Sub Ctrl_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Dim mr As New Rectangle(e.Location, New Size(1, 1))
        OldIndex = SelectedItem

        If Not GetItemRectangle(SelectedItem).IntersectsWith(mr) Then
            For i = 0 To Items.Count - 1
                If Not GetItemRectangle(i).IntersectsWith(mr) Then Continue For
                SelectedItem = i
            Next
        End If

        If OldIndex <> SelectedItem Then
            animationPos = 0
            tmrAnimation.Interval = 750
            wait = True
            Invalidate()
            RaiseEvent SelectedIndexChanged()
        End If

        Dim newSeriesSize As SizeF = G.MeasureString("New Series", headerFont)
        Dim LabelNewSeries As New Rectangle(22, GetItemRectangle(0).Y - 35, newSeriesSize.Width, newSeriesSize.Height)

        Dim favoriteSize As SizeF = G.MeasureString("Favorites", headerFont)
        Dim LabelFavorites As New Rectangle(134, GetItemRectangle(0).Y - 35, favoriteSize.Width, favoriteSize.Height)

        If LabelFavorites.Contains(e.Location) OrElse LabelNewSeries.Contains(e.Location) Then
            Me.Cursor = Cursors.Hand
        Else
            Me.Cursor = Cursors.Default
        End If

    End Sub
#End Region

#Region "Text Animation"
    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        tmrAnimation = New Timer With {.Interval = 30, .Enabled = True}
        AddHandler tmrAnimation.Tick, AddressOf OnAnimation
    End Sub

    Dim AnimationDirection As Boolean = True
    Dim wait As Boolean = True
    Sub OnAnimation(ByVal Sender As Object, e As System.EventArgs)
        Try
            If SelectedItem < 0 Then Exit Sub
            If wait Then
                wait = False
                tmrAnimation.Interval = 30
                Exit Sub
            End If
            Dim ItemTextWidth As Integer = CreateGraphics.MeasureString(Items(SelectedItem).Title, New Font("Segoe UI", 13)).Width
            If ItemTextWidth > GetItemRectangle(SelectedItem).Width - 15 Then

                If AnimationDirection Then
                    animationPos -= 1
                    If animationPos <= -(ItemTextWidth - (GetItemRectangle(SelectedItem).Width - 15)) Then
                        wait = True
                        tmrAnimation.Interval = 750
                        AnimationDirection = False
                    End If

                Else
                    animationPos += 1
                    If animationPos >= 0 Then
                        wait = True
                        tmrAnimation.Interval = 750
                        AnimationDirection = True
                    End If

                End If

            Else
                animationPos = 0
            End If
            Invalidate()
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class

Public Class DropDownMenu
    Inherits Control

    Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)
    End Sub

#Region "Declarations"
    Private MousePos As Point
    Private itemHeight As Integer = 65
    Public Event ItemClicked(ByVal Index As Integer)
#End Region

#Region "Properties "
    Private _Items As New List(Of Item)
    Public ReadOnly Property Items As List(Of Item)
        Get
            Return _Items
        End Get
    End Property

    Private _SelectedItem As Integer = -1
    Public Property SelectedItem As Integer
        Get
            Return _SelectedItem
        End Get
        Set(value As Integer)
            _SelectedItem = value
        End Set
    End Property
    Public Property NoQuery As Boolean
#End Region

#Region "Drawing"
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G = Graphics.FromImage(B)

        With G

            .DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(50, 75, 75, 75))), New Rectangle(0, 0, Width - 1, Height - 1))

            Dim path As New GraphicsPath() : path.AddEllipse(New Rectangle(-100, -100, Width + 198, Height + 198))
            Dim PGB As New PathGradientBrush(path)
            PGB.CenterColor = Color.FromArgb(3, 20, 20) : PGB.SurroundColors = {Color.FromArgb(0, 15, 15)}
            PGB.FocusScales = New Point(0.1F, 0.3F)
            e.Graphics.FillPath(PGB, path)

            If NoQuery = True Then
                Height = 0
            ElseIf Items.Count = 0 Then
                .DrawString("No TV show found.", New Font("Segoe UI", 12), Brushes.White, GetItemRectangle(0), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            Else
                For i = 0 To Items.Count - 1
                    If i = SelectedItem Then Continue For
                    DrawItem(G, Items(i), GetItemRectangle(i))
                Next

                Try
                    If SelectedItem <> -1 Then
                        Debug.WriteLine(SelectedItem)
                        Dim r = GetItemRectangle(SelectedItem)
                        Dim sItem As Item = Items.Item(SelectedItem)

                        .FillRectangle(New SolidBrush(Color.FromArgb(7, 255, 255, 255)), r)
                        DrawItem(G, sItem, r)
                    End If
                Catch ex As Exception
                End Try

            End If

            G.Dispose()
            e.Graphics.DrawImageUnscaled(B, 0, 0)
            B.Dispose()
        End With
    End Sub

    Private Sub DrawItem(G As Graphics, itm As Item, itmRect As Rectangle)
        With G
            .DrawImage(itm.Image, New Rectangle(itmRect.Left + 3, itmRect.Y + 6, 40, 54))
            .DrawString(itm.Title, New Font("Segoe UI", 12), Brushes.White, New Rectangle(itmRect.Location.X + 48, itmRect.Location.Y + 3, itmRect.Width, itmRect.Height), New StringFormat With {.Trimming = StringTrimming.EllipsisWord})
            .DrawString(itm.Author, New Font("Segoe UI Light", 9), Brushes.White, New Rectangle(itmRect.Location.X + 49, itmRect.Location.Y + 22, itmRect.Width, itmRect.Height), New StringFormat With {.Trimming = StringTrimming.EllipsisWord})
        End With
    End Sub

#End Region

#Region "Items"
    Public Sub AddItem(Title As String, Author As String, Image As Bitmap)
        Items.Add(New Item(Title, Author, Image))
        Height = itemHeight * Items.Count + 1
        Invalidate()
    End Sub

    Public Sub AddItems(itemList As Item())

        Dim MaxItem As Integer = 5
        Dim ItemCount As Integer = 1

        For Each Item As Item In itemList
            If ItemCount <= MaxItem Then
                Items.Add(New Item(Item.Title, Item.Author, Item.Image) With {.Link = Item.Link})
            End If
            ItemCount += 1
        Next

        If Not Items.Count = 0 Then
            Height = itemHeight * Items.Count + 1
        Else
            Height = itemHeight
        End If

        SelectedItem = 0
        Invalidate()
    End Sub

    Public Structure Item
        Public Property Title As String
        Public Property Author As String
        Public Property Link As String
        Public Image As Bitmap
        Public Tag As Object

        Sub New(Title As String, Author As String, Image As Bitmap)
            Me.Title = Title : Me.Image = Image : Me.Author = Author
        End Sub
    End Structure

    Private Function GetItemRectangle(index As Integer) As Rectangle
        Return New Rectangle(0, itemHeight * index, Width, itemHeight)
    End Function
#End Region

#Region "Mouse Events"
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        Dim SelectedItemRectangle As Rectangle = GetItemRectangle(SelectedItem)

        Invalidate()
        MyBase.OnMouseDown(e)
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        Focus()
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        SelectedItem = -1
        Invalidate()
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnClick(e As EventArgs)
        If GetItemRectangle(SelectedItem).Contains(MousePos) And Items.Count > 0 Then
            RaiseEvent ItemClicked(SelectedItem)
        End If
        MyBase.OnClick(e)
    End Sub


    Dim OldIndex As Integer = -1
    Private Sub Ctrl_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Dim mr As New Rectangle(e.Location, New Size(1, 1))
        MousePos = e.Location
        OldIndex = SelectedItem

        If Not GetItemRectangle(SelectedItem).IntersectsWith(mr) Then
            For i = 0 To Items.Count - 1
                If Not GetItemRectangle(i).IntersectsWith(mr) Then Continue For
                SelectedItem = i
            Next
        End If

        If OldIndex <> SelectedItem Then
            Invalidate()
        End If

    End Sub
#End Region

End Class

<DefaultEvent("Scroll")>
Public Class LogInVerticalScrollBar
    Inherits Control

#Region "Declarations"

    Private ThumbMovement As Integer
    Private TSA As Rectangle
    Private BSA As Rectangle
    Private Shaft As Rectangle
    Private Thumb As Rectangle
    Private ShowThumb As Boolean
    Private ThumbPressed As Boolean
    Private _ThumbSize As Integer = 24
    Private _Minimum As Integer = 0
    Private _Maximum As Integer = 100
    Private _Value As Integer = 0
    Private _SmallChange As Integer = 1
    Private _ButtonSize As Integer = 16
    Private _LargeChange As Integer = 10
    Private _ThumbBorder As Color = Color.FromArgb(42, 53, 53)
    Private _LineColour As Color = Color.FromArgb(99, 99, 99)
    Private _ArrowColour As Color = Color.FromArgb(40, 55, 55)
    Private _BaseColour As Color = Color.FromArgb(20, 35, 35)
    Private _ThumbColour As Color = Color.FromArgb(3, 26, 26)
    Private _ThumbSecondBorder As Color = Color.FromArgb(65, 65, 65)
    Private _FirstBorder As Color = Color.FromArgb(42, 53, 53)
    Private _SecondBorder As Color = Color.FromArgb(35, 35, 35)

#End Region

#Region "Properties & Events"

    <Category("Colours")> _
    Public Property ThumbBorder As Color
        Get
            Return _ThumbBorder
        End Get
        Set(value As Color)
            _ThumbBorder = value
            Invalidate()
        End Set
    End Property

    <Category("Colours")> _
    Public Property LineColour As Color
        Get
            Return _LineColour
        End Get
        Set(value As Color)
            _LineColour = value
            Invalidate()

        End Set
    End Property

    <Category("Colours")> _
    Public Property ArrowColour As Color
        Get
            Return _ArrowColour
        End Get
        Set(value As Color)
            _ArrowColour = value
            Invalidate()
        End Set
    End Property

    <Category("Colours")> _
    Public Property BaseColour As Color
        Get
            Return _BaseColour
        End Get
        Set(value As Color)
            _BaseColour = value
            Invalidate()
        End Set
    End Property

    <Category("Colours")> _
    Public Property ThumbColour As Color
        Get
            Return _ThumbColour
        End Get
        Set(value As Color)
            _ThumbColour = value
            Invalidate()
        End Set
    End Property

    <Category("Colours")> _
    Public Property ThumbSecondBorder As Color
        Get
            Return _ThumbSecondBorder
        End Get
        Set(value As Color)
            _ThumbSecondBorder = value
            Invalidate()
        End Set
    End Property

    <Category("Colours")> _
    Public Property FirstBorder As Color
        Get
            Return _FirstBorder
        End Get
        Set(value As Color)
            _FirstBorder = value
            Invalidate()
        End Set
    End Property

    <Category("Colours")> _
    Public Property SecondBorder As Color
        Get
            Return _SecondBorder
        End Get
        Set(value As Color)
            _SecondBorder = value
            Invalidate()
        End Set
    End Property

    Event Scroll(ByVal sender As Object)

    Property Minimum() As Integer
        Get
            Return _Minimum
        End Get
        Set(ByVal value As Integer)
            _Minimum = value
            If value > _Value Then _Value = value
            If value > _Maximum Then _Maximum = value
            InvalidateLayout()
        End Set
    End Property

    Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal value As Integer)
            _Maximum = value
            If value < _Value Then _Value = value
            If value < _Minimum Then _Minimum = value
        End Set
    End Property

    Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            Select Case value
                Case Is = _Value
                    Exit Property
                Case Is < _Minimum
                    _Value = _Minimum
                Case Is > _Maximum
                    _Value = _Maximum
                Case Else
                    _Value = value
            End Select
            InvalidatePosition()
            RaiseEvent Scroll(Me)
        End Set
    End Property

    Public Property SmallChange() As Integer
        Get
            Return _SmallChange
        End Get
        Set(ByVal value As Integer)
            Select Case value
                Case Is < 1
                Case Is >
                    CInt(_SmallChange = value)
            End Select
        End Set
    End Property

    Public Property LargeChange() As Integer
        Get
            Return _LargeChange
        End Get
        Set(ByVal value As Integer)
            Select Case value
                Case Is < 1
                Case Else
                    _LargeChange = value
            End Select
        End Set
    End Property

    Public Property ButtonSize As Integer
        Get
            Return _ButtonSize
        End Get
        Set(value As Integer)
            Select Case value
                Case Is < 16
                    _ButtonSize = 16
                Case Else
                    _ButtonSize = value
            End Select
        End Set
    End Property

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        InvalidateLayout()
    End Sub

    Private Sub InvalidateLayout()
        TSA = New Rectangle(0, 1, Width, 0)
        Shaft = New Rectangle(0, TSA.Bottom - 1, Width, Height - 3)
        ShowThumb = CBool(((_Maximum - _Minimum)))
        If ShowThumb Then
            Thumb = New Rectangle(1, 0, Width - 2, _ThumbSize)
        End If
        RaiseEvent Scroll(Me)
        InvalidatePosition()
    End Sub

    Private Sub InvalidatePosition()
        Thumb.Y = CInt(((_Value - _Minimum) / (_Maximum - _Minimum)) * (Shaft.Height - _ThumbSize) + 1)
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left AndAlso ShowThumb Then
            If TSA.Contains(e.Location) Then
                ThumbMovement = _Value - _SmallChange
            ElseIf BSA.Contains(e.Location) Then
                ThumbMovement = _Value + _SmallChange
            Else
                If Thumb.Contains(e.Location) Then
                    ThumbPressed = True
                    Return
                Else
                    If e.Y < Thumb.Y Then
                        ThumbMovement = _Value - _LargeChange
                    Else
                        ThumbMovement = _Value + _LargeChange
                    End If
                End If
            End If
            Value = Math.Min(Math.Max(ThumbMovement, _Minimum), _Maximum)
            'InvalidatePosition()
        End If
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If ThumbPressed AndAlso ShowThumb Then
            Dim ThumbPosition As Integer = e.Y - TSA.Height - (_ThumbSize \ 2)
            Dim ThumbBounds As Integer = Shaft.Height - _ThumbSize
            ThumbMovement = CInt((ThumbPosition / ThumbBounds) * (_Maximum - _Minimum)) + _Minimum
            Value = Math.Min(Math.Max(ThumbMovement, _Minimum), _Maximum)
            InvalidatePosition()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        ThumbPressed = False
    End Sub

#End Region

#Region "Draw Control"

    Sub New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or _
                            ControlStyles.UserPaint Or ControlStyles.Selectable Or _
                            ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Size = New Size(24, 50)
    End Sub

    Dim P() As Point

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Height, Height)
        Dim G = Graphics.FromImage(B)
        With G
            .TextRenderingHint = TextRenderingHint.ClearTypeGridFit
            .SmoothingMode = SmoothingMode.HighQuality
            .PixelOffsetMode = PixelOffsetMode.HighQuality
            .Clear(_BaseColour)

            P = {New Point(CInt(Width / 2), 5), New Point(CInt(Width / 4), 13), New Point(CInt(Width / 2 - 2), 13), New Point(CInt(Width / 2 - 2), Height - 13), _
                                New Point(CInt(Width / 4), Height - 13), New Point(CInt(Width / 2), Height - 5), New Point(CInt(Width - Width / 4 - 1), Height - 13), New Point(CInt(Width / 2 + 2), Height - 13), _
                                New Point(CInt(Width / 2 + 2), 13), New Point(CInt(Width - Width / 4 - 1), 13)}

            .FillPolygon(New SolidBrush(_ArrowColour), P)
            .FillRectangle(New SolidBrush(_ThumbColour), Thumb)
            .DrawRectangle(New Pen(_ThumbBorder), Thumb)
            .DrawRectangle(New Pen(_ThumbSecondBorder), Thumb.X + 1, Thumb.Y + 1, Thumb.Width - 2, Thumb.Height - 2)
            .DrawLine(New Pen(_LineColour, 2), New Point(CInt(Thumb.Width / 2 + 1), Thumb.Y + 4), New Point(CInt(Thumb.Width / 2 + 1), Thumb.Bottom - 4))
            ' .DrawRectangle(New Pen(_FirstBorder), 0, 0, Width - 1, Height - 1)
            .DrawLine(New Pen(Color.FromArgb(17, 26, 26)), 0, 0, Width - 1, 0)
            .DrawLine(New Pen(Color.FromArgb(2, 18, 18)), Width - 1, 0, Width - 1, Height - 1)
            .DrawLine(New Pen(Color.FromArgb(2, 15, 15)), 0, Height - 1, Width - 1, Height - 1)
            .DrawLine(New Pen(Color.FromArgb(2, 17, 17)), 0, 0, 0, Height - 1)
            .DrawRectangle(New Pen(_SecondBorder), 1, 1, Width - 3, Height - 3)
        End With
        ' MyBase.OnPaint(e)
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

#End Region

End Class