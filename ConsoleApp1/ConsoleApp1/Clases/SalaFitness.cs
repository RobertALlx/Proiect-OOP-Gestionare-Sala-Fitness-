using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApp1;
using ConsoleApp1.Clases;

namespace ConsoleApp1
{
    public class SalaFitness
    {
        
        private string _nume;
        private string _adresa;
        private string _programFunctionare;
        private List<Antrenor> _antrenori;
        private List<Abonat> _abonatiInregistrati;
        private List<string> _programari;

        
        public string Nume { get => _nume; set => _nume = value; }
        public string Adresa { get => _adresa; set => _adresa = value; }
        public string ProgramFunctionare { get => _programFunctionare; set => _programFunctionare = value; }
        public List<Antrenor> Antrenori { get => _antrenori; set => _antrenori = value; }
        public List<Abonat> AbonatiInregistrati { get => _abonatiInregistrati; set => _abonatiInregistrati = value; }
        public List<string> Programari { get => _programari; set => _programari = value; }

        
        public SalaFitness(string nume, string adresa, string programFunctionare)
        {
            _nume = nume;
            _adresa = adresa;
            _programFunctionare = programFunctionare;
            _antrenori = new List<Antrenor>();
            _abonatiInregistrati = new List<Abonat>();
            _programari = new List<string>();
        }

        // Metoda pentru adaugarea unui antrenor
        public void AdaugaAntrenor(Antrenor antrenor)
        {
            _antrenori.Add(antrenor);
        }

        // Metoda pentru inregistrarea unui abonat
        public void InregistreazaAbonat(Abonat abonat)
        {
            if (!_abonatiInregistrati.Exists(a => a.Cnp == abonat.Cnp))
            {
                _abonatiInregistrati.Add(abonat);
                Console.WriteLine($"Abonatul {abonat.Nume} a fost inregistrat cu succes.");
            }
            else
            {
                Console.WriteLine($"Abonatul cu CNP {abonat.Cnp} este deja inregistrat.");
            }
        }

        // Metoda pentru verificarea accesului unui abonat
        public bool VerificaAcces(string username, string password)
        {
            var abonat = _abonatiInregistrati.Find(a => a.Username == username && a.Password == password);
            return abonat != null;
        }

        // Metoda pentru adaugarea unei programari
        public void AdaugaProgramare(string detaliiProgramare)
        {
            _programari.Add(detaliiProgramare);
            SalveazaProgramareInFisier(detaliiProgramare);
            Console.WriteLine("Programarea a fost inregistrata cu succes.");
        }

        // Salvarea programarii intr-un fisier
        private void SalveazaProgramareInFisier(string detaliiProgramare)
        {
            string path = "programari.txt";
            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(detaliiProgramare);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la salvarea programarii: {ex.Message}");
            }
        }

