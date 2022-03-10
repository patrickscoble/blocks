using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blocks.Models
{
    public class TimesTableCollection
    {
        public Collection<TimesTable> TimesTables { get; set; }

        public TimesTableCollection()
        {
            TimesTables = new Collection<TimesTable>();
        }

        public void LoadTimesTables(bool include11s, bool include12s, int numberOfQuestions)
        {
            List<TimesTable> timesTables = new List<TimesTable>();

            for (int i = 1; i < 13; i++)
            {
                if (i == 11 && !include11s)
                    continue;

                if (i == 12 && !include12s)
                    continue;

                for (int j = 1; j < 13; j++)
                {
                    if (j == 11 && !include11s)
                        continue;

                    if (j == 12 && !include12s)
                        continue;

                    TimesTable timesTable = new TimesTable()
                    {
                        FirstNumber = i,
                        SecondNumber = j
                    };

                    timesTables.Add(timesTable);
                };
            };

            Random random = new Random();
            timesTables = timesTables.OrderBy(button => random.Next()).ToList();

            TimesTables = new Collection<TimesTable>(timesTables.Take(numberOfQuestions).ToList());
        }
    }
}
