using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model.Entities
{
    public class MessageAttacment : Entity
    {
        public object Content { get; set; }


        public override bool Equals(object obj)
        {
            MessageAttacment other = obj as MessageAttacment;
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

        public override bool LikeAs(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override void Reinitialization(Entity other)
        {
            MessageAttacment newExtra = other as MessageAttacment;
            if (newExtra == null) return;
            Content = newExtra.Content;
        }
    }
}
