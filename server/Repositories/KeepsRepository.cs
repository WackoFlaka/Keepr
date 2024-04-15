using Keepr.Interfaces;
namespace Keepr.Repositories;

public class KeepsRepository : IRepository<Keep>
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    public Keep Create(Keep data)
    {
        string sql = @"INSERT INTO keeps(name, description, img,  views, creatorId) VALUES(@Name, @Description, @Img, @Views, @CreatorId);
        SELECT keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account ON keep.creatorId = account.id
        WHERE keep.id = LAST_INSERT_ID();";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, _populateCreator, data).FirstOrDefault();
        return keep;
    }

    public void Delete(int id)
    {
        string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
        _db.Execute(sql, new { id });
    }

    public List<Keep> GetAll()
    {
        string sql = @"
        SELECT
        keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account ON keep.creatorId = account.id;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, _populateCreator).ToList();
        return keeps;
    }

    public Keep GetById(int id)
    {
        string sql = @"
        SELECT
        keep.*,
        COUNT(vaultkeep.id) AS kept,
        account.*
        FROM keeps keep
        LEFT JOIN vaultkeeps vaultkeep ON vaultkeep.keepId = @id 
        JOIN accounts account ON account.id = keep.creatorId
        WHERE keep.id = @id
        GROUP BY (keep.id)
        ";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, _populateCreator, new { id }).FirstOrDefault();
        return keep;
    }

    public Keep Update(Keep data)
    {
        string sql = @"
        UPDATE keeps 
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views
        WHERE id = @Id;
        
        SELECT
        keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account ON account.id = keep.creatorId
        WHERE keep.id = @Id;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, _populateCreator, data).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeepsInVault(int vaultId)
    {
        string sql = @"
        SELECT
        keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account ON account.id = keep.creatorId
        WHERE vault.id = @vaultId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, _populateCreator, new { vaultId }).ToList();
        return keeps;
    }

    private Keep _populateCreator(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }
}