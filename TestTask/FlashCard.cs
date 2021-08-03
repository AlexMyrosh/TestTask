using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    [Serializable]
    class FlashCard
    {
        public string Word { get; private set; }
        public string Translation { get; private set; }
        public DateTime NextRevise { get; private set; }
        public int AmountOfRevising { get; private set; }//Чем больше успешных повторений, тем больше промежуток времени до следующего повторения

        public FlashCard(string _word, string _translation)
        {
            Word = _word;
            Translation = _translation;
            NextRevise = DateTime.Now;
            AmountOfRevising = 0;
        }

        public void EditWord(string _word, string _translation)
        {
            Word = _word;
            Translation = _translation;
        }

        public void AddTimeToWordNextRevise()
        {
            switch (AmountOfRevising)
            {
                case 0:
                    //+5 минут
                    NextRevise = DateTime.Now.Add(new TimeSpan(0, 5, 0));
                    break;
                case 1:
                    NextRevise = DateTime.Now.Add(new TimeSpan(24, 0, 0));
                    //+24 часа
                    break;
                case 2:
                    NextRevise = DateTime.Now.Add(new TimeSpan(7,0,0,0,0));
                    //+7 дней
                    break;
                case 3:
                    NextRevise = DateTime.Now.Add(new TimeSpan(16, 0, 0, 0, 0));
                    //+16 дней
                    break;
                case 4:
                    NextRevise = DateTime.Now.Add(new TimeSpan(35, 0, 0, 0, 0));
                    //+35 дней
                    break;
                case 5:
                    NextRevise = DateTime.MaxValue;
                    break;
            }
            AmountOfRevising++;
        }
        public void SubtractTimeFromWordRevise()
        {
            if(AmountOfRevising > 4) AmountOfRevising = 3;
        }
    }
}
