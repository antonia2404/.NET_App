using entitati;
using System;

namespace app2
{
    internal class Program
    {
        private static ProduseAbstractMgr produseMgr = null;
        private static ServiciiMgr serviciiMgr = null;
        private static PachetMgr pachetMgr = null;
        private static ListaGen<ProdusAbstract> listagenerica = new ListaGen<ProdusAbstract>();
        // Caile catre fisierele XML
        private const string ProduseXMLPath = "C:\\Users\\lucac\\OneDrive\\Desktop\\POS\\app2\\Produse.xml";
        private const string PacheteXMLPath = "C:\\Users\\lucac\\OneDrive\\Desktop\\POS\\app2\\Pachete.xml";

        private static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Meniu:");
                Console.WriteLine("1. Citire si afisare produse de la consola");
                Console.WriteLine("2. Citire si afisare produse din XML");
                Console.WriteLine("3. Citire si afisare servicii de la consola");
                Console.WriteLine("4. Citire si afisare servicii din XML");
                Console.WriteLine("5. Citire si afisare pachete de la consola");
                Console.WriteLine("6. Citire si afisare pachete din XML");
                Console.WriteLine("7. Căutare produs după nume (LINQ)");
                Console.WriteLine("8. Căutare serviciu după nume (LINQ)");
                Console.WriteLine("9. Afisare pachete în ordinea crescǎtoare a prețului total");
                Console.WriteLine("10. Serializare produse");
                Console.WriteLine("11. Deserializarea produse");
                Console.WriteLine("12. Adăugare element în ListaGen");
                Console.WriteLine("13. Afișare elemente din ListaGen");
                Console.WriteLine("14. Filtrare după categorie");
                Console.WriteLine("15. Filtrare după pret");
                Console.WriteLine("16. Serializarea pachete");
                Console.WriteLine("17. Deserializarea pachete");
                Console.WriteLine("18. Serializare servicii");
                Console.WriteLine("19. Deserializarea servicii");
                Console.WriteLine("0. Ieșire");

                int optiune;
                while (!int.TryParse(Console.ReadLine(), out optiune) || optiune < 0 || optiune > 19)
                {
                    Console.WriteLine("Opțiune invalidă. Vă rugăm să selectați o opțiune validă.");
                }

