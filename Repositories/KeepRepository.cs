using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepRepository
  {
    private readonly IDbConnection _db;

    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }
    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM keeps");
    }

    public Keep GetKeepById(int id)
    {
      return _db.QueryFirstOrDefault<Keep>($"SELECT * FROM keeps WHERE id = @id", new { id });
    }

    public Keep AddKeep(Keep newkeep)
    {
      int id = _db.ExecuteScalar<int>(@"
 	  INSERT INTO keeps(Name, Description, IsPrivate, UserId, Img) Values(@Name, @Description, @IsPrivate, @UserId, @Img);
 	  SELECT LAST_INSERT_ID();", newkeep);
      newkeep.Id = id;
      return newkeep;
    }

    public Keep EditKeep(int id, Keep newkeep)
    {
      try
      {
        return _db.QueryFirstOrDefault<Keep>($@"
          UPDATE keeps SET
          Name= @Name,
          Description = @Description,
          Img = @Img
          WHERE Id = @Id;
          SELECT * FROM keeps WHERE id = @Id;
        ", newkeep);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return null;
      }
    }



    public bool DeleteKeep(int id)
    {
      int success = _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
      if (success == 0)
      {
        return false;
      }
      return true;
    }

  }
}