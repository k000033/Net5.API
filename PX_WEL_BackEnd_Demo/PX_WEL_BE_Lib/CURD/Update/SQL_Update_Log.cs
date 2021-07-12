using PX_WEL_BE_Lib.DAO.TableInfo;

namespace PX_WEL_BE_Lib.CURD.Update
{
    public class SQL_Update_Log
    {

        private TableName _tableName;


        public SQL_Update_Log(TableName T)
        {

            this._tableName = T;
        }



        public string GetUPD_PersonnelClickTimeStr()
        {
            return $@"
            update {this._tableName.PersonnelInfo} 
                set click_upd_time = getdate()
                where emp_id = @emp_id";


        }

    }
}
