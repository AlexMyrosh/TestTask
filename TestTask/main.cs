using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestTask
{
    class main
    {
        static void Main()
        {
            Deck deck = UserProgress.RestoreProgress();
            User user = new User();
            while (true)
            {
                int SelectedMenuItem = MainMenu();
                switch (SelectedMenuItem)
                {
                    case 1:
                        deck.AddWord();
                        UserProgress.SaveProgress(deck);
                        break;
                    case 2:
                        deck.RemoveWord();
                        UserProgress.SaveProgress(deck);
                        break;
                    case 3:
                        deck.EditWord();
                        UserProgress.SaveProgress(deck);
                        break;
                    case 4:
                        deck.RevisingWords(user);
                        break;
                    case 5:
                        user.ChangeWordsADay();
                        break;
                    case 6:
                        return;
                }
            }
        }

        static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Добавить новое слово");
            Console.WriteLine("2. Удалить существующее слово");
            Console.WriteLine("3. Редактировать существующее слово");
            Console.WriteLine("4. Изучать слова");
            Console.WriteLine("5. Изменить количество слов для изучения");
            Console.WriteLine("6. Выход");
            Console.Write("Выбор: ");
            Int32.TryParse(Console.ReadLine(), out int choice);
            return choice;
        }
    }
}