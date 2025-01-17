namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth0Provider;

    public KeepsController(Auth0Provider auth0Provider, KeepsService keepsService)
    {
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep data)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            data.CreatorId = userInfo.Id;
            Keep keep = _keepsService.CreateKeep(data);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetKeeps()
    {
        try
        {
            List<Keep> keeps = _keepsService.GetKeeps();
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = _keepsService.GetKeepById(keepId);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] Keep data)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.UpdateKeep(keepId, data, userInfo?.Id);
            return Ok(keep);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{keepId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _keepsService.DeleteKeep(keepId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}