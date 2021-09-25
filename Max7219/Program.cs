using Iot.Device.Max7219;
using System;
using System.Device.Spi;
using System.Threading;

namespace Max7219Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionSettings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 10_000_000,
                Mode = SpiMode.Mode0
            };
            var spi = SpiDevice.Create(connectionSettings);
            using (var devices = new Max7219(spi, cascadedDevices: 4))
            {
                devices.Init();
                devices.Brightness(15);

                ShowMessage(devices);
                //Smiley(devices);
                //FadeOut(devices);
                devices.ClearAll();
            }
        }

        private static void FadeOut(Max7219 devices)
        {
            for(int i = 15; i >= 0; i--)
            {
                Console.WriteLine($"{i}");
                devices.Brightness(i);
                Thread.Sleep(500);
            }
        }

        private static void Smiley(Max7219 devices)
        {
            var smiley = new byte[] {
                0b00111100,
                0b01000010,
                0b10100101,
                0b10000001,
                0b10100101,
                0b10011001,
                0b01000010,
                0b00111100
            };

            for (var i = 0; i < devices.CascadedDevices; i++)
            {
                for (var digit = 0; digit < 8; digit++)
                {
                    devices[i, digit] = smiley[digit];
                }
            }
        }

        private static void ShowMessage(Max7219 devices)
        {
            devices.Rotation = RotationType.Left;
            var writer = new MatrixGraphics(devices, Fonts.CP437);

            writer.Font = Fonts.CP437;
            writer.ShowMessage($"Hello World!! the date is {DateTime.Now}!", alwaysScroll: true);

            //foreach (var font in new[] { Fonts.CP437, Fonts.LCD, Fonts.Sinclair, Fonts.Tiny, Fonts.CyrillicUkrainian })
            //{
            //    writer.Font = font;
            //    writer.ShowMessage($"Hello World!! the date is {DateTime.Now}!       ", alwaysScroll: true);
            //}
        }
    }
}
