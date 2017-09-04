Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Globalization
Imports System.Security.Permissions
Imports System.Runtime.InteropServices
Imports System.ComponentModel.Design

Namespace Controls

    <DefaultProperty("SelectedPanel"), _
    DefaultEvent("SelectedIndexChanged"), _
    Designer(GetType(Design.PanelManagerDesigner))> _
    Public Class PanelManager
        Inherits System.Windows.Forms.Control

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'UserControl1 overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container
        End Sub

#End Region

        Private m_SelectedPanel As Controls.ManagedPanel

        Public Event SelectedIndexChanged As EventHandler

        'ManagedPanels
        <Editor(GetType(Editors.ManagedPanelCollectionEditor), GetType(UITypeEditor))> _
        Public ReadOnly Property ManagedPanels() As ControlCollection
            Get
                Return MyBase.Controls
            End Get
        End Property

        'SelectedPanel
        <TypeConverter(GetType(TypeConverters.SelectedPanelConverter))> _
        Public Property SelectedPanel() As Controls.ManagedPanel
            Get
                Return m_SelectedPanel
            End Get
            Set(ByVal Value As Controls.ManagedPanel)
                If m_SelectedPanel Is Value Then Return
                m_SelectedPanel = Value
                OnSelectedPanelChanged(EventArgs.Empty)
            End Set
        End Property

        'SelectedIndex
        <Browsable(False)> _
        Public Property SelectedIndex() As Int32
            Get
                Return Me.ManagedPanels.IndexOf(CType(Me.SelectedPanel, Controls.ManagedPanel))
            End Get
            Set(ByVal Value As Int32)
                If Value = -1 Then
                    Me.SelectedPanel = Nothing
                Else
                    Me.SelectedPanel = DirectCast(Me.ManagedPanels(Value), ManagedPanel)
                End If
            End Set
        End Property

        'DefaultSize
        Protected Overrides ReadOnly Property DefaultSize() As System.Drawing.Size
            Get
                Return New Size(200, 100)
            End Get
        End Property

        Protected Overridable Sub OnSelectedPanelChanged(ByVal e As EventArgs)
            Static oldSelection As ManagedPanel = Nothing
            If Not (oldSelection Is Nothing) Then
                oldSelection.Visible = False
            End If
            If Not (m_SelectedPanel Is Nothing) Then
                CType(m_SelectedPanel, Controls.ManagedPanel).Visible = True
            End If
            Dim tabChanged As Boolean
            If m_SelectedPanel Is Nothing Then
                tabChanged = Not (oldSelection Is Nothing)
            Else
                tabChanged = Not (m_SelectedPanel.Equals(oldSelection))
            End If
            If tabChanged And Me.Created Then
                RaiseEvent SelectedIndexChanged(Me, EventArgs.Empty)
            End If
            oldSelection = CType(m_SelectedPanel, Controls.ManagedPanel)
        End Sub

        Protected Overrides Sub OnControlAdded(ByVal e As System.Windows.Forms.ControlEventArgs)
            If Not (TypeOf e.Control Is Controls.ManagedPanel) Then
                Throw New ArgumentException("Only Mangel.Controls.ManagedPanels can be added to a Mangel.Controls.PanelManger.")
            End If
            If Not (Me.SelectedPanel Is Nothing) Then
                CType(Me.SelectedPanel, Controls.ManagedPanel).Visible = False
            End If
            Me.SelectedPanel = DirectCast(e.Control, Controls.ManagedPanel)
            e.Control.Visible = True
            MyBase.OnControlAdded(e)
        End Sub

        Protected Overrides Sub OnControlRemoved(ByVal e As System.Windows.Forms.ControlEventArgs)
            If Not (TypeOf e.Control Is Controls.ManagedPanel) Then Return
            If Me.ManagedPanels.Count > 0 Then
                Me.SelectedIndex = 0
            Else
                Me.SelectedPanel = Nothing
            End If
            MyBase.OnControlRemoved(e)
        End Sub

    End Class

    <Designer(GetType(Design.ManagedPanelDesigner)), _
    ToolboxItem(False)> _
    Public Class ManagedPanel
        Inherits System.Windows.Forms.ScrollableControl

        Public Sub New()
            MyBase.Dock = DockStyle.Fill
            SetStyle(ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
        End Sub

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), _
        DefaultValue(GetType(DockStyle), "Fill")> _
        Public Overrides Property Dock() As System.Windows.Forms.DockStyle
            Get
                Return MyBase.Dock
            End Get
            Set(ByVal value As System.Windows.Forms.DockStyle)
                MyBase.Dock = DockStyle.Fill
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), _
        DefaultValue(GetType(AnchorStyles), "None")> _
        Public Overrides Property Anchor() As AnchorStyles
            Get
                Return AnchorStyles.None
            End Get
            Set(ByVal value As AnchorStyles)
                MyBase.Anchor = AnchorStyles.None
            End Set
        End Property

        Protected Overrides Sub OnLocationChanged(ByVal e As System.EventArgs)
            MyBase.OnLocationChanged(e)
            MyBase.Location = Point.Empty
        End Sub

        Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
            MyBase.OnSizeChanged(e)
            If Me.Parent Is Nothing Then
                Me.Size = Size.Empty
            Else
                Me.Size = Me.Parent.ClientSize
            End If
        End Sub

        Protected Overrides Sub OnParentChanged(ByVal e As System.EventArgs)
            If Not (TypeOf Me.Parent Is Controls.PanelManager) AndAlso Not (Me.Parent Is Nothing) Then
                Throw New ArgumentException("Managed Panels may only be added to a Panel Manager.")
            End If
            MyBase.OnParentChanged(e)
        End Sub

    End Class

