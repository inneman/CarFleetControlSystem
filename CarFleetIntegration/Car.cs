using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFleetIntegration
{
    public class CarErrorEventArgs : EventArgs
    {
        public Guid FromCar { get; set; }
        public int Severity { get; set; }
    }
    public class CarMeteoChangeEventArgs : EventArgs
    {
        public Guid FromCar { get; set; }
        public int Weather { get; set; }
    }

    public delegate void CarError(CarErrorEventArgs e);
    public delegate void MeteoChanged(CarMeteoChangeEventArgs e);

    public class Car
    {
        Random rnd = new Random();

        public Guid Id { get; set; }
        public Car()
        {
            Id = Guid.NewGuid();
        }

        public event CarError ErrorJustHappened;
        public event MeteoChanged MeteoJustChanged;

        public void RunInCaseOfError()
        {
            ErrorJustHappened(new CarErrorEventArgs { Severity = rnd.Next(0, 200), FromCar = Id });
        }
        public void RunInCaseOfChange()
        {
            MeteoJustChanged(new CarMeteoChangeEventArgs { Weather = rnd.Next(0, 200), FromCar = Id});
        }

        // až bude hotová diagnóza, udělej akci podle doporučení
        public void SubscribeToService(Center center)
        {
            center.ServiceActions += ServicingOnAdvice;
        }

        public void SubscribeToMeteo(Meteo meteo)
        {
            meteo.MeteoAction += MeteoOnAdvice;
        }

        public void ServicingOnAdvice(CarRepareEventArgs e)
        {
            if (e.ForCar == Id)
                Debug.WriteLine($"Car: Přijatá oprava pro auto id={e.ForCar} je {e.ServiceAction}");
        }
        public void MeteoOnAdvice(CarGetMeteoEventArgs e)
        {
            if (e.ForCar == Id)
                Debug.WriteLine($"Car: Přijatá meteo pro auto id={e.ForCar} je {e.MeteoAction}");
        }
    }
}
