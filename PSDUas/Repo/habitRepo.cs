using PSDUas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSDUas.Repo
{
    public class habitRepo
    {
        private static HabitDatabaseEntities1 habitDatabaseEntities = new HabitDatabaseEntities1();

        public static Habit habitRegister(string name, string days_off, Guid user_id)
        {
            if(name.Length > 2 && name.Length < 100)
            {
                Habit h = Factory.Factory.insertHabit(Guid.NewGuid(), name, days_off, 0, 0, 0, DateTime.Now.ToString(), user_id, DateTime.Now);

                habitDatabaseEntities.Habit.Add(h);
                habitDatabaseEntities.SaveChanges();

                return h;
            }
            return null;
        }


    }
}