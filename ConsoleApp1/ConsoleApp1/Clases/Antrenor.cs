namespace ConsoleApp1.Clases;

public class Antrenor
{
     private string _numeComplet;
    private string _specializare;
    private int _numarMaximClienti;
    private Dictionary<string, bool> _orar;
    
    public string NumeComplet{ get => _numeComplet; set => _numeComplet = value; }
    public string Specializare { get => _specializare; set => _specializare = value; }
    public int NumarMaximClienti { get => _numarMaximClienti; set => _numarMaximClienti = value; }
    public Dictionary<string, bool> Orar { get => _orar; set => _orar = value; }

    public Antrenor(string numeComplet, string specializare, int numarMaximClienti, Dictionary<string, bool> orar)
    {
        _numeComplet = numeComplet;
        _specializare = specializare;
        _numarMaximClienti = numarMaximClienti;
        _orar = orar;
    }
}
