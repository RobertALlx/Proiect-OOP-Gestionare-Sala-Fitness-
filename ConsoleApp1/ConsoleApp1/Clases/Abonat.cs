using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Abonat
    {
        private string _nume;
        private string _cnp;
        private string _username;
        private string _password;
        private string _tipAbonament;
        private double _taxaSuplimentara; 
        private List<Antrenament> _istoricAntrenamente = new List<Antrenament>();
        
        public string Nume { get => _nume; set => _nume = value; }
        public string Cnp { get => _cnp; set => _cnp = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string TipAbonament { get => _tipAbonament; set => _tipAbonament = value; }
        public double TaxaSuplimentara { get => _taxaSuplimentara; }
        public IReadOnlyList<Antrenament> IstoricAntrenamente => _istoricAntrenamente.AsReadOnly();

        public Abonat(string nume, string cnp, string username, string password, string tipAbonament)
        {
            _nume = nume;
            _cnp = cnp;
            _username = username;
            _password = password;
            _tipAbonament = tipAbonament;
            _taxaSuplimentara = 0; // Initializare taxa suplimentara
        }

        public void AdaugaAntrenament(DateTime dataOra, TimeSpan durata, string antrenor)
        {
            _istoricAntrenamente.Add(new Antrenament(dataOra, durata, antrenor));
        }

        public void AfiseazaIstoricAntrenamente()
        {
            Console.WriteLine($"Istoricul antrenamentelor pentru {Nume}:");
            foreach (var antrenament in _istoricAntrenamente)
            {
                Console.WriteLine(antrenament);
            }
        }

        // Metoda pentru adaugarea taxei suplimentare
        public void AdaugaTaxaSuplimentara(double taxa)
        {
            _taxaSuplimentara += taxa; // Adauga taxa suplimentara la valoarea existenta
            Console.WriteLine($"Taxa suplimentara de {taxa} RON a fost adaugata la contul abonatului {_nume}.");
            Console.WriteLine($"Total taxe suplimentare pentru {_nume}: {_taxaSuplimentara} RON.");
        }
        
        public double CalculeazaCostAbonament()
        {
            double costLunar = 0;

            if (_tipAbonament == "Standard")
            {
                costLunar = 100; // Costul abonamentului Standard
            }
            else if (_tipAbonament == "Premium")
            {
                costLunar = 150; // Costul abonamentului Premium
            }

            // Adauga taxa suplimentara la costul lunar
            return costLunar + _taxaSuplimentara;
        }

        // Validare CNP
        private bool ValidareCnp(string cnp)
        {
            return Regex.IsMatch(cnp, @"^[1-8]\d{12}$");
        }

        // Suprascrierea ToString pentru detalii abonat
        public override string ToString()
        {
            return $"Abonat: {_nume}, CNP: {_cnp}, Username: {_username}, Tip Abonament: {_tipAbonament}, Taxe Suplimentare: {_taxaSuplimentara} RON";
        }
    }
}
