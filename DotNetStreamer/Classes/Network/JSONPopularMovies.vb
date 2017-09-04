Public Class JSONPopularMovies
    Public Class Result
        Public Property adult() As Boolean
            Get
                Return m_adult
            End Get
            Set(value As Boolean)
                m_adult = Value
            End Set
        End Property
        Private m_adult As Boolean
        Public Property backdrop_path() As String
            Get
                Return m_backdrop_path
            End Get
            Set(value As String)
                m_backdrop_path = Value
            End Set
        End Property
        Private m_backdrop_path As String
        Public Property id() As Integer
            Get
                Return m_id
            End Get
            Set(value As Integer)
                m_id = Value
            End Set
        End Property
        Private m_id As Integer
        Public Property original_title() As String
            Get
                Return m_original_title
            End Get
            Set(value As String)
                m_original_title = Value
            End Set
        End Property
        Private m_original_title As String
        Public Property release_date() As String
            Get
                Return m_release_date
            End Get
            Set(value As String)
                m_release_date = Value
            End Set
        End Property
        Private m_release_date As String
        Public Property poster_path() As String
            Get
                Return m_poster_path
            End Get
            Set(value As String)
                m_poster_path = Value
            End Set
        End Property
        Private m_poster_path As String
        Public Property popularity() As Double
            Get
                Return m_popularity
            End Get
            Set(value As Double)
                m_popularity = Value
            End Set
        End Property
        Private m_popularity As Double
        Public Property title() As String
            Get
                Return m_title
            End Get
            Set(value As String)
                m_title = Value
            End Set
        End Property
        Private m_title As String
        Public Property vote_average() As Double
            Get
                Return m_vote_average
            End Get
            Set(value As Double)
                m_vote_average = Value
            End Set
        End Property
        Private m_vote_average As Double
        Public Property vote_count() As Integer
            Get
                Return m_vote_count
            End Get
            Set(value As Integer)
                m_vote_count = Value
            End Set
        End Property
        Private m_vote_count As Integer
    End Class

    Public Class RootObject
        Public Property page() As Integer
            Get
                Return m_page
            End Get
            Set(value As Integer)
                m_page = Value
            End Set
        End Property
        Private m_page As Integer
        Public Property results() As List(Of Result)
            Get
                Return m_results
            End Get
            Set(value As List(Of Result))
                m_results = Value
            End Set
        End Property
        Private m_results As List(Of Result)
        Public Property total_pages() As Integer
            Get
                Return m_total_pages
            End Get
            Set(value As Integer)
                m_total_pages = Value
            End Set
        End Property
        Private m_total_pages As Integer
        Public Property total_results() As Integer
            Get
                Return m_total_results
            End Get
            Set(value As Integer)
                m_total_results = Value
            End Set
        End Property
        Private m_total_results As Integer
    End Class
End Class
