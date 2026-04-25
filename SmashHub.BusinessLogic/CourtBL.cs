using SmashHub.BusinessLogic.Core;
using SmashHub.BusinessLogic.Interfaces;
using SmashHub.Domain;

namespace SmashHub.BusinessLogic
{
    public class CourtBL : CourtApi, ICourt
    {
        private static List<Court> _courts = new()
        {
            new Court { Id = 1, Name = "Arena Badminton Club", Address = "ул. Алексей Матеевич, 65", Phone = "+373 69 123 456", Hours = "Пн–Пт: 08:00–22:00", Coach = "Игорь Петрович", CoachPhone = "+373 69 111 222", Courts = 4, Image = "" },
            new Court { Id = 2, Name = "SmashZone Chișinău", Address = "бул. Штефан чел Маре, 142", Phone = "+373 69 234 567", Hours = "Пн–Вс: 07:00–23:00", Coach = "Андрей Кожухарь", CoachPhone = "+373 69 333 444", Courts = 6, Image = "" },
            new Court { Id = 3, Name = "SportLife Center", Address = "ул. Каля Ешилор, 28", Phone = "+373 69 345 678", Hours = "Пн–Пт: 10:00–21:00", Coach = "Мария Гончар", CoachPhone = "+373 69 555 666", Courts = 3, Image = "" },
            new Court { Id = 4, Name = "Badminton Pro Hall", Address = "ул. Измаил, 92", Phone = "+373 69 456 789", Hours = "Пн–Вс: 06:00–22:00", Coach = "Дмитрий Руснак", CoachPhone = "+373 69 777 888", Courts = 8, Image = "" },
        };

        public override List<Court> GetAll() => _courts;
    }
}