using System;
using Microsoft.SPOT;

namespace appTest.HAL
{
    public static class ThreadExt
    {

        public static void SleepMicro(int us)
        {
            long start = DateTime.Now.Ticks;
            long current = DateTime.Now.Ticks;
            while ((current - start) <= us)
            {
                current = DateTime.Now.Ticks;
            }
        }
    }
}
