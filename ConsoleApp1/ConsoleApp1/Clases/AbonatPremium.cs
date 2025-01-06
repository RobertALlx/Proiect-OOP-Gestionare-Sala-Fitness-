namespace ConsoleApp1.Clases;

public class AbonatPremium:Abonat
{
     private const int _extraOre = 2; // Numar fix de ore suplimentare

    public int ExtraOre => _extraOre;

    public AbonatPremium(string nume, string cnp, string username, string password,string tipAbonament)
        : base(nume, cnp, username, password, tipAbonament)
    {
    }

    public void AfiseazaBeneficii()
    {
        Console.WriteLine($"Abonatul premium beneficiază de {_extraOre} ore suplimentare de acces.");
    }
}
