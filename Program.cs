using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using CommandLine.Text;
using Standart.Hash.xxHash;

namespace metagen
{
    internal struct Guid
    {
        public UInt64 upper;
        public UInt64 lower;

        public override string ToString()
        {
            return $"{upper:x8}{lower:x8}";
        }
    }
    internal class ProgramOptions
    {
        [Option('s', "seed",
            Default = "",
            HelpText = "seed string, e.g. package name",
            Required = false)]
        public string Seed { get; set; }

        [Option('o', "overwrite",
            HelpText = "overwrite existing .meta files",
            Required = false)]
        public bool Overwrite { get; set; }

        [Value(0,
            Min = 1,
            MetaName = "input files",
            HelpText = "files to generate .meta files for",
            Required = true)]
        public IEnumerable<string> Files { get; set; }
    };

    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<ProgramOptions>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }

        static void RunOptions(ProgramOptions opts)
        {
            // handle empty opts.Seed
            if (opts.Seed.Length == 0)
            {
                opts.Seed = Directory.GetCurrentDirectory();
            }

            UInt64 seed = xxHash64.ComputeHash(Encoding.UTF8.GetBytes(opts.Seed));
            Console.WriteLine($"seed: {opts.Seed} -> {seed:x8}");

            foreach (var f in opts.Files)
            {
                if (File.Exists(f) && f.EndsWith(@".meta"))
                {
                    continue;
                }
                else if (File.Exists(f) || Directory.Exists(f))
                {
                    Guid guid = new Guid();
                    guid.upper = xxHash64.ComputeHash(Encoding.UTF8.GetBytes(f), seed);
                    guid.lower = xxHash64.ComputeHash(Encoding.UTF8.GetBytes(f), guid.upper);

                    Console.WriteLine($"{f} -> {guid}");

                    generateMetaFile(f, guid, opts.Overwrite);
                }
            }
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.Error.WriteLine("Parser errors occurred");
            foreach (var e in errs)
            {
                Console.Error.WriteLine($"{e}");
            }
            throw new System.FormatException("Invalid command line options");
        }

        static void generateMetaFile(string filename, Guid guid, bool overwrite)
        {
            var metaPath = filename + @".meta";

            if (File.Exists(metaPath) && !overwrite)
            {
                Console.WriteLine($"Skipping existing {metaPath}.");
                return;
            }

            var metaContents = "fileFormatVersion: 2\n"
            + $"guid: {guid}\n"
            + @"MonoImporter:
  externalObjects: {}
  serializedVersion: 2
  defaultReferences: []
  executionOrder: 0
  icon: {instanceID: 0}
  userData: 
  assetBundleName: 
  assetBundleVariant: 
";
            File.WriteAllText(metaPath, metaContents);
        }
    }
}
