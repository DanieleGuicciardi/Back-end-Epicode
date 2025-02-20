namespace L3_L4.Models;

public enum TipoBiglietto
{
    Intero,
    Ridotto
}

public class Prenotazione
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public NomeSala Sala { get; set; }
    public TipoBiglietto TipoBiglietto { get; set; }
}