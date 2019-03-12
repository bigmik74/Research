using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Linq;

namespace Linq2db.tests
{
    public static class GetModelType
    {
        public static Type Get()
        {
            ConstructorInfo ci = null;
            CustomAttributeBuilder cab = null;
            FieldBuilder fb = null;

            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "ModelAssembly";

            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                assemblyName, AssemblyBuilderAccess.Run);

            ModuleBuilder moduleBuilder =
                assemblyBuilder.DefineDynamicModule("MyModule");

            TypeBuilder typeBuilder =
              moduleBuilder.DefineType("Category", TypeAttributes.Public | TypeAttributes.Class);

            ci = typeof(TableAttribute).GetConstructor(new Type[] { typeof(string) });
            cab = new CustomAttributeBuilder(ci, new object[] { "Categories" });

            typeBuilder.SetCustomAttribute(cab);
            // **** CategoryID ****
            fb = typeBuilder.DefineField("CategoryID", typeof(int), FieldAttributes.Public);
            //[PrimaryKey]
            ci = typeof(PrimaryKeyAttribute).GetConstructor(new Type[] { });
            cab = new CustomAttributeBuilder(ci, new object[] { });
            fb.SetCustomAttribute(cab);
            //[Identity]
            ci = typeof(IdentityAttribute).GetConstructor(new Type[] { });
            cab = new CustomAttributeBuilder(ci, new object[] { });
            fb.SetCustomAttribute(cab);

            // **** CategoryName ****
            fb = typeBuilder.DefineField("CategoryName", typeof(string), FieldAttributes.Public);
            //[Column]
            ci = typeof(ColumnAttribute).GetConstructor(new Type[] { });
            cab = new CustomAttributeBuilder(ci, new object[] { });
            fb.SetCustomAttribute(cab);
            //[NotNull]
            ci = typeof(NotNullAttribute).GetConstructor(new Type[] { });
            cab = new CustomAttributeBuilder(ci, new object[] { });
            fb.SetCustomAttribute(cab);

            // **** Description ****
            fb = typeBuilder.DefineField("Description", typeof(string), FieldAttributes.Public);
            //[Column]
            ci = typeof(ColumnAttribute).GetConstructor(new Type[] { });
            cab = new CustomAttributeBuilder(ci, new object[] { });
            fb.SetCustomAttribute(cab);
            //[Nullable]
            ci = typeof(NullableAttribute).GetConstructor(new Type[] { });
            cab = new CustomAttributeBuilder(ci, new object[] { });
            fb.SetCustomAttribute(cab);

            // **** Picture ****
            fb = typeBuilder.DefineField("Picture", typeof(byte[]), FieldAttributes.Public);
            //[Column]
            ci = typeof(ColumnAttribute).GetConstructor(new Type[] { });
            cab = new CustomAttributeBuilder(ci, new object[] { });
            fb.SetCustomAttribute(cab);
            //[Nullable]
            ci = typeof(NullableAttribute).GetConstructor(new Type[] { });
            cab = new CustomAttributeBuilder(ci, new object[] { });
            fb.SetCustomAttribute(cab);

            // create the dynamic class
            Type dynamicType = typeBuilder.CreateType();
            return dynamicType;

        }
    }
}
