﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProfileBook.Models
{
    [Table("User")]
    public class User: IModel
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id { get; set; }

        [Unique, Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }
        
    }
}
