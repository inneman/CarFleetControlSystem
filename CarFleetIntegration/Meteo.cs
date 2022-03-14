using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFleetIntegration
{
    public class CarGetMeteoEventArgs : EventArgs
    {
        public Guid ForCar { get; set; }
        public string MeteoAction { get; set; }
    }
    public delegate void GetMeteo(CarGetMeteoEventArgs e);
    public class Meteo
    {
        public event GetMeteo MeteoAction;
        public void SubscribeToMeteoCarChanged(Car car)
        {
            car.MeteoJustChanged += MeteoChanged;
        }
        // Když přijde informace od meteo, změň počasí
        public void MeteoChanged(CarMeteoChangeEventArgs err)
        {
            Debug.WriteLine($"Meteo: Přijata změna { err.Weather}");
            if (err.Weather < 100)
                MeteoAction(new CarGetMeteoEventArgs { MeteoAction = "Prší", ForCar = err.FromCar });
            else
                MeteoAction(new CarGetMeteoEventArgs { MeteoAction = "Svítí sluníčko", ForCar = err.FromCar });
        }
    }
}
