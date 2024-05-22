using entitati;
using System.Xml;

namespace app2
{
    internal class ServiciiMgr : ProduseAbstractMgr
    {
        public override void ReadFromConsole(uint nrElemente)
        {
            for (int i = 0; i < nrElemente; i++)
            {
                Serviciu obiect = this.ReadUnServiciu();
                AddElement(obiect);
            }
        }

        public Serviciu ReadUnServiciu()
        {
            Console.WriteLine($"Introduceți detaliile serviciului:");
            Console.Write("Nume:");
            string nume = Console.ReadLine();
            Console.Write("Cod intern:");
            string codIntern = Console.ReadLine();
            Console.Write("Descriere serviciu:");
            string descriereServiciu = Console.ReadLine();
            Console.Write("Pret:");
            int pret = int.Parse(Console.ReadLine());
            Console.Write("Categorie:");
            string categorie = Console.ReadLine();

            return new Serviciu((uint)CountElemente, nume, codIntern, descriereServiciu, categorie, pret);
        }

        public void InitListafromXML(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\lucac\\OneDrive\\Desktop\\POS\\app2\\Produse.xml");
            XmlNodeList lista_noduri = doc.SelectNodes("/produse/Serviciu");
            foreach (XmlNode nod in lista_noduri)
            {
                string nume = nod["Nume"].InnerText;
                string codIntern = nod["CodIntern"].InnerText;
                string descriereServiciu = nod["DescriereServiciu"].InnerText;
                int pret = int.Parse(nod["Pret"].InnerText);
                string categorie = nod["Categorie"].InnerText;

                AddElement(new Serviciu((uint)CountElemente + 1, nume, codIntern, descriereServiciu, categorie, pret));
            }
        }
    }
}