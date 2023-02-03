using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExtendFieldDemo.Models
{
    public abstract class BaseExtendModel<TKey> : DynamicObject
        where TKey : struct
    {
        private const string IdFiled = "ModelId";

        protected DbContext? DbContext { get; set; }
        protected string? ExtendTableName { get; set; }

        public virtual TKey Id { get; set; }

        public object? this[string extendname]
        {
            get { return this.GetExtendValue(extendname); }
            set { this.AddOrUpdateExtendValue(extendname, value!); }
        }

        [NotMapped]
        public dynamic ExtendObject => this;

        public void AddOrUpdateExtendValue(string name, object value)
        {
            if (DbContext == null || string.IsNullOrWhiteSpace(ExtendTableName))
            {
                return;
            }

            // DbContext.AuthorExtends.EntityType.FindProperty("ModelId").ClrType 可通过此方式活动属性类型。
            var dbset = DbContext.Set<Dictionary<string, object>>(ExtendTableName);
            var dic = DbContext.Set<Dictionary<string, object>>(ExtendTableName).FirstOrDefault(o => o[IdFiled].Equals(Id));
            if (dic == null)
            {
                DbContext.Set<Dictionary<string, object>>(ExtendTableName).Add(new Dictionary<string, object> { [IdFiled] = Id, [name] = value });
            }
            else
            {
                dic[name] = value;
            }
        }

        public object? GetExtendValue(string fieldName)
        {
            if (DbContext == null || string.IsNullOrWhiteSpace(ExtendTableName))
            {
                return null;
            }

            var dic = DbContext.Set<Dictionary<string, object>>(ExtendTableName).FirstOrDefault(o => o[IdFiled].Equals(Id));

            if (dic == null)
            {
                return null;
            }

            return dic[fieldName];
        }

        public DbSet<Dictionary<string, object>> GetExtendFieldDeSet()
        {
            if (DbContext == null || string.IsNullOrWhiteSpace(ExtendTableName))
            {
                return null;
            }

            return DbContext!.Set<Dictionary<string, object>>(ExtendTableName!);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string fieldName = binder.Name;
            result = GetExtendValue(fieldName)!;
            if (result == null)
            {
                return false;
            }

            return true;
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            if (DbContext == null || string.IsNullOrWhiteSpace(ExtendTableName))
            {
                return false;
            }

            var propery = DbContext.Set<Dictionary<string, object>>(ExtendTableName).EntityType.FindProperty(binder.Name);
            if (propery == null)
            {
                return false;
            }

            AddOrUpdateExtendValue(binder.Name, value);
            return true;
        }
    }
}
