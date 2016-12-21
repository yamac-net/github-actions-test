using System;

namespace Core.Example
{
    public class ExampleService : IExampleService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
