using System.Runtime.CompilerServices;

namespace Keepr.Repositories;

public class ProfileRepository
{
    private readonly IDbConnection _db;

    public ProfileRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetUserProfile(string profileId)
    {
        string sql = @"
        SELECT
        account.*
        FROM accounts account
        WHERE account.id = @profileId;";

        Profile profile = _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
        return profile;
    }

    internal List<Keep> GetUsersKeep(string profileId)
    {
        string sql = @"
        SELECT 
        keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account ON account.id = keep.creatorId
        WHERE keep.creatorId = @profileId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, _populateCreator, new { profileId }).ToList();
        return keeps;
    }

    internal List<Vault> GetUsersVaults(string profileId)
    {
        string sql = @"
        SELECT
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON account.id = vault.creatorId
        WHERE vault.creatorId = @profileId AND vault.isPrivate = 0;";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, _populateCreator, new { profileId }).ToList();
        return vaults;
    }

    private Vault _populateCreator(Vault vault, Profile profile)
    {
        vault.Creator = profile;
        return vault;
    }

    private Keep _populateCreator(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }
}