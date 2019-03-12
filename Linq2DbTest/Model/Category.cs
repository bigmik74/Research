using LinqToDB.Mapping;
using System;

namespace Linq2DbTest.Model
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey, Identity] public int CategoryID;
        [Column, NotNull] public string CategoryName;
        [Column, Nullable] public string Description;
        [Column, Nullable] public byte[] Picture;
    }
}
