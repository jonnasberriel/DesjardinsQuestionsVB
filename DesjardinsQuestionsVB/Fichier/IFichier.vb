Imports DesjardinsQuestionsVB.Enums.Codes

Namespace Fichier
    Public Interface IFichier
        Property NomComplet() As String
        Property Nom() As String

        Function TryCopier(path As String) As CodeRetour
        Function TryDetruire() As CodeRetour
        Property ExceptionCourante() As System.Exception
    End Interface
End Namespace