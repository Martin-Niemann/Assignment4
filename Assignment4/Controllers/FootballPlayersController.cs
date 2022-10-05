using Assignment1;
using Assignment4.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Assignment4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FootballPlayersController : ControllerBase
    {
        private readonly FootballPlayersManager manager = new FootballPlayersManager();

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<FootballPlayer>> Get()
        {
            IEnumerable<FootballPlayer> players = manager.GetAll();

            if (!players.Any())
                return NoContent();

            return Ok(players);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public ActionResult<FootballPlayer> Get(string id)
        {
            int parsedId;

            if (!int.TryParse(id, out parsedId))
                return BadRequest("ID must be a number");

            if (parsedId <= 0)
                return BadRequest("ID must be non-negative and non-zero");

            FootballPlayer? player = manager.GetByID(parsedId);

            if (player == null)
                return NotFound("No player with specified ID");

            return Ok(player);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<FootballPlayer> Post([FromBody] FootballPlayer? player)
        {
            if (player == null)
                return BadRequest();

            try
            {
                FootballPlayer savedPlayer = manager.Add(player);
                return Created("footballplayers/" + savedPlayer.Id, savedPlayer);
            }
            catch (Exception ex)
            when (ex is ArgumentException || ex is ArgumentOutOfRangeException)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}")]
        public ActionResult<FootballPlayer> Patch(string id, [FromBody] FootballPlayer player)
        {
            int parsedId;

            if (!int.TryParse(id, out parsedId))
                return BadRequest("ID must be a number");

            if (parsedId <= 0)
                return BadRequest("ID must be non-negative and non-zero");

            if (player == null)
                return NotFound("No player with specified ID");

            try
            {
                FootballPlayer? updatedPlayer = manager.Update(parsedId, player);

                if (updatedPlayer == null)
                    return NotFound("No player with specified ID");

                return Ok(updatedPlayer);
            }
            catch (Exception ex)
            when (ex is ArgumentException || ex is ArgumentOutOfRangeException)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public ActionResult<FootballPlayer> Delete(string id)
        {
            int parsedId;

            if (!int.TryParse(id, out parsedId))
                return BadRequest("ID must be a number");

            if (parsedId <= 0)
                return BadRequest("ID must be non-negative and non-zero");

            FootballPlayer? player = manager.Delete(parsedId);

            if (player == null)
                return NotFound("No player with specified ID");

            return Ok(player);
        }
    }
}
