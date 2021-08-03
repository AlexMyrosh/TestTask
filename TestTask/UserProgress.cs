using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    static class UserProgress
    {
        public static void SaveProgress(Deck deck)
        {
            Console.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream($"{Directory.GetCurrentDirectory()}\\Progress.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, deck);
            }
        }
        public static Deck RestoreProgress()
        {
            if (File.Exists($"{Directory.GetCurrentDirectory()}\\Progress.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream($"{Directory.GetCurrentDirectory()}\\Progress.dat", FileMode.OpenOrCreate))
                {
                    return (Deck)formatter.Deserialize(fs);
                }
            }
            return new Deck();
        }
    }
}
