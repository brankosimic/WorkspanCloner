using System;

namespace WorkspanCloner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Runner.ParseAndRun(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
