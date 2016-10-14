using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Test_EF
{
    public class Users
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public bool isLocked { get; set; }
        public bool isOnline { get; set; }
    }

    public class UserPermissions
    {
        public int ID { get; set; }
        public List<Users> refUser { get; set; }
        public List<Permissions> refPermission { get; set; }
    }

    public class Permissions
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class GroupUsers
    {
        public int ID { get; set; }
        public string refGroup { get; set; }
        public List<Users> refUser { get; set; }
    }

    public class Groups
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
    }

    public class GroupPermision
    {
        public int ID { get; set; }
        public List<Groups> refGroup { get; set; }
        public List<Permissions> refPermission { get; set; }
    }


    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<GroupUsers> GroupUsers { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<GroupPermision> GroupPermision { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new DatabaseContext())
            {
                db.Database.CreateIfNotExists();
                CRUDOperations<Users> user = new CRUDOperations<Users>();
                //user.Create(new Users { Login = "login", Password = "123", UserName = "Ivanov", CreateDate = DateTime.Now, Email = "aaa@gmail.com", isLocked = false, isOnline = true });

                var a = user.Read(1);
                
                user.Delete(a);
            }
        }
    }

    public class CRUDOperations<T> where T : class
    {
        private DatabaseContext db = new DatabaseContext();

        public void Create(T item)
        {
            db.Set<T>().Add(item);
            db.SaveChanges();
        }

        public T Read(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(T item)
        {
            db.Set<T>().Remove(item);
            db.SaveChanges();
        }
    }
}
