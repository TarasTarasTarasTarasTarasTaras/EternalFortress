﻿namespace EternalFortress.Data.EF.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<User>? Users { get; set; }
    }
}
