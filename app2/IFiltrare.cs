using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2
{
    internal interface IFiltrare
    {
        List<ProdusAbstract> Filtrare(List<ProdusAbstract> elem, ICriteriu criteriu);
    }
}
