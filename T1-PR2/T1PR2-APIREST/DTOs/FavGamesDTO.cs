﻿using System.ComponentModel.DataAnnotations;

namespace T1PR2_APIREST.DTOs
{
    public class FavGamesDTO
    {
        [Required]
        public int GameId { get; set; }
    }
}
