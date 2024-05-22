using System.Xml;
using System.Xml.Serialization;
using entitati;

namespace app2
{
    internal abstract class ProduseAbstractMgr
    {
        public List<ProdusAbstract> elemente = new List<ProdusAbstract>();

        protected int CountElemente { get; set; } = 0;

        public void AddElement(ProdusAbstract element)
        {
            if (!ExistaObiect(element))
            {
                elemente.Add(element);
                CountElemente++;
            }
            else
            {
                Console.WriteLine("Obiectul există deja în tablou.");
            }
        }

        public abstract void ReadFromConsole(uint nrElemente); 

        public virtual void Write2Console()
        {
            Console.WriteLine($"Numărul total de elemente: {CountElemente}");
            foreach (var element in elemente)
            {
                Console.WriteLine(element.Descriere()); 
            }
        }

        public bool ExistaObiect(ProdusAbstract obiect)
        {
            foreach (var elem in elemente)
            {
                if (elem != null && elem.Equals(obiect))
                //(elem != null && elem.GetType() == obiect.GetType() && elem.Nume == obiect.Nume && elem.CodIntern == obiect.CodIntern)
                {
                    return true;
                }
            }
            return false;
        }

        public void CautaDupaNume()
        {
            Console.WriteLine("Introduceți numele produsului/serviciului căutat:");
            string nume = Console.ReadLine();

            // Cautare dupa nume
            IEnumerable<ProdusAbstract> interogare_linq =
                from elem in elemente
                where elem.Nume == nume
                select elem;

            // Afisare rezultat
            if (interogare_linq.Any())
            {
                Console.WriteLine("Rezultatele căutării:");
                foreach (ProdusAbstract elem in interogare_linq)
                {
                    Console.WriteLine(elem.ToString());
                }
            }
            else
            {
                Console.WriteLine($"Nu s-au găsit produse/servicii cu numele '{nume}'.");
            }
        }

        /*public void CautaDupaCategorie()
        {

            Console.WriteLine("Introduceți categoria produsului/serviciului căutat:");
            string categorie = Console.ReadLine();

            IEnumerable<ProdusAbstract> interogare_linq =
                from elem in elemente
                where elem.Categorie == categorie
                orderby elem.Nume
                select elem;

            if (interogare_linq.Any())
            {
                Console.WriteLine("Rezultatele căutării:");
                foreach (ProdusAbstract elem in interogare_linq)
                {
                    Console.WriteLine(elem.ToString());
                }
            }
            else
            {
                Console.WriteLine($"Nu s-au găsit produse/servicii cu categoria '{categorie}'.");
            }
        }*/

        public void FiltrareDupaCategorie()
        {
            string? cat = Console.ReadLine();
            CriteriuCategorie criteriu = new CriteriuCategorie(cat);
            FiltrareCategorie filtru = new FiltrareCategorie();
            List<ProdusAbstract> rez = filtru.Filtrare(elemente, criteriu);
            if (rez.Any())
            {
                foreach(ProdusAbstract elem in rez)
                {
                    Console.WriteLine(elem.Descriere() );
                }
            }

        }

        public virtual void FiltrareDupaPret()
        {
            int prt = int.Parse(Console.ReadLine() ?? string.Empty);
            CriteriuPret criteriu = new CriteriuPret(prt);
            FiltrarePret filtru = new FiltrarePret();
            List<ProdusAbstract> rez = filtru.Filtrare(elemente, criteriu);
            if (rez.Any())
            {
                foreach (ProdusAbstract elem in rez)
                {
                    Console.WriteLine(elem.Descriere());
                }
            }

        }

        public void save2XML(string fileName)
        {
            
            Type[] prodAbstractTypes = new Type[2];
            prodAbstractTypes[0] = typeof(Serviciu);
            prodAbstractTypes[1] = typeof(Produs);

            
            XmlSerializer xs = new XmlSerializer(typeof(List<ProdusAbstract>), prodAbstractTypes);
            StreamWriter sw = new StreamWriter(fileName + ".xml");
            xs.Serialize(sw, elemente); // Serializăm lista de elemente
            sw.Close();
        }

         public List<ProdusAbstract> loadFromXML(string fileName)
         {
            Type[] prodAbstractTypes = new Type[2];
            prodAbstractTypes[0] = typeof(Serviciu);
            prodAbstractTypes[1] = typeof(Produs);

            XmlSerializer xs = new XmlSerializer(typeof(List<ProdusAbstract>), prodAbstractTypes);
            FileStream fs = new FileStream(fileName + ".xml", FileMode.Open);
            List<ProdusAbstract> deserializedElemente = (List<ProdusAbstract>)xs.Deserialize(fs);
            fs.Close();
            return deserializedElemente;
         }
    }
}