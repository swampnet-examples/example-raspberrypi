using System;
using System.Device.Gpio;
using System.Threading;

namespace Blinky
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Blinky!, ctrl+C to quit");

            var pin = 10;
            var btnPin = 26;
            //var lightTime = 1000;


            using var controller = new GpioController(PinNumberingScheme.Board);

            controller.OpenPin(pin, PinMode.Output);
            controller.OpenPin(btnPin, PinMode.InputPullUp);

            //bool ledOn = true;

            while (true)
            {
                if (controller.Read(btnPin) == PinValue.Low)
                {
                    controller.Write(pin, PinValue.High);
                }
                else
                {
                    controller.Write(pin, PinValue.Low);
                }
                //Console.WriteLine($"led: {ledOn}");
                //controller.Write(pin, ((ledOn) ? PinValue.High : PinValue.Low));
                //Thread.Sleep(lightTime);
                //ledOn = !ledOn;
            }
        }
    }
}
