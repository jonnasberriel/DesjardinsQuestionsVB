Namespace BusinessEntities

    Public Class FichierPilote
        Private _idTraitement As Integer
        Public Property idTraitement() As Integer
            Get
                Return _idTraitement
            End Get
            Set(ByVal value As Integer)
                _idTraitement = value
            End Set
        End Property

        Private _NomPrefixFichier As String
        Public Property NomPrefixFichier() As String
            Get
                Return _NomPrefixFichier
            End Get
            Set(ByVal value As String)
                _NomPrefixFichier = value
            End Set
        End Property

        Private _NomRepertoireDestination As String
        Public Property NomRepertoireDestination() As String
            Get
                Return _NomRepertoireDestination
            End Get
            Set(ByVal value As String)
                _NomRepertoireDestination = value
            End Set
        End Property
    End Class
End Namespace