                switch (optiune)
                {
                    case 1:
                        produseMgr = new ProduseMgr();
                        Console.Write("Nr. produse:");
                        uint nrProduse = uint.Parse(Console.ReadLine() ?? "0");
                        produseMgr.ReadFromConsole(nrProduse);
                        produseMgr.Write2Console();
                        break;
                    case 2:
                        produseMgr = new ProduseMgr();
                        ((ProduseMgr)produseMgr).InitListafromXML(ProduseXMLPath);
                        produseMgr.Write2Console();
                        break;
                    case 3:
                        serviciiMgr = new ServiciiMgr();
                        Console.Write("Nr. servicii:");
                        uint nrServicii = uint.Parse(Console.ReadLine() ?? "0");
                        serviciiMgr.ReadFromConsole(nrServicii);
                        serviciiMgr.Write2Console();
                        break;
                    case 4:
                        serviciiMgr = new ServiciiMgr();
                        ((ServiciiMgr)serviciiMgr).InitListafromXML(ProduseXMLPath);
                        serviciiMgr.Write2Console();
                        break;
                    case 5:
                        pachetMgr = new PachetMgr();
                        Console.Write("Nr. pachete:");
                        uint nrPachete = uint.Parse(Console.ReadLine() ?? "0");
                        pachetMgr.ReadFromConsole(nrPachete);
                        pachetMgr.Write2Console();
                        break;
                    case 6:
                        pachetMgr = new PachetMgr();
                        pachetMgr.InitListafromXML(PacheteXMLPath);
                        pachetMgr.Write2Console();
                        break;
                    case 7:
                        if (produseMgr != null)
                        {
                            produseMgr.CautaDupaNume();
                        }
                        else
                        {
                            Console.WriteLine("Nu există produse pentru a căuta.");
                        }
                        break;
                    case 8:
                        if (serviciiMgr != null)
                        {
                            serviciiMgr.CautaDupaNume();
                        }
                        else
                        {
                            Console.WriteLine("Nu există servicii pentru a căuta.");
                        }
                        break;
                    case 9:
                        if (pachetMgr != null)
                        {
                            pachetMgr.SortarePret();
                        }
                        else
                        {
                            Console.WriteLine("Nu există pachete pentru a sorta.");
                        }
                        break;
                    case 10:
                        if (produseMgr != null)
                        {
                            Console.Write("Introduceți numele fișierului pentru salvare: ");
                            string fileName = Console.ReadLine();
                            produseMgr.save2XML(fileName);
                            Console.WriteLine("Produsele au fost serializate cu succes.");
                        }
                        else
                        {
                            Console.WriteLine("Nu există produse pentru a serializa.");
                        }
                        break;
                    case 11:
                        Console.Write("Introduceți numele fișierului pentru încărcare: ");
                        string fileNameToLoad = Console.ReadLine();
                        List<ProdusAbstract> deserializedProduse = produseMgr.loadFromXML(fileNameToLoad);
                        Console.WriteLine("Produsele deserializate sunt:");
                        foreach (var produs in deserializedProduse)
                        {
                            Console.WriteLine(produs.Descriere());
                        }
                        break;
                    case 12:
                        Console.WriteLine("Adăugare element în ListaGen:");
                        Console.WriteLine("1. Produs");
                        Console.WriteLine("2. Serviciu");
                        Console.Write("Alegeți tipul de element (1 sau 2): ");
                        int tipElement;
                        while (!int.TryParse(Console.ReadLine(), out tipElement) || (tipElement != 1 && tipElement != 2))
                        {
                            Console.WriteLine("Opțiune invalidă. Vă rugăm să selectați 1 pentru Produs sau 2 pentru Serviciu.");
                        }

                        if (tipElement == 1)
                        {
                            ProduseMgr produseMgr = new ProduseMgr(); 
                            Produs produs = produseMgr.ReadUnProdus(); 
                            listagenerica.Add(produs);
                            Console.WriteLine("Produs adăugat cu succes în ListaGen.");
                        }
                        else if (tipElement == 2)
                        {
                            ServiciiMgr serviciiMgr = new ServiciiMgr();
                            Serviciu servici = serviciiMgr.ReadUnServiciu(); 
                            listagenerica.Add(servici);
                            Console.WriteLine("Serviciu adăugat cu succes în ListaGen.");
                        }
                        break;
                    case 13:
                        Console.WriteLine("Afișare elemente din ListaGen:");
                        listagenerica.AfisareElemente();
                        break;
                    case 14:
                        if (pachetMgr != null)
                        {
                            Console.WriteLine("Introdu categoria dorita: ");
                            pachetMgr.FiltrareDupaCategorie();
                        }
                        else
                        {
                            Console.WriteLine("Nu există pachete pentru a filtra.");
                        }
                        break;
                    case 15:
                        if (pachetMgr != null)
                        {
                            Console.WriteLine("Introdu pretul pentru pentru filtrare: ");
                            pachetMgr.FiltrareDupaPret();  
                        }
                        else
                        {
                            Console.WriteLine("Nu există pachete pentru a filtra.");
                        }
                        break;
                    case 16:
                        Console.Write("Introduceți numele fișierului de salvare: ");
                        string fileSaveName = Console.ReadLine();
                        pachetMgr.SerializePachete(fileSaveName);
                        break;
                    case 17:
                        pachetMgr = new PachetMgr();
                        Console.Write("Introduceți numele fișierului de încărcare: ");
                        string fileLoadName = Console.ReadLine();
                        List<Pachet> deserializedPachete = pachetMgr.DeserializePachete(fileLoadName);
                        Console.WriteLine("Pachetele deserializate sunt:");
                        foreach (var pachet in deserializedPachete)
                        {
                            Console.WriteLine(pachet.Descriere());
                        }
                        break;
                    case 18:
                        if (serviciiMgr != null)
                        {
                            Console.Write("Introduceți numele fișierului pentru salvare: ");
                            string fileName = Console.ReadLine();
                            serviciiMgr.save2XML(fileName);
                            Console.WriteLine("Serviciiile au fost serializate cu succes.");
                        }
                        else
                        {
                            Console.WriteLine("Nu există servicii pentru a serializa.");
                        }
                        break;
                    case 19:
                        Console.Write("Introduceți numele fișierului pentru încărcare: ");
                        string fileToLoad = Console.ReadLine();
                        List<ProdusAbstract> deserializedServicii = produseMgr.loadFromXML(fileToLoad);
                        Console.WriteLine("Produsele deserializate sunt:");
                        foreach (var serviciu in deserializedServicii)
                        {
                            Console.WriteLine(serviciu.Descriere());
                        }
                        break;
                    case 0:
                        exit = true; 
                        break;
                    default:
                        Console.WriteLine("Opțiune invalidă. Vă rugăm să selectați o opțiune validă.");
                        break;
                }
            }
        }
    }
}
