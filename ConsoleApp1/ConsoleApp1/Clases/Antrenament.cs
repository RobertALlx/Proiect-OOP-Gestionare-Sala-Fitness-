namespace ConsoleApp1.Clases;

public class Antrenament
{
    private DateTime _dataOra;
    private TimeSpan _durata;
    private string _antrenor;

    public DateTime DataOra{get=>_dataOra;set=>_dataOra=value;}
    public TimeSpan Durata{get=>_durata;set=>_durata=value;}
    public string Antrenor{get=>_antrenor; set=>_antrenor=value;}

    public Antrenament(DateTime dataOra, TimeSpan durata, string antrenor)
    {
        _dataOra = dataOra;
        _durata = durata;
        _antrenor = antrenor;
    }
}
