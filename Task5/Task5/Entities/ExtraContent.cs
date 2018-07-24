using System;
using System.Collections.Generic;
using System.Text;

namespace Task5
{
    public class ExtraContent : Entity
    {
        public object Content { get; set; }


        public override bool Equals(object obj)
        {
            ExtraContent other = obj as ExtraContent;
            if (other == null) return false;
            return (ID == other.ID) && (Content == Content); 
        }

        public override int GetHashCode()
        {
            int result = 0;
            try
            {
                result = ID.GetHashCode() + Content.GetHashCode();
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public override void Reinitialization(Entity other)
        {
            ExtraContent newExtra = other as ExtraContent;
            if (newExtra == null) return;
            Content = newExtra.Content;
        }
    }
}
