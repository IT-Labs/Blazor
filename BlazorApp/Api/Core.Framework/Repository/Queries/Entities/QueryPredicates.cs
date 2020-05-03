using System;
using System.Linq.Expressions;
using Core.Shared;
using Core.Shared.Interfaces;

namespace Core.Framework.Repository.Queries.Entities
{
    public static class QueryPredicates
    {
        public static Expression<Func<T, bool>> UniqueName<T>(long? id, string name) where T : AuditableEntity, IHaveName
        {
            var clean = name.Trim().ToLower();
            return x => x.Name == clean && x.Id != id;
        }

        public static Expression<Func<T, bool>> UniqueNameAmongActive<T>(long? id, string name) where T : DeletableEntity, IHaveName
        {
            var clean = name.Trim().ToLower();
            return x => x.Name == clean && x.Id != id && x.IsActive;
        }

        public static Expression<Func<T, bool>> ChangedName<T>(long? id, string name) where T : DeletableEntity, IHaveName
        {
            return x => id.HasValue && x.Id == id && x.Name != name;
        }

        public static Expression<Func<T, bool>> NameFilterPredicate<T>(string name) where T : IHaveName, IHaveLastName, IHaveFirstName
        {
            if (string.IsNullOrEmpty(name))
                return x => true;

            return x => (x.FirstName != null && (x.FirstName.ToLower().IndexOf(name.ToLower()) >= 0) || (x.LastName != null && x.LastName.ToLower().IndexOf(name.ToLower()) >= 0));
        }

        public static Expression<Func<T, bool>> UsernameFilterPredicate<T>(string username) where T : IHaveUsername
        {
            if(string.IsNullOrEmpty(username))
                return x => true;

            return x => x.Username != null && username != null && x.Username.ToLower().IndexOf(username.ToLower()) >= 0;
        }

        public static Expression<Func<T, bool>> CheckUniqueEmail<T>(string email, long? id) where T : DeletableEntity, IHaveEmail
        {
            var cleanEmail = email.Trim().ToLower();
            return x => x.Email == cleanEmail && x.IsActive && x.Id != id;
        }

        public static Expression<Func<T, bool>> EmailFilterPredicate<T>(string keyword) where T : IHaveEmail
        {
            return x => x.Email != null && x.Email.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static Expression<Func<T, bool>> EmailNotChanged<T>(long? id, string email) where T : DeletableEntity, IHaveEmail
        {
            var clean = email.Trim().ToLower();
            return x => x.Email == clean && x.Id == id;
        }

        public static Expression<Func<T, bool>> CheckUniqueUsername<T>(string username, long? id) where T : DeletableEntity, IHaveUsername
        {
            var cleanUsername = username.Trim().ToLower();
            return x => x.Username.ToLower() == cleanUsername && x.IsActive && x.Id != id;
        }

        public static Expression<Func<T, bool>> IsActive<T>(long id) where T : DeletableEntity
        {
            return x => x.Id == id && x.IsActive;
        }

        public static Expression<Func<T, bool>> ChangedUsername<T>(long? id, string username) where T : DeletableEntity, IHaveUsername 
        {
            return x => id.HasValue && x.Id == id && x.Username != username; 
        }

        public static Expression<Func<T, bool>> ChangedStatus<T>(long? id, bool isActive) where T : DeletableEntity, IHaveIsActive   
        {
            return x => id.HasValue && x.Id == id && x.IsActive == isActive;
        }
    }
}
