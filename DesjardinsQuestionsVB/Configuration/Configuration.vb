Namespace Configuration
    Public Class Configuration
        Implements IConfiguration

        Private _RepertoireFichierInconnu As String
        Public Property RepertoireFichierInconnu() As String Implements IConfiguration.RepertoireFichierInconnu
            Get
                Return _RepertoireFichierInconnu
            End Get
            Set(ByVal value As String)
                _RepertoireFichierInconnu = value
            End Set
        End Property
    End Class
End Namespace
