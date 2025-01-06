using System;
using System.Collections.Generic;
using System.IO;

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
                Console.WriteLine($"Abonatul {abonat.Nume} a fost înregistrat cu succes.");
            }
            else
            {
                Console.WriteLine($"Abonatul cu CNP {abonat.Cnp} este deja înregistrat.");
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
            Console.WriteLine("Programarea a fost înregistrată cu succes.");
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
                Console.WriteLine($"Eroare la salvarea programării: {ex.Message}");
            }
        }
    }
}
