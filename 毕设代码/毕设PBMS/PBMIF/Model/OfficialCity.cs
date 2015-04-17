using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCSFramework.Model;
using MCSFramework.BLL;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class OfficialCity
    {
        public int ID = 0;
        public string Name = "";
        public int SuperID = 0;
        public int Level = 0;

        public OfficialCity() { }

        public OfficialCity(int CityID)
        {
            Addr_OfficialCity c = new Addr_OfficialCityBLL(CityID).Model;
            if (c != null) FillMode(c);
        }

        public OfficialCity(Addr_OfficialCity c)
        {
            FillMode(c);
        }

        private void FillMode(Addr_OfficialCity c)
        {
            if (c != null)
            {
                ID = c.ID;
                Name = c.Name;
                SuperID = c.SuperID;
                Level = c.Level;
            }
        }
    }
}