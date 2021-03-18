Imports DesjardinsQuestionsVB.Configuration
Imports DesjardinsQuestionsVB.Fichier

Public Interface ITraitementBC
    Sub Traiter(fichier As IFichier, configuration As IConfiguration, fichiersPilotesPourCeFichier As IEnumerable(Of BusinessEntities.FichierPilote),
                nomRepertoireSource As String)
End Interface
