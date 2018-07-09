﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Раздел форума
    /// </summary>
    public class Section : Entity
    {
        /// <summary>
        /// Название раздела
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ссылка на раздел
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Список тем в разделе
        /// </summary>
        public List<Topic> TopicList { get; set; }
        /// <summary>
        /// Записать состояние сущности в базу
        /// </summary>
        public override void Write()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Считать состояние сущности из базы
        /// </summary>
        public override void Read()
        {
            throw new NotImplementedException();
        }
    }
}
