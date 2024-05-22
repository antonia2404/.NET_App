using System.Xml;
using System.Xml.Serialization;

namespace entitati
{

    [Serializable]
    [XmlInclude(typeof(Produs))]
    [XmlInclude(typeof(Serviciu))]
    public abstract class ProdusAbstract : IPackageable
    {
        [XmlElement("ID")]
        public uint Id { get; set; }
        [XmlElement("Numele")]
        public string? Nume { get; set; }
        [XmlElement("CodulIntern")]
        public string? CodIntern { get; set; }
        [XmlElement("Categoria")]
        public string? Categorie { get; set; }
        [XmlElement("Pretul")]
        public int? Pret { get; set; }

        protected ProdusAbstract(uint id, string? nume, string? codIntern, string? categorie, int? pret)
        {
            Id = id;
            Nume = nume;
            CodIntern = codIntern;
            Categorie = categorie;
            Pret = pret;
        }

        public ProdusAbstract()
        {

        }
        public abstract string Descriere();

        public virtual string AltaDescriere()
        {
            return $"{Nume} [{CodIntern}]";
        }

        public override string ToString()
        {
            return Descriere();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var other = (ProdusAbstract)obj;
            return this.Id == other.Id && this.Nume == other.Nume && this.CodIntern == other.CodIntern;
        }


        public static bool operator ==(ProdusAbstract produs1, ProdusAbstract produs2)
        {
            if (ReferenceEquals(produs1, produs2))
                return true;
            if (produs1 is null || produs2 is null)
                return false;

            return produs1.Equals(produs2);
        }

        public static bool operator !=(ProdusAbstract produs1, ProdusAbstract produs2)
        {
            return !(produs1 == produs2);
        }


        public virtual bool canAddToPackage(Pachet pachet)
        {
            throw new NotImplementedException();
        }

        
    }
}
