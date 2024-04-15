



namespace Keepr.Repositories;

public class AccountsRepository
{
  private readonly IDbConnection _db;

  public AccountsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Account GetByEmail(string userEmail)
  {
    string sql = "SELECT * FROM accounts WHERE email = @userEmail";
    return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
  }

  internal Account GetById(string id)
  {
    string sql = "SELECT * FROM accounts WHERE id = @id";
    return _db.QueryFirstOrDefault<Account>(sql, new { id });
  }

  internal Account Create(Account newAccount)
  {
    string sql = @"
            INSERT INTO accounts
              (name, picture, email, id, coverImg)
            VALUES
              (@Name, @Picture, @Email, @Id, 
              CASE 
                WHEN @CoverImg IS NULL THEN 'https://images.unsplash.com/photo-1494972308805-463bc619d34e?q=80&w=2073&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D' 
                ELSE @CoverImg 
              END)";
    _db.Execute(sql, newAccount);
    return newAccount;
  }

  internal Account Edit(Account update)
  {
    string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture,
              coverImg = @CoverImg
            WHERE id = @Id;";
    _db.Execute(sql, update);
    return update;
  }

  internal List<Vault> GetMyVaults(string profileId)
  {

    string sql = @"
        SELECT
        vault.*,
        account.*
        FROM vaults vault
        JOIN accounts account ON account.id = vault.creatorId
        WHERE vault.creatorId = @profileId;";

    List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, _populateCreator, new { profileId }).ToList();
    return vaults;
  }

  private Vault _populateCreator(Vault vault, Account account)
  {
    vault.Creator = account;
    return vault;
  }

  internal List<Keep> GetMyKeeps(string id)
  {
    string sql = @"
        SELECT
        keep.*,
        account.*
        FROM keeps keep
        JOIN accounts account ON account.id = keep.creatorId
        WHERE keep.creatorId = @id";

    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, _populateCreator, new { id }).ToList();
    return keeps;
  }

  private Keep _populateCreator(Keep keep, Account account)
  {
    keep.Creator = account;
    return keep;
  }
}

