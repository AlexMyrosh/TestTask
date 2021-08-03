using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class User
    {
        public int AmountOfWordsForOneSession { get; private set; }
        public int AmountOfAlreadyLearntWords { get; private set; }
        public User(int _AmountOfWordsForOneSession = 10)
        {
            AmountOfWordsForOneSession = _AmountOfWordsForOneSession;
            AmountOfAlreadyLearntWords = 0;
        }


        public void ChangeWordsADay()
        {
            Console.Clear();
            Console.WriteLine($"Текущее значение: {AmountOfWordsForOneSession} слов за одну сессию");
            Console.Write("Новое значение: ");

            if(int.TryParse(Console.ReadLine(), out int newValue) && newValue >= 0)
            {
                AmountOfWordsForOneSession = newValue;
                Console.WriteLine("Значение успешно измененно:)");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Значение не корректно:(");
                Console.ReadKey();
            }
        }
        public void LearntOneWord()
        {
            AmountOfAlreadyLearntWords++;
        }


    }
}