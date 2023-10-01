using System;
using UnityEngine;

namespace Kidbook.Models
{
    [Serializable]
    public class Post:Model<Post>
    {
        public int id;
        public string said_at;
        public int author_id;
        public int kid_id;
        public string sentence;
        public string picture;
        public int status_id;
        public DateTime created_at;
        public DateTime? updated_at;
        public int laravel_through_key;
        public Kid kid;


        private string Picture()
        {
            return string.IsNullOrEmpty(picture) ? kid.avatar : picture;
        }

        public string GetPicture()
        {
            return Consts.Pictures.Storage + "/" + kid.account_id + "/768/" + Picture();
        }
    }
}

