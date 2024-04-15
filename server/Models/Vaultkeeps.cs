namespace Keepr.Models;

public class Vaultkeeps : RepoItem<int>
{
    public int KeepId { get; set; }
    public int VaultId { get; set; }
    public string CreatorId { get; set; }
}