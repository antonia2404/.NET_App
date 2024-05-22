using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2
{
    internal class FiltrareCategorie : IFiltrare
    {
        public List<ProdusAbstract> Filtrare(List<ProdusAbstract> produse, ICriteriu criteriu)
        {
            if (criteriu is CriteriuCategorie)
            {
                CriteriuCategorie categorieCriteriu = (CriteriuCategorie)criteriu;
                List<ProdusAbstract> rezultate = new List<ProdusAbstract>();
                foreach (ProdusAbstract produs in produse)
                {
                    if (categorieCriteriu.IsIndeplinit(produs))
                    {
                        rezultate.Add(produs);
                    }
                }
                return rezultate;
            }
            else
            {
                throw new ArgumentException("Criteriul trebuie sa fie de tipul CriteriuCategorie");
            }
        }
       
    }
}
