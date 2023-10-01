using System.Collections.Generic;
using System;

namespace Kidbook.Models
{
    [System.Serializable]
    public class Account
    {
        public int id;
        public string name;
        public string bio;
        public string avatar;
        public object created_at;
        public DateTime? updated_at;
        public List<User> users;
    }
}
