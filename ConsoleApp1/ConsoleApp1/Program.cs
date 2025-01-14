using System.Text.Json;
using ConsoleApp1;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        string programariFilePath = "../../../programari.txt";
            SalaFitness salaFitness = LoadSalaFitness();
            
            salaFitness.IncarcaProgramariDinFisier(programariFilePath);
           int optiune;
 do
 {
     Console.WriteLine();
     Console.WriteLine("--------Meniu--------");
     Console.WriteLine("Selectati o optiune:");
     Console.WriteLine("1. Autentificare");
     Console.WriteLine("2. Inregistrare");
     Console.WriteLine("3. Schimbare Parola");
     Console.WriteLine("4. Listare Antrenori");
     Console.WriteLine("5. Listare Abonati");
     Console.WriteLine("6. Retrogradare Abonat");
     Console.WriteLine("7. Promovare Abonat");
     Console.WriteLine("8. Adaugare Programare");
     Console.WriteLine("9. Afiseaza Programari");
     Console.WriteLine("10. Modificare Programare");
     Console.WriteLine("11. Anulare Programare");
     Console.WriteLine("12. Listare Programari Ordonate");
     Console.WriteLine("13. Listare Programari Ordonate Dupa Durata");
     Console.WriteLine("14. Cautare Antrenor");
     Console.WriteLine("15. Recalculare Abonamente");
     Console.WriteLine("0. Iesire din aplicatie");

     try
     {
         Console.Write("Introduceti optiunea dorita: ");
         optiune = int.Parse(Console.ReadLine());

         switch (optiune)
         {
             case 1:
                 Autentificare(salaFitness);
                 break;
             case 2:
                 Inregistrare(salaFitness);
                 break;
             case 3:
                 SchimbareParola(salaFitness);
                 break;
             case 4:
                 ListareAntrenori(salaFitness);
                 break;
             case 5:
                 ListareAbonati(salaFitness);
                 break;
             case 6:
                 RetrogradareAbonat(salaFitness);
                 break;
             case 7:
                 PromovareAbonat(salaFitness);
                 break;
             case 8:
                 AdaugareProgramare(salaFitness);
                 break;
             case 9:
                 AfiseazaProgramari(salaFitness);
                 break;
             case 10:
                 ModificareProgramare(salaFitness);
                 break;
             case 11:
                 AnulareProgramare(salaFitness);
                 break;
             case 12:
                 ListareProgramariOrdonate(salaFitness);
                 break;
             case 13:
                 ListareProgramariOrdonateDupaDurata(salaFitness);
                 break;
             case 14:
                 CautareAntrenor(salaFitness);
                 break;
             case 15:
                 RecalculareAbonamente(salaFitness);
                 break;
             case 0:
                 Console.WriteLine("Iesire din aplicatie...");
                 break;
             default:
                 Console.WriteLine("Optiune invalida! Va rugam sa introduceti un numar intre 0 si 15.");
                 break;
         }
     }
     catch (FormatException) // Conversie de formatare esuata ex(a sau caractere speciale)
     {
         Console.WriteLine("Optiune invalida! Introduceti un numar valid intre 0 si 15.");
         optiune = -1;
     }
 } while(optiune!=0);

             SaveSalaFitness(salaFitness);
            
            salaFitness.SalveazaProgramariInFisier(programariFilePath);

        
    }
    static SalaFitness LoadSalaFitness()
        {
            string path = "../../../salaFitness.json";

            if (File.Exists(path))
            {
                try
                {
                    // Citire si deserializare JSON
                    string jsonContent = File.ReadAllText(path);
                    SalaFitness sala = JsonSerializer.Deserialize<SalaFitness>(jsonContent);
                    Console.WriteLine("Sala Fitness a fost incarcata din fisier.");
                    return sala;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare la incarcarea salii: {ex.Message}");
                }
            }

            // Crearea unei sali noi daca fisierul nu exista sau este corupt
            Console.WriteLine("Nu exista fisierul salaFitness.json. Se va crea o sala noua.");
            return new SalaFitness("Sala Noua", "Adresa Noua", "08:00 - 22:00");
        }

        static void SaveSalaFitness(SalaFitness salaFitness)
        {
            string path = "salaFitness.json";

            try
            {
                // Serializare JSON si salvare in fisier
                string jsonContent = JsonSerializer.Serialize(salaFitness, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, jsonContent);
                Console.WriteLine("Sala Fitness a fost salvata cu succes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la salvarea salii: {ex.Message}");
            }
        }

        static void Autentificare(SalaFitness salaFitness)
        {
                Console.Write("Introduceti username-ul: ");
                string username = Console.ReadLine();
                Console.Write("Introduceti parola: ");
                string password = Console.ReadLine();

                if (salaFitness.VerificaAcces(username, password))
                {
                    Console.WriteLine("Autentificare reusita!");
                }
                else
                {
                    Console.WriteLine("Autentificare esuata. Verificati credentialele.");
                }
            

        }
        static void Inregistrare(SalaFitness salaFitness)
        {
            Console.Write("Introduceti numele: ");
            string nume = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nume))
            {
                Console.WriteLine("Numele nu poate fi gol.");
                return;
            }

            Console.Write("Introduceti CNP-ul: ");
            string cnp = Console.ReadLine();

            while (!Abonat.ValidareCnp(cnp))
            {
                Console.WriteLine("CNP-ul introdus este invalid. Incercati din nou.");
                Console.Write("Introduceti CNP-ul: ");
                cnp = Console.ReadLine();
            }

            Console.WriteLine("CNP-ul introdus este valid.");

            if (salaFitness.AbonatiInregistrati.Any(a => a.Cnp == cnp))
            {
                Console.WriteLine("Un abonat cu acest CNP este deja inregistrat.");
                return;
            }

            Console.Write("Introduceti username-ul: ");
            string username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username-ul nu poate fi gol.");
                return;
            }

            if (salaFitness.AbonatiInregistrati.Any(a => a.Username == username))
            {
                Console.WriteLine("Acest username este deja folosit.");
                return;
            }

            Console.Write("Introduceti parola: ");
            string parola = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(parola))
            {
                Console.WriteLine("Parola nu poate fi goala.");
                return;
            }

            // Inregistrarea abonatului dupa validare
            Abonat abonat = new Abonat(nume, cnp, username, parola, "Standard");
            salaFitness.InregistreazaAbonat(abonat);
        }

       


        static void SchimbareParola(SalaFitness salaFitness)
        {
            Console.Write("Introduceti username-ul: ");
            string username = Console.ReadLine();
            Console.Write("Introduceti parola veche: ");
            string parolaVeche = Console.ReadLine();

            if (salaFitness.VerificaAcces(username, parolaVeche))
            {
                Console.Write("Introduceti parola noua: ");
                string parolaNoua = Console.ReadLine();

                var abonat = salaFitness.AbonatiInregistrati.Find(a => a.Username == username);
                abonat.Password = parolaNoua;
                Console.WriteLine("Parola a fost schimbata cu succes.");
            }
            else
            {
                Console.WriteLine("Parola veche este incorecta.");
            }
        }
        static void ListareAntrenori(SalaFitness salaFitness)
        {
            Console.WriteLine("Lista antrenorilor:");
            foreach (var antrenor in salaFitness.Antrenori)
            {
                Console.WriteLine(antrenor.NumeComplet);
            }
        }

    static void ListareAbonati(SalaFitness salaFitness)
        {
            Console.WriteLine("Lista abonatilor:");
            foreach (var abonat in salaFitness.AbonatiInregistrati)
            {
                Console.WriteLine(abonat);
            }
        }

    static void RetrogradareAbonat(SalaFitness salaFitness)
        {
            Console.Write("Introduceti username-ul abonatului pentru retrogradare: ");
            string username = Console.ReadLine();

            var abonat = salaFitness.AbonatiInregistrati.Find(a => a.Username == username);

            if (abonat != null && abonat.TipAbonament == "Premium")
            {
                abonat.TipAbonament = "Standard";
                Console.WriteLine("Abonamentul a fost retrogradat la Standard.");
            }
            else
            {
                Console.WriteLine("Abonatul nu este Premium sau nu exista.");
            }
        }

    static void PromovareAbonat(SalaFitness salaFitness)
        {
            Console.Write("Introduceti username-ul abonatului pentru promovare: ");
            string username = Console.ReadLine();

            salaFitness.PromovareAbonat(username);
        }
        
        static void AdaugareProgramare(SalaFitness salaFitness)
        {
            Console.Write("Introduceti username-ul abonatului: ");
            string username = Console.ReadLine();

            var abonat = salaFitness.AbonatiInregistrati.Find(a => a.Username == username);
            if (abonat == null)
            {
                Console.WriteLine("Abonatul nu exista.");
                return;
            }

            Console.Write("Introduceti numele antrenorului: ");
            string numeAntrenor = Console.ReadLine();

            var antrenor = salaFitness.Antrenori.Find(a => a.NumeComplet == numeAntrenor);
            if (antrenor == null)
            {
                Console.WriteLine("Antrenorul nu exista.");
                return;
            }

            Console.Write("Introduceti data si ora programarii (format: yyyy-MM-dd HH:mm): ");
            DateTime dataOra = DateTime.Parse(Console.ReadLine());

            Console.Write("Introduceti durata programarii (în ore): ");
            double durata = double.Parse(Console.ReadLine());

            salaFitness.AdaugaProgramare(abonat, antrenor, dataOra, durata);
        }


       static void AfiseazaProgramari(SalaFitness salaFitness)
        {
            salaFitness.AfiseazaProgramari();
        }
    
     static void ModificareProgramare(SalaFitness salaFitness)
        {
            Console.Write("Introduceti numele abonatului: ");
            string abonatNume = Console.ReadLine();

            Console.Write("Introduceti data si ora originala a programarii (format: yyyy-MM-dd HH:mm): ");
            DateTime dataOraOriginala = DateTime.Parse(Console.ReadLine());

            Console.Write("Introduceti noua data si ora a programarii (format: yyyy-MM-dd HH:mm): ");
            DateTime nouaDataOra = DateTime.Parse(Console.ReadLine());

            Console.Write("Introduceti noua durata a programarii (in ore): ");
            double nouaDurata = double.Parse(Console.ReadLine());

            salaFitness.ModificaProgramare(abonatNume, dataOraOriginala, nouaDataOra, nouaDurata);
        }
        
        static void AnulareProgramare(SalaFitness salaFitness)
        {
            Console.Write("Introduceti numele abonatului: ");
            string abonatNume = Console.ReadLine();

            Console.Write("Introduceti data si ora programarii (format: yyyy-MM-dd HH:mm): ");
            DateTime dataOra = DateTime.Parse(Console.ReadLine());

            salaFitness.AnuleazaProgramare(abonatNume, dataOra);
        }
        static void ListareProgramariOrdonate(SalaFitness salaFitness)
        {
            salaFitness.AfiseazaProgramariOrdonateDupaAntrenor();
        }

    
    static void ListareProgramariOrdonateDupaDurata(SalaFitness salaFitness)
        {
            salaFitness.AfiseazaProgramariOrdonateDupaDurata();
        }

    
        static void CautareAntrenor(SalaFitness salaFitness)
        {
            Console.Write("Introduceti numele antrenorului: ");
            string nume = Console.ReadLine();

            Console.Write("Introduceti specializarea antrenorului: ");
            string specializare = Console.ReadLine();

            salaFitness.CautaAntrenor(nume, specializare);
        }


        static void RecalculareAbonamente(SalaFitness salaFitness)
        {
            salaFitness.RecalculeazaAbonamenteLunare();
        }

    
    
}
