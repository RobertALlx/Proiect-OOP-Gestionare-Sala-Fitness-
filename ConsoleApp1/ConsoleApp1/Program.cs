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
                         break;
                     case 3:
                         ListareAntrenori(salaFitness);
                         break;
                     case 4:
                         ListareAbonati(salaFitness);
                         break;
                     case 5:
                         break;
                     case 6:
                         break;
                    case 7:
                
                        break;
                    case 8:
                         AfiseazaProgramari(salaFitness);
                        break;
                    case 9:
                        
                        break;
                    case 10:
                        
                        break;
                    case 11:
                        ListareProgramariOrdonate(salaFitness);
                        break;
                    case 12:
                        
                        break;
                    case 13:
                        
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

       static void AfiseazaProgramari(SalaFitness salaFitness)
        {
            salaFitness.AfiseazaProgramari();
        }



        static void ListareProgramariOrdonate(SalaFitness salaFitness)
        {
            salaFitness.AfiseazaProgramariOrdonateDupaAntrenor();
        }


        static void RecalculareAbonamente(SalaFitness salaFitness)
        {
            salaFitness.RecalculeazaAbonamenteLunare();
        }

    
    
}

