using Keepr.Interfaces;

namespace Keepr.Repositories;

public class VaultsRepository : IRepository<Vault>
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Vault Create(Vault data)
    {
        string sql = @"
        INSERT INTO
        vaults(name, description, img, isPrivate, creatorId)
        VALUES(@Name, @Description, @Img, 
        CASE
            WHEN @IsPrivate IS NULL THEN FALSE
            ELSE @IsPrivate
        END
        , @CreatorId);
        
        SELECT
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON account.id = vault.creatorId
        WHERE vault.id = LAST_INSERT_ID();";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, _populateCreator, data).FirstOrDefault();
        return vault;
    }

    public void Delete(int id)
    {
        string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
        _db.Execute(sql, new { id });
    }

    public List<Vault> GetAll()
    {
        throw new NotImplementedException();
    }

    public Vault GetById(int id)
    {
        string sql = @"
        SELECT 
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON account.id = vault.creatorId
        WHERE vault.id = @id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, _populateCreator, new { id }).FirstOrDefault();
        return vault;
    }

    public Vault Update(Vault data)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        isPrivate = @IsPrivate
        WHERE id = @Id;
        
        SELECT 
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON account.id = vault.creatorId
        WHERE vault.id = @Id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, _populateCreator, data).FirstOrDefault();
        return vault;
    }

    private Vault _populateCreator(Vault vault, Profile profile)
    {
        vault.Creator = profile;
        return vault;
    }
}