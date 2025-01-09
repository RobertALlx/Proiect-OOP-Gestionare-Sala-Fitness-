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
                Console.WriteLine("\n--- Meniu ---");
                Console.WriteLine("1. Autentificare / Inregistrare");
                Console.WriteLine("2. Schimbare parola utilizator");
                Console.WriteLine("3. Listarea tuturor antrenorilor");
                Console.WriteLine("4. Listarea tuturor abonatilor");
                Console.WriteLine("5. Retrogradare abonat de la Premium la Standard");
                Console.WriteLine("6. Promovare abonat de la standard la premium");
                Console.WriteLine("7. Adauga Programare");
                Console.WriteLine("8. Afiseaza Programare");
                Console.WriteLine("9. Modificare Programare");
                Console.WriteLine("10. Anulare Programare");
                Console.WriteLine("11. Listarea tuturor programarilor ordonate dupa numele antrenorului");
                Console.WriteLine("12. Listarea tuturor programarilor ordonate după durata");
                Console.WriteLine("13. Cautarea unui antrenor după nume si specializare");
                Console.WriteLine("14. Recalculare lunara abonament pentru fiecare client în functie de programari");
                Console.WriteLine("0. Iesire");
                Console.Write("Selectati o optiune: ");

                optiune = int.Parse(Console.ReadLine());

                 switch (optiune)
                {
                    case 1:
                        AutentificareInregistrare(salaFitness);
                        break;
                     case 2:
                         SchimbareParola(salaFitness);
                         break;
                     case 3:
                         ListareAntrenori(salaFitness);
                         break;
                     case 4:
                         ListareAbonati(salaFitness);
                         break;
                     case 5:
                          RetrogradareAbonat(salaFitness);
                         break;
                     case 6:
                           PromovareAbonat(salaFitness);
                         break;
                    case 7:
                          AdaugareProgramare(salaFitness);
                        break;
                    case 8:
                         AfiseazaProgramari(salaFitness);
                        break;
                    case 9:
                        ModificareProgramare(salaFitness);
                        break;
                    case 10:
                         AnulareProgramare(salaFitness);
                        break;
                    case 11:
                        ListareProgramariOrdonate(salaFitness);
                        break;
                    case 12:
                        ListareProgramariOrdonateDupaDurata(salaFitness);
                        break;
                    case 13:
                        CautareAntrenor(salaFitness);
                        break;
                    case 14:
                        RecalculareAbonamente(salaFitness);
                        break;
                    case 0:
                        Console.WriteLine("Iesire din aplicatie...");
                        break;
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

    static void AutentificareInregistrare(SalaFitness salaFitness)
        {
            Console.WriteLine("1. Autentificare\n2. Inregistrare");
            int optiune = int.Parse(Console.ReadLine());

            if (optiune == 1)
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
            else if (optiune == 2)
            {
                Console.Write("Introduceti numele: ");
                string nume = Console.ReadLine();
                Console.Write("Introduceti CNP-ul: ");
                string cnp = Console.ReadLine();
                Console.Write("Introduceti username-ul: ");
                string username = Console.ReadLine();
                Console.Write("Introduceti parola: ");
                string password = Console.ReadLine();

                Abonat abonat = new Abonat(nume, cnp, username, password, "Standard");
                salaFitness.InregistreazaAbonat(abonat);
            }
            else
            {
                Console.WriteLine("Optiune invalida!");
            }
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

