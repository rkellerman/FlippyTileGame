using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductKeyServer.Models;

namespace ProductKeyServer.ViewModels
{
    public class KeyViewModel
    {
        public List<Key> Keys { get; set; }
        public Key SearchEntity { get; set; }
        public Key Entity { get; set; }
        public string EventCommand { get; set; }

        public bool IsDetailAreaVisible { get; set; }
        public bool IsListAreaVisible { get; set; }
        public bool IsSearchAreaVisible { get; set; }

        public bool IsValid { get; set; }
        public string Mode { get; set; }

        public string EventArgument { get; set; }

        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }

        public KeyViewModel()
        {
            Init();

            Keys = new List<Key>();
            SearchEntity = new Key();
            Entity = new Key();
            EventCommand = "List";
        }

        public void Get()
        {
            var mgr = new KeyManager();
            Keys = mgr.Get(SearchEntity);
        }

        public void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "list":
                case "search":
                    Get();
                    break;
                case "resetsearch":
                    ResetSearch();
                    Get();
                    break;
                case "add":
                    Add();
                    break;
                case "save":
                    Save();
                    if (IsValid)
                    {
                        Get();
                    }
                    break;
                case "cancel":
                    ListMode();
                    Get();
                    break;
                case "edit":
                    IsValid = true;
                    Edit();
                    break;
                default:
                    break;
            }
        }

        private void ResetSearch()
        {
            SearchEntity = new Key();
        }

        private void ListMode()
        {
            IsValid = true;

            IsListAreaVisible = true;
            IsSearchAreaVisible = true;
            IsDetailAreaVisible = false;

            Mode = "List";
        }

        private void AddMode()
        {
            IsListAreaVisible = false;
            IsSearchAreaVisible = false;
            IsDetailAreaVisible = true;

            Mode = "Add";
        }

        private void EditMode()
        {
            IsListAreaVisible = false;
            IsSearchAreaVisible = false;
            IsDetailAreaVisible = true;

            Mode = "Edit";
        }

        private void Init()
        {
            EventCommand = "List";
            EventArgument = string.Empty;
            ValidationErrors = new List<KeyValuePair<string, string>>();
            ListMode();
        }

        private void Add()
        {
            IsValid = true;

            Entity = new Key
            {
                ProductKey = "",
                HardwareId = "",
                ExpirationDate = DateTime.Now.AddDays(3),
                LastChecked = DateTime.Now,
                IsDisabled = false
            };

            AddMode();
        }

        private void Edit()
        {
            KeyManager mgr = new KeyManager();
            Entity = mgr.Get(EventArgument);
            EditMode();
        }

        private void Save()
        {

            KeyManager mgr = new KeyManager();

            if (Mode == "Add")
            {
                mgr.Insert(Entity);
            }
            else
            {
                mgr.Update(Entity);
            }
            
            ValidationErrors = mgr.ValidationErrors;

            if (ValidationErrors.Count > 0)
            {
                IsValid = false;
            }

            if (IsValid)
            {
                
                // add data to database here
            }
            else
            {
                if (Mode == "Add")
                {

                    AddMode();
                }
                else
                {
                    EditMode();
                }
            }
        }
    }
}