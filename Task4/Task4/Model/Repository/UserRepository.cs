using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class UserRepository : Repository
    {
        public override List<Entity> Entities { get; set; }

        public override void Read()
        {
            throw new NotImplementedException();
        }
        public override void ReadAll()
        {
            throw new NotImplementedException();
        }
        public override void Write()
        {
            throw new NotImplementedException();
        }
        public override void WriteAll()
        {
            throw new NotImplementedException();
        }
    }
}
