using System;
using WorkspanCloner.Models;
using WorkspanCloner.Readers;

namespace WorkspanCloner
{
    public static class Runner
    {
        /// <summary>
        /// Parses arguments and runs the application
        /// </summary>
        /// <param name="args">Arguments.</param>
        static public string ParseAndRun(string[] args)
        {
            if (args.Length == 2)
            {
                var filePath = args[0];
                int.TryParse(args[1], out int toClone);


                if (toClone > 0)
                    return Run(filePath, toClone);
                else
                    throw new ArgumentException("Please pass the entity id as integer as second parameter. " +
                        "Example: dotnet WorkspanCloner Resource/2levelexample.json 5");
            }
            else
                throw new ArgumentException("Two parameters are required: 1. filePath and 2. entityId to clone. " +
                        "Example: dotnet WorkspanCloner Resource/2levelexample.json 5");
        }

        /// <summary>
        /// Runs the index build, clones the node and outputs the result
        /// </summary>
        /// <param name="filePath">File path to the entities json file.</param>
        /// <param name="toClone">Id of the entity to clone</param>
        static private string Run(string filePath, int toClone)
        {
            var input = JsonReader.Load<EntitiesGraph>(filePath);

            if (input.Entities != null && input.Links != null)
            {
                var index = new Index(input);
                index.Clone(toClone);

                var output = index.GetOutput();

                Console.WriteLine(output);
                return output;
            }
            else
                throw new MissingMemberException();
        }
    }
}
