Imports DesjardinsQuestionsVB.Enums.Codes
Imports DesjardinsQuestionsVB.BusinessEntities
Imports DesjardinsQuestionsVB.Configuration
Imports DesjardinsQuestionsVB.Fichier

Public Class Traitement
    Implements ITraitementBC

    Private _PrefixeNomFichierNonPilote As String
    Public Property PrefixeNomFichierNonPilote() As String
        Get
            Return _PrefixeNomFichierNonPilote
        End Get
        Set(ByVal value As String)
            _PrefixeNomFichierNonPilote = value
        End Set
    End Property

    Private _IdentifiantTraitement As String
    Public Property IdentifiantTraitement() As String
        Get
            Return _IdentifiantTraitement
        End Get
        Set(ByVal value As String)
            _IdentifiantTraitement = value
        End Set
    End Property

    Public Sub Traiter(fichier As IFichier, configuration As IConfiguration, fichiersPilotesPourCeFichier As IEnumerable(Of FichierPilote),
                       nomRepertoireSource As String) Implements ITraitementBC.Traiter
        Dim CD As CodeRetour
        Dim message As String = ""
        Dim arretTraitement As Boolean

        If fichiersPilotesPourCeFichier.Count = 0 Then
            message = $"{fichier.NomComplet}. Le fichier sera copié dans le répertoire {configuration.RepertoireFichierInconnu}"

            Instance.AjouterFichierExecution(fichier.NomComplet)
        End If

        If fichiersPilotesPourCeFichier.Count > 0 Then
            For Each fp In fichiersPilotesPourCeFichier
                If fp.idTraitement.Equals(IdentifiantTraitement) Then
                    Instance.AjouterFichierExecution(fichier.NomComplet)
                Else
                    Instance.AjouterFichierOrphelin(fichier.NomComplet, fp.NomPrefixFichier, fp.idTraitement)
                End If
            Next
        End If

        If Not String.IsNullOrWhiteSpace(message) Then
            Instance.AjouterMessage(PrefixeNomFichierNonPilote, message)
        End If

        Dim repertoiresDestination = New List(Of String)

        If fichiersPilotesPourCeFichier.Count = 0 Then
            repertoiresDestination.Add(configuration.RepertoireFichierInconnu)
        Else
            repertoiresDestination.AddRange(fichiersPilotesPourCeFichier.Where(Function(x) Not String.IsNullOrEmpty(x.NomRepertoireDestination)).Select(Function(x) String.Concat(x.NomRepertoireDestination) + "\").Distinct)
        End If

        For Each repertoireDestination In repertoiresDestination
            Dim nomCompletDestination = repertoireDestination + fichier.Nom

            CD = fichier.TryCopier(nomCompletDestination)

            If CD.Equals(CodeRetour.DossierInexistant) Then
                Dim repertoireFichierInconnu = configuration.RepertoireFichierInconnu

                Instance.AjouterMessage(CD, $"Erreur lors de la copie du fichier : {fichier.NomComplet}. 
                                              Nom du repertoire destination : {repertoireDestination}. 
                                              Le fichier sera copié dans le répertoire {repertoireFichierInconnu}")

                CD = fichier.TryCopier(repertoireFichierInconnu + fichier.Nom)

                If Not CD.Equals(CodeRetour.Succes) Then
                    Instance.AjouterMessage(CD, $"Erreur lors de la copie du fichier : {fichier.NomComplet}. 
                                                  Nom du repertoire destination : {repertoireFichierInconnu}")
                    arretTraitement = True
                    Exit For
                End If

            ElseIf Not CD.Equals(CodeRetour.Succes) Then
                Instance.AjouterMessage(CD, $"Erreur lors de la copie du fichier : {fichier.NomComplet}. 
                                              Nom du repertoire destination : {repertoireDestination}")
                arretTraitement = True
                Exit For
            End If
        Next

        If arretTraitement Then
            Throw fichier.ExceptionCourante
        End If

        CD = fichier.TryDetruire()

        If Not CD.Equals(CodeRetour.Succes) Then

            Instance.AjouterMessage(CD, $"Erreur lors de la suppression du fichier : {fichier.NomComplet}. 
                                          Nom du repertoire source : {nomRepertoireSource}")
            arretTraitement = True
        End If

        If arretTraitement Then
            Throw fichier.ExceptionCourante
        End If
    End Sub
End Class
