using Keepr.Interfaces;

namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultkeepsRepository _repository;
    private readonly VaultsService _vaultService;

    public VaultKeepsService(VaultkeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultService = vaultsService;
    }

    internal Vaultkeeps CreateVaultKeep(Vaultkeeps data)
    {
        Vault vault = _vaultService.GetVaultById(data.VaultId, data.CreatorId);
        if (vault.CreatorId != data.CreatorId) throw new Exception("Permission Denied");
        if (vault == null) throw new Exception("Permission Denied");
        Vaultkeeps vaultkeeps = _repository.Create(data);
        return vaultkeeps;
    }

    internal Vaultkeeps GetVaultKeepById(int vaultKeepId)
    {
        Vaultkeeps vault = _repository.GetVaultKeepById(vaultKeepId);
        return vault;
    }

    internal string DeleteKeepVault(int vaultKeepId, string userId)
    {
        Vaultkeeps vault = GetVaultKeepById(vaultKeepId);
        if (userId != vault.CreatorId) throw new Exception("Permission Denied.");
        _repository.DeleteKeepVault(vaultKeepId);
        return $"{vaultKeepId} has been deleted";
    }

    internal List<VaultKeepViewModels> GetKeepsInVault(int vaultId, string userId)
    {
        Vault vault = _vaultService.GetVaultById(vaultId, userId);
        if (vault.IsPrivate == true && userId != vault.CreatorId) throw new Exception("Permission Denied");
        List<VaultKeepViewModels> keeps = _repository.GetKeepsInVault(vaultId);
        return keeps;
    }
}