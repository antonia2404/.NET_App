using System;
using System.Xml;
using System.Xml.Serialization;


namespace entitati
{
    
    public class Serviciu : ProdusAbstract, IPackageable

    {
        [XmlElement("DescriereaServiciului")]
        public string? DescriereServiciu { get; set; }

        public Serviciu(uint id, string nume, string codIntern, string descriereServiciu, string categorie, int pret)
            : base(id, nume, codIntern, categorie, pret)
        {
            DescriereServiciu = descriereServiciu;
        }

        public Serviciu() { }
        public override string Descriere()
        {
            return $"Serviciu: {Nume} [{CodIntern}] Descriere: {DescriereServiciu} Preț: {Pret} Categorie: {Categorie}";
        }

        public override bool canAddToPackage(Pachet pachet)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var other = (Serviciu)obj;
            return Nume == other.Nume && CodIntern == other.CodIntern && DescriereServiciu == other.DescriereServiciu && Pret == other.Pret && Categorie == other.Categorie;
        }

        public void save2XML(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Serviciu));
            StreamWriter sw = new StreamWriter(fileName + ".xml");
            xs.Serialize(sw, this);
            sw.Close();
        }

        public Serviciu? loadFromXML(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Serviciu));
            FileStream fs = new FileStream(fileName + ".xml",FileMode.Open);
            XmlReader reader = new XmlTextReader(fs);
            //deserializare cu crearea de obiect => constructor fara param
            Serviciu? serviciu = (Serviciu?)xs.Deserialize(reader);
            fs.Close();
            return serviciu;
        }

    }
}
