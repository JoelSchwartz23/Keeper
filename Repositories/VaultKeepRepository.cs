using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepRepository(IDbConnection db)
    {
      _db = db;

    }

    //GetKeepsByVaultId
    public IEnumerable<Keep> GetVaultKeepById(int id)
    {
      return _db.Query<Keep>(@"
        SELECT * FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId
        WHERE (vaultId = @id)
      ", new { id });
    }

    //AddVaultKeep
    public VaultKeep NewVaultKeep(VaultKeep vk)
    {
      _db.Execute(@"
      INSERT INTO vaultkeeps(vaultId, keepId, userId)
      VALUES(@VaultId, @KeepId, @UserId);
      SELECT LAST_INSERT_ID();
      ", vk);
      return vk;
    }

    //DeleteVaultKeep
    public bool DeleteVaultKeep(VaultKeep vk)
    {
      int success = _db.Execute(@"DELETE FROM VaultKeeps WHERE keepId = @KeepId AND vaultId = @VaultId", vk);
      return success != 0;
    }
  }
}