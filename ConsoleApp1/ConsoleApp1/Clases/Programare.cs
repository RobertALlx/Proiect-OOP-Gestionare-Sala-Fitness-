using System;

namespace ConsoleApp1
{
    public class Programare
    {
        private Abonat _abonat;
        private Antrenor _antrenor;
        private DateTime _dataOra;
        private double _durata; // Durata rezervata in ore
        private double _durataRealizata; // Durata reala in ore

        public Abonat Abonat { get => _abonat; set => _abonat = value; }
        public Antrenor Antrenor { get => _antrenor; set => _antrenor = value; }
        public DateTime DataOra { get => _dataOra; set => _dataOra = value; }
        public double Durata { get => _durata; set => _durata = value; }
        public double DurataRealizata { get => _durataRealizata; set => _durataRealizata = value; }
        public double TaxaSuplimentara { get; private set; } = 0;
        public double TaxaPenalizare { get; private set; } = 0;  // Taxa penalizare pentru anulare

        public Programare(Abonat abonat, Antrenor antrenor, DateTime dataOra, double durata)
        {
            _abonat = abonat;
            _antrenor = antrenor;
            _dataOra = dataOra;
            _durata = durata;
            _durataRealizata = durata; // Initial, se presupune ca durata realizata este cea rezervata
        }

        // Metoda pentru calcularea taxelor suplimentare
        public void CalculeazaTaxaSuplimentara()
        {
            double oreSuplimentare = _durataRealizata - _durata;

            if (oreSuplimentare > 0)
            {
                if (_abonat.TipAbonament == "Standard")
                {
                    TaxaSuplimentara = oreSuplimentare * 5; // 5 RON/ora pentru abonati standard
                }
                else if (_abonat.TipAbonament == "Premium")
                {
                    TaxaSuplimentara = oreSuplimentare * 2; // 2 RON/ora pentru abonati premium
                }

                // Adaugam taxa in contul abonatului
                _abonat.AdaugaTaxaSuplimentara(TaxaSuplimentara);

                Console.WriteLine($"Taxa suplimentara de {TaxaSuplimentara} RON a fost adaugata pentru abonatul {_abonat.Nume}.");
            }
        }

        // Metoda pentru anularea programarii
        public void AnulareProgramare()
        {
            var oraCurenta = DateTime.Now;
            var diferentaOre = (DataOra - oraCurenta).TotalHours;

            if (diferentaOre < 24)
            {
                TaxaPenalizare = 10;  // Penalizare de 10 RON pentru anulare cu mai putin de 24 de ore
                _abonat.AdaugaTaxaSuplimentara(TaxaPenalizare); // Adaugam taxa penalizare la contul abonatului

                Console.WriteLine($"Programarea pentru {_abonat.Nume} cu antrenorul {_antrenor.NumeComplet} a fost anulata. Penalizare aplicata de {TaxaPenalizare} RON.");
            }
            else
            {
                Console.WriteLine($"Programarea pentru {_abonat.Nume} cu antrenorul {_antrenor.NumeComplet} a fost anulata fara penalizare.");
            }
        }

        // Metoda pentru modificarea programarii
        public void ModificareProgramare(DateTime nouaDataOra, double nouaDurata)
        {
            // Modificam doar daca diferenta de timp este suficient de mare
            var oraCurenta = DateTime.Now;
            var diferentaOre = (DataOra - oraCurenta).TotalHours;

            if (diferentaOre >= 24)
            {
                _dataOra = nouaDataOra;  // Modificam data si ora programarii
                _durata = nouaDurata;     // Modificam durata

                Console.WriteLine($"Programarea pentru {_abonat.Nume} cu antrenorul {_antrenor.NumeComplet} a fost modificata la {nouaDataOra} cu durata de {nouaDurata} ore.");
            }
            else
            {
                Console.WriteLine($"Nu se poate modifica programarea pentru {_abonat.Nume} cu antrenorul {_antrenor.NumeComplet}, deoarece este mai putin de 24 de ore pana la programare.");
            }
        }

        // Suprascrierea metodei ToString pentru afisarea detaliilor programarii
        public override string ToString()
        {
            return $"Programare: {_abonat.Nume}, Antrenor: {_antrenor.NumeComplet}, Data: {_dataOra}, Durata rezervata: {_durata} ore, Durata realizata: {_durataRealizata} ore, Taxa suplimentara: {TaxaSuplimentara} RON, Taxa penalizare: {TaxaPenalizare} RON";
        }
    }
}
