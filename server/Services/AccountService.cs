

namespace Keepr.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userId)
  {
    Account original = _repo.GetById(userId);
    original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
    original.CoverImg = editData.CoverImg?.Length > 0 ? editData.CoverImg : original.CoverImg;
    return _repo.Edit(original);
  }

  internal List<Vault> GetMyVaults(string id)
  {
    List<Vault> vaults = _repo.GetMyVaults(id);
    return vaults;
  }

  internal List<Keep> GetKyKeeps(string id)
  {
    List<Keep> keeps = _repo.GetMyKeeps(id);
    return keeps;
  }
}
