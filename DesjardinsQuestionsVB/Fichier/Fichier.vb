Imports DesjardinsQuestionsVB.Enums.Codes

Namespace Fichier
    Public Class Fichier
        Implements IFichier

        Private _NomComplet As String
        Public Property NomComplet() As String Implements IFichier.NomComplet
            Get
                Return _NomComplet
            End Get
            Set(ByVal value As String)
                _NomComplet = value
            End Set
        End Property

        Private _Nom As String
        Public Property Nom As String Implements IFichier.Nom
            Get
                Return _Nom
            End Get
            Set(value As String)
                _Nom = value
            End Set
        End Property

        Private _ExceptionCourante As System.Exception
        Public Property ExceptionCourante As System.Exception Implements IFichier.ExceptionCourante
            Get
                Return _ExceptionCourante
            End Get
            Set(value As System.Exception)
                _ExceptionCourante = value
            End Set
        End Property

        Public Function TryCopier(path As String) As CodeRetour Implements IFichier.TryCopier
            Throw New NotImplementedException()
        End Function

        Public Function TryDetruire() As CodeRetour Implements IFichier.TryDetruire
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace