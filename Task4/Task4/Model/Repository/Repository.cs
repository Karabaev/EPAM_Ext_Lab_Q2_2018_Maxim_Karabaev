using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{ 
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    abstract public class Repository
    {
        protected string DBConnectionString { get; set; }
        abstract public void WriteAll();
        abstract public void ReadAll();
        abstract public void Write();
        abstract public void Read();
    }
}