End Namespace

Namespace Design

    Public Class PanelManagerDesigner
        Inherits System.Windows.Forms.Design.ParentControlDesigner

        Private m_verbs As DesignerVerbCollection = New DesignerVerbCollection
        Private m_DesignerHost As IDesignerHost
        Private m_SelectionService As ISelectionService

        Private ReadOnly Property HostControl() As Controls.PanelManager
            Get
                Return DirectCast(Me.Control, Controls.PanelManager)
            End Get
        End Property

        Public Sub New()
            MyBase.New()

            Dim verb1 As New DesignerVerb("Add MangedPanel", AddressOf OnAddPanel)
            Dim verb2 As New DesignerVerb("Remove ManagedPanel", AddressOf OnRemovePanel)
            m_verbs.AddRange(New DesignerVerb() {verb1, verb2})

        End Sub

        Protected Overrides Sub OnPaintAdornments(ByVal pe As System.Windows.Forms.PaintEventArgs)
            'Don't want DrawGrid Dots.
        End Sub

        Public Overrides ReadOnly Property Verbs() As System.ComponentModel.Design.DesignerVerbCollection
            Get
                If m_verbs.Count = 2 Then
                    If HostControl.ManagedPanels.Count > 0 Then
                        m_verbs(1).Enabled = True
                    Else
                        m_verbs(1).Enabled = False
                    End If
                End If
                Return m_verbs
            End Get
        End Property

        Public ReadOnly Property DesignerHost() As IDesignerHost
            Get
                If m_DesignerHost Is Nothing Then
                    m_DesignerHost = DirectCast(GetService(GetType(IDesignerHost)), IDesignerHost)
                End If
                Return m_DesignerHost
            End Get
        End Property

        Public ReadOnly Property SelectionService() As ISelectionService
            Get
                If m_SelectionService Is Nothing Then
                    m_SelectionService = DirectCast(getservice(GetType(ISelectionService)), ISelectionService)
                End If
                Return m_SelectionService
            End Get
        End Property

        Private Sub OnAddPanel(ByVal sender As Object, ByVal e As EventArgs)

            Dim oldManagedPanels As Control.ControlCollection = HostControl.Controls

            RaiseComponentChanging(TypeDescriptor.GetProperties(HostControl)("ManagedPanels"))

            Dim P As Controls.ManagedPanel = DirectCast(DesignerHost.CreateComponent(GetType(Controls.ManagedPanel)), Controls.ManagedPanel)
            P.Text = P.Name
            HostControl.ManagedPanels.Add(P)

            RaiseComponentChanged(TypeDescriptor.GetProperties(HostControl)("ManagedPanels"), oldManagedPanels, HostControl.ManagedPanels)
            HostControl.SelectedPanel = P

            SetVerbs()

        End Sub

        Private Sub OnRemovePanel(ByVal sender As Object, ByVal e As EventArgs)

            Dim oldManagedPanels As Control.ControlCollection = HostControl.Controls

            If HostControl.SelectedIndex < 0 Then Return

            RaiseComponentChanging(TypeDescriptor.GetProperties(HostControl)("TabPages"))

            DesignerHost.DestroyComponent(CType(HostControl.ManagedPanels(HostControl.SelectedIndex), Controls.ManagedPanel))

            RaiseComponentChanged(TypeDescriptor.GetProperties(HostControl)("ManagedPanels"), oldManagedPanels, HostControl.ManagedPanels)

            SelectionService.SetSelectedComponents(New IComponent() {HostControl}, SelectionTypes.Auto)

            SetVerbs()
        End Sub

        Private Sub SetVerbs()

            Select Case HostControl.ManagedPanels.Count
                Case 0
                    Verbs(1).Enabled = False
                Case 1
                    Verbs(1).Enabled = True
            End Select

        End Sub

        Protected Overrides Sub PostFilterProperties(ByVal properties As System.Collections.IDictionary)
            properties.Remove("AutoScroll")
            properties.Remove("AutoScrollMargin")
            properties.Remove("AutoScrollMinSize")
            properties.Remove("Text")
            MyBase.PostFilterProperties(properties)
        End Sub

        Public Overrides Sub OnSetComponentDefaults()
            HostControl.ManagedPanels.Add(DirectCast(DesignerHost.CreateComponent(GetType(Controls.ManagedPanel)), Controls.ManagedPanel))
            HostControl.ManagedPanels.Add(DirectCast(DesignerHost.CreateComponent(GetType(Controls.ManagedPanel)), Controls.ManagedPanel))
            Dim pm As Controls.PanelManager = DirectCast(Me.Control, Controls.PanelManager)
            pm.ManagedPanels(0).Text = pm.ManagedPanels(0).Name
            pm.ManagedPanels(1).Text = pm.ManagedPanels(1).Name
            HostControl.SelectedIndex = 0
        End Sub

    End Class

    Public Class ManagedPanelDesigner
        Inherits System.Windows.Forms.Design.ScrollableControlDesigner

        Private m_verbs As DesignerVerbCollection = New DesignerVerbCollection
        Private m_SelectionService As ISelectionService

        Private ReadOnly Property HostControl() As Controls.ManagedPanel
            Get
                Return DirectCast(Me.Control, Controls.ManagedPanel)
            End Get
        End Property

        Public ReadOnly Property SelectionService() As ISelectionService
            Get
                If m_SelectionService Is Nothing Then
                    m_SelectionService = DirectCast(getservice(GetType(ISelectionService)), ISelectionService)
                End If
                Return m_SelectionService
            End Get
        End Property

        Public Sub New()
            MyBase.New()

            Dim verb1 As New DesignerVerb("Select PanelManager", AddressOf OnSelectManager)
            m_verbs.Add(verb1)

        End Sub

        Private Sub OnSelectManager(ByVal sender As Object, ByVal e As EventArgs)
            If Not Me.HostControl.Parent Is Nothing Then
                Me.SelectionService.SetSelectedComponents(New Component() {Me.HostControl.Parent})
            End If
        End Sub

        Public Overrides ReadOnly Property SelectionRules() As System.Windows.Forms.Design.SelectionRules
            Get
                Return System.Windows.Forms.Design.SelectionRules.Visible
            End Get
        End Property

        Protected Overrides Sub OnPaintAdornments(ByVal pe As System.Windows.Forms.PaintEventArgs)
            MyBase.OnPaintAdornments(pe)
            Dim penColor As Color
            If Me.Control.BackColor.GetBrightness >= 0.5 Then
                penColor = ControlPaint.Dark(Me.Control.BackColor)
            Else
                penColor = Color.White
            End If
            Dim dashedPen As New Pen(penColor)
            Dim borderRectangle As Rectangle = Me.Control.ClientRectangle
            borderRectangle.Width -= 1
            borderRectangle.Height -= 1
            dashedPen.DashStyle = Drawing2D.DashStyle.Dash
            pe.Graphics.DrawRectangle(dashedPen, borderRectangle)
            dashedPen.Dispose()
        End Sub

        Public Overrides ReadOnly Property Verbs() As System.ComponentModel.Design.DesignerVerbCollection
            Get
                Return m_verbs
            End Get
        End Property

        Protected Overrides Sub PostFilterProperties(ByVal properties As System.Collections.IDictionary)
            properties.Remove("Anchor")
            properties.Remove("TabStop")
            properties.Remove("TabIndex")
            MyBase.PostFilterProperties(properties)
        End Sub

        Public Overrides Sub OnSetComponentDefaults()
            MyBase.InitializeNewComponent(Nothing)
            Me.Control.Visible = True
        End Sub

    End Class

End Namespace

Namespace Editors

    Public Class ManagedPanelCollectionEditor
        Inherits System.ComponentModel.Design.CollectionEditor

        Public Sub New(ByVal type As Type)
            MyBase.New(type)
        End Sub

        Protected Overrides Function CreateCollectionItemType() As System.Type
            Return GetType(Controls.ManagedPanel)
        End Function

    End Class

End Namespace

Namespace TypeConverters

    Public Class SelectedPanelConverter
        Inherits ReferenceConverter

        Public Sub New()
            MyBase.New(GetType(Controls.ManagedPanel))
        End Sub

        Protected Overrides Function IsValueAllowed(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object) As Boolean
            If Not (context Is Nothing) Then
                Dim pm As Controls.PanelManager = DirectCast(context.Instance, Controls.PanelManager)
                Return pm.ManagedPanels.Contains(CType(value, Controls.ManagedPanel))
            End If
            Return False
        End Function

    End Class

End Namespace