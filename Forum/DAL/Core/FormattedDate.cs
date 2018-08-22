namespace DAL.Core
{
    using System;

    public class FormattedDate
    {
        public DateTime Date { get; set; }

        public FormattedDate(DateTime date)
        {
            this.Date = date.Date;
        }

        public override string ToString()
        {
            return this.Date.ToString("dd-MM-yyyy");
        }

        public static implicit operator FormattedDate(DateTime dateTime)
        {
            return new FormattedDate(dateTime);
        }
    }
}
