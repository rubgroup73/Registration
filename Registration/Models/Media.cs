using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registration.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public Media(int id,string name,string description,string path)
        {
            Id = id;
            Name = name;
            Description = description;
            Path = path;
        }
        public Media()
        {

        }
    }
}