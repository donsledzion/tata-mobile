using UnityEngine;

namespace Kidbook.Models
{
    public class User : Model<User>
    {
        public int id;
        public string name;
        public string email;
        public string role;
        public string email_verified_at;
        public string status_id;
        public string created_at;
        public string updated_at;
        public string token;
    }
}

