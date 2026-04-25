using SmashHub.BusinessLogic.Core;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.BusinessLogic
{
    public class TournamentBL : TournamentApi, ITournament
    {
        private static List<Tournament> _tournaments = new()
        {
            new Tournament { Id = 1, Title = "Кубок Кишинёва 2026", Date = "2026-04-15", Location = "Arena Badminton Club", Level = "Все уровни", Description = "Ежегодный открытый турнир", ExternalUrl = "https://example.com/1" },
            new Tournament { Id = 2, Title = "Spring Smash Open", Date = "2026-05-03", Location = "SmashZone Chișinău", Level = "Средний / Продвинутый", Description = "Весенний турнир", ExternalUrl = "https://example.com/2" },
            new Tournament { Id = 3, Title = "Новичок Challenge", Date = "2026-05-20", Location = "SportLife Center", Level = "Начинающий", Description = "Турнир для начинающих", ExternalUrl = "https://example.com/3" },
            new Tournament { Id = 4, Title = "Moldova National Championship", Date = "2026-06-10", Location = "Badminton Pro Hall", Level = "Профессиональный", Description = "Чемпионат Молдовы", ExternalUrl = "https://example.com/4" },
            new Tournament { Id = 5, Title = "Summer Doubles League", Date = "2026-07-01", Location = "Arena Badminton Club", Level = "Все уровни", Description = "Летняя парная лига", ExternalUrl = "https://example.com/5" },
        };

        public override List<Tournament> GetAll() => _tournaments;
    }
}