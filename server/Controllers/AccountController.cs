namespace Keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> UpdateAccount([FromBody] Account data)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.Edit(data, userInfo?.Id));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _accountService.GetMyVaults(userInfo.Id);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("keeps")]
  [Authorize]
  public async Task<ActionResult<List<Keep>>> GetMyKeeps()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _accountService.GetKyKeeps(userInfo.Id);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
