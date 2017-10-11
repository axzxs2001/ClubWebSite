using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NewClubWebSite.Model.DataModel
{

    public class ClubWebSiteDbContext
    {
        string _dataFile;
        public ClubWebSiteDbContext()
        {
            _dataFile = $"{System.IO.Directory.GetCurrentDirectory()}/data.bin";
            if (!File.Exists(_dataFile))
            {
                SaveChanges();
            }else
            {
                ReadData();
            }
        }

        public int SaveChanges()
        {
            try
            {
                var writer = new FileStream(_dataFile, FileMode.CreateNew, FileAccess.Write);
                var formater = new BinaryFormatter();
                formater.Serialize(writer, this);
                writer.Close();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        ClubWebSiteDbContext ReadData()
        {
            try
            {
                var reader = new FileStream(_dataFile, FileMode.Open, FileAccess.Read);
                var formater = new BinaryFormatter();
                var dbContext = formater.Deserialize(reader) as ClubWebSiteDbContext;
                reader.Close();
                this._users = dbContext.Users;
                this._actives = dbContext.Actives;
                this._enrolls = dbContext.Enrolls;
                return dbContext;
            }
            catch
            {
                return null;
            }
        }


        List<User> _users = new List<User>() { new User { ID=1, Name="admin", UserName="admin", Password="654321", Actives=new List<Active> ()} };
        /// <summary>
        /// 用户
        /// </summary>
        public List<User> Users
        {
            get
            {
                return _users;
            }
        }
        List<Active> _actives = new List<Active>();
        /// <summary>
        /// 活动
        /// </summary>
        public List<Active> Actives
        {
            get
            {
                return _actives;
            }
        }
        List<Enroll> _enrolls = new List<Enroll>();
        /// <summary>
        /// 报名
        /// </summary>
        public List<Enroll> Enrolls
        {
            get
            {
                return _enrolls;
            }
        }
    }
}
