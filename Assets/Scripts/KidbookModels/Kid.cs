using System;
using UnityEngine;


namespace Kidbook.Models
{
    [System.Serializable]
    public class Kid : Model<Kid>
    {
        public int id;
        public int account_id;
        public string first_name;
        public string last_name;
        public string dim_name;
        public string birth_date;
        public string about;
        public int gender;
        public string avatar;
        public DateTime? created_at;
        public DateTime? updated_at;

        public string GetThumb()
        {
            return Consts.Pictures.Storage + "/" + account_id + "/160/" + avatar;
        }
    }
}