        // Salvare in fisier JSON
        public void SaveToFile(string fileName)
        {
            try
            {
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, jsonContent);
                Console.WriteLine("Starea salii a fost salvata cu succes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la salvarea fisierului: {ex.Message}");
            }
        }

        // Incarcare din fisier JSON
        public static SalaFitness LoadFromFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string jsonContent = File.ReadAllText(fileName);
                    return System.Text.Json.JsonSerializer.Deserialize<SalaFitness>(jsonContent);
                }
                else
                {
                    Console.WriteLine("Fisierul nu exista. Se va crea o sală noua.");
                    return new SalaFitness("Sala Noua", "Adresa Noua", "08:00 - 22:00");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la incarcarea fisierului: {ex.Message}");
                return new SalaFitness("Sala Noua", "Adresa Noua", "08:00 - 22:00");
            }
        }
        
        public void PromovareAbonat(string username)
        {
            var abonat = _abonatiInregistrati.Find(a => a.Username == username);

            if (abonat == null)
            {
                Console.WriteLine("Abonatul nu există.");
                return;
            }

            if (abonat.TipAbonament == "Premium")
            {
                Console.WriteLine("Abonatul este deja Premium.");
                return;
            }

            abonat.TipAbonament = "Premium";
            Console.WriteLine($"Abonatul {abonat.Nume} a fost promovat la Premium.");
        }
        public void AdaugaProgramare(Abonat abonat, Antrenor antrenor, DateTime dataOra, double durata)
        {
            if (!_abonatiInregistrati.Contains(abonat))
            {
                Console.WriteLine("Abonatul nu este înregistrat.");
                return;
            }

            if (!_antrenori.Contains(antrenor))
            {
                Console.WriteLine("Antrenorul nu este înregistrat.");
                return;
            }

            // Cream un string structurat pentru programare
            string programare = $"{abonat.Nume},{antrenor.NumeComplet},{dataOra.ToString("yyyy-MM-dd HH:mm")},{durata}";
            _programari.Add(programare);

            Console.WriteLine($"Programarea pentru {abonat.Nume} cu {antrenor.NumeComplet} a fost adăugată cu succes.");
        }
        
        public void AfiseazaProgramari()
        {
            if (_programari.Count == 0)
            {
                Console.WriteLine("Nu exista programari inregistrate.");
                return;
            }

            Console.WriteLine("Lista programarilor:");
            foreach (var detalii in _programari)
            {
                // Deserializam string-ul în componente
                var detaliiSplit = detalii.Split(',');

                // Verificam dacă string-ul are toate componentele necesare
                if (detaliiSplit.Length != 4)
                {
                    Console.WriteLine($"Eroare: Programarea '{detalii}' nu este într-un format valid.");
                    continue;
                }

                string abonat = detaliiSplit[0];
                string antrenor = detaliiSplit[1];
                string dataOra = detaliiSplit[2];
                string durata = detaliiSplit[3];

                Console.WriteLine($"Abonat: {abonat}, Antrenor: {antrenor}, Data si Ora: {dataOra}, Durata: {durata} ore");
            }
        }
        public void SalveazaProgramariInFisier(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var programare in _programari)
                    {
                        sw.WriteLine(programare);
                    }
                }
                Console.WriteLine("Programarile au fost salvate în fisier.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la salvarea programarilor în fisier: {ex.Message}");
            }
        }
        
        public void IncarcaProgramariDinFisier(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            _programari.Add(line);
                        }
                    }
                    Console.WriteLine("Programarile au fost incarcate din fisier.");
                }
                else
                {
                    Console.WriteLine("Fisierul de programari nu exista. Se va crea unul nou la salvare.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la incarcarea programarilor din fisier: {ex.Message}");
            }
        }
        
        public void ModificaProgramare(string abonatNume, DateTime dataOraOriginala, DateTime nouaDataOra, double nouaDurata)
        {
            // Cautam programarea in lista
            var programareIndex = _programari.FindIndex(p => p.StartsWith(abonatNume) && p.Contains(dataOraOriginala.ToString("yyyy-MM-dd HH:mm")));
    
            if (programareIndex == -1)
            {
                Console.WriteLine("Programarea nu a fost gasita.");
                return;
            }

            // Deserializam string-ul pentru a extrage detaliile
            var detalii = _programari[programareIndex].Split(',');

            if (detalii.Length != 4)
            {
                Console.WriteLine("Formatul programarii este invalid.");
                return;
            }

            string antrenor = detalii[1]; // Numele antrenorului

            // Cream un nou string cu datele actualizate
            string programareActualizata = $"{abonatNume},{antrenor},{nouaDataOra.ToString("yyyy-MM-dd HH:mm")},{nouaDurata}";

            // Actualizam programarea in lista
            _programari[programareIndex] = programareActualizata;

            Console.WriteLine("Programarea a fost modificata cu succes.");
        }
        
        public void AnuleazaProgramare(string abonatNume, DateTime dataOra)
        {
            // Cautam programarea in lista
            var programareIndex = _programari.FindIndex(p => p.StartsWith(abonatNume) && p.Contains(dataOra.ToString("yyyy-MM-dd HH:mm")));

            if (programareIndex == -1)
            {
                Console.WriteLine("Programarea nu a fost gasita.");
                return;
            }

            // Eliminam programarea din lista
            _programari.RemoveAt(programareIndex);

            Console.WriteLine("Programarea a fost anulata cu succes.");
        }
        
        public void AfiseazaProgramariOrdonateDupaAntrenor()
        {
            if (_programari.Count == 0)
            {
                Console.WriteLine("Nu exista programari inregistrate.");
                return;
            }

            // Sortam programarile dupa numele antrenorului
            var programariOrdonate = _programari.OrderBy(p => p.Split(',')[1]).ToList();

            Console.WriteLine("Lista programarilor ordonate dupa numele antrenorului:");
            foreach (var detalii in programariOrdonate)
            {
                var detaliiSplit = detalii.Split(',');
                if (detaliiSplit.Length != 4)
                {
                    Console.WriteLine($"Format invalid pentru programare: {detalii}");
                    continue;
                }

                string abonat = detaliiSplit[0];
                string antrenor = detaliiSplit[1];
                string dataOra = detaliiSplit[2];
                string durata = detaliiSplit[3];

                Console.WriteLine($"Abonat: {abonat}, Antrenor: {antrenor}, Data si Ora: {dataOra}, Durata: {durata} ore");
            }
        }
        
        public void AfiseazaProgramariOrdonateDupaDurata()
        {
            if (_programari.Count == 0)
            {
                Console.WriteLine("Nu exista programari inregistrate.");
                return;
            }

            // Sortam programarile dupa durata (al patrulea element din string)
            var programariOrdonate = _programari.OrderBy(p => double.Parse(p.Split(',')[3])).ToList();

            Console.WriteLine("Lista programarilor ordonate dupa durata:");
            foreach (var detalii in programariOrdonate)
            {
                var detaliiSplit = detalii.Split(',');
                if (detaliiSplit.Length != 4)
                {
                    Console.WriteLine($"Format invalid pentru programare: {detalii}");
                    continue;
                }

                string abonat = detaliiSplit[0];
                string antrenor = detaliiSplit[1];
                string dataOra = detaliiSplit[2];
                string durata = detaliiSplit[3];

                Console.WriteLine($"Abonat: {abonat}, Antrenor: {antrenor}, Data si Ora: {dataOra}, Durata: {durata} ore");
            }
        }
        
        public void CautaAntrenor(string nume, string specializare)
        {
            var antrenoriGasiti = _antrenori.Where(a => a.NumeComplet.Contains(nume, StringComparison.OrdinalIgnoreCase) && a.Specializare.Equals(specializare, StringComparison.OrdinalIgnoreCase)).ToList();

            if (antrenoriGasiti.Count == 0)
            {
                Console.WriteLine("Nu a fost gasit niciun antrenor care sa corespunda criteriilor.");
                return;
            }

            Console.WriteLine("Antrenori gasiti:");
            foreach (var antrenor in antrenoriGasiti)
            {
                Console.WriteLine($"Nume: {antrenor.NumeComplet}, Specializare: {antrenor.Specializare}, Numar maxim clienti: {antrenor.NumarMaximClienti}");
            }
        }
        //StringComparison.OrdinalIgnoreCase pentru a face comparatia insensibila la litere mari/mici.
        
        public void RecalculeazaAbonamenteLunare()
        {
            foreach (var abonat in _abonatiInregistrati)
            {
                double costLunar = abonat.TipAbonament == "Standard" ? 100 : 150;

                foreach (var programare in _programari)
                {
                    var detalii = programare.Split(',');
                    if (detalii.Length != 4) continue;

                    string numeAbonat = detalii[0];
                    double durataProgramare = double.Parse(detalii[3]);

                    if (numeAbonat == abonat.Nume)
                    {
                        double durataSuplimentara = durataProgramare > 2 ? durataProgramare - 2 : 0;
                        double taxaSuplimentara = durataSuplimentara * (abonat.TipAbonament == "Standard" ? 5 : 2);
                        costLunar += taxaSuplimentara;
                    }
                }

                abonat.TaxaSuplimentara = costLunar;
                Console.WriteLine($"Abonamentul recalculat pentru {abonat.Nume}: {costLunar} RON.");
            }
        }
    }
}