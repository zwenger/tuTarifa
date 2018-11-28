using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication6.Models
{
    public class Viaje
    {
        public int Id { get; set; }
        [Required]
        public string Origen { get; set; }
        [Required]
        public string Destino { get; set; }

        public double Costo { get; set; }

        
      

        public void CalcularCosto()
        {
            
            string url = "https://maps.googleapis.com/maps/api/directions/json?origin="+this.Origen+"+tucuman&destination="+this.Destino+"+tucuman"+"&key=AIzaSyDKu6COjyU7RgAFgtSPqK5b9sISD_3iveY";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            Costo = ((double)m.routes[0].legs[0].distance.value * 0.021) + 21;

        }
    }

    
}