using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Question 2
namespace Distance_de_Levenshtein
{
    internal class ChargementMotDansDico
    {
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
    }
}
