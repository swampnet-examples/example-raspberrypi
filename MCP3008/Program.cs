using Iot.Device.Adc;
using System;
using System.Device.Spi;
using System.Threading;

namespace MCP3008
{
    // https://github.com/dotnet/iot/blob/main/src/devices/Mcp3xxx/README.md
    internal class Program
    {
        static void Main(string[] args)
        {
            int chipSelectLine = 0;
            int busId = 0;

            // basically which pin we read from
            int channel_pot = 0;
            int channel_moisture = 1;

            var hardwareSpiSettings = new SpiConnectionSettings(busId, chipSelectLine)
            {
                ClockFrequency = 1000000
            };

            using (SpiDevice spi = SpiDevice.Create(hardwareSpiSettings))
            {
                using (Mcp3008 mcp = new Mcp3008(spi))
                {
                    var lastValue_pot = 0.0;
                    var lastValue_moisture = 0.0;
                    while (true)
                    {
                        lastValue_pot = Read(channel_pot, mcp, lastValue_pot);
                        lastValue_moisture = Read(channel_moisture, mcp, lastValue_moisture);
                        Thread.Sleep(100);
                    }
                }
            }
        }

        private static double Read(int channel, Mcp3008 mcp, double lastValue)
        {
            double value = mcp.Read(channel);

            // The chip is 10-bit, which means that it will generate values from
            // 0-1023 (2^10 is 1024).
            // We can transform the value to a more familiar 0-100 scale by dividing
            // the 10-bit value by 10.24.
            value = value / 10.24;
            value = Math.Round(value);
            if (lastValue != value)
            {
                lastValue = value;
                Console.WriteLine($"[{DateTime.Now:HH\\:mm\\:ss}] [{channel}] {value}%");
            }

            return lastValue;
        }
    }
}
