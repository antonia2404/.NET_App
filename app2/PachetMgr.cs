using entitati;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace app2
{
    internal class PachetMgr : ProduseAbstractMgr
    {
        private ProduseAbstractMgr produseMgr;
        private ServiciiMgr serviciiMgr;

        public PachetMgr()
        {
            this.produseMgr = new ProduseMgr();
            this.serviciiMgr = new ServiciiMgr();
        }

        public override void ReadFromConsole(uint nrElemente) 
        {
            for (int i = 0; i < nrElemente; i++)
            {
                Pachet pachet = ReadUnPachet();
                AddElement(pachet);
            }
        }

        public Pachet ReadUnPachet()
        {
            Console.WriteLine("Introduceți detalii pentru pachet:");

            Console.Write("Nume pachet: ");
            string numePachet = Console.ReadLine();

            Console.Write("Cod intern pachet: ");
            string codInternPachet = Console.ReadLine();

            Console.Write("Categorie pachet: ");
            string categoriePachet = Console.ReadLine();

            Pachet pachet = new Pachet((uint)CountElemente + 1, numePachet, codInternPachet, categoriePachet, 0);

            Console.Write("Numărul de elemente din pachet:");
            uint nrElementePachet = uint.Parse(Console.ReadLine() ?? "0");

            bool hasProduct = false; // Flag pentru a verifica dacă pachetul conține deja un produs

            for (int i = 0; i < nrElementePachet; i++)
            {
                Console.WriteLine($"Introduceți detaliile elementului {i + 1} din pachet:");
                Console.Write("1 pentru produs, 2 pentru serviciu:");
                int tipElement = int.Parse(Console.ReadLine() ?? "0");

                if (tipElement == 1)
                {
                    if (hasProduct)
                    {
                        Console.WriteLine("Un pachet poate conține doar un singur produs. Restul de elemente vor fi adăugate ca servicii.");
                        Serviciu serviciu = serviciiMgr.ReadUnServiciu();
                        pachet.elem_pachet.Add(serviciu);
                    }
                    else
                    {
                        Produs produs = ((ProduseMgr)produseMgr).ReadUnProdus();
                        pachet.elem_pachet.Add(produs);
                        hasProduct = true; // Setăm flag-ul ca pachetul să conțină un produs
                    }
                }
                else if (tipElement == 2)
                {
                    Serviciu serviciu = serviciiMgr.ReadUnServiciu();
                    pachet.elem_pachet.Add(serviciu);
                }
                else
                {
                    Console.WriteLine("Opțiune invalidă.");
                }
            }

            return pachet;
        }

        public void SortarePret()
        {
            List<Pachet> pacheteSortate = new List<Pachet>();

            foreach (var element in elemente)
            {
                if (element is Pachet pachet)
                {
                    pacheteSortate.Add(pachet);
                }
            }

            pacheteSortate.Sort((p1, p2) => p1.CalculPretTotal().CompareTo(p2.CalculPretTotal()));

            Console.WriteLine("Pachetele sortate în ordinea crescătoare a prețului total:");
            foreach (var pachet in pacheteSortate)
            {
                Console.WriteLine($"Pachet: {pachet.Nume} - Preț total: {pachet.CalculPretTotal()}");
            }
        }

    public void InitListafromXML(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList pachetNodes = doc.SelectNodes("/Pachete/Pachet");
            foreach (XmlNode pachetNode in pachetNodes)
            {

                string numePachet = pachetNode.SelectSingleNode("infoPach/nume")?.InnerText;
                string codInternPachet = pachetNode.SelectSingleNode("infoPach/codIntern")?.InnerText;
                string categoriePachet = pachetNode.SelectSingleNode("infoPach/categorie")?.InnerText;
                int pretTotalPachet = 0;

                Pachet pachet = new Pachet((uint)CountElemente + 1, numePachet, codInternPachet, categoriePachet, pretTotalPachet);

                XmlNodeList produseNodes = pachetNode.SelectNodes("Produse/Produs");
                foreach (XmlNode produsNode in produseNodes)
                {
                    string nume = produsNode.SelectSingleNode("Nume")?.InnerText;
                    string codIntern = produsNode.SelectSingleNode("CodIntern")?.InnerText;
                    string producator = produsNode.SelectSingleNode("Producator")?.InnerText;
                    int pret = int.Parse(produsNode.SelectSingleNode("Pret")?.InnerText);
                    string categorie = produsNode.SelectSingleNode("Categorie")?.InnerText;

                    Produs produs = new Produs((uint)CountElemente + 1, nume, codIntern, producator, pret, categorie);
                    pachet.elem_pachet.Add(produs);

                    pretTotalPachet += pret;

                }

                XmlNodeList serviciiNodes = pachetNode.SelectNodes("Servicii/Serviciu");
                foreach (XmlNode serviciuNode in serviciiNodes)
                {
                    string nume = serviciuNode.SelectSingleNode("Nume")?.InnerText;
                    string codIntern = serviciuNode.SelectSingleNode("CodIntern")?.InnerText;
                    string descriereServiciu = serviciuNode.SelectSingleNode("DescriereServiciu")?.InnerText;
                    int pret = int.Parse(serviciuNode.SelectSingleNode("Pret")?.InnerText);
                    string categorie = serviciuNode.SelectSingleNode("Categorie")?.InnerText;

                    Serviciu serviciu = new Serviciu((uint)CountElemente + 1, nume, codIntern, descriereServiciu, categorie, pret);
                    pachet.elem_pachet.Add(serviciu);

                    pretTotalPachet += pret;
                }
                pachet.Pret = pretTotalPachet;
                AddElement(pachet);
            }
        }

        public override void FiltrareDupaPret()
        {
            int pret = int.Parse(Console.ReadLine() ?? string.Empty);
            List<Pachet> pacheteFiltrate = new List<Pachet>();
            foreach (var element in elemente)
            {
                if (element is Pachet pachet)
                {
                    if (pachet.CalculPretTotal() == pret)
                    {
                        pacheteFiltrate.Add(pachet);
                    }
                }
            }

            if (pacheteFiltrate.Any())
            {
                Console.WriteLine($"Pachetele cu prețul total de {pret}:");
                foreach (var pachet in pacheteFiltrate)
                {
                    Console.WriteLine(pachet.Descriere());
                }
            }
            else
            {
                Console.WriteLine($"Nu s-au găsit pachete cu prețul total de {pret}.");
            }
        }

        public void SerializePachete(string fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Pachet>));
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    List<Pachet> pachete = new List<Pachet>();
                    foreach (var element in elemente)
                    {
                        if (element is Pachet pachet)
                        {
                            pachete.Add(pachet);
                        }
                    }
                    serializer.Serialize(writer, pachete);
                }
                Console.WriteLine("Serializare cu succes!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la serializare: {ex.Message}");
            }
        }

        public List<Pachet> DeserializePachete(string fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Pachet>));
                using (StreamReader reader = new StreamReader(fileName))
                {
                    List<Pachet> pachete = (List<Pachet>)serializer.Deserialize(reader);
                    elemente.Clear();
                    foreach (var pachet in pachete)
                    {
                        elemente.Add(pachet);
                    }
                    CountElemente = elemente.Count;
                    Console.WriteLine("Deserializare cu succes!");
                    return pachete;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la deserializare: {ex.Message}");
                return new List<Pachet>();
            }
        }
    }
}
