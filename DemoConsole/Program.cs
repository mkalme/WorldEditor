using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using WorldEditor;

namespace DemoConsole {
    class Program {
        private static string RegionFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\.minecraft\\saves\\world_1_12_2\\region";

        static void Main(string[] args) {
            LoadUpWorld(RegionFolder);

            //WriteWorld(RegionFolder);

            Console.WriteLine("Done");

            Console.ReadLine();
        }

        private static void WriteWorld(string regionFolder) {
            //string[] files = Directory.GetFiles(regionFolder);

            //Region[] regions = new Region[files.Length];

            //Parallel.For(0, files.Length, j => {
            //    regions[j] = Region.FromFile(files[j]);

            //    for (int k = 0; k < regions[j].Chunks.Count; k++) {
            //        var chunk = regions[j].Chunks[k];

            //        for (int l = 0; l < chunk.Sections.Count; l++) {
            //            var section = chunk.Sections[l];

            //            if (!section.IsEmpty()) {
            //                section.Lock(section.Unlock());
            //            }
            //        }
            //    }

            //    regions[j].SaveToFile(regionFolder);
            //});
        }

        private static void LoadUpWorld(string regionFolder) {
            string[] files = Directory.GetFiles(regionFolder);

            long[] times = new long[1];
            Stopwatch watch = new Stopwatch();

            for (int i = 0; i < times.Length; i++) {
                watch.Restart();

                Region[] regions = new Region[files.Length];
                Parallel.For(0, files.Length, j => {
                    regions[j] = Region.FromFile(files[j]);

                    for (int k = 0; k < regions[j].Chunks.Count; k++) {
                        var chunk = regions[j].Chunks[k];

                        for (int l = 0; l < chunk.Sections.Count; l++) {
                            var section = chunk.Sections[l];

                            if (!section.IsEmpty()) {
                                section.Lock(section.Unlock());
                            }
                        }
                    }
                });

                watch.Stop();
                times[i] = watch.ElapsedMilliseconds;

                Console.WriteLine($"{i + 1}/{times.Length}");
            }

            Console.WriteLine($"Average time: {times.Sum() / times.Length}");
        }
    }
}
