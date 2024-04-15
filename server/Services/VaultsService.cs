

namespace Keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repository;

    public VaultsService(VaultsRepository repository)
    {
        _repository = repository;
    }

    internal Vault CreateVault(Vault data)
    {
        Vault vault = _repository.Create(data);
        return vault;
    }

    internal string DeleteVault(int vaultId, string userId)
    {
        Vault vaultToDelete = GetVaultById(vaultId, userId);
        if (vaultToDelete.CreatorId != userId) throw new Exception("Permission Denied. This vault does not belong to you!");
        _repository.Delete(vaultId);
        return $"{vaultToDelete.Name} has been deleted.";
    }

    internal Vault GetVaultById(int vaultId, string userid)
    {
        Vault vault = _repository.GetById(vaultId);
        if (vault.IsPrivate == true && vault.CreatorId != userid) throw new Exception("Vault is private. Permission Denied");
        return vault;
    }

    internal Vault UpdateVault(int vaultId, Vault data, string userId)
    {
        Vault vaultToUpdate = GetVaultById(vaultId, userId);
        if (vaultToUpdate.CreatorId != userId) throw new Exception("Permission Denied. This vault is not yours!");
        vaultToUpdate.Name = data.Name ?? vaultToUpdate.Name;
        vaultToUpdate.Description = data.Description ?? vaultToUpdate.Description;
        vaultToUpdate.Img = data.Img ?? vaultToUpdate.Img;
        vaultToUpdate.IsPrivate = data.IsPrivate ?? vaultToUpdate.IsPrivate;

        Vault vault = _repository.Update(vaultToUpdate);
        return vault;
    }
}