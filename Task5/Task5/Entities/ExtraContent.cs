using System;
using System.Collections.Generic;
using System.Text;

namespace Task5
{
    public class ExtraContent : Entity
    {
        public override bool Equals(object obj)
        {
            ExtraContent other = obj as ExtraContent;
            if (other == null) return false;
            return GetHashCode() == other.GetHashCode(); 
        }

        public override int GetHashCode()
        {
            int result = 0;
            result = (int)ID ^ 2; // болванка, пока не придумаю поля класса
            return result;
        }

        public override void Reinitialization(Entity other)
        {
            throw new NotImplementedException();
        }
    }
}
