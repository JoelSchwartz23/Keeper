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
      return _db.Query<Keep>($"SELECT * FROM keeps WHERE isPrivate = false");
    }

    public IEnumerable<Keep> GetByUserId(string id)
    {
      return _db.Query<Keep>($"SELECT * FROM keeps WHERE userId = @id", new { id });
    }

    public Keep NewKeep(Keep newkeep)
    {
      int id = _db.ExecuteScalar<int>(@"
 	  INSERT INTO keeps(name, description, isPrivate, userId, img) Values(@Name, @Description, @IsPrivate, @UserId, @Img);
 	  SELECT LAST_INSERT_ID();", newkeep);
      newkeep.Id = id;
      return newkeep;
    }

    public Keep EditKeep(Keep newkeep)
    {
      _db.Execute($@"
          UPDATE keeps SET
          name= @Name,
          description = @Description,
          isPrivate = @IsPrivate,
          img = @Img,
          keeps=@keeps,
          views=@views
         WHERE id = @Id AND userId =@userId;
        ", newkeep);
      return newkeep;
    }



    public bool DeleteKeep(string keepId, string userId)
    {

      int success = _db.Execute(@"DELETE FROM keeps WHERE id = @keepId && userId = @userId", new { keepId, userId });
      return success != 0;

    }
  }
}