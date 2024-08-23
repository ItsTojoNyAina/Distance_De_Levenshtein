using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Question 3
namespace Distance_de_Levenshtein
{
   internal class RechercheExistance
    {
        // Sous-programme pour vérifier si un mot existe dans le dictionnaire
        static bool MotExisteDansDictionnaire(Dictionary<string, bool> dictionnaireMots, string mot)
        {
            return dictionnaireMots.ContainsKey(mot);
        }
    }
}
