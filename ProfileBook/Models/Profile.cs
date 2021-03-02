using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProfileBook.Models
{
    [Table ("Profile")]
    public class Profile: IModel
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id { get; set; }
        [Column("nic_kname")]
        public string nick_name { get; set; }
        [Column("name")]
        public string name { get; set; }
        [Column("description")]
        public string description { get; set; }
        [Column("image_path")]
        public string image_path { get; set; }
        [Column("user_id")]
        public int user_id { get; set; }
        [Column("date")]
        public DateTime date { get; set; }
    }
}
