using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductKeyServer.Models
{
    public class KeyManager
    {
        private readonly ApplicationDbContext _context;
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }

        public KeyManager()
        {
            _context = new ApplicationDbContext();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }

        public List<Key> Get(Key entity)
        {
            List<Key> ret = new List<Key>();

            ret = CreateData();

            if (!string.IsNullOrEmpty(entity.ProductKey))
            {
                ret = ret.FindAll(k => k.ProductKey.ToLower().StartsWith(entity.ProductKey.ToLower()));
            }

            return ret;
        }

        public Key Get(string productKey)
        {
            List<Key> list = new List<Key>();
            Key ret = new Key();

            list = CreateData();

            ret = list.Find(p => p.ProductKey == productKey);

            return ret;
        }

        public bool Update(Key entity)
        {
            bool ret = false;

            ret = Validate(entity);

            if (ret)
            {
                // update
                Key key = _context.Keys.FirstOrDefault(k => k.ProductKey == entity.ProductKey);
                key.ExpirationDate = entity.ExpirationDate;
                key.HardwareId = entity.HardwareId;
                key.IsDisabled = entity.IsDisabled;
                key.LastChecked = entity.LastChecked;

                _context.SaveChanges();
            }

            return ret;
        }

        public bool Validate(Key entity)
        {
            ValidationErrors.Clear();

            // create custom validation errors

            return true;
        }

        public bool Insert(Key entity)
        {
            bool ret = false;

            ret = Validate(entity);

            if (ret)
            {
                // TODO:  Create insert code here

                _context.Keys.Add(entity);
                _context.SaveChanges();
            }

            return ret;
        }

        private List<Key> CreateData()
        {
            var keys = _context.Keys.ToList();
            return keys;
        }
    }

}
  