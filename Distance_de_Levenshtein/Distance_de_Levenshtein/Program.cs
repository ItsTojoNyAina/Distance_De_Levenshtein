using System;
using System.Collections.Generic;
using System.IO;


namespace Distance_de_Levenshtein
{ 
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
            }
}