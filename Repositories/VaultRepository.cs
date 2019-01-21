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
    public IEnumerable<Vault> GetAll()
    {
      return _db.Query<Vault>("SELECT * FROM vaults");
    }

    public Vault GetVaultById(int id)
    {
      return _db.QueryFirstOrDefault<Vault>($"SELECT * FROM vaults WHERE id = @id", new { id });
    }

    public Vault AddVault(Vault newvault)
    {
      int id = _db.ExecuteScalar<int>(@"
 	  INSERT INTO vaults(Name, Description, UserId) Values(@Name, @Description, @UserId);
 	  SELECT LAST_INSERT_ID();", newvault);
      newvault.Id = id;
      return newvault;
    }

    public Vault EditVault(int id, Vault newvault)
    {
      try
      {
        return _db.QueryFirstOrDefault<Vault>($@"
          UPDATE vaults SET
          Name= @Name,
          Description = @Description,
          UserId = @UserId
          WHERE Id = @Id;
          SELECT * FROM vaults WHERE id = @Id;
        ", newvault);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return null;
      }
    }



    public bool DeleteVault(int id)
    {
      int success = _db.Execute("DELETE FROM vaults WHERE id = @id", new { id });
      if (success == 0)
      {
        return false;
      }
      return true;
    }

  }
}