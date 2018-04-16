using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SuperUncleServer
{
    /// <summary>
    /// RankListService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class RankListService : System.Web.Services.WebService
    {
        [WebMethod]
        public void upLoadData(string MapName, string UserName, int CostTime, int Score)
        {
            //操作数据库
            DB_Controler.connectDB();

            //查询mapName的排行榜是否存在
            if (DB_Controler.CheckExistsTable(MapName))
            {
                DB_Controler.insertTableData(MapName, UserName, CostTime, Score);
            }
            else
            {
                DB_Controler.createTable(MapName);
                DB_Controler.insertTableData(MapName, UserName, CostTime, Score);
            }
        }

        [WebMethod]
        public List<RL_Data> getRankList(string MapName)
        {
            return DB_Controler.searchTableData(MapName);
        }
    }

}