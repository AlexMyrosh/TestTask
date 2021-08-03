using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    [Serializable]
    class Deck
    {
        public List<FlashCard> Cards { get; private set; }
        public Deck(List<FlashCard> _cards)
        {
            Cards = _cards;
        }
        public Deck(params FlashCard[] _FlashCards)
        {
            Cards = new List<FlashCard>();
            Cards.AddRange(_FlashCards);
        }
        public Deck()
        {
            Cards = new List<FlashCard>();
        }


        public void EditWord()
        {
            Console.Clear();
            Console.Write("Какое слово надо изменить (Слово или перевод): ");
            string WordToDelete = Console.ReadLine();

            int NumberOfLineInFile = 0;
            foreach (var item in Cards)
            {
                if (item.Word.ToLower() == WordToDelete.ToLower() || item.Translation.ToLower() == WordToDelete.ToLower())
                {
                    Console.WriteLine("Введите новые значения");
                    Console.Write("Слово: ");
                    string NewWord = Console.ReadLine();
                    Console.Write("Перевод: ");
                    string NewTranslation = Console.ReadLine();
                    item.EditWord(NewWord, NewTranslation);
                    EditWordInFile(NewWord, NewTranslation, NumberOfLineInFile);
                    Console.WriteLine("Слово успешно изменено!");
                    Console.ReadKey();
                    return;
                }
                NumberOfLineInFile++;
            }
            Console.WriteLine("Слово не найдено!");
            Console.ReadKey();
        }
        private void EditWordInFile(string _NewWord, string _NewTranslation, int NumberOfLineInFile)
        {
            string[] lines = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Words.txt", Encoding.Default);

            if (lines[NumberOfLineInFile] != null)
                lines[NumberOfLineInFile] = $"{_NewWord};{_NewTranslation}";
            File.WriteAllLines($"{Directory.GetCurrentDirectory()}\\Words.txt", lines);
        }

        public void RemoveWord()
        {
            Console.Clear();
            Console.Write("Какое слово надо удалить (слово или перевод): ");
            string WordToDelete = Console.ReadLine();
            foreach (var item in Cards)
            {

                if (item.Word.ToLower() == WordToDelete.ToLower() || item.Translation.ToLower() == WordToDelete.ToLower())
                {
                    Cards.Remove(item);
                    RemoveWordFromFile(item);
                    Console.WriteLine("Слово успешно удалено!");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("Слово не найдено!");
            Console.ReadKey();
        }
        private void RemoveWordFromFile(FlashCard card)
        {
            List<string> Lines = File.ReadLines($"{Directory.GetCurrentDirectory()}\\Words.txt").Where(l => l != $"{card.Word};{card.Translation}").ToList();
            File.WriteAllLines($"{Directory.GetCurrentDirectory()}\\Words.txt", Lines);
        }

        public void AddWord()
        {
            Console.Clear();
            Console.Write("Слово: ");
            string word = Console.ReadLine();
            Console.Write("Перевод: ");
            string translation = Console.ReadLine();
            FlashCard card = new FlashCard(word, translation);
            Cards.Add(card);
            AddWordToFile(word, translation);
            Console.WriteLine("Слово успешно добавлено!");
            Console.ReadKey();
        }
        private void AddWordToFile(string _word, string _transcription)
        {
            using (StreamWriter writer = new StreamWriter($"{Directory.GetCurrentDirectory()}\\Words.txt", true))
            {
                writer.WriteLine($"{_word};{_transcription}");
                writer.Close();
            }
        }

        public void RevisingWords(User user)
        {
            foreach (var word in Cards)
            {
                if (word.NextRevise <= DateTime.Now)
                {
                    if (user.AmountOfAlreadyLearntWords >= user.AmountOfWordsForOneSession) break;
                    int result = CheckWordForCorectness(word, user); //1 - ответ правильный, 0 - ответ неправильный, -1 - выход в главное меню
                    if (result < 0) return;
                    else if (result > 0)
                    {
                        user.LearntOneWord();
                        word.AddTimeToWordNextRevise();
                    }
                    else
                    {
                        word.SubtractTimeFromWordRevise();
                    }
                }
                UserProgress.SaveProgress(this);
            }
            Console.WriteLine("Слова закончились или вы выполнили свою норму");
        }
        private int CheckWordForCorectness(FlashCard card, User user)
        {
            Console.Clear();
            Console.WriteLine($"Повтрено слов: {user.AmountOfAlreadyLearntWords}, осталось: {user.AmountOfWordsForOneSession - user.AmountOfAlreadyLearntWords}");
            Console.WriteLine("Для выхода в меню, введите '-1'");
            Console.Write($"{card.Word} - ");
            string answer = Console.ReadLine();
            if (answer == "-1") return -1;
            if (answer.ToLower() == card.Translation.ToLower())
            {
                Console.WriteLine("Ответ правильный!");
                Console.ReadKey();
                return 1;
            }
            Console.WriteLine("Ответ неправильный!");
            Console.ReadKey();
            return 0;
        }
    }
}
