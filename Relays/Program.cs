using System;
using System.Device.Gpio;
using System.Threading;

namespace Relays
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Relay!, ctrl+C to quit");

            var r1 = 10;
            var r2 = 12;
            var r3 = 16;
            var r4 = 18;

            using var controller = new GpioController(PinNumberingScheme.Board);

            // High is off
            controller.OpenPin(r1, PinMode.Output, PinValue.High);
            controller.OpenPin(r2, PinMode.Output, PinValue.High);
            controller.OpenPin(r3, PinMode.Output, PinValue.High);
            controller.OpenPin(r4, PinMode.Output, PinValue.High);

            while (true)
            {
                Toggle(controller, r1);
                Toggle(controller, r2);
                Toggle(controller, r3);
                Toggle(controller, r4);
            }
        }


        private static void Toggle(GpioController controller, int r)
        {
            var t = 1000;

            Console.WriteLine($"{r} Low (ON)");
            controller.Write(r, PinValue.Low);
            Thread.Sleep(t);

            Console.WriteLine($"{r} High (OFF)");
            controller.Write(r, PinValue.High);
            Thread.Sleep(t*2);
        }
    }
}
