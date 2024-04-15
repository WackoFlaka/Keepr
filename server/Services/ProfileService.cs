

namespace Keepr.Services;

public class ProfileService
{
    private readonly ProfileRepository _repository;

    public ProfileService(ProfileRepository repository)
    {
        _repository = repository;
    }

    internal Profile GetUserProfile(string profileId)
    {

        Profile profile = _repository.GetUserProfile(profileId);
        return profile;
    }

    internal List<Keep> GetUsersKeeps(string profileId)
    {
        List<Keep> keeps = _repository.GetUsersKeep(profileId);
        return keeps;
    }

    internal List<Vault> GetUsersVaults(string profileId)
    {
        List<Vault> vaults = _repository.GetUsersVaults(profileId);

        return vaults;
    }
}