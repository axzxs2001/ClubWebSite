using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ClubWebSite.Model.Entity;
using System.Runtime.Serialization.Formatters;

namespace ClubWebSite.Model
{
    /// <summary>
    /// 数据处理
    /// </summary>
    public class DataHandle
    {
        /// <summary>
        /// 数据文据存放路径
        /// </summary>
        string _dataDir;
        /// <summary>
        /// 内存数据
        /// </summary>
        Dictionary<string, Entity.Active> _actives;
        Dictionary<string, Entity.User> _users;
        public DataHandle(string dataDir)
        {
            _dataDir = dataDir;
            _actives = Read<Active>();
            _users = Read<User>();
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="entity">实体</typeparam>     
        /// <returns></returns>
        public bool AddEntity<T>(T entity) where T : EntityObject
        {
            var typeName = entity.GetType().Name;
            if (_data.Keys.Contains(typeName))
            {
                _data[typeName].Add(entity.ID, entity);
            }
            else
            {
                var dic = new Dictionary<string, EntityObject>();
                dic.Add(entity.ID, entity);
                _data.Add(typeName, dic);
            }
            return Write();
        }
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool Remove(EntityObject entity)
        {
            var typeName = entity.GetType().Name;
            if (_data.Keys.Contains(typeName))
            {
                _data[typeName].Remove(entity.ID);
                return Write();
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 按照ID查询对象
        /// </summary>
        /// <param name="id">实体ID</typeparam>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public T GetEntity<T>(string id) where T : EntityObject
        {
            var typeName = typeof(T).Name;
            //集合是否包含类型
            if (_data.Keys.Contains(typeName))
            {
                return _data[typeName][id] as T;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页数</param>
        /// <param name="countPerPage">每页记数数</param>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public (List<T> entities, int count) GetPageEntities<T>(int pageIndex, int countPerPage) where T : EntityObject
        {
            var typeName = typeof(T).Name;
            if (!_data.Keys.Contains(typeName))
            {

                return (new List<T>(), 0);
            }
            var entityList = _data[typeName].Values.Skip((pageIndex - 1) * countPerPage).Take(countPerPage).ToList();
            //转成具体的子类
            var list = new List<T>();
            foreach (T t in entityList)
            {
                list.Add(t);
            }
            return (list, _data[typeName].Count);
        }

        /// <summary>
        /// 查询全部实体
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public List<EntityObject> GetAll(Type type)
        {
            var typeName = type.Name;
            if (!_data.Keys.Contains(typeName))
            {

                return new List<EntityObject>();
            }
            else
            {
                return _data[typeName].Values.ToList();
            }
        }


        /// <summary>
        /// 把实体类保存到文件中
        /// </summary>
        /// <param name="entitys">写入实体集合</param>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        bool Write()
        {

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_data);
            var file = Path.Combine(_dataDir, $"ClubWebSite_Data.json");

            // var formater=new System.Runtime.Serialization.Formatters..
            using (var fileStream = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(json);
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Flush();
            }
            return true;
        }
        /// <summary>
        /// 读取实体
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public Dictionary<string, T> Read<T>() where T:EntityObject
        {
            var typeName = typeof(T).Name;
            var file = Path.Combine(_dataDir, $"{typeName}.json");
            var dic= new Dictionary<string, T>(); 
            if (File.Exists(file))
            {
                using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    var bytes = new byte[fileStream.Length];
                    fileStream.Read(bytes, 0, bytes.Length);
                    var json = System.Text.Encoding.UTF8.GetString(bytes);
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject(json) as List<T>;
                    foreach(var item in list)
                    {
                        dic.Add(item.ID, item);
                    }                
                }
            }
            return dic;
        }
    }
}
