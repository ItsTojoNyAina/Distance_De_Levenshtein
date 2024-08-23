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
        Dictionary<string, bool> dictionnaireMots = ChargerMotsDansDictionnaire(cheminFichier);

        if (dictionnaireMots != null)
        {
            Console.WriteLine($"Nombre total de mots chargés : {dictionnaireMots.Count}");

            // Demander à l'utilisateur d'entrer un mot
            Console.Write("Entrez un mot pour vérifier s'il existe dans le dictionnaire : ");
            string motUtilisateur = Console.ReadLine();

            // Vérifier si le mot existe dans le dictionnaire
            bool motExiste = MotExisteDansDictionnaire(dictionnaireMots, motUtilisateur);

            // Afficher le résultat
            if (motExiste)
            {
                Console.WriteLine($"Le mot '{motUtilisateur}' existe dans le dictionnaire.");
            }
            else
            {
                Console.WriteLine($"Le mot '{motUtilisateur}' n'existe pas dans le dictionnaire.");
            }
        }
        else
        {
            Console.WriteLine("Erreur lors du chargement des mots dans le dictionnaire.");
        }
    }

    // Sous-programme pour charger les mots dans un dictionnaire
    static Dictionary<string, bool> ChargerMotsDansDictionnaire(string cheminFichier)
    {
        Dictionary<string, bool> dictionnaireMots = new Dictionary<string, bool>();

        try
        {
            foreach (string ligne in File.ReadLines(cheminFichier))
            {
                string mot = ligne.Trim();

                if (!string.IsNullOrEmpty(mot) && !dictionnaireMots.ContainsKey(mot))
                {
                    dictionnaireMots.Add(mot, true);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.Message}");
            return null;
        }

        return dictionnaireMots;
    }

    // Sous-programme pour vérifier si un mot existe dans le dictionnaire
    static bool MotExisteDansDictionnaire(Dictionary<string, bool> dictionnaireMots, string mot)
    {
        // Vérifie si le mot donné par l'utilisateur existe dans le dictionnaire
        return dictionnaireMots.ContainsKey(mot);
    }
}
