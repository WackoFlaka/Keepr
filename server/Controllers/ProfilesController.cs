namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
    private readonly ProfileService _profileService;
    private readonly Auth0Provider _auth0Provider;

    public ProfilesController(Auth0Provider auth0Provider, ProfileService profileService)
    {
        _auth0Provider = auth0Provider;
        _profileService = profileService;
    }

    // /api/profiles/profileId
    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetUserProfile(string profileId)
    {
        try
        {
            Profile profile = _profileService.GetUserProfile(profileId);
            return Ok(profile);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetUsersKeeps(string profileId)
    {
        try
        {
            List<Keep> keeps = _profileService.GetUsersKeeps(profileId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetUsersVaults(string profileId)
    {
        try
        {
            List<Vault> vaults = _profileService.GetUsersVaults(profileId);
            return Ok(vaults);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}