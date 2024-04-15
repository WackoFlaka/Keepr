namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultkeepsController : ControllerBase
{
    private readonly VaultKeepsService _vaultkeepsService;
    private readonly Auth0Provider _auth0Provider;

    public VaultkeepsController(Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService)
    {
        _auth0Provider = auth0Provider;
        _vaultkeepsService = vaultKeepsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vaultkeeps>> CreateVaultKeep([FromBody] Vaultkeeps data)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            data.CreatorId = userInfo?.Id;
            Vaultkeeps vaultkeeps = _vaultkeepsService.CreateVaultKeep(data);
            return Ok(vaultkeeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteKeepVault(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultkeepsService.DeleteKeepVault(vaultKeepId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultKeepId}")]
    public ActionResult<VaultKeepViewModels> GetVaultKeepById(int vaultKeepId)
    {
        try
        {
            Vaultkeeps keeps = _vaultkeepsService.GetVaultKeepById(vaultKeepId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}