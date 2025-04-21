using Microsoft.EntityFrameworkCore;
using T1PR2_APIREST.Context;
using T1PR2_APIREST.Models;

namespace T1PR2_APIREST.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context, IServiceProvider serviceProvider)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if we already have games in the database
            if (await context.Games.AnyAsync())
            {
                return;
            }

            var games = new List<Game>
            {

                new Game
                {
                   Title = "Mystic Realms",
                   Description = "An epic fantasy RPG where players embark on quests to uncover ancient secrets and battle mythical creatures.",
                   DeveloperTeamName = "DreamForge Studios"
                },
                new Game
                {
                   Title = "Eco Warriors",
                   Description = "A strategy game focused on saving the environment by managing resources and combating pollution.",
                   DeveloperTeamName = "Planet Protectors"
                },
                new Game
                {
                   Title = "Galactic Racers",
                   Description = "High-speed intergalactic racing with customizable spaceships and challenging tracks.",
                   DeveloperTeamName = "Stellar Speed"
                },
                new Game
                {
                   Title = "Chef's Challenge",
                   Description = "A cooking simulation game where players compete to create culinary masterpieces.",
                   DeveloperTeamName = "Kitchen Kings"
                },
                new Game
                {
                   Title = "Brain Benders",
                   Description = "A series of logic puzzles and riddles designed to test and improve cognitive skills.",
                   DeveloperTeamName = "Puzzle Masters"
                },
                new Game
                {
                   Title = "Farm Frenzy",
                   Description = "Manage a bustling farm, grow crops, and care for animals in this fun simulation game.",
                   DeveloperTeamName = "Harvest Games"
                },
                new Game
                {
                   Title = "Code Quest",
                   Description = "Learn programming concepts by solving coding challenges and building virtual projects.",
                   DeveloperTeamName = "Dev Adventures"
                },
                new Game
                {
                   Title = "Legends of Time",
                   Description = "Travel through different eras to solve mysteries and shape the course of history.",
                   DeveloperTeamName = "Chrono Studios"
                },
                new Game
                {
                   Title = "Zen Blocks",
                   Description = "A relaxing block-stacking game with soothing visuals and calming music.",
                   DeveloperTeamName = "Tranquil Games",
                   ImageUrl = "https://example.com/images/zen-blocks.png"
                },
                new Game
                {
                   Title = "Deep Sea Discovery",
                   Description = "Explore the ocean depths, uncover hidden treasures, and learn about marine biology.",
                   DeveloperTeamName = "Aqua Explorers"
                }

            };

            await context.Games.AddRangeAsync(games);
            await context.SaveChangesAsync();
        }
    }
}