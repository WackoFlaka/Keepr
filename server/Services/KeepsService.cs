

namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repository;
    private readonly VaultsService _vaultsService;

    public KeepsService(KeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
    }

    internal Keep CreateKeep(Keep data)
    {
        Keep keep = _repository.Create(data);
        return keep;
    }

    internal string DeleteKeep(int keepId, string userId)
    {
        Keep keepToDelete = GetKeepById(keepId);
        if (keepToDelete.CreatorId != userId) throw new Exception("Permission Denied. This keep does not belong to you.");
        _repository.Delete(keepId);
        return $"{keepToDelete.Name} has been deleted";
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repository.GetById(keepId);
        keep.Views++;
        _repository.Update(keep);
        if (keep == null) throw new Exception($"Invalid id: {keepId}");
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = _repository.GetAll();
        return keeps;
    }

    internal List<Keep> GetKeepsInVault(int vaultId, string userId)
    {
        _vaultsService.GetVaultById(vaultId, userId);

        List<Keep> keeps = _repository.GetKeepsInVault(vaultId);
        return keeps;
    }

    internal Keep UpdateKeep(int keepId, Keep data, string userId)
    {
        Keep keepToUpdate = GetKeepById(keepId);
        if (keepToUpdate.CreatorId != userId) throw new Exception("Permission Denied. This Keep does not belong to you");

        keepToUpdate.Name = data.Name ?? keepToUpdate.Name;
        keepToUpdate.Description = data.Description ?? keepToUpdate.Description;
        keepToUpdate.Img = data.Img ?? keepToUpdate.Img;

        Keep keep = _repository.Update(keepToUpdate);
        return keep;
    }
}