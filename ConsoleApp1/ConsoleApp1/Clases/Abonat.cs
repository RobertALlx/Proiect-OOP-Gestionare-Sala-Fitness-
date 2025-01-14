using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using ConsoleApp1.Clases;

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
        public double TaxaSuplimentara
        {
            get => _taxaSuplimentara;
            set => _taxaSuplimentara = value;
        }

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

        public static bool ValidareCnp(string cnp)
        {
            if (string.IsNullOrEmpty(cnp) || cnp.Length != 13)
            {
                Console.WriteLine("CNP-ul nu are 13 caractere.");
                return false;
            }

            if (!Regex.IsMatch(cnp, @"^\d{13}$"))
            {
                Console.WriteLine("CNP-ul nu este numeric.");
                return false;
            }

            int sex = int.Parse(cnp[0].ToString());
            if (sex < 1 || sex > 8)
            {
                Console.WriteLine("Prima cifra a CNP-ului este invalida.");
                return false;
            }

            int an = int.Parse(cnp.Substring(1, 2));
            int luna = int.Parse(cnp.Substring(3, 2));
            int zi = int.Parse(cnp.Substring(5, 2));
            int secol = (sex <= 2 || sex == 7 || sex == 8) ? 1900 : 2000;
            if (sex == 5 || sex == 6) secol = 2000;
            if (sex == 7 || sex == 8) secol = 1800;

            an += secol;

            if (!DateTime.TryParse($"{an}-{luna:D2}-{zi:D2}", out _))
            {
                Console.WriteLine("Data de nastere din CNP este invalida.");
                return false;
            }

            int[] controlWeights = { 2, 7, 9, 1, 4, 6, 3, 5, 8, 2, 7, 9 };
            int suma = 0;
            for (int i = 0; i < 12; i++)
            {
                suma += int.Parse(cnp[i].ToString()) * controlWeights[i];
            }

            int cifraControlCalculata = suma % 11;
            if (cifraControlCalculata == 10) cifraControlCalculata = 1;

            if (int.Parse(cnp[12].ToString()) != cifraControlCalculata)
            {
                Console.WriteLine("Cifra de control a CNP-ului este incorecta.");
                return false;
            }

            return true;
        }



        // Suprascrierea ToString pentru detalii abonat
        public override string ToString()
        {
            return $"Abonat: {_nume}, CNP: {_cnp}, Username: {_username}, Tip Abonament: {_tipAbonament}, Taxe Suplimentare: {_taxaSuplimentara} RON";
        }
    }
}