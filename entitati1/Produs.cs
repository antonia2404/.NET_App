using entitati;
using System.Xml.Serialization;
using System.Xml;

public class Produs : ProdusAbstract
{

    [XmlElement("ProducatorulProdusului")]
    public string? Producator { get; set; }

    public Produs(uint id, string nume, string codIntern, string producator, int pret, string categorie)
        : base(id, nume, codIntern, categorie, pret)
    {
        Producator = producator;
    }

    public Produs() { }
    public override string Descriere()
    {
        return $"Produs: {Nume} [{CodIntern}] Producător: {Producator} Preț: {Pret} Categorie: {Categorie}";
    }
    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
            return false;

        var other = (Produs)obj;
        return Nume == other.Nume && CodIntern == other.CodIntern && Producator == other.Producator && Pret==other.Pret && Categorie==other.Categorie;
    }

    public override bool canAddToPackage(Pachet pachet)
    {
        // Verificăm dacă în pachet nu există deja un produs
        return !pachet.elem_pachet.Exists(elem => elem is Produs);
    }

    public void save2XML(string fileName)
    {
        XmlSerializer xs = new XmlSerializer(typeof(Produs));
       StreamWriter sw = new StreamWriter(fileName + ".xml");
       xs.Serialize(sw, this);
       sw.Close();
    }

    public Produs? loadFromXML(string fileName)
    {
        XmlSerializer xs = new XmlSerializer(typeof(Produs));
        FileStream fs = new FileStream(fileName + ".xml", FileMode.Open);
        XmlReader reader = new XmlTextReader(fs);
        //deserializare cu crearea de obiect => constructor fara param
        Produs? produs = (Produs?)xs.Deserialize(reader);
        fs.Close();
        return produs;
    }
}