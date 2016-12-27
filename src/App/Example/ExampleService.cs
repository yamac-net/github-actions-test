using System;

namespace App.Example
{
    public class ExampleService : IExampleService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
