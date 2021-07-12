using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace PX_WEL_BE_Lib.DAO.ParamsHandling
{
    class ParamsModelBind
    {

        public Dictionary<TK, TV> TranModelToDict<TK, TV, TModel>(TModel modelBinding)
        {
            PropertyInfo[] modelAttr = modelBinding.GetType().GetProperties();
            Dictionary<TK, TV> dictTrans = new Dictionary<TK, TV>();

            foreach (var info in modelAttr)
            {

                dictTrans.Add((TK)(object)info.Name, (TV)(object)info.GetValue(modelBinding, null));
            }

            return dictTrans;


        }

    }
}
