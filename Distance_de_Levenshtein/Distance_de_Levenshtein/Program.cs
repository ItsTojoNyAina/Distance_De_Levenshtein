using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Chemin vers le fichier texte contenant les mots en français
        string filePath = "liste_francais.txt";

        // Charger le dictionnaire dans un HashSet pour des recherches rapides
        HashSet<string> dictionary = LoadWords(filePath);

        Console.WriteLine("Veuillez entrer un mot : ");
        string inputWord = Console.ReadLine();

        // Vérifier si le mot existe dans le dictionnaire
        if (dictionary.Contains(inputWord.ToLower()))
        {
            Console.WriteLine("Le mot existe dans le dictionnaire.");
        }
        else
        {
            Console.WriteLine("Le mot n'existe pas dans le dictionnaire.");

            // Obtenir les suggestions de mots proches
            List<string> suggestions = GetClosestWords(inputWord, dictionary);

            if (suggestions.Count > 0)
            {
                Console.WriteLine("Suggestions:");
                foreach (var suggestion in suggestions)
                {
                    Console.WriteLine(suggestion);
                }
            }
            else
            {
                Console.WriteLine("Aucune suggestion trouvée.");
            }
        }

        // Lire et analyser le fichier texte
        string inputFilePath = "texte.txt";
        string outputFilePath = "texte.err";
        AnalyzeTextFile(inputFilePath, outputFilePath, dictionary);

        Console.WriteLine("Analyse terminée. Consultez le fichier texte.err pour les résultats.");
    }

    // Charger les mots du fichier texte dans un HashSet pour des recherches rapides
    static HashSet<string> LoadWords(string filePath)
    {
        HashSet<string> words = new HashSet<string>();

        try
        {
            foreach (var line in File.ReadLines(filePath))
            {
                words.Add(line.Trim().ToLower()); // Ajouter en minuscules pour éviter les erreurs de casse
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors du chargement du fichier : " + ex.Message);
        }

        return words;
    }

    // Obtenir les mots les plus proches
    static List<string> GetClosestWords(string word, IEnumerable<string> dictionary, int maxDistance = 2)
    {
        List<string> closestWords = new List<string>();

        foreach (var dictWord in dictionary)
        {
            int distance = LevenshteinDistance(word.ToLower(), dictWord);
            if (distance <= maxDistance)
            {
                closestWords.Add(dictWord);
            }
        }

        return closestWords.OrderBy(w => LevenshteinDistance(word, w)).ToList();
    }

    // Calculer la distance de Levenshtein
    static int LevenshteinDistance(string a, string b)
    {
        int[,] costs = new int[a.Length + 1, b.Length + 1];

        for (int i = 0; i <= a.Length; i++)
            costs[i, 0] = i;

        for (int j = 0; j <= b.Length; j++)
            costs[0, j] = j;

        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                costs[i, j] = Math.Min(
                    Math.Min(costs[i - 1, j] + 1, costs[i, j - 1] + 1),
                    costs[i - 1, j - 1] + cost);
            }
        }

        return costs[a.Length, b.Length];
    }

    // Analyser le fichier texte et écrire les corrections
    static void AnalyzeTextFile(string inputFilePath, string outputFilePath, HashSet<string> dictionary)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (var line in File.ReadLines(inputFilePath))
                {
                    string[] words = line.Split(new char[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var word in words)
                    {
                        string cleanedWord = word.ToLower().Trim();

                        if (!dictionary.Contains(cleanedWord))
                        {
                            List<string> suggestions = GetClosestWords(cleanedWord, dictionary);

                            if (suggestions.Count > 0)
                            {
                                writer.WriteLine($"{cleanedWord} – {string.Join(", ", suggestions)}.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de l'analyse du fichier texte : " + ex.Message);
        }
    }
}
