using PSDUas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSDUas.Factory
{
    public class Factory
    {
        public static Habit insertHabit(Guid id, string name, string days_off, int current_streak, int longest_streak,
            int log_count, string logs, Guid user_id, DateTime created_at)
        {
            Habit h = new Habit();
            h.Id = id;
            h.name = name;
            h.days_off = days_off;
            h.current_streak = current_streak;
            h.longest_streak = longest_streak;
            h.log_count = log_count;
            h.logs = logs;
            h.user_id = user_id;
            h.created_at = created_at;
            return h;
        }

        public static Badge insertBadge(Guid id, string name, string description, Guid user_id, DateTime created_at)
        {
            Badge b = new Badge();
            b.Id = id;
            b.name = name;
            b.description = description;
            b.user_id = user_id;
            b.created_at = created_at;
            return b;
        }
    }
}