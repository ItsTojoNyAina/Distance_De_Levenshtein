using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        // Chemin du fichier texte contenant les mots en français
        string cheminFichier = "liste_francais.txt";

        // Initialiser le dictionnaire pour stocker les mots
        Dictionary<string, bool> dictionnaireMots = new Dictionary<string, bool>();

        try
        {
            // Lire le fichier ligne par ligne
            foreach (string ligne in File.ReadLines(cheminFichier))
            {
                // Supprimer les espaces ou les nouvelles lignes en trop
                string mot = ligne.Trim();

                // Ajouter le mot dans le dictionnaire
                if (!string.IsNullOrEmpty(mot) && !dictionnaireMots.ContainsKey(mot))
                {
                    dictionnaireMots.Add(mot, true);
                }
            }

            // Afficher le nombre total de mots chargés
            Console.WriteLine($"Nombre total de mots chargés : {dictionnaireMots.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
        }
    }
}

