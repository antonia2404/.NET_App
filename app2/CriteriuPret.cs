using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2
{
    internal class CriteriuPret: ICriteriu
    {
        public int Pret { get; set; }
        public CriteriuPret(int pret)
        {
            Pret = pret;
        }
        public bool IsIndeplinit(ProdusAbstract element)
        {
            return element.Pret == Pret; 
        }
    }
}
