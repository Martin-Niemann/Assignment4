using Assignment1;

namespace Assignment4.Managers
{
    public class FootballPlayersManager
    {
        private static int nextId = 1;

        private static List<FootballPlayer> footballPlayers = new List<FootballPlayer>()
        {
            new FootballPlayer() { Id = nextId++, Name = "Christian Eriksen", ShirtNumber = 14, Age = 30},
            new FootballPlayer() { Id = nextId++, Name = "Kasper Dolberg", ShirtNumber = 12, Age = 24},
            new FootballPlayer() { Id = nextId++, Name = "Joakim Mæhle", ShirtNumber = 5, Age = 25},
            new FootballPlayer() { Id = nextId++, Name = "Simon Kjær", ShirtNumber = 4, Age = 33},
            new FootballPlayer() { Id = nextId++, Name = "Pierre-Emile Højbjerg", ShirtNumber = 23, Age = 27}
        };

        public IEnumerable<FootballPlayer> GetAll()
        {
            return footballPlayers;
        }

        public FootballPlayer? GetByID(int id)
        {
            return footballPlayers.Find(f => f.Id == id);
        }

        public FootballPlayer Add(FootballPlayer player)
        {
            player.Id = nextId++;
            player.ValidateAll();
            footballPlayers.Add(player);
            return player;
        }

        public FootballPlayer? Update(int id, FootballPlayer player)
        {
            if(GetByID(id) == null)
                return null;

            try 
            {
                if (player.NameValidator())
                    footballPlayers[id - 1].Name = player.Name;
            } 
            catch 
            { }

            try 
            {
                if (player.ShirtNumberValidator())
                    footballPlayers[id - 1].ShirtNumber = player.ShirtNumber;
            } 
            catch 
            { }
            
            try
            {
                if (player.AgeValidator())
                    footballPlayers[id - 1].Age = player.Age;
            } 
            catch
            { }

            return GetByID(id);
        }

        public FootballPlayer? Delete(int id)
        {
            FootballPlayer? toBeRemoved = GetByID(id);

            if (toBeRemoved == null)
                return null;

            footballPlayers.Remove(toBeRemoved);

            return toBeRemoved;
        }
    }
}
