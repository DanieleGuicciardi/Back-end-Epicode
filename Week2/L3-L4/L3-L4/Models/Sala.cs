namespace L3_L4.Models;

public enum NomeSala
{
    SalaNord,
    SalaSud,
    SalaEst
}

public class Sala
{
    public NomeSala Nome { get; set; }
    public int Capienza { get; set; } = 120;
    public int BigliettiVenduti { get; set; }
    public int BigliettiRidotti { get; set; }
    public List<Prenotazione> Prenotazioni { get; set; } = new List<Prenotazione>();
}