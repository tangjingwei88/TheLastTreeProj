using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageConfigManager : ProtoBase {

    public class StageConfig
    {
        public int ID;
        public string Name;


        public Dictionary<int, int> ResultDic = new Dictionary<int, int>();                     //存储结果
        public List<string> ItemTemplateList = new List<string>();                           //关卡Template

        public StageConfig()
        {
            ID = 1;
            Name = "";
        }


        public StageConfig(m.StageConfig s)
        {
            ID = s.RankID;
            Name = s.Name;

            string[] str2List = s.TemplateList.Split(';');
            for (int i = 0; i < str2List.Length; i++)
            {
                ItemTemplateList.Add(str2List[i]);         
            }
            
        }


    }


    #region 数据

    public static List<StageConfig> stageConfigList = new List<StageConfig>();
    public static Dictionary<int, StageConfig> stageConfigDic = new Dictionary<int, StageConfig>();

    #endregion




    /// <summary>
    /// 初始化
    /// </summary>
    public static void Init()
    {
        ReadData();
    }


    #region 读取数据

    private static void ReadData()
    {
        List<m.StageConfig> stageConfig = LoadPoto<m.StageConfig>("StageConfig");
        for (int i = 0; i < stageConfig.Count; i++)
        {
            m.StageConfig sc = stageConfig[i];
            StageConfig script = new StageConfig(sc);
            if(!stageConfigDic.ContainsKey(script.ID))
            {
                stageConfigDic.Add(script.ID, script);
                stageConfigList.Add(script);
            }

        }
    }

    #endregion

    public static StageConfig GetStageConfig(int ID)
    {
        if (stageConfigDic.ContainsKey(ID))
        {
            return stageConfigDic[ID];
        }
        else {
            Debug.LogError("GetStageConfig failed ID = " + ID.ToString());
            return new StageConfig();
        }
    }

    #region 接口


    #endregion
}
