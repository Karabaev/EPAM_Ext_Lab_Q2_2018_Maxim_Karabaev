namespace DAL.Core
{
    using System;

    /// <summary>
    /// Обертка DateTime Для хранения только даты.
    /// </summary>
    public class FormattedDate
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="date">Объект, дату которого необходимо скопировать.</param>
        public FormattedDate(DateTime date)
        {
            this.Date = date.Date;
        }

        public DateTime Date { get; set; }

        /// <summary>
        /// Возвращает строковое представление даты.
        /// </summary>
        /// <returns>Строка даты по шаблоку dd-MM-yyyy.</returns>
        public override string ToString()
        {
            return this.Date.ToString("dd-MM-yyyy");
        }

        /// <summary>
        /// Перегрузка неявного приведения из DateTime.
        /// </summary>
        /// <param name="dateTime">Объект, дату которого необходимо скопировать.</param>
        public static implicit operator FormattedDate(DateTime dateTime)
        {
            return new FormattedDate(dateTime);
        }
    }
}
