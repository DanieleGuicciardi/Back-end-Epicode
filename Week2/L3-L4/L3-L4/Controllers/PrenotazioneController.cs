using Microsoft.AspNetCore.Mvc;
using L3_L4.Models;

namespace L3_L4.Controllers
{
    public class PrenotazioneController : Controller
    {
        // Simuliamo un "database" con liste statiche
        private static List<Sala> Sale = new List<Sala>
        {
            new Sala { Nome = NomeSala.SalaNord },
            new Sala { Nome = NomeSala.SalaEst },
            new Sala { Nome = NomeSala.SalaSud }
        };

        private static List<Prenotazione> Prenotazioni = new List<Prenotazione>();

        // 1️⃣ Mostra il form di prenotazione (GET)
        public IActionResult Index()
        {
            ViewBag.Sale = Sale; // Passiamo l'elenco delle sale alla View
            return View();
        }

        // 2️⃣ Salva una nuova prenotazione (POST)
        [HttpPost]
        public IActionResult Prenota(Prenotazione nuovaPrenotazione)
        {
            if (string.IsNullOrEmpty(nuovaPrenotazione.Nome) || string.IsNullOrEmpty(nuovaPrenotazione.Cognome))
            {
                return BadRequest("Nome e Cognome sono obbligatori!");
            }

            // Trova la sala corrispondente
            Sala salaSelezionata = Sale.FirstOrDefault(s => s.Nome == nuovaPrenotazione.Sala);

            if (salaSelezionata == null)
            {
                return BadRequest("Sala non valida!");
            }

            // Controllo sulla capienza
            if (salaSelezionata.BigliettiVenduti >= salaSelezionata.Capienza)
            {
                return BadRequest("Sala piena! Prenotazione non possibile.");
            }

            // Aggiunge la prenotazione
            Prenotazioni.Add(nuovaPrenotazione);
            salaSelezionata.BigliettiVenduti++;

            if (nuovaPrenotazione.TipoBiglietto == TipoBiglietto.Ridotto)
            {
                salaSelezionata.BigliettiRidotti++;
            }

            return RedirectToAction("Index");
        }

        // 3️⃣ Restituisce i dati aggiornati delle sale (JSON per AJAX)
        public JsonResult GetDatiSale()
        {
            var datiSale = Sale.Select(s => new
            {
                nome = s.Nome.ToString(),
                postiLiberi = s.Capienza - s.BigliettiVenduti,
                bigliettiVenduti = s.BigliettiVenduti,
                bigliettiRidotti = s.BigliettiRidotti
            });

            return Json(datiSale);
        }
    }
}
