using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;

    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }

    public IEnumerable<Vault> GetByUserId(string id)
    {
      return _db.Query<Vault>($"SELECT * FROM vaults WHERE userId = @id", new { id });
    }

    public Vault GetVaultById(int id)
    {
      return _db.QueryFirstOrDefault<Vault>($"SELECT * FROM Vaults WHERE id = @id", new { id });
    }

    public Vault NewVault(Vault newvault)
    {
      int id = _db.ExecuteScalar<int>(@"
 	  INSERT INTO vaults(Name, Description, UserId) Values(@Name, @Description, @UserId);
 	  SELECT LAST_INSERT_ID();", newvault);
      newvault.Id = id;
      return newvault;
    }

    public bool DeleteVault(string vaultId, string userId)
    {

      int success = _db.Execute(@"DELETE FROM vaults WHERE id = @vaultId && userId = @userId", new { vaultId, userId });
      return success != 0;
    }

  }
}