using System;
using System.Collections.Generic;
namespace Task5
{
    public class Database
    {
        public List<Message> Messages = new List<Message>();
        public List<ExtraContent> ExtraContents = new List<ExtraContent>();
        public List<Permission> Rights = new List<Permission>();
        public List<Role> Roles = new List<Role>();
        public List<Section> Sections = new List<Section>();
        public List<Topic> Topics = new List<Topic>();
        public List<User> Users = new List<User>();
    }
}
