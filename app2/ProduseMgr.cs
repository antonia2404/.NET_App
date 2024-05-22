using entitati;
using System;
using System.Xml;

namespace app2
{
    internal class ProduseMgr : ProduseAbstractMgr
    {
        public override void ReadFromConsole(uint nrElemente)
        {
            for (int i = 0; i < nrElemente; i++)
            {
                Produs obiect = this.ReadUnProdus();
                AddElement(obiect);
            }
        }

        public Produs ReadUnProdus()
        {
            Console.WriteLine($"Introduceți detaliile produsului:");
            Console.Write("Nume:");
            string nume = Console.ReadLine();
            Console.Write("Cod intern:");
            string codIntern = Console.ReadLine();
            Console.Write("Producător:");
            string producator = Console.ReadLine();
            Console.Write("Pret:");
            int pret = int.Parse(Console.ReadLine());
            Console.Write("Categorie:");
            string categorie = Console.ReadLine();

            return new Produs((uint)CountElemente, nume, codIntern, producator, pret, categorie);
        }

        public void InitListafromXML(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\lucac\\OneDrive\\Desktop\\POS\\app2\\Produse.xml");
            XmlNodeList lista_noduri = doc.SelectNodes("/produse/Produs");
            foreach (XmlNode nod in lista_noduri)
            {
                string nume = nod["Nume"].InnerText;
                string codIntern = nod["CodIntern"].InnerText;
                string producator = nod["Producator"].InnerText;
                int pret = int.Parse(nod["Pret"].InnerText);
                string categorie = nod["Categorie"].InnerText;

                AddElement(new Produs((uint)CountElemente + 1, nume, codIntern, producator, pret, categorie));
            }
        }
    }
}

