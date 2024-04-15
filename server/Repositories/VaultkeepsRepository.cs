using Keepr.Interfaces;

namespace Keepr.Repositories;

public class VaultkeepsRepository : IRepository<Vaultkeeps>
{
    private readonly IDbConnection _db;

    public VaultkeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Vaultkeeps Create(Vaultkeeps data)
    {
        string sql = @"
        INSERT INTO
        vaultkeeps(keepId, vaultId, creatorId)
        VALUES(@KeepId, @VaultId, @CreatorId);
        
        SELECT
        vaultkeep.*
        FROM vaultkeeps vaultkeep
        WHERE vaultkeep.id = LAST_INSERT_ID();";

        Vaultkeeps vaultkeeps = _db.Query<Vaultkeeps>(sql, data).FirstOrDefault();
        return vaultkeeps;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Vaultkeeps> GetAll()
    {
        throw new NotImplementedException();
    }

    public Vaultkeeps GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Vaultkeeps Update(Vaultkeeps data)
    {
        throw new NotImplementedException();
    }

    internal void DeleteKeepVault(int vaultKeepId)
    {
        string sql = "DELETE FROM vaultkeeps WHERE id = @vaultkeepId LIMIT 1;";
        _db.Execute(sql, new { vaultKeepId });
    }

    internal List<VaultKeepViewModels> GetKeepsInVault(int vaultId)
    {
        string sql = @"
        SELECT
        vaultkeep.*,
        keep.*,
        account.*
        FROM vaultkeeps vaultkeep
        JOIN keeps keep ON keep.id = vaultkeep.keepId
        JOIN accounts account ON account.id = keep.creatorId
        WHERE vaultkeep.vaultId = @vaultId;";

        List<VaultKeepViewModels> keeps = _db.Query<Vaultkeeps, VaultKeepViewModels, Profile, VaultKeepViewModels>(sql, (vaultkeeps, vaultkeepviewmodels, profile) =>
        {
            vaultkeepviewmodels.VaultKeepId = vaultkeeps.Id;
            vaultkeepviewmodels.CreatorId = vaultkeeps.CreatorId;
            vaultkeepviewmodels.Creator = profile;
            return vaultkeepviewmodels;
        }, new { vaultId }).ToList();
        return keeps;
    }

    internal Vaultkeeps GetVaultKeepById(int vaultKeepId)
    {
        string sql = @"
        SELECT
        vaultkeep.*
        FROM vaultkeeps vaultkeep
        WHERE vaultkeep.id = @vaultKeepId
        ";

        Vaultkeeps vault = _db.Query<Vaultkeeps>(sql, new { vaultKeepId }).FirstOrDefault();
        return vault;
    }
}