using System.Globalization;

namespace Client.Infrastructure
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime miladiDate)
        {
            PersianCalendar pr = new();

            var _shamsi =
                $"{pr.GetYear(miladiDate).ToString().PadLeft(4,'0')}/{pr.GetMonth(miladiDate).ToString().PadLeft(2,'0')}/{pr.GetDayOfMonth(miladiDate).ToString().PadLeft(2,'0')}";

            return _shamsi;
        }
    }
}
