using entitati;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
public class Pachet : ProdusAbstract
{
    public List<ProdusAbstract> elem_pachet { get; set; }

    public Pachet() { }

    public Pachet(uint id, string nume, string codIntern, string categorie, int pret) : base(id, nume, codIntern, categorie, pret)
    {
        elem_pachet = new List<ProdusAbstract>();
    }

    /*public override string Descriere()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"Pachet: {Nume} [{CodIntern}] - Categorie: {Categorie}");
        builder.AppendLine("Elemente pachet:");
        foreach (var elem in elem_pachet)
        {
            builder.AppendLine(elem.Descriere());
        }
        return builder.ToString();
    }*/
    public override string Descriere()
    {
        string description = $"Pachet: {Nume} [{CodIntern}] - Categorie: {Categorie} - Pret: {Pret}\n";
        description += "Elemente pachet:\n";
        foreach (var elem in elem_pachet)
        {
            description += elem.Descriere() + "\n";
        }
        return description;
    }


    public int CalculPretTotal()
    {
        int pretTotal = 0;
        foreach (var element in elem_pachet)
        {
            pretTotal += element.Pret ?? 0;
        }
        return pretTotal;
    }


    public override bool canAddToPackage(Pachet pachet)
    {
        // Verificăm dacă pachetul conține deja un produs
        if (elem_pachet.Exists(elem => elem is Produs))
        {
            Console.WriteLine("Un pachet poate conține doar un singur produs.");
            return false;
        }
        return true;
    }

}