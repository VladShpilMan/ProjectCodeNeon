using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Linq;
using System.Buffers.Text;

namespace __ProjectCodeNeon.Entities
{

    public class ImplantController : MonoBehaviour
    {
        private string _jsonFileName = "implants.json";
        private List<Implant> _allImplantList;
        public ImplantController()
        {
            LoadImplantsFromJson();
        }
        public Implant GetImplantById(int id)
        {
            return _allImplantList.Find(implant => implant.Id == id);
        }

        public List<Implant> GetAllImplantsBasedOnList(string entitysImplants)
        {
            return new List<Implant>();
        }

        public void DeleteImplantById(List<Implant> myImplatsList, int id)
        {
            myImplatsList = myImplatsList.Where(implant => implant.Id != id).ToList();
        }

        private void LoadImplantsFromJson()
        {
            try
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(_jsonFileName))
                {
                    string jsonText = sr.ReadToEnd();
                    _allImplantList = JsonUtility.FromJson<List<Implant>>(jsonText);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception: {e.Message}");
            }
        }
    }
}