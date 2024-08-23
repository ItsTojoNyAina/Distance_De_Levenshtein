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

        
        List<string> dictionary = LoadWords(filePath);

  
        Console.WriteLine("Veuillez entrer un mot : ");
        string inputWord = Console.ReadLine();

        // 2. Vérifier si le mot existe dans le dictionnaire
        if (dictionary.Contains(inputWord))
        {
            Console.WriteLine("Le mot existe dans le dictionnaire.");
        }
        else
        {
            Console.WriteLine("Le mot n'existe pas dans le dictionnaire.");

            //4. Obtenir les suggestions de mots proches
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
    }

    // 2. Sous-programmes pour charger les mots du fichier texte
    static List<string> LoadWords(string filePath)
    {
        List<string> words = new List<string>();

        try
        {
            foreach (var line in File.ReadLines(filePath))
            {
                words.Add(line.Trim());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors du chargement du fichier : " + ex.Message);
        }

        return words;
    }

    // 4. Sous-programme pour obtenir les mots les plus proches
    static List<string> GetClosestWords(string word, List<string> dictionary, int maxDistance = 2)
    {
        List<string> closestWords = new List<string>();

        foreach (var dictWord in dictionary)
        {
            int distance = LevenshteinDistance(word, dictWord);
            if (distance <= maxDistance)
            {
                closestWords.Add(dictWord);
            }
        }

        return closestWords.OrderBy(w => LevenshteinDistance(word, w)).ToList();
    }

    // Levenshtein
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
}
