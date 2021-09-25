using Iot.Device.SensorHub;
using System;
using System.Device.I2c;
using System.Threading;

namespace SenseHub
{
    class Program
    {
        static void Main(string[] args)
        {
            const int I2cBusId = 1;

            // 0x17
            I2cConnectionSettings connectionSettings = new(I2cBusId, SensorHub.DefaultI2cAddress);
            SensorHub sh = new(I2cDevice.Create(connectionSettings));

            if (sh.TryReadOffBoardTemperature(out var t))
            {
                Console.WriteLine($"OffBoard temperature {t}");
            }

            if (sh.TryReadBarometerPressure(out var p))
            {
                Console.WriteLine($"Pressure {p}");
            }

            if (sh.TryReadBarometerTemperature(out var bt))
            {
                Console.WriteLine($"Barometer temperature {bt}");
            }

            if (sh.TryReadIlluminance(out var l))
            {
                Console.WriteLine($"Illuminance {l}");
            }

            if (sh.TryReadOnBoardTemperature(out var ot))
            {
                Console.WriteLine($"OnBoard temperature {ot}");
            }

            if (sh.TryReadRelativeHumidity(out var h))
            {
                Console.WriteLine($"Relative humidity {h}");
            }

            Console.WriteLine($"Detecting motion, ctrl+c to quit");
            while (true)
            {
                if (sh.IsMotionDetected)
                {
                    Console.WriteLine($"{DateTime.Now:T} Motion detected");
                }

                Thread.Sleep(500);
            }
        }
    }
}
